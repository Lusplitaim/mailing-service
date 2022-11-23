using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Scheduler.ApiRetrievers
{
    public class JokesApiRetriever : ApiRetriever<Joke>
    {
        public JokesApiRetriever(CronTask task) : base(task)
        {
        }

        public override string CreateRequestParams()
        {
            return string.Empty;
        }
    }
}
