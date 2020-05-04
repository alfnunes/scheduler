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

            // Executar lista de Jobs
            ListaDeExecucao(output, scheduler.Jobs);

            return output;
        }

        private IEnumerable<IEnumerable<int>> ListaDeExecucao(List<List<int>> output, List<JobEntity> jobs, int posicaoCorrente = 0)
        {
            if (posicaoCorrente < jobs.Count)
            {
                var jobAtual = jobs[posicaoCorrente];

                if (posicaoCorrente > 0 && jobs[posicaoCorrente - 1].DataConclusao > jobAtual.DataConclusao.AddHours(-jobAtual.TempoEstimado))
                    throw new DataConclusaoException("Data máxima de execução não foi respeitada!");

                var temJobRegistrado = output.Any();
                var tempoTotalUltimoArray = temJobRegistrado ?
                jobs.Where(job => output[output.Count - 1].Select(item => item).Contains(job.Id)).Sum(job => job.TempoEstimado) : 0;

                if (jobAtual.TempoEstimado + tempoTotalUltimoArray <= 8F && temJobRegistrado)
                    output[output.Count - 1].Add(jobAtual.Id);
                else
                    output.Add(new List<int> { jobAtual.Id });

                ListaDeExecucao(output, jobs, ++posicaoCorrente);
            }

            return output;
        }
    }
}
