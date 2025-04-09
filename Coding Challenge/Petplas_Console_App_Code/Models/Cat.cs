using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Models
{
    internal class Cat : Pet
    {
        public string CatColor { get; set; }

        public Cat(string name, int age, string breed, string catColor)
            : base (name, age, breed, "Cat")
        {
            CatColor = catColor;
        }

        public override string ToString()
        {
            return base.ToString() + ", Cat Color: " + CatColor;
        }
    }
}
