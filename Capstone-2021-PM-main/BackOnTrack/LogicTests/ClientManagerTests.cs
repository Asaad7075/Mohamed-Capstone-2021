using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicTests
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/04/16
    /// 
    /// Tests client accessor methods.
    /// </summary>
    [TestClass]
    public class ClientManagerTests
    {
        private IClientAccessor _clientAccessor;


        [TestInitialize]
        public void TestSetup() 
        {
            _clientAccessor = new ClientFake();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/16
        /// </summary>
        [TestMethod]
        public void SelectClientFirstLastNameByEmailGood()
        {
            // Arrange
            string[] expectedResult = new string[]
            {
                "Chantal",
                "Shirley"
            };
            string[] actualResult;
            string email = "Chantal@back-on-track.com";

            // Act
            actualResult = _clientAccessor.SelectClientFirstLastNameByEmail(email);

            // Assert
            Assert.IsTrue(expectedResult.SequenceEqual(actualResult));
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/16
        /// </summary>
        [TestMethod]
        public void SelectClientIdByEmailGood()
        {
            // Arrange
            int expectedResult = 10004;
            int actualResult;
            string email = "Chantal@back-on-track.com";

            // Act
            actualResult = _clientAccessor.SelectClientIdByEmail(email);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/30
        /// </summary>
        [TestMethod]
        public void InsertNewClientAccountGood()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult;
            Client client = new Client
            {
                ClientID = 10924,
                Email = "Tester@gmail.com",
                FirstName = "John",
                LastName = "Doe"
            };
            string passwordHash = "helloworld".hashSHA256().ToUpper();

            // Act
            actualResult = _clientAccessor.InsertNewClientAccount(client, passwordHash);

            // Assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/30
        /// </summary>
        [TestMethod]
        public void SelectClientByEmailAndPasswordGood()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult;
            Client client = new Client
            {
                ClientID = 10924,
                Email = "Tester@gmail.com",
                FirstName = "John",
                LastName = "Doe"
            };
            string passwordHash = "helloworld".hashSHA256().ToUpper();
            _clientAccessor.InsertNewClientAccount(client, passwordHash);

            // Act
            actualResult = _clientAccessor.SelectClientByEmailAndPassword(client.Email, passwordHash);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
