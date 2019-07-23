
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace AmateurLeague.Entity
{
    public enum SportGenderTypes
    {
        Men,
        Women,
        Coed
    }

    [DataContract(IsReference = true)]
    public class Sport
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public SportGenderTypes Type { get; set; }

        [Column("GenderType")]
        public string GenderType
        {
            get { return Type.ToString(); }
            private set { Type = value.ParseEnum<SportGenderTypes>(); }
        }

        public ICollection<League> Leagues { get; set; }

        public override string ToString()
        {
            var strBldr = new StringBuilder();
            strBldr.Append($"{{\"Id\": \"{Id}\",");
            strBldr.Append($"\"Name\": \"{Name}\",");
            strBldr.Append($"\"GenderType\": \"{GenderType}\"}}");

            return strBldr.ToString();
        }
    }
}