using System;

namespace scheduler.job.src.exception
{
    public class JobException : Exception
    {
        public JobException(string mensagem) : base(mensagem)
        {
        }
    }
}
