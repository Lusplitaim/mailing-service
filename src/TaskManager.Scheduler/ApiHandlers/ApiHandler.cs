using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Scheduler.ApiRetrievers;
using TaskManager.Scheduler.EmailSenders;
using TaskManager.Scheduler.Writers;

namespace TaskManager.Scheduler.ApiHandlers
{
    [ApiHandler]
    public abstract class ApiHandler<TModel> : IApiHandlerInvoker
    {
        protected ApiRetriever<TModel> _dataRetriever;
        protected ICsvWriter<TModel> _writer;
        protected IEmailSender _emailSender;

        public virtual async Task InvokeAsync()
        {
            TModel data = await _dataRetriever.RetrieveData();
            string filename = _writer.Write(data);
            await _emailSender.SendWithAttachmentAsync(filename);
        }
    }
}
