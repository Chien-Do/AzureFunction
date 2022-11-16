using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Net.Http;
using System.Net;

namespace FunctionAppDemo.DurableFunctions
{
    public static class HTTPStart
    {
        [FunctionName("HttpStart")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "start/{functionName}")] HttpRequestMessage req,
            [DurableClient] IDurableClient orchestratorClient,
            string functionName,
            ILogger log)
        {
            log.LogInformation("Processing HTTP START FF....");
            if (string.IsNullOrEmpty(functionName))
            {
                log.LogError("Orchestrator Name can not be null.");
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("Orchestrator Name can not be null. Please try again!")
                };
            }
            try
            {
                var orchestratorInput = await req.Content.ReadAsAsync<object>();
                string instanceId = await orchestratorClient.StartNewAsync(functionName, orchestratorInput);

                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(instanceId)
                };
            }
            catch (Exception ex)
            {
                log.LogError($"Error start {functionName} function: {ex.Message}");
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(ex.Message)
                };
            }
        }
    }
}
