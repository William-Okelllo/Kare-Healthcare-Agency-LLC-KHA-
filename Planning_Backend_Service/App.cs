using EASendMail;
using IShop.Core;
using Ishop.Infa;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient; 
using System.Text;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Timers;
using Attachment = System.Net.Mail.Attachment;
using SmtpClient = EASendMail.SmtpClient;
using Timer = System.Timers.Timer;
using Dapper;
using IShop.Core.Interface;
using Azure;

namespace Planning_Backend_Service
{
    public class App
    {
        private readonly Timer _timer;

        public App()
        {

            _timer = new Timer(10000) { AutoReset = true };
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
                                string logLine = $"Date: {DateTime.Now.ToString()} | Failed Sending Email To: {Recipient} | Attached File: {(string.IsNullOrEmpty(Files) ? "None" : Path.GetFileName(Files))}| Status: Unsent | Error: {ex.Message}";
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
                string logLine = $"Date: {DateTime.Now.ToString()} | |-- Failed to update email on db {recipient} | Count -- {CurrentCount} | Response  | {Response}";
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
                                if (DateTime.Now.DayOfWeek == runtimeDayOfWeek &&
                                  DateTime.Now.TimeOfDay.Hours == 10 && //10 AM
                                  DateTime.Now.TimeOfDay.Minutes == 00)
                                {
                                    // Run your method or logic here

                                    InsertTimesheetRecords();
                                    DateTime currentDate = DateTime.Now;
                                    DateTime nextSunday = currentDate.Date.AddDays(1); // Next day (Sunday)
                                    int daysPassed = (int)nextSunday.DayOfWeek;
                                    daysPassed = (daysPassed == 0) ? 0 : 7 - daysPassed;
                                    DateTime from_Date = nextSunday.Date.AddDays(-daysPassed); // Previous Sunday or same day if it's already Sunday
                                    DateTime end_Date = from_Date.AddDays(6); // Following Saturday

                                    Console.WriteLine("Current Date: " + currentDate);
                                    Console.WriteLine("From Date: " + from_Date);
                                    Console.WriteLine("End Date: " + end_Date);
                                    Console.WriteLine("--success-- ");
                                }
                                else
                                {
                                    Console.WriteLine("--Awaiting for " + Runtime + " 10:00 AM  ");
                                    
                                }
                                }
                            }
                        }


                    }
                }




            }




        public bool TimesheetExists(DateTime from_Date, string employeeUsername)
        {
            bool exists = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT 1 FROM Timesheets WHERE From_Date = @From_Date AND Owner = @Owner";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@From_Date", from_Date);
                        command.Parameters.AddWithValue("@Owner", employeeUsername);
                        exists = (command.ExecuteScalar() != null);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking timesheet existence: {ex.Message}");
            }

            return exists;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Id, Username, Fullname, DprtName, Designation, Email " +
                          "FROM Employees " +
                          "WHERE Active = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Employee employee = new Employee
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Username = reader["Username"].ToString(),
                                    Fullname = reader["Fullname"].ToString(),
                                    DprtName = reader["DprtName"].ToString(),
                                    Designation = reader["Designation"].ToString(),
                                    Email = reader["Email"].ToString()
                                    // Add other properties as needed
                                };

                                employees.Add(employee);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employees: {ex.Message}");
            }

            return employees;
        }



        public void InsertTimesheetRecords()
        {
            DateTime currentDate = DateTime.Now;
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)currentDate.DayOfWeek + 7) % 7;
            DateTime from_Date = currentDate.Date.AddDays(daysUntilMonday);
            DateTime end_Date = from_Date.AddDays(6);

            Console.WriteLine("Current Date: " + currentDate);
            Console.WriteLine("From Date: " + from_Date);
            Console.WriteLine("End Date: " + end_Date);



            try
            {
                // Get the list of employees
                List<Employee> employees = GetEmployees();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var employee in employees)
                    {
                        // Check if the timesheet already exists for the current week and employee
                        if (!TimesheetExists(from_Date, employee.Username))
                        {
                            // Create a new Timesheet record
                            string insertQuery = @"
                        INSERT INTO Timesheets (CreatedOn, From_Date, End_Date, Department, Owner, Tt, Direct_Hours, InDirect_Hours, Status,Locked,leave)
                        VALUES (@CreatedOn, @From_Date, @End_Date, @Department, @Owner, @Tt, @Direct_Hours, @InDirect_Hours, @Status,@Locked,@leave)";

                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@CreatedOn", from_Date.AddDays(1));
                                insertCommand.Parameters.AddWithValue("@From_Date", from_Date);
                                insertCommand.Parameters.AddWithValue("@End_Date", end_Date);
                                insertCommand.Parameters.AddWithValue("@Department", employee.DprtName);
                                insertCommand.Parameters.AddWithValue("@Owner", employee.Username);
                                insertCommand.Parameters.AddWithValue("@Tt", 0);
                                insertCommand.Parameters.AddWithValue("@Direct_Hours", 0);
                                insertCommand.Parameters.AddWithValue("@InDirect_Hours", 0);
                                insertCommand.Parameters.AddWithValue("@Status", 0);
                                insertCommand.Parameters.AddWithValue("@Locked", false);
                                insertCommand.Parameters.AddWithValue("@leave", 0);
                                // Execute the SQL command to insert the new Timesheet record
                                insertCommand.ExecuteNonQuery();

                                string logLine = $"Timesheet Set :  | From Date  {from_Date} | End Date  {end_Date} | Employee Account  {employee.Username} | Department  {employee.DprtName} ";
                                File.AppendAllLines(logFilePath, new string[] { logLine });
                            }
                        }
                        else
                        {
                            string logLine =  $"Date : {DateTime.Now}  Timesheet Already Exists :  |  Employee Account  {employee.Username} | Department  {employee.DprtName} ";
                            File.AppendAllLines(logFilePath, new string[] { logLine });
                        }
                    }
                    Locking();
                    Notifications_Send();
                    Leavesupdate();
                }
            }
            catch (Exception ex)
            {
                string logLine = $"ERROR : Inserting Timesheet  :   |  {ex.Message}";
                File.AppendAllLines(logFilePath, new string[] { logLine });
                Console.WriteLine($"Error inserting timesheets: {ex.Message}");
            }
        }

        public void Locking()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Business_mail FROM Configs";
                string baseAddress = connection.QueryFirstOrDefault<string>(query);

                // Assuming you have a base address for the HttpClient
                using (HttpClient client = new HttpClient())
                {
                    if (Uri.TryCreate(baseAddress, UriKind.Absolute, out Uri uri))
                    {
                        client.BaseAddress = uri;

                        HttpResponseMessage response = client.GetAsync("Timesheet/InsertTimesheet").Result;

                        // Check if the request was successful
                        if (response.IsSuccessStatusCode)
                        {
                            // Read and handle the response content
                            string responseBody = response.Content.ReadAsStringAsync().Result;
                            string logLine = $"Date: {DateTime.Now.ToString()} | - - - - - Old Timesheets locked - - - - - | {responseBody} ";
                            File.AppendAllLines(logFilePath, new string[] { logLine });
                        }
                        else
                        {
                            Console.WriteLine("Request failed with status: " + response.StatusCode);
                            string logLine = $"Date: {DateTime.Now.ToString()} |- - - - - Error locking Old Timesheets - - - - - - - -| {response} ";
                            File.AppendAllLines(logFilePath, new string[] { logLine });
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid URL format: " + baseAddress);
                    }
                }
            }
        }


        public void Notifications_Send()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Business_mail FROM Configs";
                string baseAddress = connection.QueryFirstOrDefault<string>(query);

                // Assuming you have a base address for the HttpClient
                using (HttpClient client = new HttpClient())
                {
                    if (Uri.TryCreate(baseAddress, UriKind.Absolute, out Uri uri))
                    {
                        client.BaseAddress = uri;

                        HttpResponseMessage response = client.GetAsync("Notifications/Recurring").Result;

                        // Check if the request was successful
                        if (response.IsSuccessStatusCode)
                        {
                            // Read and handle the response content
                            string responseBody = response.Content.ReadAsStringAsync().Result;
                            string logLine = $"Date: {DateTime.Now.ToString()} | - - - - -Recurring Emails sent - - - - - | {responseBody} ";
                            File.AppendAllLines(logFilePath, new string[] { logLine });
                        }
                        else
                        {
                            Console.WriteLine("Request failed with status: " + response.StatusCode);
                            string logLine = $"Date: {DateTime.Now.ToString()} |- - - - - Error sending Recurring mails - - - - - - - -| {response} ";
                            File.AppendAllLines(logFilePath, new string[] { logLine });
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid URL format: " + baseAddress);
                    }
                }
            }
        }
        public void Leavesupdate()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Business_mail FROM Configs";
                string baseAddress = connection.QueryFirstOrDefault<string>(query);

                // Assuming you have a base address for the HttpClient
                using (HttpClient client = new HttpClient())
                {
                    if (Uri.TryCreate(baseAddress, UriKind.Absolute, out Uri uri))
                    {
                        client.BaseAddress = uri;

                        HttpResponseMessage response = client.GetAsync("API/Updatetimesheetservice").Result;

                        // Check if the request was successful
                        if (response.IsSuccessStatusCode)
                        {
                            // Read and handle the response content
                            string responseBody = response.Content.ReadAsStringAsync().Result;
                            string logLine = $"Date: {DateTime.Now.ToString()} | - - - - -Leaves  Balances updated - - - - - | {responseBody} ";
                            File.AppendAllLines(logFilePath, new string[] { logLine });
                        }
                        else
                        {
                            Console.WriteLine("Request failed with status: " + response.StatusCode);
                            string logLine = $"Date: {DateTime.Now.ToString()} |- - - - - Error udating leaves balances  - - - - - - - -| {response} ";
                            File.AppendAllLines(logFilePath, new string[] { logLine });
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid URL format: " + baseAddress);
                    }
                }
            }
        }



        public void Start()
        {
            _timer.Start();
            string logLine = $"Service started sucessfully :  |  Date  {DateTime.Now} |Activated  ";
            File.AppendAllLines(logFilePath, new string[] { logLine });
        }

        public void Stop()
        {
            string logLine = $"Service stopped sucessfully :  |  Date  {DateTime.Now} | Deactivated ";
            File.AppendAllLines(logFilePath, new string[] { logLine });
            _timer.Stop();
        }





    }

}
