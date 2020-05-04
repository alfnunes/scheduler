using System;
using System.Linq;
using scheduler.job.src.service;

namespace scheduler.job
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Scheduler Test");

            if (!args.Any())
                throw new Exception("Argumento obrigatório!");

            var output = new SchedulerService().ExecuteJobs(args[0]);

            foreach (var jobExecution in output)
            {
                Console.Write("[");
                var log = string.Empty;

                foreach (var job in jobExecution)
                {
                    log += $"{job},";
                }

                Console.Write(log.Substring(0, log.LastIndexOf(',')));
                Console.Write("]");
                Console.WriteLine("");
            }
        }
    }
}
