﻿namespace AmateurLeague.Entity
{
    public class TeamPlayer
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
