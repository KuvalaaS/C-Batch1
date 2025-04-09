using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Models
{
    public class AdoptionEvent
    {
        private List<IAdoptable> Participants;

        public AdoptionEvent()
        {
            Participants = new List<IAdoptable>();
        }

        public void RegisterParticipant(IAdoptable participant)
        {
            Participants.Add(participant);
            Console.WriteLine("Participant registered for the adoption event.");
        }

        public void HostEvent()
        {
            Console.WriteLine("Adoption Event Hosted. Starting adoptions:");
            foreach (var participant in Participants)
            {
                participant.Adopt();
            }
        }
    }
}
