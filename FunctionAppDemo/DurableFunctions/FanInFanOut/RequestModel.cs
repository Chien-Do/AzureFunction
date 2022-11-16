namespace FunctionAppDemo.DurableFunctions.FanInFanOut
{
    public class RequestModel
    {
        public bool ForceCrash { get; set; }
        public string ServerName { get; set; }
    }
}
