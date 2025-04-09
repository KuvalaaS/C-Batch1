using PetPals.Dao;
using PetPals.Models;
using System;
using System.Collections.Generic;
using System.IO;
using PetPals.Exceptions;


class Program
{
    static void Main(string[] args)
    {
        IPetService service = new PetServiceImpl();
        PetShelter shelter = new PetShelter();

        while (true)
        {
            Console.WriteLine("\n===== PetPals Platform =====");
            Console.WriteLine("1. Show Available Pets ");
            Console.WriteLine("2. Record Cash Donation");
            Console.WriteLine("3. Show Adoption Events");
            Console.WriteLine("4. Register for Event");
            Console.WriteLine("5. Add Pet to Shelter (with age check)");
            Console.WriteLine("6.Read Pets from File (file handling)");
            Console.WriteLine("7.Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    service.ShowAvailablePets();
                    break;

                case "2":
                    try
                    {
                        Console.Write("Enter Donor Name: ");
                        string donor = Console.ReadLine();

                        Console.Write("Enter Donation Amount: ");
                        decimal amount = decimal.Parse(Console.ReadLine());

                        if (amount < 10)
                            throw new InsufficientFundsException("Donation must be at least ₹10.");

                        Console.Write("Enter Donation Date (yyyy-mm-dd): ");
                        DateTime date = DateTime.Parse(Console.ReadLine());

                        service.RecordDonation(donor, amount, date);
                    }
                    catch (InsufficientFundsException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please enter numeric values for amount and valid date format.");
                    }
                    break;

                case "3":
                    service.ShowAdoptionEvents();
                    break;

                case "4":
                    Console.Write("Enter Participant Name: ");
                    string participant = Console.ReadLine();
                    Console.Write("Enter Participant Type (Shelter/Adopter): ");
                    string type = Console.ReadLine();
                    Console.Write("Enter Event ID to Register: ");
                    int eventId = int.Parse(Console.ReadLine());

                    service.RegisterParticipant(participant, type, eventId);
                    break;

                case "5":
                    try
                    {
                        Console.Write("Enter Pet Name: ");
                        string pname = Console.ReadLine();

                        Console.Write("Enter Pet Age: ");
                        int age = int.Parse(Console.ReadLine());

                        if (age <= 0)
                            throw new InvalidPetAgeException("Pet age must be greater than 0.");

                        Console.Write("Enter Pet Breed: ");
                        string breed = Console.ReadLine();

                        Console.Write("Enter Pet Type (e.g., Dog/Cat): ");
                        string ptype = Console.ReadLine();

                        Pet newPet = new Pet(pname, age, breed, ptype);
                        shelter.AddPet(newPet);
                        service.AddPet(newPet);

                        Console.WriteLine("Pet added to shelter and database.");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Age must be a number.");
                    }
                    catch (InvalidPetAgeException ex)
                    {
                        Console.WriteLine("Validation Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    break;


                case "6":
                    Console.Write("Enter file path to read pets: ");
                    string filePath = Console.ReadLine();
                    try
                    {
                        string[] lines = File.ReadAllLines(filePath);
                        Console.WriteLine("File Contents:");
                        foreach (string line in lines)
                            Console.WriteLine(line);
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine("File Error: " + ex.Message);
                    }
                    break;

                case "7":
                    Console.WriteLine("Exiting PetPals. Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
