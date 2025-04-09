using System;
using System.Data.SqlClient;
using PetPals.Util;
using System.Data.SqlClient;
using PetPals.Models;
using PetPals.Exceptions;


namespace PetPals.Dao
{
    public class PetServiceImpl : IPetService
    {
        static SqlConnection con;
        static SqlCommand cmd;
        static SqlDataReader dr;

        public void ShowAvailablePets()
        {
            try
            {
                con = DBConnUtil.GetConnection(); 
                cmd = new SqlCommand("SELECT name, age, breed, type FROM tbl_pets WHERE availableforadoption = 1", con);
                dr = cmd.ExecuteReader();

                Console.WriteLine("\nAvailable Pets:");
                while (dr.Read())
                {
                    try
                    {
                        string name = dr["name"].ToString();
                        int age = Convert.ToInt32(dr["age"]);
                        string breed = dr["breed"].ToString();
                        string type = dr["type"].ToString();

                        Console.WriteLine($"Name: {name}, Age: {age}, Breed: {breed}, Type: {type}");
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Pet information is incomplete or missing.");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            dr.Close();
            con.Close();

        }

        public void RecordDonation(string name, decimal amount, DateTime date)
        {
            try
            {
                con = DBConnUtil.GetConnection();
                cmd = new SqlCommand("INSERT INTO tbl_donations (donorname, donationtype, donationamount, donationdate) VALUES (@name, 'Cash', @amount, @date)", con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Donation recorded successfully.");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            dr.Close();
            con.Close();
        }

        public void ShowAdoptionEvents()
        {
            try
            {
                con = DBConnUtil.GetConnection();
                cmd = new SqlCommand("SELECT eventid, eventname, eventdate, location FROM tbl_adoptionevents", con);
                dr = cmd.ExecuteReader();

                Console.WriteLine("\nUpcoming Adoption Events:");
                while (dr.Read())
                {
                    Console.WriteLine($"ID: {dr[0]}, Name: {dr[1]}, Date: {Convert.ToDateTime(dr[2]).ToShortDateString()}, Location: {dr[3]}");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            dr.Close();
            con.Close();

        }

        public void RegisterParticipant(string name, string type, int eventId)
        {
            try
            {
                con = DBConnUtil.GetConnection();
                cmd = new SqlCommand("INSERT INTO tbl_participants (participantname, participanttype, eventid) VALUES (@name, @type, @eid)", con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@eid", eventId);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Registered successfully.");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            dr.Close();
            con.Close();
        }
        public void AddPet(Pet pet)
        {
            try
            {
                if (pet.Age <= 0)
                    throw new InvalidPetAgeException("Pet age must be greater than 0.");

                con = DBConnUtil.GetConnection();
                cmd = new SqlCommand("INSERT INTO tbl_pets (name, age, breed, type, availableforadoption) VALUES (@name, @age, @breed, @type, @available)", con);
                cmd.Parameters.AddWithValue("@name", pet.Name);
                cmd.Parameters.AddWithValue("@age", pet.Age);
                cmd.Parameters.AddWithValue("@breed", pet.Breed);
                cmd.Parameters.AddWithValue("@type", pet.Type);
                cmd.Parameters.AddWithValue("@available", 1);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Pet added to database.");
            }
            catch (InvalidPetAgeException ex)
            {
                Console.WriteLine("Validation Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding pet: " + ex.Message);
            }
            con.Close();
        }

    }
}
