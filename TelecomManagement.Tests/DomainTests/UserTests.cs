using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;

namespace TelecomManagement.Tests.DomainTests
{
    
        [TestClass]
        public class UserTests
        {

            [TestMethod]
            public void Username_Validation_Correct()
            {
                // Arrange
                var user = new User();
                user.Username = "testusername";

                // Act
                bool isValid = Validator.TryValidateObject(user, new ValidationContext(user), null, true);

                // Assert
                Assert.IsTrue(isValid);
            }

            [TestMethod]
            public void Password_Validation_Correct()
            {
                // Arrange
                var user = new User();
                user.Password = "TestPassword1!";

                // Act
                bool isValid = Validator.TryValidateObject(user, new ValidationContext(user), null, true);

                // Assert
                Assert.IsTrue(isValid);
            }

            [TestMethod]
            public void LastLoggedIn_Updated_Correctly()
            {
                // Arrange
                var user = new User();
                DateTime initialLastLoggedIn = user.LastLoggedIn;

                // Act
                user.LastLoggedIn = DateTime.Now;

                // Assert
                Assert.AreNotEqual(initialLastLoggedIn, user.LastLoggedIn);
            }

            [TestMethod]
            public void Authentication_Successful_With_Valid_Credentials()
            {
                // Arrange
                var user = new User();
                user.Username = "testuser";
                user.Password = "TestPassword1!";

                // Act
                var isAuthenticated = AuthenticateUser(user, "testuser", "TestPassword1!");

                // Assert
                Assert.IsTrue(isAuthenticated);
            }

            [TestMethod]
            public void Authentication_Fails_With_Invalid_Credentials()
            {
                // Arrange
                var user = new User();
                user.Username = "testuser";
                user.Password = "TestPassword1!";

                // Act
                var isAuthenticated = AuthenticateUser(user, "testuser", "InvalidPassword");

                // Assert
                Assert.IsFalse(isAuthenticated);
            }

            private bool AuthenticateUser(User user, string username, string password)
            {
                return user.Username == username && user.Password == password;
            }
        }
    }



