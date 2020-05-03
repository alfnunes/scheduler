using System;

namespace scheduler.job.src.enitty
{
    public class JobEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataConclusao { get; set; }
        public float TempoEstimado { get; set; }
    }
}
