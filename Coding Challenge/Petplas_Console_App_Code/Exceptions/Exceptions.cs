using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Exceptions
{
 
    /// Thrown when a pet age is invalid (e.g., negative).
  
    public class InvalidPetAgeException : ApplicationException
    {
        public InvalidPetAgeException(string message) : base(message) { }
    }


    /// Thrown when the donation amount is less than the allowed minimum.

    public class InsufficientFundsException : ApplicationException
    {
        public InsufficientFundsException(string message) : base(message) { }
    }


    /// Thrown when there is an error related to pet adoption.

    public class AdoptionException : Exception
    {
        public AdoptionException(string message) : base(message) { }
    }
}
