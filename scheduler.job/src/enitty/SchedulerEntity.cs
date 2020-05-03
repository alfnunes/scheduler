using System;
using System.Collections.Generic;

namespace scheduler.job.src.enitty
{
    public class SchedulerEntity
    {
        public DateTime JanelaInicio { get; set; }
        public DateTime JanelaFim { get; set; }
        public List<JobEntity> Jobs { get; set; } = new List<JobEntity>();
    }
}
