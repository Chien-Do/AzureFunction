using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionAppDemo.DurableFunctions.Aggregator.Models
{
    public class VoteRequestModel
    {
        public string Candidate { get; set; }
    }
}
