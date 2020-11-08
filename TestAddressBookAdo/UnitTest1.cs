using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBook_ADO;
using System.Collections.Generic;

namespace TestAddressBookAdo
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UpdateContactAndTestTheDB()
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
    }
}
