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
        public DogFactsApiHandler(CronTask task)
        {
            _dataRetriever = new DogFactsApiRetriever(task);
            _writer = new DogFactsCsvWriter();
            _emailSender = new DogFactsEmailSender(task);
        }

        /*public override async Task InvokeAsync()
        {
            var data = await _dataRetriever.RetrieveData();
            string filename = _writer.Write(data);
            await _emailSender.SendAsync(filename);
        }*/
    }
}
