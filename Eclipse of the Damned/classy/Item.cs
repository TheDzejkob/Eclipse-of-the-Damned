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
        public string Type { get; set; }
        public float Durability { get; set; }

        public string ImgPath { get; set; }

        public Item(int id, string name, string description, string type, float durability, string imgpath) 
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
            Durability = durability;
            ImgPath = imgpath;
        }
    }
}
