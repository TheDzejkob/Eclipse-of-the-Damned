using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse_of_the_Damned.classy
{
    public class Class
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        
        public Class(string className, int classID)
        {
            ClassID = classID;
            ClassName = className;
        }
    }
}
