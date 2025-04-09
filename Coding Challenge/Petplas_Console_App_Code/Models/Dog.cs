using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Models
{
    internal class Dog : Pet
    {
        public string DogBreed { get; set; }

        public Dog(string name, int age, string breed, string dogBreed)
            : base(name, age, breed , "dog")
        {
            DogBreed = dogBreed;
        }

        public override string ToString()
        {
            return base.ToString() + ", Dog Breed: " + DogBreed;
        }
    }
}
