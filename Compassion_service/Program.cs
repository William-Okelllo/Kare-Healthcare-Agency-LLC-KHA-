using Topshelf;
using Compassion_service;
class program
{
    static void Main(String[] args)
    {
        var exitcode = HostFactory.Run(x =>
        {
            x.Service<Compassion>(s =>
            {
                s.ConstructUsing(Heartbeat => new Compassion());
                s.WhenStarted(heartbeat => heartbeat.Start());
                s.WhenStopped(heartbeat => heartbeat.Stop());
            });

            x.RunAsLocalSystem();

            x.SetServiceName("CompassionService");
            x.SetDisplayName("Compassion Service version 1.1.7");
            x.SetDescription("Compassion service version 1.1.7");
        });

        int exitcodevalue = (int)Convert.ChangeType(exitcode, exitcode.GetTypeCode());
        Environment.ExitCode = exitcodevalue;
    }
}