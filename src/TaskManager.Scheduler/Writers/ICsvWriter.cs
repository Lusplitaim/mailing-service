using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Scheduler.Writers
{
    public interface ICsvWriter<TModel>
    {
        string Write(TModel model);
    }
}
