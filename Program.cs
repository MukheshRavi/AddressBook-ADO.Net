using System;
using System.Collections.Generic;

namespace AddressBook_ADO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book ADO Problem");
            AddressBookRepository addressBookRepository = new AddressBookRepository();

            // Act
            //List<string> actualList = addressBookRepository.GetContactsAddedInPeriod(new DateTime(2020, 11, 05), new DateTime(2020, 11, 11));
            // List<ContactDetails> actualContactList = addressBookRepository.GetContactsByCityOrState("Hyd", "Telangana");
            ContactDetails contact = new ContactDetails();
            contact.FirstName = "David";
            contact.LastName = "Warner";
            contact.PhoneNumber = "9876543210";
            contact.Email = "davi@gmail.com";
            contact.Area = "Sun nagar";
            contact.City = "Vizag";
            contact.State = "AndhraPradesh";
            contact.Country = "India";
            contact.AddressBookName = "My Book";
            contact.ContactType = "Family";


            // Act
            addressBookRepository.AddNewContact(contact);

        }
    }
}
