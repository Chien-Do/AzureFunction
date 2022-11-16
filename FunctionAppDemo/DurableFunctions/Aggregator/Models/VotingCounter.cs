using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionAppDemo.DurableFunctions.Aggregator.Models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class VotingCounter : IVotingCounter
    {
        [JsonProperty("voting")]
        public Dictionary<string, int> Voting { get; set; } = new  Dictionary<string, int>();

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

    public interface IVotingCounter
    {
        Dictionary<string, int> Voting { get; set; }

        void Add(string candidate);

        void Reset(string candidate);

        Dictionary<string, int> Get();
    }
}
