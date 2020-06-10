using System;
using Topshelf;

namespace Job
{
    class Job
    {
        public static void Main()
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<ScheduleService>(s =>
                {
                    s.ConstructUsing(name => new ScheduleService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Topshelf with Quartz");
                x.SetDisplayName("Topshelf with Quartz");
                x.SetServiceName("Topshelf-Quartz");
            });

            var exitCode = (int) Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
