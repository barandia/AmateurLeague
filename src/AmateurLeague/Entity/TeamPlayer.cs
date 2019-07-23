namespace AmateurLeague.Entity
{
    public class TeamPlayer
    {
        public string TeamId { get; set; }
        public Team Team { get; set; }
        public string PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
