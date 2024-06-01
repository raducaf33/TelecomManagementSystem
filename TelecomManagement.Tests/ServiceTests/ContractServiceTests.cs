// <copyright file=ContractceTests.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Contract Service Tests class. </summary>

using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;
using TelecomManagement.Data;
using TelecomManagement.Services;
using System.Net.Sockets;
using System.Data.Entity;

namespace TelecomManagement.Tests.ServiceTests
{
    [TestClass]
    public class ContractServiceTests
    {

        private Mock<DbSet<Contract>> mockSet;
        private Mock<DbSet<ContractBonus>> mockSetContractBonus;
        private Mock<TelecomContext> mockContext;
        private ContractService contractService;
        private List<Contract> contractList;

        [TestInitialize]
        public void Setup()
        {
            contractList = new List<Contract>
            {
                new Contract { Id = 1, DataIncheiere = DateTime.Now, DataExpirare = DateTime.Now.AddDays(30), AbonamentId = 1, ClientId = 1 },
                new Contract { Id = 2, DataIncheiere = DateTime.Now, DataExpirare = DateTime.Now.AddDays(30), AbonamentId = 2, ClientId = 2 }
            };

            var queryable = contractList.AsQueryable();

            mockSet = new Mock<DbSet<Contract>>();
            mockSet.As<IQueryable<Contract>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Contract>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Contract>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Contract>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => contractList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Contract>())).Callback<Contract>((entity) => contractList.Remove(entity));
            mockSet.Setup(m => m.Add(It.IsAny<Contract>())).Callback<Contract>((entity) =>
            {
                if (entity.DataIncheiere == default(DateTime) || entity.Id == 0 || entity.AbonamentId == 0 || entity.ClientId == 0)
                {
                    throw new ArgumentNullException("Data de încheiere nu poate fi null.");
                }

                if (entity.Id < 0 || entity.AbonamentId < 0 || entity.ClientId < 0)
                {
                    throw new ArgumentException("Valorile pentru MinuteBonus, SMSuriBonus și TraficDateBonus nu pot fi negative.");
                }
                contractList.Add(entity);
            });

            mockContext = new Mock<TelecomContext>();
            mockContext.Setup(m => m.Set<Contract>()).Returns(mockSet.Object);
            contractService = new ContractService(new ContractRepository(mockContext.Object));
        }

        [TestMethod]
        public void AddContract_WithIdNegativ_ShouldThrowArgumentNullException()
        {
            // Arrange
            var contract = new Contract
            {
                Id = -1,
                AbonamentId = 1,
                ClientId = 2,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Today.AddDays(-30)

            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(contract));
        }

        [TestMethod]
        public void AddContract_WithAbonamentIdNegativ_ShouldThrowArgumentNullException()
        {
            // Arrange
            var contract = new Contract
            {
                Id = 1,
                AbonamentId = -1,
                ClientId = 2,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Today.AddDays(-30)

            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(contract));
        }

        [TestMethod]
        public void AddContract_WithClientIdNegativ_ShouldThrowArgumentNullException()
        {
            // Arrange
            var contract = new Contract
            {
                Id = 1,
                AbonamentId = 1,
                ClientId = -2,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Today.AddDays(-30)

            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(contract));
        }

        [TestMethod]
        public void EmptyId()
        {
            // Arrange
            var contract = new Contract
            {
                Id = 0, // Set to an empty value
                        // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (contract.Id == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        [TestMethod]
        public void EmptyAbonamentId()
        {
            // Arrange
            var contract = new Contract
            {
                AbonamentId = 0, // Set to an empty value
                                 // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (contract.AbonamentId == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        [TestMethod]
        public void EmptyClientId()
        {
            // Arrange
            var contract = new Contract
            {
                ClientId = 0, // Set to an empty value
                              // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (contract.ClientId == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        [TestMethod]
        public void TestAddContractWithValidData()
        {
            var contract = new Contract
            {
                Id = 3,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Now.AddDays(30),
                AbonamentId = 3,
                ClientId = 3
            };

            contractService.Create(contract);

            mockSet.Verify(m => m.Add(It.IsAny<Contract>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateContract_WithNegativeId()
        {
            // Arrange
            // Arrange
            var contract = new Contract
            {
                Id = 3,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Now.AddDays(30),
                AbonamentId = 3,
                ClientId = 3
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(contract);

            // Modificați numele clientului
            contract.Id = -3;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void UpdateContract_WithNegativeAbonamentId()
        {
            // Arrange
            // Arrange
            var contract = new Contract
            {
                Id = 3,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Now.AddDays(30),
                AbonamentId = 3,
                ClientId = 3
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(contract);

            // Modificați numele clientului
            contract.AbonamentId = -3;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void UpdateContract_WithNegativeClientId()
        {
            // Arrange
            // Arrange
            var contract = new Contract
            {
                Id = 3,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Now.AddDays(30),
                AbonamentId = 3,
                ClientId = 3
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(contract);

            // Modificați numele clientului
            contract.ClientId = -3;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void ContractDuration_ShouldBeLessThan1Year()
        {
            // Arrange
            var contractId = 1;
            var startDate = new DateTime(2023, 1, 1); // Data de început a contractului
            var endDate = startDate.AddMonths(6); // Data de încheiere este cu 6 luni mai târziu
            var contract = new Contract { Id = contractId, DataIncheiere = endDate, DataExpirare = endDate.AddMonths(1) }; // Durata totală a contractului este de 6 luni
            mockSet.Setup(m => m.Find(contractId)).Returns(contract);

            // Act
            contractService.Create(contract); // Salvăm contractul folosind ContractService

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Contract>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext
            var durationInMonths = (contract.DataExpirare - contract.DataIncheiere).Days / 30; // Durata contractului în luni
            Assert.IsTrue(durationInMonths < 12); // Verificăm dacă durata este mai mică de 12 luni
        }
        [TestMethod]
        public void ContractDuration_ShouldBeGreaterThan1Year()
        {
            // Arrange
            var contractId = 1;
            var startDate = new DateTime(2023, 1, 1); // Data de început a contractului
            var endDate = startDate.AddYears(2); // Data de încheiere este cu 2 ani mai târziu
            var contract = new Contract { Id = contractId, ClientId = 1, AbonamentId = 1, DataIncheiere = endDate, DataExpirare = endDate.AddMonths(1) }; // Durata totală a contractului este de 2 ani și 1 lună
            mockSet.Setup(m => m.Find(contractId)).Returns(contract);

            // Act
            var durationInYears = endDate.Year - startDate.Year; // Durata contractului în ani

            // Assert
            Assert.IsTrue(durationInYears > 1);
        }

        [TestMethod]
        public void ContractSignedInIanuarie_ShouldReturnTrue()
        {
            // Arrange
            var contractId = 1;
            var contract = new Contract { Id = contractId, ClientId = 1, AbonamentId = 1, DataIncheiere = new DateTime(2023, 1, 15), DataExpirare = new DateTime(2024, 1, 15) };
            mockSet.Setup(m => m.Find(contractId)).Returns(contract);

            // Act
            contractService.Create(contract); // Salvăm contractul folosind ContractService

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Contract>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(1, contract.DataIncheiere.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }



        [TestMethod]
        public void ContractSignedInFebruarie_ShouldReturnTrue()
        {
            // Arrange
            var contractId = 1;
            var contract = new Contract { Id = contractId, ClientId = 1, AbonamentId = 1, DataIncheiere = new DateTime(2023, 2, 15), DataExpirare = new DateTime(2024, 2, 15) };
            mockSet.Setup(m => m.Find(contractId)).Returns(contract);

            // Act
            contractService.Create(contract); // Salvăm contractul folosind ContractService

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Contract>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(2, contract.DataIncheiere.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        [TestMethod]
        public void ContractSignedInMartie_ShouldReturnTrue()
        {
            // Arrange
            var contractId = 1;
            var contract = new Contract { Id = contractId, ClientId = 1, AbonamentId = 1, DataIncheiere = new DateTime(2023, 3, 15), DataExpirare = new DateTime(2024, 3, 15) };
            mockSet.Setup(m => m.Find(contractId)).Returns(contract);

            // Act
            contractService.Create(contract); // Salvăm contractul folosind ContractService

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Contract>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(3, contract.DataIncheiere.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        [TestMethod]
        public void ContractSignedInAprilie_ShouldReturnTrue()
        {
            // Arrange
            var contractId = 1;
            var contract = new Contract { Id = contractId, ClientId = 1, AbonamentId = 1, DataIncheiere = new DateTime(2023, 4, 15), DataExpirare = new DateTime(2024, 4, 15) };
            mockSet.Setup(m => m.Find(contractId)).Returns(contract);

            // Act
            contractService.Create(contract); // Salvăm contractul folosind ContractService

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Contract>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(4, contract.DataIncheiere.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }


        [TestMethod]
        public void ContractSignedInMai_ShouldReturnTrue()
        {
            // Arrange
            var contractId = 1;
            var contract = new Contract { Id = contractId, ClientId = 1, AbonamentId = 1, DataIncheiere = new DateTime(2023, 5, 15), DataExpirare = new DateTime(2024, 5, 15) };
            mockSet.Setup(m => m.Find(contractId)).Returns(contract);

            // Act
            contractService.Create(contract); // Salvăm contractul folosind ContractService

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Contract>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(5, contract.DataIncheiere.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        [TestMethod]
        public void ContractSignedInIunie_ShouldReturnTrue()
        {
            // Arrange
            var contractId = 1;
            var contract = new Contract { Id = contractId, ClientId = 1, AbonamentId = 1, DataIncheiere = new DateTime(2023, 6, 15), DataExpirare = new DateTime(2024, 6, 15) };
            mockSet.Setup(m => m.Find(contractId)).Returns(contract);

            // Act
            contractService.Create(contract); // Salvăm contractul folosind ContractService

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Contract>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(6, contract.DataIncheiere.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        [TestMethod]
        public void ContractSignedInIulie_ShouldReturnTrue()
        {
            // Arrange
            var contractId = 1;
            var contract = new Contract { Id = contractId, ClientId = 1, AbonamentId = 1, DataIncheiere = new DateTime(2023, 7, 15), DataExpirare = new DateTime(2024, 7, 15) };
            mockSet.Setup(m => m.Find(contractId)).Returns(contract);

            // Act
            contractService.Create(contract); // Salvăm contractul folosind ContractService

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Contract>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(7, contract.DataIncheiere.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        [TestMethod]
        public void ContractSignedInAugust_ShouldReturnTrue()
        {
            // Arrange
            var contractId = 1;
            var contract = new Contract { Id = contractId, ClientId = 1, AbonamentId = 1, DataIncheiere = new DateTime(2023, 8, 15), DataExpirare = new DateTime(2024, 8, 15) };
            mockSet.Setup(m => m.Find(contractId)).Returns(contract);

            // Act
            contractService.Create(contract); // Salvăm contractul folosind ContractService

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Contract>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(8, contract.DataIncheiere.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        [TestMethod]
        public void ContractSignedInSeptembrie_ShouldReturnTrue()
        {
            // Arrange
            var contractId = 1;
            var contract = new Contract { Id = contractId, ClientId = 1, AbonamentId = 1, DataIncheiere = new DateTime(2023, 9, 15), DataExpirare = new DateTime(2024, 9, 15) };
            mockSet.Setup(m => m.Find(contractId)).Returns(contract);

            // Act
            contractService.Create(contract); // Salvăm contractul folosind ContractService

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Contract>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(9, contract.DataIncheiere.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }
        [TestMethod]
        public void ContractSignedInOctombrie_ShouldReturnTrue()
        {
            // Arrange
            var contractId = 1;
            var contract = new Contract { Id = contractId, ClientId = 1, AbonamentId = 1, DataIncheiere = new DateTime(2023, 10, 15), DataExpirare = new DateTime(2024, 10, 15) };
            mockSet.Setup(m => m.Find(contractId)).Returns(contract);

            // Act
            contractService.Create(contract); // Salvăm contractul folosind ContractService

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Contract>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(10, contract.DataIncheiere.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        [TestMethod]
        public void ContractSignedInNoiembrie_ShouldReturnTrue()
        {
            // Arrange
            var contractId = 1;
            var contract = new Contract { Id = contractId, ClientId = 1, AbonamentId = 1, DataIncheiere = new DateTime(2023, 11, 15), DataExpirare = new DateTime(2024, 11, 15) };
            mockSet.Setup(m => m.Find(contractId)).Returns(contract);

            // Act
            contractService.Create(contract); // Salvăm contractul folosind ContractService

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Contract>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(11, contract.DataIncheiere.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        [TestMethod]
        public void ContractSignedInDecembrie_ShouldReturnTrue()
        {
            // Arrange
            var contractId = 1;
            var contract = new Contract { Id = contractId, ClientId = 1, AbonamentId = 1, DataIncheiere = new DateTime(2023, 12, 15), DataExpirare = new DateTime(2024, 12, 15) };
            mockSet.Setup(m => m.Find(contractId)).Returns(contract);

            // Act
            contractService.Create(contract); // Salvăm contractul folosind ContractService

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Contract>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(12, contract.DataIncheiere.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        [TestMethod]
        public void ContractService_ShouldIndicate_ContractExpired()
        {
            // Arrange
            var dataIncheiere = DateTime.Today.AddYears(-2); // Contract încheiat acum 2 ani
           

            // Act
            var esteExpirat = contractService.EsteContractExpirat(dataIncheiere);

            // Assert
            Assert.IsTrue(esteExpirat, "Contractul ar trebui să fie expirat.");
        }

        [TestMethod]
        public void ContractService_ShouldIndicate_ContractNotExpired()
        {
            // Arrange
            var dataIncheiere = DateTime.Today.AddMonths(-6); // Contract încheiat acum 6 luni
            

            // Act
            var esteExpirat = contractService.EsteContractExpirat(dataIncheiere);

            // Assert
            Assert.IsFalse(esteExpirat, "Contractul nu ar trebui să fie expirat.");
        }

       
    }
}
