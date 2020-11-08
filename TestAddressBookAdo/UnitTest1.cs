using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBook_ADO;
using System.Collections.Generic;
using System;

namespace TestAddressBookAdo
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestUpdatedContact()
        {
            // Arrange
            AddressBookRepository addressBookRepository = new AddressBookRepository();

            // Act
            addressBookRepository.UpdateContact("Ravi", "Prasad", "phoneNumber", "9441870640");
            List<ContactDetails> contactlist = addressBookRepository.GetAddressBookDetails();
            ContactDetails contact = contactlist.Find(contact => contact.FirstName == "Ravi" &&
                                                        contact.LastName == "Prasad");

            // Assert
            Assert.AreEqual("9441870640", contact.PhoneNumber);
        }
        [TestMethod]
        public void TestContactsBetweenDate()
        {
            // Arrange
            AddressBookRepository addressBookRepository = new AddressBookRepository();

            // Act
            List<string> actualList = addressBookRepository.GetContactsAddedInPeriod(new DateTime(2020, 11, 05), new DateTime(2020, 11, 11));
            List<string> expectedList = new List<string>();
            expectedList.Add("Kane williamson");

            // Assert
            Assert.AreEqual(actualList.Count,expectedList.Count);
        }
    }
}
