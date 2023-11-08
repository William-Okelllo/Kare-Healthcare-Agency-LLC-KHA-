using Topshelf;
using App_Service;
class program
{
    static void Main(String[] args)
    {
        var exitcode = HostFactory.Run(x =>
        {
            x.Service<App>(s =>
            {
                s.ConstructUsing(Heartbeat => new App());
                s.WhenStarted(heartbeat => heartbeat.Start());
                s.WhenStopped(heartbeat => heartbeat.Stop());
            });

            x.RunAsLocalSystem();

            x.SetServiceName("AppService");
            x.SetDisplayName("App Service version 7.7.7 - Planning");
            x.SetDescription("App service version 7.7.7 - Planning");
        });

        int exitcodevalue = (int)Convert.ChangeType(exitcode, exitcode.GetTypeCode());
        Environment.ExitCode = exitcodevalue;
    }
}