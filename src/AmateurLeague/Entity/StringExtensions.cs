﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AmateurLeague.Entity
{
    public static class StringExtensions
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
