
using System.Collections.Generic;
using System.Text;

namespace AmateurLeague.Entity
{
    /**
     * A league can have many teams
     */
    //[JsonObject(IsReference = true)]
    public class League
    {
        public string Name { get; set; }
        public Sport Sport { get; set; }
        public ICollection<Team> Teams { get; set; }

        public override string ToString()
        {
            var strBldr = new StringBuilder();
            strBldr.Append($"{{\"Name\": \"{Name}\",");
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
