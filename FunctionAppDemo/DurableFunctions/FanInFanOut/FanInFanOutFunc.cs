using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDemo.DurableFunctions.FanInFanOut
{
    public static class FanInFanOutFunc
    {
        [FunctionName("FanInFanOutFunction")]
        public static async Task<IActionResult> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            log.LogInformation("======Processing Fan In Fan Out Function....");
            try
            {
                var input = context.GetInput<FIFOReq>();

                var tasks = new List<Task<bool>>();
                foreach (var server in input.servers)
                {
                    tasks.Add(context.CallActivityAsync<bool>("CheckItemStatusActivityFunc", new RequestModel()
                    {
                        ServerName = server.Name,
                        IsCrash = server.IsCrash
                    }));
                }

                await Task.WhenAll(tasks);

                // Check is any task crashed
                bool isHasTaskCrashed = tasks.Select(task => task.Result).ToList().Any(x => !x);
                if (isHasTaskCrashed)
                    return new BadRequestObjectResult("System Run Failed");

                return new OkObjectResult("System OK");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
