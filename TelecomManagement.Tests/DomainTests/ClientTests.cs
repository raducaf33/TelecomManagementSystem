// <copyright file=ClientTests.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Client Tests class. </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;

namespace TelecomManagement.Tests.DomainTests
{
    /// <summary>
    /// Defines test class ClientTests.
    /// </summary>

        [TestClass]
        public class ClientTests
        {
        
        
        private int CalculateExpectedAge(Client client, DateTime currentDate)
        {
            return client.CalculateAgeFromCnp(client.CNP, currentDate);
        }
        /// <summary>
        /// Defines test method Client_ShouldCalculateCorrectAge_FromCNP_Male_Between_1800_1899.
        /// </summary>

        [TestMethod]
        public void Client_ShouldCalculateCorrectAge_FromCNP_Male_Between_1800_1899()
        {
            // Arrange
            var cnp = "3531020154897"; // Născut în 1932
            var client = new Client { CNP = cnp };

            // Act
            var expectedAge = CalculateExpectedAge(client, new DateTime(2024, 6, 2));
            var actualAge = client.CalculateAgeFromCnp(cnp, new DateTime(2024, 6, 2));

            // Assert
            Assert.AreEqual(expectedAge, actualAge, "Vârsta calculată este incorectă pentru un CNP masculin născut după anul 2000.");
        }

        /// <summary>
        /// Defines test method Client_ShouldCalculateCorrectAge_FromCNP_Male_Between_1900_1990.
        /// </summary>


        [TestMethod]
        public void Client_ShouldCalculateCorrectAge_FromCNP_Male_Between_1900_1999()
        {
            // Arrange
            var cnp = "1801103168973"; // Născut în 1932
            var client = new Client { CNP = cnp };

            // Act
            var expectedAge = CalculateExpectedAge(client, new DateTime(2024, 6, 2));
            var actualAge = client.CalculateAgeFromCnp(cnp, new DateTime(2024, 6, 2));

            // Assert
            Assert.AreEqual(expectedAge, actualAge, "Vârsta calculată este incorectă pentru un CNP masculin născut după anul 2000.");
        }

        /// <summary>
        /// Defines test method Client_ShouldCalculateCorrectAge_FromCNP_Male_After_2000.
        /// </summary>

        [TestMethod]
        public void Client_ShouldCalculateCorrectAge_FromCNP_Male_After_2000()
        {
            // Arrange
            var cnp = "5021103161615"; // Născut în 2002
            var client = new Client { CNP = cnp };

            // Act
            var expectedAge = CalculateExpectedAge(client, new DateTime(2024, 6, 2));
            var actualAge = client.CalculateAgeFromCnp(cnp, new DateTime(2024, 6, 2));

            // Assert
            Assert.AreEqual(expectedAge, actualAge, "Vârsta calculată este incorectă pentru un CNP masculin născut după anul 2000.");
        }

        /// <summary>
        /// Defines test method Client_ShouldCalculateCorrectAge_FromCNP_Female_Between_1900_1999.
        /// </summary>


        [TestMethod]
        public void Client_ShouldCalculateCorrectAge_FromCNP_Female_Between_1900_1999()
        {
            // Arrange
            var cnp = "2801103166912"; // Născută în 1932
            var client = new Client { CNP = cnp };

            // Act
            var expectedAge = CalculateExpectedAge(client, new DateTime(2024, 6, 2));
            var actualAge = client.CalculateAgeFromCnp(cnp, new DateTime(2024, 6, 2));

            // Assert
            Assert.AreEqual(expectedAge, actualAge, "Vârsta calculată este incorectă pentru un CNP masculin născut după anul 2000.");
        }

        /// <summary>
        /// Defines test method Client_ShouldCalculateCorrectAge_FromCNP_Female_After_2000.
        /// </summary>

        [TestMethod]
        public void Client_ShouldCalculateCorrectAge_FromCNP_Female_After_2000()
        {
            // Arrange
            var cnp = "6021103163557"; // Născută în 2006
            var client = new Client { CNP = cnp };

            // Act
            var expectedAge = CalculateExpectedAge(client, new DateTime(2024, 6, 2));
            var actualAge = client.CalculateAgeFromCnp(cnp, new DateTime(2024, 6, 2));

            // Assert
            Assert.AreEqual(expectedAge, actualAge, "Vârsta calculată este incorectă pentru un CNP masculin născut după anul 2000.");
        }

        /// <summary>
        /// Defines test method Client_ShouldCalculateCorrectAge_FromCNP_Before2000.
        /// </summary>
        [TestMethod]
            public void Client_ShouldCalculateCorrectAge_FromCNP_Before2000()
            {
                // Arrange
                var cnp = "1960101123456"; // Născut în 1960
                var client = new Client { CNP = cnp };

            // Act
            var expectedAge = CalculateExpectedAge(client, new DateTime(2024, 6, 2));
            var actualAge = client.CalculateAgeFromCnp(cnp, new DateTime(2024, 6, 2));

            // Assert
            Assert.AreEqual(expectedAge, actualAge, "Vârsta calculată este incorectă pentru un CNP masculin născut după anul 2000.");
        }

        /// <summary>
        /// Defines test method Client_ShouldCalculateCorrectAge_FromCNP_After2000.
        /// </summary>

        [TestMethod]
            public void Client_ShouldCalculateCorrectAge_FromCNP_After2000()
            {
                // Arrange
                var cnp = "5010101123456"; // Născut în 2001
                var client = new Client { CNP = cnp };

            // Act
            var expectedAge = CalculateExpectedAge(client, new DateTime(2024, 6, 2));
            var actualAge = client.CalculateAgeFromCnp(cnp, new DateTime(2024, 6, 2));

            // Assert
            Assert.AreEqual(expectedAge, actualAge, "Vârsta calculată este incorectă pentru un CNP masculin născut după anul 2000.");
        }

        /// <summary>
        /// Defines test method Client_ShouldCalculateCorrectAge_ForInvalidCNP_Length.
        /// </summary>
        [TestMethod]
            public void Client_ShouldThrowException_ForInvalidCNP_Length()
            {
                // Arrange
                var cnp = "123456"; // CNP invalid
                var client = new Client { CNP = cnp };

                // Act & Assert
                var ex = Assert.ThrowsException<ArgumentException>(() => { var age = client.Varsta; });
                Assert.AreEqual("CNP-ul trebuie să aibă 13 caractere.", ex.Message);
            }

        /// <summary>
        /// Defines test method Client_ShouldHaveNonEmptyNume.
        /// </summary>

        [TestMethod]
            public void Client_ShouldHaveNonEmptyNume()
            {
                // Arrange
                var client = new Client { Nume = "Popescu" };

                // Act
                var nume = client.Nume;

                // Assert
                Assert.IsFalse(string.IsNullOrEmpty(nume), "Numele nu ar trebui să fie gol.");
            }

        /// <summary>
        /// Defines test method Client_ShouldHaveNonEmptyPrenume.
        /// </summary>

        [TestMethod]
            public void Client_ShouldHaveNonEmptyPrenume()
            {
                // Arrange
                var client = new Client { Prenume = "Ion" };

                // Act
                var prenume = client.Prenume;

                // Assert
                Assert.IsFalse(string.IsNullOrEmpty(prenume), "Prenumele nu ar trebui să fie gol.");
            }

        /// <summary>
        /// Defines test method Client_Email_ShouldBeValidFormat.
        /// </summary>

        [TestMethod]
            public void Client_Email_ShouldBeValidFormat()
            {
                // Arrange
                var client = new Client { Email = "test@example.com" };

                // Act
                var email = client.Email;

                // Assert
                Assert.IsTrue(email.Contains("@") && email.Contains("."), "Email-ul trebuie să aibă un format valid.");
            }
        /// <summary>
        /// Defines test method Client_Telefon_ShouldBeValidFormat.
        /// </summary>

        [TestMethod]
            public void Client_Telefon_ShouldBeValidFormat()
            {
                // Arrange
                var client = new Client { Telefon = "0712834578" };

                // Act
                var telefon = client.Telefon;

                // Assert
                Assert.IsTrue(telefon.Length == 10 && telefon.StartsWith("07"), "Numărul de telefon trebuie să aibă un format valid.");
            }
        }
    }

