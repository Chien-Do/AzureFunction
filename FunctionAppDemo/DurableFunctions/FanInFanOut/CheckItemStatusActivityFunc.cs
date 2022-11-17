using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionAppDemo.DurableFunctions.FanInFanOut
{
    public static class CheckItemStatusActivityFunc
    {
        [FunctionName("CheckItemStatusActivityFunc")]
        public static bool RunActivity([ActivityTrigger] RequestModel input, ILogger log)
        {
            log.LogInformation("-----Running CheckStatusServer Activity");

            if (input == null)
            {
                log.LogError("Run CheckItemStatusActivityFunc error: Input NULL");
                throw new ArgumentException("Server Information can not be NULL");
            }

            log.LogInformation($"CheckStatusServer: {input.ServerName}");

            return !input.IsCrash;
        }
    }
}
