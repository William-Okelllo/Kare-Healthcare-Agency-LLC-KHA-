using Topshelf;
using Compassion_service;

class program
{
    static void Main(String[] args)
    {
        var exitcode = HostFactory.Run(x =>
        {
            x.Service<StartUpAfrica>(s =>
            {
                s.ConstructUsing(Heartbeat => new StartUpAfrica());
                s.WhenStarted(heartbeat => heartbeat.Start());
                s.WhenStopped(heartbeat => heartbeat.Stop());
            });

            x.RunAsLocalSystem();

            x.SetServiceName("StartUpAfricaService");
            x.SetDisplayName("StartUpAfrica Service version 1.1.7");
            x.SetDescription("StartUpAfrica service version 1.1.7");
        });

        int exitcodevalue = (int)Convert.ChangeType(exitcode, exitcode.GetTypeCode());
        Environment.ExitCode = exitcodevalue;
    }
}