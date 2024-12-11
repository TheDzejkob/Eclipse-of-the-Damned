using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse_of_the_Damned.classy
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public int Weight { get; set; }

        public int Value { get; set; }

        public int Rarity { get; set; }
        public float Durability { get; set; }

        public string ImagePath { get; set; }

        public Item(int id, string name, string description, string category, float durability, string imagePath) 
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
            Durability = durability;
            ImagePath = imagePath;
        }
    }
}
