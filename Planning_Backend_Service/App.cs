using Dapper;
using EASendMail;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Mvc;
using SmtpClient = EASendMail.SmtpClient;
using Timer = System.Timers.Timer;
using Attachment = System.Net.Mail.Attachment;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using Ishop.Infa;
using IShop.Core;
using System.Globalization;

namespace Planning_Backend_Service
{
    public class App
    {
        private readonly Timer _timer;

        public App()
        {

            _timer = new Timer(30000) { AutoReset = true };
            _timer.Elapsed += TimeElapsed;
        }
        private string connectionString = "Data Source=.;Initial Catalog=Planning; User ID=sa; Password=1234;Integrated Security=True;";


        private string logFilePath = @"C:\Temp\Demos\PlanningBackendService.log";

        private void TimeElapsed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                // Query the database to retrieve unsent emails.
                System.Data.DataTable unsentEmails = GetUnsentEmailsFromDatabase(connectionString);
                DataTable unsentSms = GetUnsentSmsFromDatabase(connectionString);


                foreach (DataRow emailRow in unsentEmails.Rows)
                {
                    string Recipient = emailRow["Recipient"].ToString();
                    string subject = emailRow["Subject"].ToString();
                    string body = emailRow["Body"].ToString();
                    string Files = emailRow["Files"].ToString();
                    string Trials = emailRow["Trials"].ToString();


                    try
                    {
                        // Send the email using EASendMail.
                        int.TryParse(Trials, out int trialsValue);
                        SendEmailUsingEASendMail(Recipient, subject, body, Files, trialsValue);

                    }
                    catch (Exception ex)
                    {

                    }

                }
                foreach (DataRow emailRow in unsentSms.Rows)
                {
                    string Recipient = emailRow["RecipientNumber"].ToString();
                    string MessageText = emailRow["MessageText"].ToString();
                    string Trials = emailRow["Trials"].ToString();
                    try
                    {
                        // Send the  using Sms.
                        int.TryParse(Trials, out int trialsValue);
                        SendSms(Recipient, MessageText, trialsValue);


                    }
                    catch (Exception ex)
                    {

                    }


                }

                CheckTimesheetDate();

            }
            catch (Exception ex)
            {
                // Update the log file with the error encountered during database query.
                string logLine = $"Date: {DateTime.Now.ToString()} | Error: {ex.Message}";
                File.AppendAllLines(logFilePath, new string[] { logLine });
            }
        }

        private DataTable GetUnsentEmailsFromDatabase(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Recipient, Subject, Body,Files,Trials FROM Outgoingemails WHERE Status = 0 and Trials <=4";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable unsentEmails = new DataTable();
                    adapter.Fill(unsentEmails);
                    return unsentEmails;
                }
            }
        }

        private void SendEmailUsingEASendMail(string Recipient, string subject, string body, string Files, int Trials)
        {

            string query = "SELECT Password, Email, Smtp, Port FROM Configs";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string password = reader["Password"].ToString();
                            string email = reader["Email"].ToString();
                            string smtp = reader["Smtp"].ToString();
                            int port = Convert.ToInt32(reader["Port"]);




                            SmtpMail mail = new SmtpMail("TryIt");
                            SmtpClient client = new SmtpClient();

                            mail.From = email;
                            mail.To = Recipient;
                            mail.Subject = subject;
                            mail.TextBody = body;
                            if (!string.IsNullOrEmpty(Files))
                            {
                                Attachment attachment = new Attachment(Files);
                                mail.AddAttachment(Files);
                            }
                            // Set up your SMTP server settings (replace with your own).
                            SmtpServer server = new SmtpServer(smtp);
                            server.User = email;
                            server.Password = password;
                            server.Port = port;
                            server.ConnectType = SmtpConnectType.ConnectSSLAuto;

                            try
                            {

                                client.SendMail(server, mail);
                                int CurrentCount = Trials + 1;
                                String Response = "Success";
                                bool Status = true;
                                UpdateEmailStatusInDatabase(connectionString, Recipient, subject, body, CurrentCount, Response, Status);
                                string logLine = $"Date: {DateTime.Now.ToString()} | Sending Email To: {Recipient} |  Attached File: {(string.IsNullOrEmpty(Files) ? "None" : Path.GetFileName(Files))} | Status: Sent | Marked Sent on Db ";
                                File.AppendAllLines(logFilePath, new string[] { logLine });

                            }
                            catch (Exception ex)
                            {
                                int CurrentCount = Trials + 1;
                                string truncatedMessage = ex.Message.Length <= 100 ? ex.Message : ex.Message.Substring(0, 100);
                                String Response = truncatedMessage;
                                bool Status = false;
                                UpdateEmailStatusInDatabase(connectionString, Recipient, subject, body, CurrentCount, Response, Status);
                                string logLine = $"Date: {DateTime.Now.ToString()} | Sending sms To: {Recipient} | Attached File: {(string.IsNullOrEmpty(Files) ? "None" : Path.GetFileName(Files))}| Status: Unsent | Error: {ex.Message}";
                                File.AppendAllLines(logFilePath, new string[] { logLine });
                            }
                        }
                    }
                }
            }
        }
        private void UpdateEmailStatusInDatabase(string connectionString, string recipient, string subject, string body, int CurrentCount, string Response, bool Status)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Outgoingemails " +
                           "SET Status = @Status, Trials = @Trials, Response = @Response " +
                           "WHERE Recipient = @Recipient AND Body = @body";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@Trials", CurrentCount);
                        command.Parameters.AddWithValue("@Response", Response);
                        command.Parameters.AddWithValue("@Recipient", recipient);
                        command.Parameters.AddWithValue("@body", body);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            string logLine = $"Date: {DateTime.Now.ToString()} |-- Updated email on db {recipient} | Count -- {CurrentCount} | Response  | {Response}";
                            File.AppendAllLines(logFilePath, new string[] { logLine });
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string logLine = $"Date: {DateTime.Now.ToString()} | |-- Updated email on db {recipient} | Count -- {CurrentCount} | Response  | {Response}";
                File.AppendAllLines(logFilePath, new string[] { logLine });
            }

        }



        private DataTable GetUnsentSmsFromDatabase(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT RecipientNumber, MessageText,Trials FROM Outgoingsms WHERE IsSent = 0 and Trials <=4";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable unsentSms = new DataTable();
                    adapter.Fill(unsentSms);
                    return unsentSms;
                }
            }
        }

        public void SendSms(string recipientNumber, string messageText, int trials)
        {
            try
            {
                string query = "SELECT APIkey, partnerID, shortcode, APIUrl FROM sms_Configs";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string apiKey = reader["APIkey"].ToString();
                                string shortcode = reader["shortcode"].ToString();
                                string partnerID = reader["partnerID"].ToString();
                                string apiUrl = reader["APIUrl"].ToString();

                                using (HttpClient client = new HttpClient())
                                {
                                    // Prepare the request body
                                    var requestBody = new
                                    {
                                        apikey = apiKey,
                                        partnerID = partnerID,
                                        message = messageText,
                                        shortcode = shortcode,
                                        mobile = recipientNumber
                                    };

                                    var json = JsonConvert.SerializeObject(requestBody);

                                    try
                                    {
                                        var response = client.PostAsync(apiUrl, new StringContent(json, Encoding.UTF8, "application/json")).Result;

                                        // Check if the SMS was sent successfully
                                        if (response.IsSuccessStatusCode)
                                        {
                                            int currentCount = trials + 1;
                                            string responseMessage = "Success";
                                            bool status = true;
                                            UpdateSmsStatusInDatabase(recipientNumber, messageText, currentCount, responseMessage, status);
                                            string logLine = $"Date: {DateTime.Now.ToString()} | From Username {shortcode} | Sending SMS To: {recipientNumber} | Status: Success | Marked Sent on Db";
                                            File.AppendAllLines(logFilePath, new string[] { logLine });
                                            Console.WriteLine("-------Success");
                                        }
                                        else
                                        {
                                            // Handle unsuccessful SMS sending
                                            int currentCount = trials + 1;
                                            string responseMessage = "SMS sending failed. Status code: " + response.StatusCode;
                                            bool status = false;
                                            UpdateSmsStatusInDatabase(recipientNumber, messageText, currentCount, responseMessage, status);
                                            string logLine = $"Date: {DateTime.Now.ToString()} | From Username {shortcode} | Sending SMS To: {recipientNumber} | Status: Unsent | Error: {responseMessage}";
                                            File.AppendAllLines(logFilePath, new string[] { logLine });
                                            Console.WriteLine("-------SMS sending failed");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        int currentCount = trials + 1;
                                        string responseMessage = ex.Message;
                                        bool status = false;
                                        UpdateSmsStatusInDatabase(recipientNumber, messageText, currentCount, responseMessage, status);
                                        string logLine = $"Date: {DateTime.Now.ToString()} | From Username {shortcode} | Sending SMS To: {recipientNumber} | Status: Unsent | Error: {ex.Message}";
                                        File.AppendAllLines(logFilePath, new string[] { logLine });
                                        Console.WriteLine("-------Error: " + ex.Message);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions here.
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void UpdateSmsStatusInDatabase(string recipientNumber, string messageText, int currentCount, string responseMessage, bool status)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE OutgoingSms " +
                                   "SET IsSent = @Status, Trials = @Trials, Response = @Response " +
                                   "WHERE RecipientNumber = @RecipientNumber AND MessageText = @MessageText";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@Trials", currentCount);
                        command.Parameters.AddWithValue("@Response", responseMessage);
                        command.Parameters.AddWithValue("@RecipientNumber", recipientNumber);
                        command.Parameters.AddWithValue("@MessageText", messageText);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("SMS status updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No records were updated. SMS not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any database update exceptions here.
                Console.WriteLine("An error occurred while updating the SMS status in the database: " + ex.Message);
            }
        }


        public void CheckTimesheetDate()
        {

            string query = "SELECT  Runtime FROM Configs";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string Runtime = reader["Runtime"].ToString();

                            if (Enum.TryParse(Runtime, out DayOfWeek runtimeDayOfWeek))
                            {
                                DayOfWeek currentDayOfWeek = DateTime.Now.DayOfWeek;
                                // Check if the runtime day is the same as today and the time is 12:00 AM
                                if (DateTime.Now.DayOfWeek == runtimeDayOfWeek &&
                                  DateTime.Now.TimeOfDay.Hours == 13 && // 1:00 PM
                                  DateTime.Now.TimeOfDay.Minutes == 30)
                                {
                                    // Run your method or logic here
                                    InsertTimesheetRecords();
                                    Console.WriteLine("--success-- ");
                                }
                                else
                                {
                                    Console.WriteLine("--Awaiting for " + Runtime + " 1:30 PM");
                                }
                            }
                        }


                    }
                }




            }
        }







        public void InsertTimesheetRecords()
        {



            try
            {
                string query = "SELECT Email, Password, SSRSReportsUrl, Business_mail,Smtp,port FROM Configs";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string Business_mail = reader["Business_mail"].ToString();




                                using (HttpClient client = new HttpClient())
                                {
                                    // Set the base address of your web app
                                    client.BaseAddress = new Uri(Business_mail);

                                    // Call the action method URL synchronously
                                    HttpResponseMessage response = client.GetAsync("/Timesheet/InsertTimesheet").Result;

                                    if (response.IsSuccessStatusCode)
                                    {

                                        string logLine = $"Date: {DateTime.Now.ToString()} |System Timesheet SetUp  | Status: Success | ";
                                        File.AppendAllLines(logFilePath, new string[] { logLine });

                                    }
                                    else
                                    {
                                        string logLine = $"Date: {DateTime.Now.ToString()} |System Timesheet SetUp  | Status: Success | ";
                                        File.AppendAllLines(logFilePath, new string[] { logLine });
                                    }
                                }

                            }
                        }
                    }
                }

            }
            catch
            {

            }
        }



        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }





    }

}
