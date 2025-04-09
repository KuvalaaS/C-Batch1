using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Models
{
    public class ItemDonation : Donation
    {
        public string ItemType { get; set; }

        public ItemDonation(string donorName, decimal amount, string itemType)
            : base(donorName, amount)
        {
            ItemType = itemType;
        }

        public override void RecordDonation()
        {
            Console.WriteLine("Item Donation Recorded:");
            Console.WriteLine("Donor: " + DonorName);
            Console.WriteLine("Amount: " + Amount);
            Console.WriteLine("Item Type: " + ItemType);
        }
    }

}
