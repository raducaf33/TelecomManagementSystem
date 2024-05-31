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
    public class PlataTests
    {
       
            [TestMethod]
            public void TestPlataCuDateValide()
            {
                // Arrange
                var plata = new Plata
                {
                    ContractId = 1,
                    EstePlatita = true,
                    SumaPlata = 100,
                    DataPlata = DateTime.Now
                };

                // Act
                bool isValid = ValidatePlata(plata);

                // Assert
                Assert.IsTrue(isValid);
            }

            [TestMethod]
            public void TestPlataCuSumaNegativa()
            {
                // Arrange
                var plata = new Plata
                {
                    ContractId = 1,
                    EstePlatita = true,
                    SumaPlata = -100,
                    DataPlata = DateTime.Now
                };

                // Act
                bool isValid = ValidatePlata(plata);

                // Assert
                Assert.IsFalse(isValid);
            }

            [TestMethod]
            public void TestPlataFaraContractID()
            {
                // Arrange
                var plata = new Plata
                {
                    // ContractId lipsă
                    EstePlatita = true,
                    SumaPlata = 100,
                    DataPlata = DateTime.Now
                };

                // Act
                bool isValid = ValidatePlata(plata);

                // Assert
                Assert.IsFalse(isValid);
            }

            [TestMethod]
            public void TestPlataCuSumaZero()
            {
                // Arrange
                var plata = new Plata
                {
                    ContractId = 1,
                    EstePlatita = true,
                    SumaPlata = 0,
                    DataPlata = DateTime.Now
                };

                // Act
                bool isValid = ValidatePlata(plata);

                // Assert
                Assert.IsTrue(isValid);
            }

            private bool ValidatePlata(Plata plata)
            {
                // Validarea folosind System.ComponentModel.DataAnnotations.Validator
                var validationContext = new ValidationContext(plata);
                var validationResults = new List<ValidationResult>();

                return Validator.TryValidateObject(plata, validationContext, validationResults, true);
            }
        }
    }

