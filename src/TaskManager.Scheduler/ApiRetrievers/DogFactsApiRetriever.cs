using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using TaskManager.Core.Models;

namespace TaskManager.Scheduler.ApiRetrievers
{
    public class DogFactsApiRetriever : ApiRetriever<IEnumerable<DogFact>>
    {
        public DogFactsApiRetriever(CronTask task) : base(task)
        {
        }

        public override string CreateRequestParams()
        {
            return "?number=1";
        }
    }
}
