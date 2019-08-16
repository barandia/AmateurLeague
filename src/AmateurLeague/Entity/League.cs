
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AmateurLeague.Entity
{
    /**
     * A league can have many teams
     */
    [DataContract(IsReference = true)]
    public class League
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public Sport Sport { get; set; }
        public ICollection<Team> Teams { get; set; }

        public override string ToString()
        {
            var strBldr = new StringBuilder();
            strBldr.Append($"{{\"LeagueId\": \"{LeagueId}\",");
            strBldr.Append($"\"LeagueName\": \"{LeagueName}\",");
            strBldr.Append($"\"Sport\": {Sport.ToString()}");
            if (Teams != null)
            {
                strBldr.Append(",\"Teams\": [");
                strBldr.Append(string.Join(",", Teams));
                strBldr.Append("]");
            }
            strBldr.Append("}");
            return strBldr.ToString();
        }
    }
}
