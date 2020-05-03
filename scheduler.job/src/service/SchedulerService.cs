using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using scheduler.job.src.enitty;
using scheduler.job.src.exception;

namespace scheduler.job.src.service
{
    public class SchedulerService
    {
        public IEnumerable<IEnumerable<int>> ExecuteJobs(string schedule)
        {
            var scheduler = JsonConvert.DeserializeObject<SchedulerEntity>(schedule);

            if (scheduler == null || !scheduler.Jobs.Any())
                throw new JobException("Não foram encontrados jobs nesse Schedule!");

            // Exception se conter jobs únicos com mais de 8 horas
            if (scheduler.Jobs.Any(job => job.TempoEstimado > 8F))
                throw new JobException("Existe algum job específico com execução de mais de 8 horas!");

            return null;
        }
    }
}
