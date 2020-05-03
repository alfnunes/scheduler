using System.IO;
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
            string data = string.Empty;

            using (StreamReader file = new StreamReader("../../../fixtures/massa-vazia.json"))
            {
                data = file.ReadToEnd();
            }

            Assert.Throws<JobException>(() => _schedulerService.ExecuteJobs(data));
        }

        [Fact]
        public void DeveRetornarExcecaoComAMassaSemOJob()
        {
            string data = string.Empty;

            using (StreamReader file = new StreamReader("../../../fixtures/massa-sem-job.json"))
            {
                data = file.ReadToEnd();
            }

            Assert.Throws<JobException>(() => _schedulerService.ExecuteJobs(data));
        }

        [Fact]
        public void DeveRetornarExcecaoComUmJobComMaisDeOitoHoras()
        {
            string data = string.Empty;

            using (StreamReader file = new StreamReader("../../../fixtures/massa-sucesso-job-com-mais-de-oito-horas.json"))
            {
                data = file.ReadToEnd();
            }

            Assert.Throws<JobException>(() => _schedulerService.ExecuteJobs(data));
        }
    }
}
