// <copyright file=ContractBonusServiceTests.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the ContractBonus Service Tests class. </summary>

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

/// <summary>
/// Defines ContractBonusServiceTests Tests class.
/// </summary>


namespace TelecomManagement.Tests.ServiceTests
{
    [TestClass]
    public class ContractBonusServiceTests
    {   /// <summary>
        /// Gets or sets the ContractBonus DbSet.
        /// </summary>
        private Mock<DbSet<ContractBonus>> mockSet;

        /// <summary>
        /// Gets or sets the Telecom Context.
        /// </summary>
        private Mock<TelecomContext> mockContext;

        /// <summary>
        /// Gets or sets the ContractBonusService service.
        /// </summary>
        private ContractBonusService contractBonusService;

        /// <summary>
        /// Gets or sets the ContractBonus list.
        /// </summary>
        private List<ContractBonus> contractBonusList;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        
        [TestInitialize]
        public void Setup()
        {
            // Data de azi
            var today = DateTime.Today;

            // Mock pentru DbSet<ContractBonus>
            contractBonusList = new List<ContractBonus>
    {
        new ContractBonus { Id = 1, ContractId = 1, BonusId = 1, DataIncheiere = today, DataExpirare = today.AddDays(30) },
        new ContractBonus { Id = 2, ContractId = 2, BonusId = 2, DataIncheiere = today, DataExpirare = today.AddDays(30) }
    };
            var queryable = contractBonusList.AsQueryable();
            mockSet = new Mock<DbSet<ContractBonus>>();
            mockSet.As<IQueryable<ContractBonus>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<ContractBonus>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<ContractBonus>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<ContractBonus>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => contractBonusList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<ContractBonus>())).Callback<ContractBonus>((entity) => contractBonusList.Remove(entity));
            mockSet.Setup(m => m.Add(It.IsAny<ContractBonus>())).Callback<ContractBonus>((entity) =>
            {
                if (entity.DataIncheiere == default(DateTime))
                {
                    throw new ArgumentException("Data de încheiere nu poate fi null.");
                }

                if (entity.Id < 0 || entity.ContractId < 0 || entity.BonusId < 0 )
                {
                    throw new ArgumentException("Valorile pentru MinuteBonus, SMSuriBonus și TraficDateBonus nu pot fi negative.");
                }
                contractBonusList.Add(entity);
            });

            // Mock pentru TelecomContext
            mockContext = new Mock<TelecomContext>();
            mockContext.Setup(m => m.Set<ContractBonus>()).Returns(mockSet.Object);

            // Initializare ContractBonusService cu mockContext
            contractBonusService = new ContractBonusService(new ContractBonusRepository(mockContext.Object));
        }

        /// <summary>
        /// Defines test method AddContractBonus_WithIdNegativ_ShouldThrowArgumentNullException.
        /// </summary>


        [TestMethod]
        public void AddContractBonus_WithIdNegativ_ShouldThrowArgumentNullException()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                Id = -1,
                ContractId = 1,
                BonusId = 2,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Today.AddDays(-30)
              
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(contractBonus));
        }

        /// <summary>
        /// Defines test method AddContractBonus_WithContractIdNegativ_ShouldThrowArgumentNullException.
        /// </summary>


        [TestMethod]
        public void AddContractBonus_WithContractIdNegativ_ShouldThrowArgumentNullException()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                Id = 1,
                ContractId = -1,
                BonusId = 2,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Today.AddDays(-30)

            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(contractBonus));
        }

        /// <summary>
        /// Defines test method AddContractBonus_WithBonusIdNegativ_ShouldThrowArgumentNullException.
        /// </summary>

        [TestMethod]
        public void AddContractBonus_WithBonusIdNegativ_ShouldThrowArgumentNullException()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                Id = 1,
                ContractId = 1,
                BonusId = -2,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Today.AddDays(-30)

            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(contractBonus));
        }

        /// <summary>
        /// Defines test method EmptyId.
        /// </summary>


        [TestMethod]
        public void EmptyId()
        {
            // Arrange
            var contractbonus = new ContractBonus
            {
                Id = 0, // Set to an empty value
                            // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (contractbonus.Id == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }
        /// <summary>
        /// Defines test method EmptyContractId.
        /// </summary>


        [TestMethod]
        public void EmptyContractId()
        {
            // Arrange
            var contractbonus = new ContractBonus
            {
                ContractId = 0, // Set to an empty value
                        // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (contractbonus.ContractId == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }
        /// <summary>
        /// Defines test method EmptyBonusId.
        /// </summary>


        [TestMethod]
        public void EmptyBonusId()
        {
            // Arrange
            var contractbonus = new ContractBonus
            {
                BonusId = 0, // Set to an empty value
                                // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (contractbonus.BonusId == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }
        /// <summary>
        /// Defines test method AddContractBonus_ValidData_ShouldAddToRepository.
        /// </summary>


        [TestMethod]
        public void AddContractBonus_ValidData_ShouldAddToRepository()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                Id = 3,
                ContractId = 3,
                BonusId = 3,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Now.AddDays(30)
            };

            // Act
            contractBonusService.Create(contractBonus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<ContractBonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines test method UpdateContractBonus_WithNegativeId.
        /// </summary>
        [TestMethod]
        public void UpdateContractBonus_WithNegativeId()
        {
            // Arrange
            // Arrange
            var  contractBonus = new ContractBonus
            {
                Id = 2,
                ContractId = 2,
                BonusId = 1,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Now.AddDays(30)
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(contractBonus);

            // Modificați numele clientului
            contractBonus.Id = -2;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines test method UpdateContractBonus_WithNegativeContractId.
        /// </summary>

        [TestMethod]
        public void UpdateContractBonus_WithNegativeContractId()
        {
            // Arrange
            // Arrange
            var contractBonus = new ContractBonus
            {
                Id = 2,
                ContractId = 2,
                BonusId = 1,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Now.AddDays(30)
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(contractBonus);

            // Modificați numele clientului
            contractBonus.Id = -2;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }


        /// <summary>
        /// Defines test method UpdateContractBonus_WithNegativeUserId.
        /// </summary>

        [TestMethod]
        public void UpdateContractBonus_WithNegativeUserId()
        {
            // Arrange
            // Arrange
            var contractBonus = new ContractBonus
            {
                Id = 2,
                ContractId = 2,
                BonusId = 1,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Now.AddDays(30)
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(contractBonus);

            // Modificați numele clientului
            contractBonus.ContractId = -2;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines test method UpdateContractBonus_WithNegativeBonusId.
        /// </summary>

        [TestMethod]
        public void UpdateContractBonus_WithNegativeBonusId()
        {
            // Arrange
            // Arrange
            var contractBonus = new ContractBonus
            {
                Id = 2,
                ContractId = 2,
                BonusId = 1,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Now.AddDays(30)
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(contractBonus);

            // Modificați numele clientului
            contractBonus.ContractId = -1;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }


        /// <summary>
        /// Defines test method BonusAdddedToContractInIanuarie_ShouldReturnTrue.
        /// </summary>

        [TestMethod]
        public void BonusAdddedToContractInIanuarie_ShouldReturnTrue()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                ContractId = 3,
                BonusId = 3,
                DataIncheiere = new DateTime(2023, 1, 10),
                DataExpirare = new DateTime(2024, 1, 10)
            };

            // Act
            contractBonusService.Create(contractBonus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<ContractBonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            // Check if the contract was signed in January
            Assert.AreEqual(1, contractBonus.DataIncheiere.Month);
        }

        /// <summary>
        /// Defines test method BonusAdddedToContractInFebruarieShouldReturnTrue.
        /// </summary>
        [TestMethod]
        public void BonusAdddedToContractInFebruarieShouldReturnTrue()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                ContractId = 3,
                BonusId = 3,
                DataIncheiere = new DateTime(2023, 2, 10),
                DataExpirare = new DateTime(2024, 2, 10)
            };

            // Act
            contractBonusService.Create(contractBonus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<ContractBonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            // Check if the contract was signed in January
            Assert.AreEqual(2, contractBonus.DataIncheiere.Month);
        }
        /// <summary>
        /// Defines test method BonusAdddedToContractInMartie_ShouldReturnTrue.
        /// </summary>
        [TestMethod]
        public void BonusAdddedToContractInMartie_ShouldReturnTrue()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                ContractId = 3,
                BonusId = 3,
                DataIncheiere = new DateTime(2023, 3, 10),
                DataExpirare = new DateTime(2024, 3, 10)
            };

            // Act
            contractBonusService.Create(contractBonus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<ContractBonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            // Check if the contract was signed in January
            Assert.AreEqual(3, contractBonus.DataIncheiere.Month);
        }
        /// <summary>
        /// Defines test method BonusAdddedToContractInAprilie_ShouldReturnTrue.
        /// </summary>
        [TestMethod]
        public void BonusAdddedToContractInAprilie_ShouldReturnTrue()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                ContractId = 3,
                BonusId = 3,
                DataIncheiere = new DateTime(2023, 4, 10),
                DataExpirare = new DateTime(2024, 4, 10)
            };

            // Act
            contractBonusService.Create(contractBonus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<ContractBonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            // Check if the contract was signed in January
            Assert.AreEqual(4, contractBonus.DataIncheiere.Month);
        }

        /// <summary>
        /// Defines test method BonusAdddedToContractInMai_ShouldReturnTrue.
        /// </summary>

        [TestMethod]
        public void BonusAdddedToContractInMai_ShouldReturnTrue()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                ContractId = 3,
                BonusId = 3,
                DataIncheiere = new DateTime(2023, 5, 10),
                DataExpirare = new DateTime(2024, 5, 10)
            };

            // Act
            contractBonusService.Create(contractBonus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<ContractBonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            // Check if the contract was signed in January
            Assert.AreEqual(5, contractBonus.DataIncheiere.Month);
        }

        /// <summary>
        /// Defines test method BonusAdddedToContractInIunie_ShouldReturnTrue.
        /// </summary>

        [TestMethod]
        public void BonusAdddedToContractInIunie_ShouldReturnTrue()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                ContractId = 3,
                BonusId = 3,
                DataIncheiere = new DateTime(2023, 6, 10),
                DataExpirare = new DateTime(2024, 6, 10)
            };

            // Act
            contractBonusService.Create(contractBonus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<ContractBonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            // Check if the contract was signed in January
            Assert.AreEqual(6, contractBonus.DataIncheiere.Month);
        }

        /// <summary>
        /// Defines test method BonusAdddedToContractInIulie_ShouldReturnTrue.
        /// </summary>

        [TestMethod]
        public void BonusAdddedToContractInIulie_ShouldReturnTrue()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                ContractId = 3,
                BonusId = 3,
                DataIncheiere = new DateTime(2023, 7, 10),
                DataExpirare = new DateTime(2024, 7, 10)
            };

            // Act
            contractBonusService.Create(contractBonus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<ContractBonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            // Check if the contract was signed in January
            Assert.AreEqual(7, contractBonus.DataIncheiere.Month);
        }

        /// <summary>
        /// Defines test method BonusAddedToContractInAugust_ShouldReturnTrue.
        /// </summary>

        [TestMethod]
        public void BonusAddedToContractInAugust_ShouldReturnTrue()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                ContractId = 3,
                BonusId = 3,
                DataIncheiere = new DateTime(2023, 8, 10),
                DataExpirare = new DateTime(2024, 8, 10)
            };

            // Act
            contractBonusService.Create(contractBonus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<ContractBonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            // Check if the contract was signed in January
            Assert.AreEqual(8, contractBonus.DataIncheiere.Month);
        }
        /// <summary>
        /// Defines test method BonusAdddedToContractInSeptembrie_ShouldReturnTrue.
        /// </summary>


        [TestMethod]
        public void BonusAdddedToContractInSeptembrie_ShouldReturnTrue()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                ContractId = 3,
                BonusId = 3,
                DataIncheiere = new DateTime(2023, 9, 10),
                DataExpirare = new DateTime(2024, 9, 10)
            };

            // Act
            contractBonusService.Create(contractBonus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<ContractBonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            // Check if the contract was signed in January
            Assert.AreEqual(9, contractBonus.DataIncheiere.Month);
        }

        /// <summary>
        /// Defines test method BonusAdddedToContractInOctombrie_ShouldReturnTrue.
        /// </summary>

        [TestMethod]
        public void BonusAdddedToContractInOctombrie_ShouldReturnTrue()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                ContractId = 3,
                BonusId = 3,
                DataIncheiere = new DateTime(2023, 10, 10),
                DataExpirare = new DateTime(2024, 10, 10)
            };

            // Act
            contractBonusService.Create(contractBonus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<ContractBonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            // Check if the contract was signed in January
            Assert.AreEqual(10, contractBonus.DataIncheiere.Month);
        }

        /// <summary>
        /// Defines test method BonusAdddedToContractInOctombrie_ShouldReturnTrue.
        /// </summary>

        [TestMethod]
        public void BonusAdddedToContractInNoiembrie_ShouldReturnTrue()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                ContractId = 3,
                BonusId = 3,
                DataIncheiere = new DateTime(2023, 11, 10),
                DataExpirare = new DateTime(2024, 11, 10)
            };

            // Act
            contractBonusService.Create(contractBonus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<ContractBonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            // Check if the contract was signed in January
            Assert.AreEqual(11, contractBonus.DataIncheiere.Month);
        }

        /// <summary>
        /// Defines test method BonusAdddedToContractInDecembrie_ShouldReturnTrue.
        /// </summary>
        
        [TestMethod]
        public void BonusAdddedToContractInDecembrie_ShouldReturnTrue()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                ContractId = 3,
                BonusId = 3,
                DataIncheiere = new DateTime(2023, 12, 10),
                DataExpirare = new DateTime(2024, 12, 10)
            };

            // Act
            contractBonusService.Create(contractBonus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<ContractBonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            // Check if the contract was signed in January
            Assert.AreEqual(12, contractBonus.DataIncheiere.Month);
        }
    }


}
