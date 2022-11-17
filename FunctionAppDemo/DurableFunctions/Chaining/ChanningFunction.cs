using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace FunctionAppDemo.DurableFunctions.Chaining
{
    public static class ChanningFunction
    {
        [FunctionName("ChanningFunction")]
        public static async Task<IActionResult> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            try
            {
                int firstCallVal = await context.CallActivityAsync<int>("ActivityFunc", 0);

                int secondCallVal = await context.CallActivityAsync<int>("ActivityFunc", firstCallVal);

                int thirdCallVal = await context.CallActivityAsync<int>("ActivityFunc", secondCallVal);

                var msg = $"------Finish Channing Function: First {firstCallVal}; Second {secondCallVal}; Third {thirdCallVal}";
                log.LogInformation(msg);

                return new OkObjectResult(msg);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}