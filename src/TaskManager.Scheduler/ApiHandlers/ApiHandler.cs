using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Scheduler.ApiRetrievers;

namespace TaskManager.Scheduler.ApiHandlers
{
    [ApiHandler]
    public abstract class ApiHandler<TModel> : IApiHandlerInvoker
    {
        protected ApiRetriever<TModel> _dataRetriever;
        public TModel Data { get; private set; }

        public virtual async Task InvokeAsync()
        {
            await _dataRetriever.RetrieveData();
        }
    }
}
