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
   
        [TestClass]
        public class ClientTests
        {
        [TestMethod]
        public void Client_ShouldCalculateCorrectAge_FromCNP_Male_Between_1900_1999()
        {
            // Arrange
            var cnp = "1801103168973"; // Născut în 1932
            var client = new Client { CNP = cnp };

            // Act
            var varsta = client.Varsta;

            // Assert
            Assert.AreEqual(90, varsta, "Vârsta calculată este incorectă pentru un CNP masculin născut între 1900 și 1999.");
        }

        [TestMethod]
        public void Client_ShouldCalculateCorrectAge_FromCNP_Female_Between_1900_1999()
        {
            // Arrange
            var cnp = "2801103166912"; // Născută în 1932
            var client = new Client { CNP = cnp };

            // Act
            var varsta = client.Varsta;

            // Assert
            Assert.AreEqual(90, varsta, "Vârsta calculată este incorectă pentru un CNP feminin născut între 1900 și 1999.");
        }

        [TestMethod]
        public void Client_ShouldCalculateCorrectAge_FromCNP_Male_After_2000()
        {
            // Arrange
            var cnp = "5021103161615"; // Născut în 2002
            var client = new Client { CNP = cnp };

            // Act
            var varsta = client.Varsta;

            // Assert
            Assert.AreEqual(22, varsta, "Vârsta calculată este incorectă pentru un CNP masculin născut după anul 2000.");
        }

        [TestMethod]
        public void Client_ShouldCalculateCorrectAge_FromCNP_Female_After_2000()
        {
            // Arrange
            var cnp = "6021103163557"; // Născută în 2006
            var client = new Client { CNP = cnp };

            // Act
            var varsta = client.Varsta;

            // Assert
            Assert.AreEqual(16, varsta, "Vârsta calculată este incorectă pentru un CNP feminin născut după anul 2000.");
        }
        [TestMethod]
            public void Client_ShouldCalculateCorrectAge_FromCNP_Before2000()
            {
                // Arrange
                var cnp = "1960101123456"; // Născut în 1960
                var client = new Client { CNP = cnp };

                // Act
                var varsta = client.Varsta;

                // Assert
                Assert.AreEqual(28, varsta, "Vârsta calculată este incorectă pentru un CNP născut înainte de 2000.");
            }

            [TestMethod]
            public void Client_ShouldCalculateCorrectAge_FromCNP_After2000()
            {
                // Arrange
                var cnp = "5010101123456"; // Născut în 2001
                var client = new Client { CNP = cnp };

                // Act
                var varsta = client.Varsta;

                // Assert
                Assert.AreEqual(23, varsta, "Vârsta calculată este incorectă pentru un CNP născut după 2000.");
            }
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

            [TestMethod]
            public void Client_Telefon_ShouldBeValidFormat()
            {
                // Arrange
                var client = new Client { Telefon = "0712345678" };

                // Act
                var telefon = client.Telefon;

                // Assert
                Assert.IsTrue(telefon.Length == 10 && telefon.StartsWith("07"), "Numărul de telefon trebuie să aibă un format valid.");
            }
        }
    }

