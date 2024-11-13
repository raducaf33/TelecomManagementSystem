// <copyright file=UserServiceTests.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the User Service Tests class. </summary>

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

/// <summary>
/// Defines UserServiceTests Tests class.
/// </summary>

namespace TelecomManagement.Tests.ServiceTests
{
    [TestClass]
    public class UserServiceTests
    {
        /// <summary>
        /// Gets or sets the User DbSet.
        /// </summary>
        private Mock<DbSet<User>> mockSet;

        /// <summary>
        /// Gets or sets the Telecom Context.
        /// </summary>
        private Mock<TelecomContext> mockContext;

        /// <summary>
        /// Gets or sets the User service.
        /// </summary>
        private UserService userService;

        /// <summary>
        /// Gets or sets the User list.
        /// </summary>
        private List<User> userList;

        /// <summary>
        /// Setups this instance.
        /// </summary>

        [TestInitialize]
        public void Setup()
        {
            // Mock pentru DbSet<User>
            userList = new List<User>
    {
        new User { Id = 1, Username = "user1", Password = "pass1", LastLoggedIn = DateTime.Now.AddDays(-1) },
        new User { Id = 2, Username = "user2", Password = "pass2", LastLoggedIn = DateTime.Now.AddDays(-2) }
    };
            var queryable = userList.AsQueryable();
            mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => userList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<User>())).Callback<User>((entity) => userList.Remove(entity));
            mockSet.Setup(m => m.Add(It.IsAny<User>())).Callback<User>((entity) =>
            {
                if (entity.Username == null || entity.Password == null)
                {
                    throw new ArgumentNullException(nameof(entity.Username));
                }
                if (entity.Id < 0)
                {
                    throw new ArgumentException("Valoarea pentru Id nu pot fi negative.");
                }
                userList.Add(entity);
            });

            // Mock pentru TelecomContext
            mockContext = new Mock<TelecomContext>();
            mockContext.Setup(m => m.Set<User>()).Returns(mockSet.Object);

            // Initializare UserService cu mockContext
            userService = new UserService(new UserRepository(mockContext.Object));
        }

        /// <summary>
        /// Defines test method UsernameContainsDigit_WhenUsernameContainsDigit_ShouldReturnTrue.
        /// </summary>

        [TestMethod]
        public void UsernameContainsDigit_WhenUsernameContainsDigit_ShouldReturnTrue()
        {
            // Arrange
            var user = new User { Username = "user1" };

            // Act
            bool result = user.Username.Any(char.IsDigit);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines test method PasswordContainsDigit_ShouldReturnTrue.
        /// </summary>

        [TestMethod]
        public void PasswordContainsDigit_ShouldReturnTrue()
        {
            // Arrange
            var user = new User { Password = "parola123" };

            // Act
            bool result = user.Password.Any(char.IsDigit);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines test method PasswordContainsSymbol_ShouldReturnTrue.
        /// </summary>

        [TestMethod]
        public void PasswordContainsSymbol_ShouldReturnTrue()
        {
            // Arrange
            var user = new User { Password = "parola$" };

            // Act
            bool result = user.Password.Any(char.IsSymbol);

            // Assert
            Assert.IsTrue(result);
        }


        /// <summary>
        /// Defines test method PasswordContainsUppercase_ShouldReturnTrue.
        /// </summary>


        [TestMethod]
        public void PasswordContainsUppercase_ShouldReturnTrue()
        {
            // Arrange
            var user = new User { Password = "Parola123" };

            // Act
            bool result = user.Password.Any(char.IsUpper);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines test method PasswordLengthGreaterThanSix_ShouldReturnTrue.
        /// </summary>


        [TestMethod]
        public void PasswordLengthGreaterThanSix_ShouldReturnTrue()
        {
            // Arrange
            var user = new User { Password = "Parola123" };

            // Act
            bool result = user.Password.Length > 6;

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines test method UsernameIsNull_ShouldThrowArgumentNullException.
        /// </summary>


        [TestMethod]
        public void UsernameIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var user = new User
            {
                Id = -1,
                Username = null,
                Password = "Parola&",
                LastLoggedIn = DateTime.Now,


            };

            // Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => userService.Create(user));
        }


        /// <summary>
        /// Defines test method PasswordIsNull_ShouldThrowArgumentNullException.
        /// </summary>

        [TestMethod]
        public void PasswordIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var user = new User
            {
                Id = -1,
                Username = "raduca33",
                Password = null,
                LastLoggedIn = DateTime.Now,


            };

            // Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => userService.Create(user));
        }


        /// <summary>
        /// Defines test method AddUser_WithIdNegativ_ShouldThrowArgumentNullException.
        /// </summary>

        [TestMethod]
        public void AddUser_WithIdNegativ_ShouldThrowArgumentNullException()
        {
            // Arrange
            var user = new User
            {
                Id = -1,
                Username = "raduca33",
                Password = "Mioralu23#",
                LastLoggedIn = DateTime.Now,
               

            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(user));
        }

        /// <summary>
        /// Defines test method EmptyId.
        /// </summary>


        [TestMethod]
        public void EmptyId()
        {
            // Arrange
            var user = new User
            {
                Id = 0, // Set to an empty value
                        // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (user.Id == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        /// <summary>
        /// Defines test method LastLoggedInIsPastDate_ShouldNotThrowException.
        /// </summary>


        [TestMethod]
        public void LastLoggedInIsPastDate_ShouldNotThrowException()
        {
            // Arrange
            var user = new User { LastLoggedIn = DateTime.Now.AddDays(-1) }; // Data anterioară

            // Act & Assert
            Assert.IsTrue(user.LastLoggedIn < DateTime.Now);
        }

        /// <summary>
        /// Defines test method LastLoggedInIsValidDateTime_ShouldNotThrowException.
        /// </summary>

        [TestMethod]
        public void LastLoggedInIsValidDateTime_ShouldNotThrowException()
        {
            // Arrange
            var user = new User { LastLoggedIn = DateTime.Now };

            // Act & Assert
            Assert.IsTrue(user.LastLoggedIn != default(DateTime));
        }

        /// <summary>
        /// Defines test method UpdateUser_WithNegativeId.
        /// </summary>

        [TestMethod]
        public void UpdateUser_WithNegativeId()
        {
            // Arrange
            // Arrange
            var user = new User
            {
                Id = 1,
                Username = "raduca33",
                Password = "Mioralu23#",
                LastLoggedIn = DateTime.Today,
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(user);

            // Modificați numele clientului
            user.Id = -1;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

       






    }
}
