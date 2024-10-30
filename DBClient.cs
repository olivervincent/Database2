using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace HotelDBConnection
{
    class DBClient
    {
        //Database connection string - replace it with the connnection string to your own database 
        string connectionString = "Server=localhost,1433;Database=master;User Id=sa;Password=Localhost1!;TrustServerCertificate=True;";

        private int GetMaxHotelNo(SqlConnection connection)
        {
            Console.WriteLine("Calling -> GetMaxHotelNo");
            
            string queryStringMaxHotelNo = "SELECT  MAX(Hotel_No)  FROM Hotel";
            Console.WriteLine($"SQL applied: {queryStringMaxHotelNo}");

            //Apply SQL command
            SqlCommand command = new SqlCommand(queryStringMaxHotelNo, connection);
            SqlDataReader reader = command.ExecuteReader();

            //Assume undefined value 0 for max hotel_no
            int MaxHotel_No = 0;

            //Is there any rows in the query
            if  (reader.Read())
            {
                //Yes, get max hotel_no
                MaxHotel_No = reader.GetInt32(0); //Reading int from 1st column
            }

            //Close reader
            reader.Close();
            
            Console.WriteLine($"Max hotel#: {MaxHotel_No}");
            Console.WriteLine();

            //Return max hotel_no
            return MaxHotel_No;
        }

        private int DeleteHotel(SqlConnection connection, int hotel_no)
        {
            Console.WriteLine("Calling -> DeleteHotel");

            //This SQL command will delete one row from the Hotel table: The one with primary key hotel_No
            string deleteCommandString = $"DELETE FROM Hotel  WHERE Hotel_No = {hotel_no}";
            Console.WriteLine($"SQL applied: {deleteCommandString}");

            //Apply SQL command
            SqlCommand command = new SqlCommand(deleteCommandString, connection);
            Console.WriteLine($"Deleting hotel #{hotel_no}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            //Return number of rows affected
            return numberOfRowsAffected;
        }

        private int UpdateHotel(SqlConnection connection, Hotel hotel)
        {
            Console.WriteLine("Calling -> UpdateHotel");

            //This SQL command will update one row from the Hotel table: The one with primary key hotel_No
            string updateCommandString = $"UPDATE Hotel SET Name='{hotel.Name}', Address='{hotel.Address}' WHERE Hotel_No = {hotel.Hotel_No}";
            Console.WriteLine($"SQL applied: {updateCommandString}");

            //Apply SQL command
            SqlCommand command = new SqlCommand(updateCommandString, connection);
            Console.WriteLine($"Updating hotel #{hotel.Hotel_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            //Return number of rows affected
            return numberOfRowsAffected;
        }

        private int InsertHotel(SqlConnection connection, Hotel hotel)
        {
            Console.WriteLine("Calling -> InsertHotel");

            //This SQL command will insert one row into the Hotel table with primary key hotel_No
            string insertCommandString = $"INSERT INTO Hotel VALUES({hotel.Hotel_No}, '{hotel.Name}', '{hotel.Address}')";
            Console.WriteLine($"SQL applied: {insertCommandString}");

            //Apply SQL command
            SqlCommand command = new SqlCommand(insertCommandString, connection);
            
            Console.WriteLine($"Creating hotel #{hotel.Hotel_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();
            
            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            //Return number of rows affected 
            return numberOfRowsAffected;
        }

        private List<Hotel> ListAllHotels(SqlConnection connection)
        {
            Console.WriteLine("Calling -> ListAllHotels");

            //This SQL command will fetch all rows and columns from the Hotel table
            string queryStringAllHotels = "SELECT * FROM Hotel";
            Console.WriteLine($"SQL applied: {queryStringAllHotels}");

            //Apply SQL command
            SqlCommand command = new SqlCommand(queryStringAllHotels, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("Listing all hotels:");

            //NO rows in the query 
            if (!reader.HasRows)
            {
                //End here
                Console.WriteLine("No hotels in database");
                reader.Close();
                
                //Return null for 'no hotels found'
                return null;
            }

            //Create list of hotels found
            List<Hotel> hotels = new List<Hotel>();
            while (reader.Read())
            {
                //If we reached here, there is still one hotel to be put into the list 
                Hotel nextHotel = new Hotel()
                {
                    Hotel_No = reader.GetInt32(0), //Reading int from 1st column
                    Name = reader.GetString(1),    //Reading string from 2nd column
                    Address = reader.GetString(2)  //Reading string from 3rd column
                };

                //Add hotel to list
                hotels.Add(nextHotel);

                Console.WriteLine(nextHotel);
            }

            //Close reader
            reader.Close();
            Console.WriteLine();

            //Return list of hotels
            return hotels;
        }

        private Hotel GetHotel(SqlConnection connection, int hotel_no)
        {
            Console.WriteLine("Calling -> GetHotel");

            //This SQL command will fetch the row with primary key hotel_no from the Hotel table
            string queryStringOneHotel = $"SELECT * FROM Hotel WHERE hotel_no = {hotel_no}";
            Console.WriteLine($"SQL applied: {queryStringOneHotel}");

            //Prepare SQK command
            SqlCommand command = new SqlCommand(queryStringOneHotel, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine($"Finding hotel#: {hotel_no}");

            //NO rows in the query? 
            if (!reader.HasRows)
            {
                //End here
                Console.WriteLine("No hotels in database");
                reader.Close();

                //Return null for 'no hotel found'
                return null;
            }

            //Fetch hotel object from teh database
            Hotel hotel = null; 
            if (reader.Read())
            {
                hotel = new Hotel()
                {
                    Hotel_No = reader.GetInt32(0), //Reading int from 1st column
                    Name = reader.GetString(1),    //Reading string from 2nd column
                    Address = reader.GetString(2)  //Reading string from 3rd column
                };

                Console.WriteLine(hotel);
            }

            //Close reader
            reader.Close();
            Console.WriteLine();

            //Return found hotel
            return hotel;
        }
        
        private int InsertFacility(SqlConnection connection, string facilityName)
        {
            Console.WriteLine("Calling -> InsertFacility");

            string insertCommandString = $"INSERT INTO Facilities (Facility_Name) VALUES ('{facilityName}')";
            Console.WriteLine($"SQL applied: {insertCommandString}");

            SqlCommand command = new SqlCommand(insertCommandString, connection);
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int UpdateFacility(SqlConnection connection, int facilityId, string newFacilityName)
        {
            Console.WriteLine("Calling -> UpdateFacility");

            string updateCommandString = $"UPDATE Facilities SET Facility_Name='{newFacilityName}' WHERE Facility_Id = {facilityId}";
            Console.WriteLine($"SQL applied: {updateCommandString}");

            SqlCommand command = new SqlCommand(updateCommandString, connection);
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int DeleteFacility(SqlConnection connection, int facilityId)
        {
            Console.WriteLine("Calling -> DeleteFacility");

            // First, delete related entries in HotelFacilities
            string deleteHotelFacilitiesCommandString = $"DELETE FROM HotelFacilities WHERE Facility_Id = {facilityId}";
            Console.WriteLine($"SQL applied: {deleteHotelFacilitiesCommandString}");

            SqlCommand deleteHotelFacilitiesCommand = new SqlCommand(deleteHotelFacilitiesCommandString, connection);
            int numberOfRowsAffectedHotelFacilities = deleteHotelFacilitiesCommand.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected in HotelFacilities: {numberOfRowsAffectedHotelFacilities}");

            // Then, delete the facility
            string deleteFacilityCommandString = $"DELETE FROM Facilities WHERE Facility_Id = {facilityId}";
            Console.WriteLine($"SQL applied: {deleteFacilityCommandString}");

            SqlCommand deleteFacilityCommand = new SqlCommand(deleteFacilityCommandString, connection);
            int numberOfRowsAffectedFacility = deleteFacilityCommand.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected in Facilities: {numberOfRowsAffectedFacility}");
            Console.WriteLine();

            return numberOfRowsAffectedFacility;
        }

        private List<string> ListAllFacilities(SqlConnection connection)
        {
            Console.WriteLine("Calling -> ListAllFacilities");

            string queryStringAllFacilities = "SELECT Facility_Name FROM Facilities";
            Console.WriteLine($"SQL applied: {queryStringAllFacilities}");

            SqlCommand command = new SqlCommand(queryStringAllFacilities, connection);
            SqlDataReader reader = command.ExecuteReader();

            List<string> facilities = new List<string>();
            while (reader.Read())
            {
                facilities.Add(reader.GetString(0));
            }

            reader.Close();
            Console.WriteLine();

            return facilities;
        }
        
        private int InsertHotelFacility(SqlConnection connection, int hotelNo, int facilityId)
        {
            Console.WriteLine("Calling -> InsertHotelFacility");

            string insertCommandString = $"INSERT INTO HotelFacilities (Hotel_No, Facility_Id) VALUES ({hotelNo}, {facilityId})";
            Console.WriteLine($"SQL applied: {insertCommandString}");

            SqlCommand command = new SqlCommand(insertCommandString, connection);
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }
        
        private int DeleteHotelFacility(SqlConnection connection, int hotelNo, int facilityId)
        {
            Console.WriteLine("Calling -> DeleteHotelFacility");

            string deleteCommandString = $"DELETE FROM HotelFacilities WHERE Hotel_No = {hotelNo} AND Facility_Id = {facilityId}";
            Console.WriteLine($"SQL applied: {deleteCommandString}");

            SqlCommand command = new SqlCommand(deleteCommandString, connection);
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }
        
        private List<string> ListAllHotelFacilities(SqlConnection connection, int hotelNo)
        {
            Console.WriteLine("Calling -> ListAllHotelFacilities");

            string queryStringAllHotelFacilities = $"SELECT f.Facility_Name FROM Facilities f JOIN HotelFacilities hf ON f.Facility_Id = hf.Facility_Id WHERE hf.Hotel_No = {hotelNo}";
            Console.WriteLine($"SQL applied: {queryStringAllHotelFacilities}");

            SqlCommand command = new SqlCommand(queryStringAllHotelFacilities, connection);
            SqlDataReader reader = command.ExecuteReader();

            List<string> facilities = new List<string>();
            while (reader.Read())
            {
                facilities.Add(reader.GetString(0));
            }

            reader.Close();
            Console.WriteLine();

            return facilities;
        }
        
        public void Start()
        {
            // Apply 'using' to connection (SqlConnection) in order to call Dispose (interface IDisposable)
            // whenever the 'using' block exits
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open connection
                connection.Open();

                // List all hotels in the database
                ListAllHotels(connection);

                // Create a new hotel with primary key equal to current max primary key plus 1
                Hotel newHotel = new Hotel()
                {
                    Hotel_No = GetMaxHotelNo(connection) + 1,
                    Name = "New Hotel",
                    Address = "Maglegaardsvej 2, 4000 Roskilde"
                };

                // Insert the hotel into the database
                InsertHotel(connection, newHotel);

                // List all hotels including the newly inserted one
                ListAllHotels(connection);

                // Get the newly inserted hotel from the database in order to update it
                Hotel hotelToBeUpdated = GetHotel(connection, newHotel.Hotel_No);

                // Alter Name and Address properties
                hotelToBeUpdated.Name += "(updated)";
                hotelToBeUpdated.Address += "(updated)";

                // Update the hotel in the database
                UpdateHotel(connection, hotelToBeUpdated);

                // List all hotels including the updated one
                ListAllHotels(connection);

                // Get the updated hotel in order to delete it
                Hotel hotelToBeDeleted = GetHotel(connection, hotelToBeUpdated.Hotel_No);

                // Delete the hotel
                DeleteHotel(connection, hotelToBeDeleted.Hotel_No);

                // List all hotels - now without the deleted one
                ListAllHotels(connection);

                // Insert a new facility
                InsertFacility(connection, "Baby Pool");

                // List all facilities
                List<string> facilities = ListAllFacilities(connection);
                Console.WriteLine("Facilities:");
                facilities.ForEach(Console.WriteLine);

                // Update an existing facility
                UpdateFacility(connection, 1, "Swimming Pool New Name");

                // List all facilities after update
                facilities = ListAllFacilities(connection);
                Console.WriteLine("Facilities after update:");
                facilities.ForEach(Console.WriteLine);

                // Delete a facility
                DeleteFacility(connection, 1);

                // List all facilities after deletion
                facilities = ListAllFacilities(connection);
                Console.WriteLine("Facilities after deletion:");
                facilities.ForEach(Console.WriteLine);

                // Insert a new hotel facility
                InsertHotelFacility(connection, 3, 2);

                // List all facilities for the new hotel
                List<string> hotelFacilities = ListAllHotelFacilities(connection, newHotel.Hotel_No);
                Console.WriteLine($"Facilities for hotel {newHotel.Hotel_No}:");
                hotelFacilities.ForEach(Console.WriteLine);

                // Delete a hotel facility
                DeleteHotelFacility(connection, 2, 4);

                // List all facilities for the new hotel after deletion
                hotelFacilities = ListAllHotelFacilities(connection, newHotel.Hotel_No);
                Console.WriteLine($"Facilities for hotel {newHotel.Hotel_No} after deletion:");
                hotelFacilities.ForEach(Console.WriteLine);
            }
        }
    }
}
