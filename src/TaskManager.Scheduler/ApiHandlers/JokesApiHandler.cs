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
    public class JokesApiHandler : ApiHandler<Joke>
    {
        public JokesApiHandler(CronTask task): base(task)
        {
            _writer = new JokesCsvWriter();
        }
    }
}
