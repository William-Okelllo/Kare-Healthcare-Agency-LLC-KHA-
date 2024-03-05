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

            x.SetServiceName("Timenex_BackEnd_Service");
            x.SetDisplayName("Timenex Backend Service version 12.0.10 ");
            x.SetDescription("Timenex Backend Service version 12.0.10");
        });

        int exitcodevalue = (int)Convert.ChangeType(exitcode, exitcode.GetTypeCode());
        Environment.ExitCode = exitcodevalue;
    }
}