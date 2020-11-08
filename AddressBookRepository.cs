using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace AddressBook_ADO
{
    class AddressBookRepository
    {
        public static string connectionString = @"Server=MUKESH\SQLEXPRESS; Initial Catalog =addressBookService;;Integrated Security=True;Connect Timeout=30;
Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection = new SqlConnection(connectionString);
        ///UC12
        public void GetAddresses()
        {
            ContactDetails contactDetails = new ContactDetails();
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
                            contactDetails.FirstName = reader.GetString(0);
                            contactDetails.LastName = reader.GetString(1);
                            contactDetails.PhoneNumber = reader.GetString(2);
                            contactDetails.Email = reader.GetString(3);
                            contactDetails.Area= reader.GetString(4);
                            contactDetails.City= reader.GetString(5);
                            contactDetails.State = reader.GetString(6);
                            contactDetails.Country = reader.GetString(7);
                            contactDetails.AddressBookName = reader.GetString(8);
                            contactDetails.ContactType = reader.GetString(9);

                            Console.WriteLine(contactDetails.FirstName+"  "+ contactDetails.LastName+"  "+ contactDetails.Area+"  "+contactDetails.City
                                +"  "+contactDetails.State+"  "+contactDetails.Country+" "+contactDetails.AddressBookName+" "+contactDetails.ContactType);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                    //this.connection.Close();
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
    }
}
