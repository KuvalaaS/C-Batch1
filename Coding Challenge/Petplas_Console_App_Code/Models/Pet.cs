using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Models
{
    public class Pet 
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public string Type { get; set; }

        public Pet(string name, int age, string breed, string type)
        {
            Name = name;
            Age = age;
            Breed = breed;
            Type = type;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}, Breed: {Breed}, Type: {Type}";
        }
    }
}
