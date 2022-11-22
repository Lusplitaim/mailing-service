using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;
using TaskManager.Scheduler.ApiRetrievers;

namespace TaskManager.Scheduler.ApiHandlers
{
    public class DogFactsApiHandler : ApiHandler<IEnumerable<DogFact>>
    {
        public DogFactsApiHandler(CronTask task)
        {
            _dataRetriever = new DogFactsApiRetriever(task);
        }

        public override async Task InvokeAsync()
        {
            var data = await _dataRetriever.RetrieveData();
            return;
        }
    }
}
