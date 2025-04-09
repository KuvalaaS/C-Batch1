using System;
using PetPals.Models;

namespace PetPals.Dao
{
    public interface IPetService
    {
        void ShowAvailablePets();
        void RecordDonation(string name, decimal amount, DateTime date);
        void ShowAdoptionEvents();
        void RegisterParticipant(string name, string type, int eventId);
        void AddPet(Pet pet);
    }
}
