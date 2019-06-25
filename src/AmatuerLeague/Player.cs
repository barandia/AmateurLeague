using System;

namespace AmatuerLeague
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
        public string Id { get; private set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Player()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
