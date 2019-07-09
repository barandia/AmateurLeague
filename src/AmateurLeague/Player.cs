using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AmateurLeague
{
    public enum GenderType 
    {
        Male,
        Female
    }

    /**
     * A player can be on one team per league
     */
    public class Player
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public GenderType Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<TeamPlayer> TeamPlayers { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
