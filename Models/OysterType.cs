using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{

    public static class OysterType
    {
        public enum OysterTypes
        {
            Arcachon_3,
            Arcachon_4,
            Arguin_2,
            Arguin_3,
            Arguin_4
        }


        public static string GetOysterTypeString(OysterTypes type)
        {
            return type.ToString().Replace('_', ' ');
        }

        public static List<OysterTypes> GetOysterTypes()
        {
            return Enum.GetValues(typeof(OysterTypes)).Cast<OysterTypes>().ToList();
        }
    }
}