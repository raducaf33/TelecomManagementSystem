// <copyright file=ContractTests.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Contract Tests class. </summary>

using global::TelecomManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;

/// <summary>
/// Defines test class ContractTests.
/// </summary>
/// 
namespace TelecomManagement.Tests.DomainTests
{
    

    
        [TestClass]
        public class ContractTests
        {

        /// <summary>
        /// Defines test method Contract_ShouldHaveValidId.
        /// </summary>
        [TestMethod]
            public void Contract_ShouldHaveValidId()
            {
                // Arrange
                var contract = new Contract { Id = 1 };

                // Act
                var id = contract.Id;

                // Assert
                Assert.IsTrue(id > 0, "Id-ul contractului ar trebui să fie mai mare decât 0.");
            }
        /// <summary>
        /// Defines test method Contract_ShouldHaveValidDataIncheiere.
        /// </summary>
        [TestMethod]
            public void Contract_ShouldHaveValidDataIncheiere()
            {
                // Arrange
                var contract = new Contract { DataIncheiere = DateTime.Now };

                // Act
                var dataIncheiere = contract.DataIncheiere;

                // Assert
                Assert.IsTrue(dataIncheiere <= DateTime.Now, "Data încheierii contractului ar trebui să fie o dată validă.");
            }

        /// <summary>
        /// Defines test method Contract_ShouldHaveValidDataExpirare.
        /// </summary>

        [TestMethod]
            public void Contract_ShouldHaveValidDataExpirare()
            {
                // Arrange
                var contract = new Contract { DataExpirare = DateTime.Now.AddMonths(24) };

                // Act
                var dataExpirare = contract.DataExpirare;

                // Assert
                Assert.IsTrue(dataExpirare > DateTime.Now, "Data expirării contractului ar trebui să fie o dată viitoare.");
            }

        /// <summary>
        /// Defines test method Contract_ShouldHaveValidAbonamentId.
        /// </summary>

        [TestMethod]
            public void Contract_ShouldHaveValidAbonamentId()
            {
                // Arrange
                var contract = new Contract { AbonamentId = 1 };

                // Act
                var abonamentId = contract.AbonamentId;

                // Assert
                Assert.IsTrue(abonamentId > 0, "Id-ul abonamentului ar trebui să fie mai mare decât 0.");
            }

        /// <summary>
        /// Defines test method Contract_ShouldHaveValidClientId.
        /// </summary>

        [TestMethod]
            public void Contract_ShouldHaveValidClientId()
            {
                // Arrange
                var contract = new Contract { ClientId = 1 };

                // Act
                var clientId = contract.ClientId;

                // Assert
                Assert.IsTrue(clientId > 0, "Id-ul clientului ar trebui să fie mai mare decât 0.");
            }

        /// <summary>
        /// Defines test method Contract_ShouldHaveNonNullClient.
        /// </summary>

        [TestMethod]
            public void Contract_ShouldHaveNonNullClient()
            {
                // Arrange
                var client = new Client { Id = 1, Nume = "Popescu", Prenume = "Ion", Email = "test@example.com", Telefon = "0712345678", CNP = "1960101123456", UserId = 1 };
                var contract = new Contract { Client = client };

                // Act
                var contractClient = contract.Client;

                // Assert
                Assert.IsNotNull(contractClient, "Contractul ar trebui să aibă un client asociat.");
            }
        /// <summary>
        /// Defines test method Contract_ShouldHaveNonNullAbonament.
        /// </summary>


        [TestMethod]
            public void Contract_ShouldHaveNonNullAbonament()
            {
                // Arrange
                var abonament = new Abonament { Id = 1, Nume = "Abonament Test", Pret = 50, MinuteIncluse = 100, SMSuriIncluse = 50, TraficDateInclus = 1 };
                var contract = new Contract { Abonament = abonament };

                // Act
                var contractAbonament = contract.Abonament;

                // Assert
                Assert.IsNotNull(contractAbonament, "Contractul ar trebui să aibă un abonament asociat.");
            }

        /// <summary>
        /// Defines test method Contract_DataIncheiere_ShouldBeEarlierThanDataExpirare.
        /// </summary>

        [TestMethod]
            public void Contract_DataIncheiere_ShouldBeEarlierThanDataExpirare()
            {
                // Arrange
                var contract = new Contract
                {
                    DataIncheiere = DateTime.Now,
                    DataExpirare = DateTime.Now.AddMonths(24)
                };

                // Act
                var dataIncheiere = contract.DataIncheiere;
                var dataExpirare = contract.DataExpirare;

                // Assert
                Assert.IsTrue(dataIncheiere < dataExpirare, "Data încheierii ar trebui să fie mai devreme decât data expirării.");
            }


        }
    }


