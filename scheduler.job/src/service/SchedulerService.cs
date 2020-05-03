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

            // Exception jobs que estão fora da janela de execução
            if (scheduler.Jobs.Any(job =>
                 job.DataConclusao.AddHours(-job.TempoEstimado) < scheduler.JanelaInicio ||
                 job.DataConclusao > scheduler.JanelaFim))
                throw new JanelaException("O Scheduler contém jobs no horário fora da janela de execução!");

            // Ordenar por data de conclusão
            scheduler.Jobs = scheduler.Jobs.OrderBy(j => j.DataConclusao).ToList();

            var output = new List<List<int>>();

            // TODO fazer regra para comparar Data de conclusão e não deixar de passar 8 horas somando os jobs (quebrar em array)

            return output;
        }
    }
}
