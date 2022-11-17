using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionAppDemo.DurableFunctions.FanInFanOut
{
    public class FIFOReq
    {
        public List<ServerInfoModel> servers { get; set; }
    }

    public class ServerInfoModel
    {
        public string Name { get; set; }
        public bool IsCrash { get; set; }
    }
}
