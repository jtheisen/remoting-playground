using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingPlayground.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public String Index()
        {
            return "hello";
        }

        [HttpPost("/execute")]
        public async Task<String> Execute()
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var requestJson = await reader.ReadToEndAsync();

                var expr = ExpressionSerialization.Deserialize(requestJson);

                var del = expr.Compile();

                var result = del.DynamicInvoke();

                if (result is Task taskResult)
                {
                    await taskResult;

                    result = result.GetType().GetProperty("Result").GetValue(result);
                }

                return JsonConvert.SerializeObject(result);
            }
        }
    }
}
