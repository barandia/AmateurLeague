using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AmateurLeague
{
    /**
     * A team can have one or many players (Roster).
     * A team is exactly in one league
     */
    public class Team
    {
        public string Id { get; private set;}
        public string Name { get; set;}
        // Todo: const or readonly? Need to figure out how to set this property only once.
        public string LeagueName { get; private set;}   
        public string CaptainEmailAddress { get; set;}
        public string CoCaptainEmailAddress { get; set;} 
        public List<string> Roster = new List<string>();

        public Team(string leagueName) 
        {
            Id = Guid.NewGuid().ToString();
            LeagueName = leagueName;
        }

        public void AddPlayer(string emailAddress) 
        {
            Roster.Add(emailAddress);
        }

        public void RemovePlayer(string emailAddress)
        {
            Roster.Remove(emailAddress);
        }

        public bool IsPlayerOnRoster(string emailAddress)
        {
            return Roster.Contains(emailAddress);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
