using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace AddressBook_ADO
{
    public class AddressBookRepository
    {
        public static string connectionString = @"Server=MUKESH\SQLEXPRESS; Initial Catalog =addressBookService;;Integrated Security=True;Connect Timeout=30;
                       Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection = new SqlConnection(connectionString);

        public List<ContactDetails> contactsList = new List<ContactDetails>();
        ///UC16 Retrieve All Details
        public List<ContactDetails> GetAddressBookDetails()
        {
            try
            {
                using (connection)
                {
                    string query = @"SELECT CD.* ,A.Area, A.city,A.State,A.Country,CM1.AddressBookName,CM1.ContactType from contactdetails CD inner join AddressDetails A on
                                       CD.FirstName = A.FirstName and CD.LastName=A.LastName
                                      inner join(select AddressBookName,ContactType, FirstName,LastName from BookNameContactType BC 
				                   inner join ContactTypeMap CM on BC.Contactid = CM.NameTypeid) CM1 
                                  on CD.FirstName = CM1.FirstName and CD.LastName=CM1.LastName";

                    SqlCommand command = new SqlCommand(query, connection);
                    this.connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ContactDetails contactDetails = new ContactDetails();
                            contactDetails.FirstName = reader.GetString(0);
                            contactDetails.LastName = reader.GetString(1);
                            contactDetails.PhoneNumber = reader.GetString(2);
                            contactDetails.Email = reader.GetString(3);
                            contactDetails.Area = reader.GetString(5);
                            contactDetails.City = reader.GetString(6);
                            contactDetails.State = reader.GetString(7);
                            contactDetails.Country = reader.GetString(8);
                            contactDetails.AddressBookName = reader.GetString(9);
                            contactDetails.ContactType = reader.GetString(10);

                            Console.WriteLine(contactDetails.FirstName + "  " + contactDetails.LastName + "  " + contactDetails.PhoneNumber + " " + contactDetails.Area + "  " + contactDetails.City
                                + "  " + contactDetails.State + "  " + contactDetails.Country + " " + contactDetails.AddressBookName + " " + contactDetails.ContactType);
                            Console.WriteLine("\n");
                            contactsList.Add(contactDetails);
                        }
                    }

                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                    return contactsList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        /// <summary>
        /// UC 17
        /// Here we are updating the contact details
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="column"></param>
        /// <param name="newValue"></param>
        public void UpdateContact(string firstName, string lastName, string column, string newValue)
        {
            try
            {
                // Open connection
                connection.Open();

                // Declare a command and give all its properties
                SqlCommand command = new SqlCommand();
                command.CommandText = "update contactdetails set " + column + " = '" + newValue + "' where firstname = '"
                                        + firstName + "' and lastname = '" + lastName + "'";
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// UC 18
        ///This method retrieves contacts between particular date
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<string> GetContactsAddedInPeriod(DateTime startdate, DateTime endDate)
        {
            try
            {
                // Open connection
                connection.Open();

                // Declare a command and give all its properties
                SqlCommand command = new SqlCommand();
                command.CommandText = @"SELECT CD.* ,A.Area, A.city,A.State,A.Country,CM1.AddressBookName,CM1.ContactType from contactdetails CD inner join AddressDetails A on
                                       CD.FirstName = A.FirstName and CD.LastName=A.LastName
                                      inner join(select AddressBookName,ContactType, FirstName,LastName from BookNameContactType BC 
				                   inner join ContactTypeMap CM on BC.Contactid = CM.NameTypeid) CM1 
                                  on CD.FirstName = CM1.FirstName and CD.LastName=CM1.LastName where CD.date_added between" + "'" +
                                        startdate + "' and '" + endDate + "'";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                List<string> List = new List<string>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContactDetails contactDetails = new ContactDetails();
                        contactDetails.FirstName = reader.GetString(0);
                        contactDetails.LastName = reader.GetString(1);
                        contactDetails.PhoneNumber = reader.GetString(2);
                        contactDetails.Email = reader.GetString(3);
                        contactDetails.date = reader.GetDateTime(4);
                        contactDetails.Area = reader.GetString(5);
                        contactDetails.City = reader.GetString(6);
                        contactDetails.State = reader.GetString(7);
                        contactDetails.Country = reader.GetString(8);
                        contactDetails.AddressBookName = reader.GetString(9);
                        contactDetails.ContactType = reader.GetString(10);

                        Console.WriteLine(contactDetails.FirstName + "  " + contactDetails.LastName + "  " + contactDetails.PhoneNumber + " " + contactDetails.Area + "  " + contactDetails.City
                            + "  " + contactDetails.State + "  " + contactDetails.Country + " " + contactDetails.AddressBookName + " " + contactDetails.ContactType);
                        Console.WriteLine("\n");
                        List.Add(contactDetails.FirstName + " " + contactDetails.LastName);
                    }
                }
                else
                {
                    Console.WriteLine("No data found");
                }
                reader.Close();
                return List;
            }


            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// UC 19 Gets the state of the contacts by city or.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public List<ContactDetails> GetContactsByCityOrState(string city, string state)
        {
            try { 
                // Open connection
                connection.Open();

                // Declare a command and give all its properties
                SqlCommand command = new SqlCommand();
                command.CommandText = @"SELECT CD.* ,A.Area, A.city,A.State,A.Country,CM1.AddressBookName,CM1.ContactType from contactdetails CD inner join AddressDetails A on
                                       CD.FirstName = A.FirstName and CD.LastName=A.LastName
                                      inner join(select AddressBookName,ContactType, FirstName,LastName from BookNameContactType BC 
				                   inner join ContactTypeMap CM on BC.Contactid = CM.NameTypeid) CM1 
                                  on CD.FirstName = CM1.FirstName and CD.LastName=CM1.LastName where A.City = '" +
                                        city + "' or A.State =  '" + state + "'";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                List<ContactDetails> List = new List<ContactDetails>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContactDetails contactDetails = new ContactDetails();
                        contactDetails.FirstName = reader.GetString(0);
                        contactDetails.LastName = reader.GetString(1);
                        contactDetails.PhoneNumber = reader.GetString(2);
                        contactDetails.Email = reader.GetString(3);
                        contactDetails.Area = reader.GetString(5);
                        contactDetails.City = reader.GetString(6);
                        contactDetails.State = reader.GetString(7);
                        contactDetails.Country = reader.GetString(8);
                        contactDetails.AddressBookName = reader.GetString(9);
                        contactDetails.ContactType = reader.GetString(10);

                        Console.WriteLine(contactDetails.FirstName + "  " + contactDetails.LastName + "  " + contactDetails.PhoneNumber + " " + contactDetails.Area + "  " + contactDetails.City
                            + "  " + contactDetails.State + "  " + contactDetails.Country + " " + contactDetails.AddressBookName + " " + contactDetails.ContactType);
                        Console.WriteLine("\n");
                        List.Add(contactDetails);
                    }
                }
                else
                {
                    Console.WriteLine("No data found");
                }
                reader.Close();
                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
