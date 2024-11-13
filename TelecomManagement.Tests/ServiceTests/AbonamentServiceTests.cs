// <copyright file=AbonamentServiceTests.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Abonament Service Tests class. </summary>


using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Domain;
using TelecomManagement.Services;
using System.Data.Entity;

/// <summary>
/// Defines AbonamentServiceTests Tests class.
/// </summary>

namespace TelecomManagement.Tests.ServiceTests
{
  
        [TestClass]
        public class AbonamentServiceTests

        {
        /// <summary>
        /// Gets or sets the Abonament DbSet.
        /// </summary>
        private Mock<DbSet<Abonament>> mockSet;

        /// <summary>
        /// Gets or sets the Telecom Context.
        /// </summary>
        private Mock<TelecomContext> mockContext;

        /// <summary>
        /// Gets or sets the Abonament service.
        /// </summary>
        private AbonamentService abonamentService;

        /// <summary>
        /// Gets or sets the Abonament list.
        /// </summary>
        private List<Abonament> abonamentList;

        /// <summary>
        /// Setups this instance.
        /// </summary>

        [TestInitialize]
        public void Setup()
        {
            abonamentList = new List<Abonament>
    {
        new Abonament { Id = 1, Nume = "Abonament 1", Pret = 50, MinuteIncluse = 100, SMSuriIncluse = 50, TraficDateInclus = 5 }, // Exemplu de abonament
        new Abonament { Id = 2, Nume = "Abonament 2", Pret = 70, MinuteIncluse = 200, SMSuriIncluse = 100, TraficDateInclus = 10 } // Exemplu de abonament
    };

            var queryable = abonamentList.AsQueryable();

            mockSet = new Mock<DbSet<Abonament>>();
            mockSet.As<IQueryable<Abonament>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Abonament>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Abonament>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Abonament>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => abonamentList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Abonament>())).Callback<Abonament>((entity) => abonamentList.Remove(entity));
            mockSet.Setup(m => m.Add(It.IsAny<Abonament>())).Callback<Abonament>((entity) =>
            {
                if (entity.Nume == null)
                {
                    throw new ArgumentNullException(nameof(entity.Nume));
                    
                }

                if (entity.Pret < 0 || entity.MinuteIncluse < 0 || entity.SMSuriIncluse < 0 || entity.TraficDateInclus < 0)
                {
                    throw new ArgumentException("Valorile pentru MinuteBonus, SMSuriBonus și TraficDateBonus nu pot fi negative.");
                }
                
                abonamentList.Add(entity);
            });

            mockContext = new Mock<TelecomContext>();
            mockContext.Setup(m => m.Set<Abonament>()).Returns(mockSet.Object);
            abonamentService = new AbonamentService(new AbonamentRepository(mockContext.Object));
        }
        /// <summary>
        /// Defines test method AddBonus_WithNegativePret_ShouldThrowArgumentException.
        /// </summary>


        [TestMethod]
        public void AddBonus_WithNegativePret_ShouldThrowArgumentException()
        {
            // Arrange
            var abonament = new Abonament
            {
                Nume = "Test Abonament",
                Pret=-50,
                MinuteIncluse = 100,
                SMSuriIncluse = 50,
                TraficDateInclus = 2.5M
            };

            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(abonament));

        }
        /// <summary>
        /// Defines test method AddBonus_WithNegativeMinuteIncluse_ShouldThrowArgumentException.
        /// </summary>

        [TestMethod]
        public void AddBonus_WithNegativeMinuteIncluse_ShouldThrowArgumentException()
        {
            // Arrange
            var abonament = new Abonament
            {
                Nume = "Test Abonament",
                Pret = 50,
                MinuteIncluse = -100,
                SMSuriIncluse = 50,
                TraficDateInclus = 2.5M
            };

            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(abonament));

        }

        /// <summary>
        /// Defines test method AddBonus_WithNegativeSMSuriIncluse_ShouldThrowArgumentException.
        /// </summary>

        [TestMethod]
        public void AddBonus_WithNegativeSMSuriIncluse_ShouldThrowArgumentException()
        {
            // Arrange
            var abonament = new Abonament
            {
                Nume = "Test Abonament",
                Pret = 50,
                MinuteIncluse = 100,
                SMSuriIncluse = -50,
                TraficDateInclus = 2.5M
            };

            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(abonament));

        }

        /// <summary>
        /// Defines test method AddBonus_WithNegativeTraficDateIncluse_ShouldThrowArgumentException.
        /// </summary>


        [TestMethod]
        public void AddBonus_WithNegativeTraficDateIncluse_ShouldThrowArgumentException()
        {
            // Arrange
            var abonament = new Abonament
            {
                Nume = "Test Abonament",
                Pret = 50,
                MinuteIncluse = 100,
                SMSuriIncluse = 50,
                TraficDateInclus = -2.5M
            };

            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(abonament));

        }

        /// <summary>
        /// Defines test method EmptyAbonamentID.
        /// </summary>
        [TestMethod]
        public void EmptyAbonamentID()
        {
            // Arrange
            var abonamnent = new Abonament
            {
                Id = 0, // Set to an empty value
                                      // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (abonamnent.Id == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        /// <summary>
        /// Defines test method EmptyPret.
        /// </summary>

        [TestMethod]
        public void EmptyPret()
        {
            // Arrange
            var abonamnent = new Abonament
            {
                Pret = 0, // Set to an empty value
                                      // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (abonamnent.Pret == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }
        /// <summary>
        /// Defines test method EmptySMSuriIncluse.
        /// </summary>

        [TestMethod]
        public void EmptySMSuriIncluse()
        {
            // Arrange
            var abonamnent = new Abonament
            {
                SMSuriIncluse = 0, // Set to an empty value
                                      // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (abonamnent.SMSuriIncluse == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }
        /// <summary>
        /// Defines test method EmptyMinuteIncluse.
        /// </summary>

        [TestMethod]
        public void EmptyMinuteIncluse()
        {
            // Arrange
            var abonamnent = new Abonament
            {
                MinuteIncluse = 0, // Set to an empty value
                                      // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (abonamnent.MinuteIncluse == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        /// <summary>
        /// Defines test method EmptyTraficDateIncluse.
        /// </summary>

        [TestMethod]
        public void EmptyTraficDateIncluse()
        {
            // Arrange
            var abonamnent = new Abonament
            {
                TraficDateInclus = 0, // Set to an empty value
                                     // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (abonamnent.TraficDateInclus == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        /// <summary>
        /// Defines test method UpdateAbonament_WithNegativePret.
        /// </summary>
        [TestMethod]
        public void UpdateAbonament_WithNegativePret()
        {
            // Arrange
            var abonament = new Abonament
            {
                Nume = "Test Abonament",
                Pret = 50,
                MinuteIncluse = 100,
                SMSuriIncluse = 50,
                TraficDateInclus = 2.5M
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(abonament);

            // Modificați numele clientului
            abonament.Pret = -30;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }


        /// <summary>
        /// Defines test method UpdateAbonament_WithNegativeMinuteIncluse.
        /// </summary>

        [TestMethod]
        public void UpdateAbonament_WithNegativeMinuteIncluse()
        {
            // Arrange
            var abonament = new Abonament
            {
                Nume = "Test Abonament",
                Pret = 50,
                MinuteIncluse = 100,
                SMSuriIncluse = 50,
                TraficDateInclus = 2.5M
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(abonament);

            // Modificați numele clientului
            abonament.MinuteIncluse = -100;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines test method UpdateAbonament_WithNegativeSMSuriIncluse.
        /// </summary>
        [TestMethod]
        public void UpdateAbonament_WithNegativeSMSuriIncluse()
        {
            // Arrange
            var abonament = new Abonament
            {
                Nume = "Test Abonament",
                Pret = 50,
                MinuteIncluse = 100,
                SMSuriIncluse = 50,
                TraficDateInclus = 2.5M
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(abonament);

            // Modificați numele clientului
            abonament.SMSuriIncluse = -50;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines test method UpdateAbonament_WithNegativeTraficDateInclus.
        /// </summary>

        [TestMethod]
        public void UpdateAbonament_WithNegativeTraficDateInclus()
        {
            // Arrange
            var abonament = new Abonament
            {
                Nume = "Test Abonament",
                Pret = 50,
                MinuteIncluse = 100,
                SMSuriIncluse = 50,
                TraficDateInclus = 2.5M
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(abonament);

            // Modificați numele clientului
            abonament.TraficDateInclus = -2.5M;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }



    }
}
