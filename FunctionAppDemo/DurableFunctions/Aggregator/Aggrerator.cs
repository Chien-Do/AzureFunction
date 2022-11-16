using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using FunctionAppDemo.DurableFunctions.Aggregator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionAppDemo.DurableFunctions.Aggregator
{
    public class Aggregator
    {
        [FunctionName("QueueTrigger")]
        public async Task Run(
            [QueueTrigger(Constants.Queue, Connection = "AzureWebJobsStorage")] string queueItem,
            [DurableClient] IDurableEntityClient entityClient)
        {
            var model = JsonConvert.DeserializeObject<VoteRequestModel>(queueItem);
            var entityId = new EntityId(Constants.Entity, Constants.Entity);
            await entityClient.SignalEntityAsync(entityId, "add", model.Candidate);
        }

        [FunctionName("Aggerator")]
        public async Task<IActionResult> GetEntities(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestMessage req,
            [DurableClient] IDurableEntityClient entityClient)
        {

            var entityId = new EntityId(Constants.Entity, Constants.Entity);
            await entityClient.SignalEntityAsync(entityId, "get");

            var entity =
                await entityClient.ReadEntityStateAsync<VotingCounter>(entityId);

            return (ActionResult)new OkObjectResult(entity);
        }

    }
}
