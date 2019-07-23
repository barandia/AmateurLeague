using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace AmateurLeague.Entity
{
    public enum GenderType
    {
        Male,
        Female
    }

    /**
     * A player can be on one team per league
     */
    //[DataContract(IsReference = true)]
    public class Player
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }

        public override string ToString()
        {
            var strBldr = new StringBuilder();
            strBldr.Append($"{{\"EmailAddress\": \"{EmailAddress}\",");
            strBldr.Append($"\"FirstName\": \"{FirstName}\",");
            strBldr.Append($"\"LastName\": \"{LastName}\",");
            strBldr.Append($"\"Gender\": \"{Gender}\",");
            strBldr.Append($"\"DateOfBirth\": \"{DateOfBirth}\"}}");

            return strBldr.ToString();
        }
    }
}