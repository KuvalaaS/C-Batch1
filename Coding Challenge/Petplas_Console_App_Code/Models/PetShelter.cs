using System;
using System.Collections.Generic;

namespace PetPals.Models
{
    public class PetShelter
    {
    
        private List<Pet> availablePets;

        public PetShelter()
        {
            availablePets = new List<Pet>();
        }

      
        public void AddPet(Pet pet)
        {
            if (pet != null)
                availablePets.Add(pet);
        }

  
        public void RemovePet(Pet pet)
        {
            if (pet != null && availablePets.Contains(pet))
                availablePets.Remove(pet);
        }

   
        public void ListAvailablePets()
        {
            Console.WriteLine("\nAvailable Pets in Shelter:");

            if (availablePets.Count == 0)
            {
                Console.WriteLine("No pets available for adoption.");
                return;
            }

            foreach (var pet in availablePets)
            {
                Console.WriteLine(pet);
            }
        }

        
        public List<Pet> GetAvailablePets()
        {
            return new List<Pet>(availablePets);
        }
    }
}
