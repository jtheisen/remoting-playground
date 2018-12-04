using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RemotingPlayground.TestClient
{
    class Client
    {
        private readonly String url;

        public Client(String url)
        {
            this.url = url;
        }

        public Task<T> ExecuteAsync<T>(Expression<Func<T>> expr) => ExecuteImplAsync<T>(expr);

        public Task<T> ExecuteAsync<T>(Expression<Func<Task<T>>> expr) => ExecuteImplAsync<T>(expr);

        async Task<T> ExecuteImplAsync<T>(LambdaExpression expr)
        {
            var requestJson = ExpressionSerialization.Serialize(expr);

            var webClient = new WebClient();

            webClient.Headers.Add("Content-Type", "application/json");
            var resultJson = await webClient.UploadStringTaskAsync(url, requestJson);

            var result = JsonConvert.DeserializeObject<T>(resultJson);

            return result;
        }
    }
}
