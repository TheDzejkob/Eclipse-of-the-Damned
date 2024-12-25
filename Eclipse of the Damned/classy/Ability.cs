using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse_of_the_Damned.classy
{
    public class Ability
    {
        public string Name;
        public string Description;
        public int Heal;
        public int Damage;
        public int Armor;
        public int Cost;
        public Ability( string name, string description, int heal, int damage, int armor, int cost) 
        {
            Name = name;
            Description = description;
            Heal = heal;
            Damage = damage;
            Armor = armor;
            Cost = cost;
        }
    }
}
