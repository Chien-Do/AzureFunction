using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionAppDemo.DurableFunctions.Aggregator.Models;

namespace FunctionAppDemo.DurableFunctions.Aggregator
{
    public static class OnCandidateVoting
    {
        [FunctionName("OnCandidateVoting")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function,  "post", Route = null)] HttpRequest req,
            ILogger log,
            [Queue(Constants.Queue)] IAsyncCollector<VoteRequestModel> orderQueue)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var order = JsonConvert.DeserializeObject<VoteRequestModel>(requestBody);
            await orderQueue.AddAsync(order);

            return new OkObjectResult("");
        }
    }
}
