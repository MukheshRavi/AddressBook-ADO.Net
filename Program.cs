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
            List<string> actualList = addressBookRepository.GetContactsAddedInPeriod(new DateTime(2020, 11, 05), new DateTime(2020, 11, 11));
        }
    }
}
