namespace FunctionAppDemo.DurableFunctions.FanInFanOut
{
    public class RequestModel
    {
        public bool IsCrash { get; set; }
        public string ServerName { get; set; }
    }
}
