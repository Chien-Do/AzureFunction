using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDemo.DurableFunctions.Aggregator
{

    public interface IVotingCounter
    {
        Dictionary<string, int> Voting { get; set; }

        void Add(string candidate);

        void Reset(string candidate);

        Dictionary<string, int> Get();
    }
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class VotingCounter : IVotingCounter
    {
        [JsonProperty("voting")]
        public Dictionary<string, int> Voting { get; set; } = new Dictionary<string, int>();

        public void Add(string candidate)
        {
            if (this.Voting.ContainsKey(candidate))
            {
                this.Voting[candidate]++;
            }
            else
            {
                this.Voting.Add(candidate, 1);
            }
        }

        public void Reset(string candidate) => this.Voting[candidate] = 0;

        public Dictionary<string, int> Get() => this.Voting;
    }
}
