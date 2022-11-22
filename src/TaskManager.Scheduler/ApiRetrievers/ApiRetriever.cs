using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Scheduler.ApiRetrievers
{
    public abstract class ApiRetriever<TModel>
    {
        private CronTask _task;
        public ApiRetriever(CronTask task)
        {
            _task = task;
        }

        public abstract string CreateRequestParams();

        public Task<TModel> RetrieveData()
        {
            var client = new RestClient(_task.Api.Url);
            string reqParams = CreateRequestParams();
            var request = new RestRequest(reqParams);
            return client.GetAsync<TModel>(request)!;
        }
    }
}
