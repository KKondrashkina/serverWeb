using System;
using System.Threading.Tasks;
using Quartz;

namespace Job
{
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Hello world!");
        }
    }
}
