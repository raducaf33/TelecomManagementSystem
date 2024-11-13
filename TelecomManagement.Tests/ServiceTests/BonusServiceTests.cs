// <copyright file=BonusServiceTests.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Bonus Service Tests class. </summary>


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

/// <summary>
/// Defines BonusServiceTests Tests class.
/// </summary>

namespace TelecomManagement.Tests.ServiceTests
{

    [TestClass]
    public class BonusServiceTests
    {  /// <summary>
       /// Gets or sets the Bonus DbSet.
       /// </summary>
        private Mock<DbSet<Bonus>> mockSet;

        /// <summary>
        /// Gets or sets the Telecom Context.
        /// </summary>
        private Mock<TelecomContext> mockContext;
        /// <summary>
        /// Gets or sets the Bonus list.
        /// </summary>
        private List<Bonus> bonusList;
        /// <summary>
        /// Gets or sets the Bonus Service.
        /// </summary>
        private BonusService bonusService;
        private Mock<IRepositoryBase<Bonus>> mockBonusRepository;

        /// <summary>
        /// Setups this instance.
        /// </summary>
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

     


        /// <summary>
        /// Defines test method AddBonus_WithValidData_ShouldAddToRepository.
        /// </summary>

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
       
        /// <summary>
        /// Defines test method GetBonusById_WithInvalidId_ShouldReturnNull.
        /// </summary>
        [TestMethod]
        public void GetBonusById_WithInvalidId_ShouldReturnNull()
        {
            // Act
            var bonus = mockSet.Object.Find(999);

            // Assert
            Assert.IsNull(bonus);
        }
        /// <summary>
        /// Defines test method EmptyBonusId.
        /// </summary>

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
        /// <summary>
        /// Defines test method EmptyBonusName.
        /// </summary>
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
        /// <summary>
        /// Defines test method EmptyBonusMinuteBonus.
        /// </summary>


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

        /// <summary>
        /// Defines test method EmptyBonusSMSuriBonus.
        /// </summary>


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

        /// <summary>
        /// Defines test method EmptyBonusTraficDateBonus.
        /// </summary>


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

       

        /// <summary>
        /// Defines test method UpdateBonus_WithNullBonus_ShouldThrowArgumentNullException.
        /// </summary>


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateBonus_WithNullBonus_ShouldThrowArgumentNullException()
        {
            // Act
            bonusService.Update(null);
        }
        /// <summary>
        /// Defines test method UpdateBonus_WithVNegativeValues.
        /// </summary>



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

        /// <summary>
        /// Defines test method UpdateBonus_WithNegativeMInuteBonus.
        /// </summary>


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

        /// <summary>
        /// Defines test method UpdateBonus_WithNegativeSMSuriBonus.
        /// </summary>


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
        /// <summary>
        /// Defines test method UpdateBonus_WithNegativeTraficDateBonus.
        /// </summary>
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
        /// <summary>
        /// Defines test method DeleteBonus_WithINValidId_ShouldRemoveFromRepository.
        /// </summary>

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
        /// <summary>
        /// Defines test method Create_ShouldAddBonus.
        /// </summary>


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
        /// <summary>
        /// Defines test method Create_NullFactura_ShouldThrowArgumentNullException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            bonusService.Create(null);

            // Assert is handled by ExpectedException
        }

        /// <summary>
        /// Defines test method Update_ShouldUpdateFactura.
        /// </summary>

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
        /// <summary>
        /// Defines test method Update_NullFactura_ShouldThrowArgumentNullException.
        /// </summary>

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            bonusService.Update(null);

            // Assert is handled by ExpectedException
        }

        /// <summary>
        /// Defines test method Delete_ShouldRemoveFactura.
        /// </summary>


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

        /// <summary>
        /// Defines test method Delete_NullFactura_ShouldThrowArgumentNullException.
        /// </summary>




        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            bonusService.Delete((Bonus)null);

            // Assert is handled by ExpectedException
        }

        /// <summary>
        /// Defines test method DeleteById_ShouldRemoveFacturaById.
        /// </summary>

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

        /// <summary>
        /// Defines test method Find_ShouldReturnFacturaById.
        /// </summary>

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

        /// <summary>
        /// Defines test method FindAll_ShouldReturnAllFacturi.
        /// </summary>

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


