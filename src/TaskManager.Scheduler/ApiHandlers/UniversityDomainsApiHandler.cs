using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;
using TaskManager.Scheduler.Writers;

namespace TaskManager.Scheduler.ApiHandlers
{
    public class UniversityDomainsApiHandler : ApiHandler<IEnumerable<University>>
    {
        public UniversityDomainsApiHandler(CronTask task) : base(task)
        {
            _writer = new UniversityDomainsCsvWriter();
        }
    }
}
