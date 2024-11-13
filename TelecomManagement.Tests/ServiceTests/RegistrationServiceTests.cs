// <copyright file=RegistrationServiceTests.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Registration Service Tests class. </summary>

using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Services;
using TelecomManagement.Domain;
using System.Data.Entity;
using System.Net.Sockets;


/// <summary>
/// Defines RegistrationServiceTests Tests class.
/// </summary>


namespace TelecomManagement.Tests.ServiceTests
{
    [TestClass]
    public class RegistrationServiceTests
    { /// <summary>
      /// Gets or sets the UserRepository.
      /// </summary>

        private Mock<UserRepository> userRepositoryMock;

        /// <summary>
        /// Gets or sets the ClientRepository.
        /// </summary>
        private Mock<ClientRepository> clientRepositoryMock;

        /// <summary>
        /// Gets or sets the RegistrationService service.
        /// </summary>
        private RegistrationService registrationService;

        /// <summary>
        /// Setups this instance.
        /// </summary>

        [TestInitialize]
            public void Setup()
            {
                userRepositoryMock = new Mock<UserRepository>(new Mock<TelecomContext>().Object);
                clientRepositoryMock = new Mock<ClientRepository>(new Mock<TelecomContext>().Object);
                registrationService = new RegistrationService(userRepositoryMock.Object, clientRepositoryMock.Object);
            }

        /// <summary>
        /// Defines test method Register_WithFirstLetterOfNumeLowerCase_ShouldThrowArgumentException.
        /// </summary>


        [TestMethod]
            public void Register_WithFirstLetterOfNumeLowerCase_ShouldThrowArgumentException()
            {
                Assert.ThrowsException<ArgumentException>(() => registrationService.Register("username", "password", "nume", "Prenume", "email@example.com", "0123456789", "1234567890123"));
            }

        /// <summary>
        /// Defines test method Register_WithFirstLetterOfPrenumeLowerCase_ShouldThrowArgumentException.
        /// </summary>


        [TestMethod]
            public void Register_WithFirstLetterOfPrenumeLowerCase_ShouldThrowArgumentException()
            {
                Assert.ThrowsException<ArgumentException>(() => registrationService.Register("username", "password", "Nume", "Prenume", "email@example.com", "0123456789", "1234567890123"));
            }

        /// <summary>
        /// Defines test method Register_WithInvalidEmail_ShouldThrowArgumentException.
        /// </summary>


        [TestMethod]
            public void Register_WithInvalidEmail_ShouldThrowArgumentException()
            {
                Assert.ThrowsException<ArgumentException>(() => registrationService.Register("username", "password", "Nume", "Prenume", "email.com", "0123456789", "1234567890123"));
            }

        /// <summary>
        /// Defines test method Register_WithInvalidPhoneNumber_ShouldThrowArgumentException.
        /// </summary>

        [TestMethod]
            public void Register_WithInvalidPhoneNumber_ShouldThrowArgumentException()
            {
                Assert.ThrowsException<ArgumentException>(() => registrationService.Register("username", "password", "Nume", "Prenume", "email@example.com", "123456789", "1234567890123"));
            }

        /// <summary>
        /// Defines test method Register_WithInvalidCNP_ShouldThrowArgumentException.
        /// </summary>


        [TestMethod]
            public void Register_WithInvalidCNP_ShouldThrowArgumentException()
            {
                Assert.ThrowsException<ArgumentException>(() => registrationService.Register("username", "password", "Nume", "Prenume", "email@example.com", "0123456789", "12345"));
            }


        /// <summary>
        /// Defines test method Register_ValidInput_ShouldAddUserAndClient.
        /// </summary>

        [TestMethod]
            public void Register_ValidInput_ShouldAddUserAndClient()
            {
                // Arrange
                string username = "username";
                string password = "password";
                string nume = "Nume";
                string prenume = "Prenume";
                string email = "email@example.com";
                string telefon = "0123456789";
                string cnp = "1234567890123";

                // Act
                registrationService.Register(username, password, nume, prenume, email, telefon, cnp);

                // Assert
                userRepositoryMock.Verify(repo => repo.Create(It.Is<User>(u => u.Username == username)), Times.Once);
                clientRepositoryMock.Verify(repo => repo.Create(It.Is<Client>(c => c.Nume == nume)), Times.Once);
            }

           
        }
    }
    
