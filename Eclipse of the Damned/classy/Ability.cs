using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse_of_the_Damned.classy
{
    public class Ability
    {
        public int Id;
        public string Name;
        public string Description;
        public int Heal;
        public int Demage;
        public int Armor;
        public Ability( int id, string name, string description, int heal, int demage, int armor) 
        {
            Id = id;
            Name = name;
            Description = description;
            Heal = heal;
            Armor = armor;
        }
    }
}
