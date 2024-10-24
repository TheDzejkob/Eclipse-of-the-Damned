using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse_of_the_Damned.classy
{
    public class Entity
    {
        public string EntityName { get; set; }
        public bool Gender { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Health { get; set; }

        public int Armor { get; set; }
        public List<Item> Inventory { get; set; }

        public Class EntityClass { get; set; }
        public Race EntityRace { get; set; }


        public Entity(string entityName, int level, int experience, int health, int armor, List<Item> inventory, Class entityClass, Race entityRace, bool gender)
        {
            EntityName = entityName;
            Gender = gender;
            Level = level;
            Experience = experience;
            Health = health;
            Armor = armor;
            Inventory = inventory;
            EntityClass = entityClass;
            EntityRace = entityRace;
        }
    }
}
