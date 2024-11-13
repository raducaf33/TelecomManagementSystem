// <copyright file=PlataTests.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Plata Tests class. </summary>


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;

/// <summary>
/// Defines test class PlataTests.
/// </summary>

namespace TelecomManagement.Tests.DomainTests
{
    [TestClass]
    public class PlataTests
    {  /// <summary>
       /// Defines test method TestPlataCuDateValide.
       /// </summary>

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
        /// <summary>
        /// Defines test method TestPlataCuSumaNegativa.
        /// </summary>
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
        /// <summary>
        /// Defines test method TestPlataCuSumaZero.
        /// </summary>


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

