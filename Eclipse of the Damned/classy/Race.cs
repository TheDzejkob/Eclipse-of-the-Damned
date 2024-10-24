using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse_of_the_Damned.classy
{
    public class Race
    {

        public int RaceID { get; set; }
        public string RaceName { get; set; }
               
        public Race(string raceName, int raceID)
        {

            RaceName = raceName;
            RaceID = raceID;
        }
    }
}
