﻿using System;
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
            addressBookRepository.UpdateContact("Ravi", "Prasad", "PhoneNumber", "9441870640");
            List<ContactDetails> contactlist = addressBookRepository.GetAddressBookDetails();
            ContactDetails contact = contactlist.Find(contact => contact.FirstName == "Ravi" &&
                                                        contact.LastName == "Prasad");
        }
    }
}
