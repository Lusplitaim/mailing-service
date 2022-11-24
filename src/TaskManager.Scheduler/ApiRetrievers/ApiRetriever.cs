using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Scheduler.ApiRetrievers
{
    public class ApiRetriever<TModel>
    {
        private CronTask _task;
        public ApiRetriever(CronTask task)
        {
            _task = task;
        }

        public virtual string CreateRequestParams()
        {
            string? requestParams = _task.UrlParamsString;
            if (requestParams is null) return string.Empty;
            if (requestParams.StartsWith('?')) return requestParams;
            else return $"/{requestParams}";
        }

        public Task<TModel> RetrieveData()
        {
            var client = new RestClient(_task.Api.Url);
            string reqParams = CreateRequestParams();
            var request = new RestRequest(reqParams);
            return client.GetAsync<TModel>(request)!;
        }
    }
}
