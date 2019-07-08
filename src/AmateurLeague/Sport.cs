using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmateurLeague
{
    public enum SportGenderTypes
    {
        Men,
        Women,
        Coed
    }

    public class Sport
    {
        public string Name { get; private set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SportGenderTypes Type { get; private set; }

        public Sport(string name, SportGenderTypes type)
        {
            Name = name;
            Type = type;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
