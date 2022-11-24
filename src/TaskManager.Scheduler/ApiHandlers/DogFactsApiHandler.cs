using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;
using TaskManager.Scheduler.ApiRetrievers;
using TaskManager.Scheduler.EmailSenders;
using TaskManager.Scheduler.Writers;

namespace TaskManager.Scheduler.ApiHandlers
{
    public class DogFactsApiHandler : ApiHandler<IEnumerable<DogFact>>
    {
        public DogFactsApiHandler(CronTask task): base(task)
        {
            _dataRetriever = new DogFactsApiRetriever(task);
            _writer = new DogFactsCsvWriter();
        }
    }
}
