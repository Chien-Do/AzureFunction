using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;


namespace FunctionAppDemo.DurableFunctions.Chaining.ActivityFunctions
{
    public static class ActivityFunc
    {
        [FunctionName("ActivityFunc")]
        public static int Run([ActivityTrigger] int number, ILogger log)
        {
            number += 10;
            log.LogInformation($"Process FirstActivity - | Input: {number} Output: {number} |");
            return number;
        }
    }
}
