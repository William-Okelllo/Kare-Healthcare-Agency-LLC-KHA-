using Planning_Backend_Service;
using Topshelf;
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

            x.SetServiceName("BackendService");
            x.SetDisplayName("Backend Service version 7.7.7 - Planning");
            x.SetDescription("Backend service version 7.7.7 - Planning");
        });

        int exitcodevalue = (int)Convert.ChangeType(exitcode, exitcode.GetTypeCode());
        Environment.ExitCode = exitcodevalue;
    }
}