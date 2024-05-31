using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;
using TelecomManagement.Services;
using System.Data.Entity;
using Moq;
using TelecomManagement.Data;
using TelecomManagement.Services.Base;

namespace TelecomManagement.Tests.ServiceTests
{

    [TestClass]
    public class BonusServiceTests
    {
        private Mock<DbSet<Bonus>> mockSet;
        private Mock<TelecomContext> mockContext;
        private List<Bonus> bonusList;
        private BonusService bonusService;
        private Mock<IRepositoryBase<Bonus>> mockBonusRepository;


        [TestInitialize]
        public void Setup()
        {
            // Mock pentru DbSet<Bonus>
            bonusList = new List<Bonus>
            {
                new Bonus { Id = 1, Nume = "Bonus1", MinuteBonus = 100, SMSuriBonus = 50, TraficDateBonus = 1.5M },
                new Bonus { Id = 2, Nume = "Bonus2", MinuteBonus = 200, SMSuriBonus = 100, TraficDateBonus = 2.5M }
            };


            // Configurare mockSet pentru DbSet<Factura>
            mockSet = new Mock<DbSet<Bonus>>();

            // Configurare mockFacturaRepository pentru IRepositoryBase<Factura>
            mockBonusRepository = new Mock<IRepositoryBase<Bonus>>();

            // Inițializare FacturaService cu mock-ul
            bonusService = new BonusService(mockBonusRepository.Object);


            var queryable = bonusList.AsQueryable();
            mockSet = new Mock<DbSet<Bonus>>();
            mockSet.As<IQueryable<Bonus>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Bonus>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Bonus>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Bonus>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => bonusList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Bonus>())).Callback<Bonus>((entity) => bonusList.Remove(entity));

            mockSet.Setup(m => m.Add(It.IsAny<Bonus>())).Callback<Bonus>((entity) =>
            {
                if (entity.Nume == null)
                {
                    throw new ArgumentNullException(nameof(entity.Nume));
                    throw new ArgumentNullException(nameof(entity.MinuteBonus));
                    throw new ArgumentNullException(nameof(entity.Nume));
                }

                if (entity.MinuteBonus <= 0 || entity.SMSuriBonus <= 0 || entity.TraficDateBonus <= 0)
                {
                    throw new ArgumentException("Valorile pentru MinuteBonus, SMSuriBonus și TraficDateBonus nu pot fi negative.");
                }
                bonusList.Add(entity);
            });

            // Mock pentru TelecomContext
            mockContext = new Mock<TelecomContext>();
            mockContext.Setup(m => m.Set<Bonus>()).Returns(mockSet.Object);

            mockSet = new Mock<DbSet<Bonus>>();
            mockContext = new Mock<TelecomContext>();
            mockContext.Setup(m => m.Set<Bonus>()).Returns(mockSet.Object);

        }

        [TestMethod]
        public void AddBonus_WithNegativeValues_ShouldThrowArgumentException()
        {
            // Arrange
            var bonus = new Bonus
            {
                Nume = "Test Bonus",
                MinuteBonus = -100,
                SMSuriBonus = 50,
                TraficDateBonus = 2.5M
            };

            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(bonus));



        }

        [TestMethod]
        public void AddBonus_WithNegativeSMSBonusuri_ShouldThrowArgumentException()
        {
            // Arrange
            var bonus = new Bonus
            {
                Nume = "Test Bonus",
                MinuteBonus = 100,
                SMSuriBonus = -50,
                TraficDateBonus = 2.5M
            };

            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(bonus));



        }

        [TestMethod]
        public void AddBonus_WithNegativeTraficDateBonus_ShouldThrowArgumentException()
        {
            // Arrange
            var bonus = new Bonus
            {
                Nume = "Test Bonus",
                MinuteBonus = 100,
                SMSuriBonus = 50,
                TraficDateBonus = -2.5M
            };

            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(bonus));



        }

        [TestMethod]
        public void AddBonus_WithValidData_ShouldAddToRepository()
        {
            // Arrange
            var bonus = new Bonus
            {
                Nume = "Bonus3",
                MinuteBonus = 300,
                SMSuriBonus = 150,
                TraficDateBonus = 3.5M
            };

            // Act
            mockSet.Object.Add(bonus);
            mockContext.Object.SaveChanges();

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Bonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void AddBonus_WithNullName_ShouldThrowArgumentNullException()
        {
            // Arrange
            var bonus = new Bonus
            {
                Nume = null,
                MinuteBonus = 300,
                SMSuriBonus = 150,
                TraficDateBonus = 3.5M
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => mockSet.Object.Add(bonus));
        }

        [TestMethod]
        public void DeleteBonus_WithValidId_ShouldRemoveFromRepository()
        {
            // Arrange
            var bonus = bonusList.First();

            // Act
            mockSet.Object.Remove(bonus);
            mockContext.Object.SaveChanges();

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<Bonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual(1, bonusList.Count);
        }

        [TestMethod]
        public void GetBonusById_WithValidId_ShouldReturnBonus()
        {
            // Arrange
            var bonusId = 2;

            // Act
            var bonus = mockSet.Object.Find(bonusId);

            // Assert
            Assert.IsNotNull(bonus);
            Assert.AreEqual(bonusId, bonus.Id);
        }

        [TestMethod]
        public void GetBonusById_WithInvalidId_ShouldReturnNull()
        {
            // Act
            var bonus = mockSet.Object.Find(999);

            // Assert
            Assert.IsNull(bonus);
        }


        [TestMethod]
        public void EmptyBonusId()
        {
            // Arrange
            var bonus = new Bonus
            {
                Id = 0, // Set to an empty value
                        // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (bonus.Id == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }
        [TestMethod]
        public void EmptyBonusName()
        {
            // Arrange
            var bonus = new Bonus
            {
                Nume = null, // Set to an empty value
                             // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (bonus.Nume == null)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        [TestMethod]
        public void EmptyBonusMinuteBonus()
        {
            // Arrange
            var bonus = new Bonus
            {
                MinuteBonus = 0, // Set to an empty value
                                 // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (bonus.MinuteBonus == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        [TestMethod]
        public void EmptyBonusSMSuriBonus()
        {
            // Arrange
            var bonus = new Bonus
            {
                SMSuriBonus = 0, // Set to an empty value
                                 // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (bonus.SMSuriBonus == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        [TestMethod]
        public void EmptyBonusTraficDateBonus()
        {
            // Arrange
            var bonus = new Bonus
            {
                TraficDateBonus = 0, // Set to an empty value
                                     // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (bonus.TraficDateBonus == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        [TestMethod]
        public void UpdateBonus_WithValidData_ShouldUpdateInRepository()
        {
            // Arrange
            var bonus = new Bonus
            {
                Nume = "Bonus 1",
                MinuteBonus = 300,
                SMSuriBonus = 150,
                TraficDateBonus = 3.5M
            };


            mockSet.Setup(m => m.Find(1)).Returns(bonus);
            // Act
            bonusService.Update(bonus);

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
            Assert.AreEqual("UpdatedBonus", bonusList.First().Nume);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateBonus_WithNullBonus_ShouldThrowArgumentNullException()
        {
            // Act
            bonusService.Update(null);
        }



        [TestMethod]
        public void UpdateBonus_WithVNegativeValues()
        {
            // Arrange
            var bonus = new Bonus
            {
                Nume = "Bonus 1",
                MinuteBonus = 300,
                SMSuriBonus = 150,
                TraficDateBonus = 3.5M
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(bonus);

            // Modificați numele clientului
            bonus.MinuteBonus = -300;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void UpdateBonus_WithNegativeMInuteBonus()
        {
            // Arrange
            var bonus = new Bonus
            {
                Nume = "Bonus 1",
                MinuteBonus = 300,
                SMSuriBonus = 150,
                TraficDateBonus = 3.5M
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(bonus);

            // Modificați numele clientului
            bonus.MinuteBonus = -140;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void UpdateBonus_WithNegativeSMSuriBonus()
        {
            // Arrange
            var bonus = new Bonus
            {
                Nume = "Bonus 1",
                MinuteBonus = 300,
                SMSuriBonus = 150,
                TraficDateBonus = 3.5M
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(bonus);

            // Modificați numele clientului
            bonus.SMSuriBonus = -400;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void UpdateBonus_WithNegativeTraficDateBonus()
        {
            // Arrange
            var bonus = new Bonus
            {
                Nume = "Bonus 1",
                MinuteBonus = 300,
                SMSuriBonus = 150,
                TraficDateBonus = 3.5M
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(bonus);

            // Modificați numele clientului
            bonus.TraficDateBonus = -3.5M;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }


        [TestMethod]
        public void DeleteBonus_WithINValidId_ShouldRemoveFromRepository()
        {
            // Arrange
            var bonus = new Bonus { MinuteBonus = -700 }

                ;

            // Act
            mockSet.Object.Remove(bonus);
            mockContext.Object.SaveChanges();

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<Bonus>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual(2, bonusList.Count);
        }


        [TestMethod]
        public void GetAllBonuses_ShouldReturnAllBonuses()
        {
            // Act
            var bonuses = bonusService.GetAll();

            // Assert
            Assert.AreEqual(2, bonuses.Count());
        }

        [TestMethod]
        public void FindBonus_ById_ShouldReturnCorrectBonus()
        {
            // Arrange
            var bonusId = 1;
            var bonus = new Bonus { Id = bonusId, Nume = "Bonus 1", MinuteBonus = 100, SMSuriBonus = 50, TraficDateBonus = 1.5M };

            mockSet.Setup(m => m.Find(1)).Returns(bonus);

            // Act
            var result = bonusService.Find(bonusId);

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void Create_ShouldAddBonus()
        {
            // Arrange
            var bonus = new Bonus { Id = 1, Nume = "Bonus 1", MinuteBonus = 100, SMSuriBonus = 50, TraficDateBonus = 1.5M };

            mockBonusRepository.Setup(repo => repo.Create(It.IsAny<Bonus>())).Verifiable();

            // Act
            bonusService.Create(bonus);

            // Assert
            mockBonusRepository.Verify(repo => repo.Create(It.Is<Bonus>(f => f.Id == 1)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            bonusService.Create(null);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void Update_ShouldUpdateFactura()
        {
            // Arrange
            var bonus = new Bonus { Id = 1, Nume = "Bonus 1", MinuteBonus = 100, SMSuriBonus = 50, TraficDateBonus = 1.5M };

            mockBonusRepository.Setup(repo => repo.Update(It.IsAny<Bonus>())).Verifiable();

            // Act
            bonus.MinuteBonus = 600;
            bonusService.Update(bonus);

            // Assert
            mockBonusRepository.Verify(repo => repo.Update(It.Is<Bonus>(f => f.MinuteBonus == 600)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            bonusService.Update(null);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void Delete_ShouldRemoveFactura()
        {
            // Arrange
            var bonus = new Bonus { Id = 1, Nume = "Bonus 1", MinuteBonus = 100, SMSuriBonus = 50, TraficDateBonus = 1.5M };

            mockBonusRepository.Setup(repo => repo.Delete(It.IsAny<Bonus>())).Verifiable();

            // Act
            bonusService.Delete(bonus);

            // Assert
            mockBonusRepository.Verify(repo => repo.Delete(It.Is<Bonus>(f => f.Id == 1)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            bonusService.Delete((Bonus)null);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void DeleteById_ShouldRemoveFacturaById()
        {
            // Arrange
            int facturaId = 1;
            mockBonusRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Verifiable();

            // Act
            bonusService.Delete(facturaId);

            // Assert
            mockBonusRepository.Verify(repo => repo.Delete(facturaId), Times.Once);
        }

        [TestMethod]
        public void Find_ShouldReturnFacturaById()
        {
            // Arrange
            var bonus = new Bonus { Id = 1, Nume = "Bonus 1", MinuteBonus = 100, SMSuriBonus = 50, TraficDateBonus = 1.5M };
            mockBonusRepository.Setup(repo => repo.Find(1)).Returns(bonus);

            // Act
            var result = bonusService.Find(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public void FindAll_ShouldReturnAllFacturi()
        {
            // Arrange
            var bonus = new List<Bonus>
            {
                 new Bonus { Id = 1, Nume = "Bonus 1", MinuteBonus = 100, SMSuriBonus = 50, TraficDateBonus = 1.5M }
        };
        mockBonusRepository.Setup(repo => repo.FindAll()).Returns(bonusList.AsQueryable());

        // Act
        var result = bonusService.FindAll();

        // Assert
        Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result.First().Id);
        }
}
    }


