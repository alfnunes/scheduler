using System.Collections.Generic;
using System.IO;
using System.Linq;
using scheduler.job.src.exception;
using scheduler.job.src.service;
using Xunit;

namespace scheduler.test
{
    public class SchedulerTest
    {
        private readonly SchedulerService _schedulerService;

        public SchedulerTest() => _schedulerService = new SchedulerService();

        [Fact]
        public void DeveRetornarExcecaoComAMassaVazia()
        {
            var data = string.Empty;

            using (StreamReader file = new StreamReader("../../../fixtures/massa-vazia.json"))
            {
                data = file.ReadToEnd();
            }

            Assert.Throws<JobException>(() => _schedulerService.ExecuteJobs(data));
        }

        [Fact]
        public void DeveRetornarExcecaoComAMassaSemOJob()
        {
            var data = string.Empty;

            using (StreamReader file = new StreamReader("../../../fixtures/massa-sem-job.json"))
            {
                data = file.ReadToEnd();
            }

            Assert.Throws<JobException>(() => _schedulerService.ExecuteJobs(data));
        }

        [Fact]
        public void DeveRetornarExcecaoComUmJobComMaisDeOitoHoras()
        {
            var data = string.Empty;

            using (StreamReader file = new StreamReader("../../../fixtures/massa-sucesso-job-com-mais-de-oito-horas.json"))
            {
                data = file.ReadToEnd();
            }

            Assert.Throws<JobException>(() => _schedulerService.ExecuteJobs(data));
        }

        [Fact]
        public void DeveRetornarExcecaoComUmJobForaDaJanelaInicial()
        {
            var data = string.Empty;

            using (StreamReader file = new StreamReader("../../../fixtures/massa-com-uma-data-menor-que-janela-inicial.json"))
            {
                data = file.ReadToEnd();
            }

            Assert.Throws<JanelaException>(() => _schedulerService.ExecuteJobs(data));
        }

        [Fact]
        public void DeveRetornarExcecaoComUmJobForaDaJanelaFinal()
        {
            var data = string.Empty;

            using (StreamReader file = new StreamReader("../../../fixtures/massa-com-uma-data-maior-que-janela-final.json"))
            {
                data = file.ReadToEnd();
            }

            Assert.Throws<JanelaException>(() => _schedulerService.ExecuteJobs(data));
        }

        [Fact]
        public void DeveRetornarExcecaoComUmJobSemRespeitarADataDeConclusao()
        {
            var data = string.Empty;

            using (StreamReader file = new StreamReader("../../../fixtures/massa-sem-repeitar-data-conclusao.json"))
            {
                data = file.ReadToEnd();
            }

            Assert.Throws<DataConclusaoException>(() => _schedulerService.ExecuteJobs(data));
        }

        [Fact]
        public void DeveRetonarOutputDeExecucaoComSucesso()
        {
            var expectedCount = 3;
            var expectedIds = new List<int>() { 1, 3, 2 };
            var data = string.Empty;

            using (StreamReader file = new StreamReader("../../../fixtures/massa-sucesso.json"))
            {
                data = file.ReadToEnd();
            }

            var output = _schedulerService.ExecuteJobs(data);
            var actual = output.SelectMany(job => job);

            Assert.True(output.Any());
            Assert.Equal(expectedCount, actual.Count());
            Assert.Equal(expectedIds, actual);

        }

        [Fact]
        public void DeveRetonarOutputDeExecucaoComSucessoComJobDeOitoHoras()
        {
            var expectedCount = 4;
            var expectedIds = new List<int>() { 1, 4, 3, 2 };
            var data = string.Empty;

            using (StreamReader file = new StreamReader("../../../fixtures/massa-sucesso-job-com-oito-horas.json"))
            {
                data = file.ReadToEnd();
            }

            var output = _schedulerService.ExecuteJobs(data);
            var actual = output.SelectMany(job => job);

            Assert.True(output.Any());
            Assert.Equal(expectedCount, actual.Count());
            Assert.Equal(expectedIds, actual);
        }
    }
}
