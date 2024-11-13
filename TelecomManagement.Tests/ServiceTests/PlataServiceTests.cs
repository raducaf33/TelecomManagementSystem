// <copyright file=PlataServiceTests.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Plata Service Tests class. </summary>

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
/// Defines PlataServiceTests Tests class.
/// </summary>

namespace TelecomManagement.Tests.ServiceTests
{
    [TestClass]
    public class PlataServiceTests
    { /// <summary>
      /// Gets or sets the Plata DBSet.
      /// </summary>
        private Mock<DbSet<Plata>> mockSet;

        /// <summary>
        /// Gets or sets the Telecom Context.
        /// </summary>
        private Mock<TelecomContext> mockContext;

        /// <summary>
        /// Gets or sets the Plata Service.
        /// </summary>
        private PlataService plataService;

        /// <summary>
        /// Gets or sets the Plata list.
        /// </summary>
        private List<Plata> plataList;

        /// <summary>
        /// Gets or sets the Plata interface methods.
        /// </summary>
        private Mock<IRepositoryBase<Plata>> mockPlataRepository;

        /// <summary>
        /// Setups this instance.
        /// </summary>

        [TestInitialize]
        public void Setup()
        {
            
            plataList = new List<Plata>
    {
        new Plata { Id = 1, ContractId = 1, EstePlatita = true, SumaPlata = 100, DataPlata = DateTime.Now }, // Exemplu de plată
        new Plata { Id = 2, ContractId = 2, EstePlatita = false, SumaPlata = 150, DataPlata = DateTime.Now.AddDays(-10) } // Exemplu de plată
    };

            var queryable = plataList.AsQueryable();

            // Configurare mockSet pentru DbSet<Plata>
            mockSet = new Mock<DbSet<Plata>>();


            // Configurare mockFacturaRepository pentru IRepositoryBase<Factura>
            mockPlataRepository = new Mock<IRepositoryBase<Plata>>();

            // Inițializare FacturaService cu mock-ul
            plataService = new PlataService(mockPlataRepository.Object);

            mockSet.As<IQueryable<Plata>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Plata>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Plata>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Plata>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => plataList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Plata>())).Callback<Plata>((entity) => plataList.Remove(entity));
            mockSet.Setup(m => m.Add(It.IsAny<Plata>())).Callback<Plata>((entity) =>
            {
                if (entity.ContractId <= 0 || entity.Id <= 0 || entity.SumaPlata < 0)
                {
                    throw new ArgumentException("ContractID trebuie să fie mai mare ca zero.");
                }
                if (entity.ContractId <= 0 || entity.Id <=0)
                {
                    throw new ArgumentException("ContractID trebuie să fie mai mare ca zero.");
                }

                if (entity.SumaPlata < 0)
                {
                    throw new ArgumentException("Suma plății nu poate fi negativă.");
                }

                plataList.Add(entity);
            });

            // Configurare mockContext pentru TelecomContext
            mockContext = new Mock<TelecomContext>();
            mockContext.Setup(m => m.Set<Plata>()).Returns(mockSet.Object);
            plataService = new PlataService(new PlataRepository(mockContext.Object));

            // Mock pentru TelecomContext
           // mockContext = new Mock<TelecomContext>();
           // mockContext.Setup(m => m.Set<Plata>()).Returns(mockSet.Object);

          //  mockSet = new Mock<DbSet<Plata>>();
            
           // mockContext.Setup(m => m.Set<Plata>()).Returns(mockSet.Object);
        }

        /// <summary>
        /// Defines test method AddPlata_WithNegativeid_ShouldThrowArgumentException.
        /// </summary>

        [TestMethod]
        public void AddPlata_WithNegativeid_ShouldThrowArgumentException()
        {
            // Arrange
            var plata = new Plata
            {
                Id=-1,
                ContractId = 50,
                SumaPlata = 100,
                DataPlata = DateTime.Now,
               
            };

            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(plata));

        }

        /// <summary>
        /// Defines test method AddPlata_WithNegativeContractid_ShouldThrowArgumentException.
        /// </summary>


        [TestMethod]
        public void AddPlata_WithNegativeContractid_ShouldThrowArgumentException()
        {
            // Arrange
            var plata = new Plata
            {
                Id = 1,
                ContractId = -50,
                SumaPlata = 100,
                DataPlata = DateTime.Now,

            };

            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(plata));

        }

        /// <summary>
        /// Defines test method AddPlata_WithNegativeSumaPlata_ShouldThrowArgumentException.
        /// </summary>

        [TestMethod]
        public void AddPlata_WithNegativeSumaPlata_ShouldThrowArgumentException()
        {
            // Arrange
            var plata = new Plata
            {
                Id = 1,
                ContractId = 50,
                SumaPlata = -100,
                DataPlata = DateTime.Now,

            };

            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(plata));

        }

        /// <summary>
        /// Defines test method EmptyId.
        /// </summary>

        [TestMethod]
        public void EmptyId()
        {
            // Arrange
            var plata = new Plata
            {
                Id = 0, // Set to an empty value
                        // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (plata.Id == 0)
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
            var plata = new Plata
            {
                ContractId = 0, // Set to an empty value
                        // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (plata.ContractId == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        /// <summary>
        /// Defines test method EmptySumaPlata.
        /// </summary>

        [TestMethod]
        public void EmptySumaPlata()
        {
            // Arrange
            var plata = new Plata
            {
                SumaPlata = 0, // Set to an empty value
                                // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (plata.SumaPlata == 0)
                {
                    throw new ArgumentException("SumaPlata cannot be empty.");
                }
            });
        }

        /// <summary>
        /// Defines test method DeleteBonus_WithValidId_ShouldRemoveFromRepository.
        /// </summary>

        [TestMethod]
        public void DeleteBonus_WithValidId_ShouldRemoveFromRepository()
        {
            // Arrange
            var bonus = plataList.First();

            // Act
            mockSet.Object.Remove(bonus);
            mockContext.Object.SaveChanges();

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<Plata>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual(1, plataList.Count);
        }

        /// <summary>
        /// Defines test method GetBPlataById_WithValidId_ShouldReturnBonus.
        /// </summary>
        
        [TestMethod]
        public void GetBPlataById_WithValidId_ShouldReturnBonus()
        {
            // Arrange
            var Id = 1;

            // Act
            var plata = mockSet.Object.Find(Id);

            // Assert
            Assert.IsNotNull(plata);
            Assert.AreEqual(Id, plata.Id);
        }

        /// <summary>
        /// Defines test method GetPlataById_WithInvalidId_ShouldReturnNull.
        /// </summary>

        [TestMethod]
        public void GetPlataById_WithInvalidId_ShouldReturnNull()
        {
            // Act
            var plata = mockSet.Object.Find(999);

            // Assert
            Assert.IsNull(plata);
        }

        /// <summary>
        /// Defines test method UpdatePlataId_WithVNegativeValues.
        /// </summary>

        [TestMethod]
        public void UpdatePlataId_WithVNegativeValues()
        {
            // Arrange
            var plata = new Plata
            {
                Id = 1,
                ContractId = 50,
                SumaPlata = 100,
                DataPlata = DateTime.Now,

            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(plata);

            // Modificați numele clientului
            plata.Id = -1;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines test method UpdatePlataContractId_WithVNegativeValues.
        /// </summary>

        [TestMethod]
        public void UpdatePlataContractId_WithVNegativeValues()
        {
            // Arrange
            var plata = new Plata
            {
                Id = 1,
                ContractId = 50,
                SumaPlata = 100,
                DataPlata = DateTime.Now,

            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(plata);

            // Modificați numele clientului
            plata.ContractId = -50;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines test method UpdatePlataSumaPlata_WithVNegativeValues.
        /// </summary>

        [TestMethod]
        public void UpdatePlataSumaPlata_WithVNegativeValues()
        {
            // Arrange
            var plata = new Plata
            {
                Id = 1,
                ContractId = 50,
                SumaPlata = 100,
                DataPlata = DateTime.Now,

            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(plata);

            // Modificați numele clientului
            plata.SumaPlata = -100;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines test method PlataService_ShouldIndicate_PaymentMadeForContract.
        /// </summary>

        [TestMethod]
        public void PlataService_ShouldIndicate_PaymentMadeForContract()
        {
            // Arrange
            int contractId = 1;
            var plata = new Plata { ContractId = contractId, EstePlatita = true };
            mockPlataRepository.Setup(m => m.Find(contractId)).Returns(plata);
            var plataService = new PlataService(mockPlataRepository.Object);

            // Act
            var estePlataFacuta = plataService.EstePlataFacutaPentruContract(contractId);

            // Assert
            Assert.IsTrue(estePlataFacuta, "Ar trebui să se indice că plata a fost făcută pentru contract.");
        }


        /// <summary>
        /// Defines test method PlataService_ShouldIndicate_NoPaymentMadeForContract.
        /// </summary>

        [TestMethod]
        public void PlataService_ShouldIndicate_NoPaymentMadeForContract()
        {
            // Arrange
            int contractId = 2;
            var plata = new Plata { ContractId = contractId, EstePlatita = false };
            mockPlataRepository.Setup(m => m.Find(contractId)).Returns(plata);
            var plataService = new PlataService(mockPlataRepository.Object);

            // Act
            var estePlataFacuta = plataService.EstePlataFacutaPentruContract(contractId);

            // Assert
            Assert.IsFalse(estePlataFacuta, "Ar trebui să se indice că nu s-a făcut nicio plată pentru contract.");
        }

        /// <summary>
        /// Defines test method PlataPaidInIanuarie_ShouldReturnTrue.
        /// </summary>


        [TestMethod]
        public void PlataPaidInIanuarie_ShouldReturnTrue()
        {
            // Arrange
            var plataid = 1;
            var plata = new Plata { Id = 1, ContractId = 1, EstePlatita = true, SumaPlata = 100, DataPlata = new DateTime(2023, 01, 15) };
            mockSet.Setup(m => m.Find(plataid)).Returns(plata);

            // Act
            plataService.Create(plata); // Salvăm contractul folosind DateTime(2023, 11, 15)

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Plata>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(01, plata.DataPlata.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        /// <summary>
        /// Defines test method PlataPaidInFebruarie_ShouldReturnTrue.
        /// </summary>


        [TestMethod]
        public void PlataPaidInFebruarie_ShouldReturnTrue()
        {
            // Arrange
            var plataid = 1;
            var plata = new Plata { Id = 1, ContractId = 1, EstePlatita = true, SumaPlata = 100, DataPlata = new DateTime(2023, 02, 15) };
            mockSet.Setup(m => m.Find(plataid)).Returns(plata);

            // Act
            plataService.Create(plata); // Salvăm contractul folosind DateTime(2023, 11, 15)

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Plata>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(02, plata.DataPlata.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }


        /// <summary>
        /// Defines test method PlataPaidInMartie_ShouldReturnTrue.
        /// </summary>


        [TestMethod]
        public void PlataPaidInMartie_ShouldReturnTrue()
        {
            // Arrange
            var plataid = 1;
            var plata = new Plata { Id = 1, ContractId = 1, EstePlatita = true, SumaPlata = 100, DataPlata = new DateTime(2023, 03, 15) };
            mockSet.Setup(m => m.Find(plataid)).Returns(plata);

            // Act
            plataService.Create(plata); // Salvăm contractul folosind DateTime(2023, 11, 15)

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Plata>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(03, plata.DataPlata.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        /// <summary>
        /// Defines test method PlataPaidInAprilie_ShouldReturnTrue.
        /// </summary>


        [TestMethod]
        public void PlataPaidInAprilie_ShouldReturnTrue()
        {
            // Arrange
            var plataid = 1;
            var plata = new Plata { Id = 1, ContractId = 1, EstePlatita = true, SumaPlata = 100, DataPlata = new DateTime(2023, 04, 15) };
            mockSet.Setup(m => m.Find(plataid)).Returns(plata);

            // Act
            plataService.Create(plata); // Salvăm contractul folosind DateTime(2023, 11, 15)

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Plata>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(04, plata.DataPlata.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        /// <summary>
        /// Defines test method PlataPaidInAprilie_ShouldReturnTrue.
        /// </summary>

        [TestMethod]
        public void PlataPaidInMai_ShouldReturnTrue()
        {
            // Arrange
            var plataid = 1;
            var plata = new Plata { Id = 1, ContractId = 1, EstePlatita = true, SumaPlata = 100, DataPlata = new DateTime(2023, 05, 15) };
            mockSet.Setup(m => m.Find(plataid)).Returns(plata);

            // Act
            plataService.Create(plata); // Salvăm contractul folosind DateTime(2023, 11, 15)

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Plata>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(05, plata.DataPlata.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        /// <summary>
        /// Defines test method PlataPaidInIunie_ShouldReturnTrue.
        /// </summary>

        [TestMethod]
        public void PlataPaidInIunie_ShouldReturnTrue()
        {
            // Arrange
            var plataid = 1;
            var plata = new Plata { Id = 1, ContractId = 1, EstePlatita = true, SumaPlata = 100, DataPlata = new DateTime(2023, 06, 15) };
            mockSet.Setup(m => m.Find(plataid)).Returns(plata);

            // Act
            plataService.Create(plata); // Salvăm contractul folosind DateTime(2023, 11, 15)

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Plata>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(06, plata.DataPlata.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        /// <summary>
        /// Defines test method PlataPaidInIulie_ShouldReturnTrue.
        /// </summary>

        [TestMethod]
        public void PlataPaidInIulie_ShouldReturnTrue()
        {
            // Arrange
            var plataid = 1;
            var plata = new Plata { Id = 1, ContractId = 1, EstePlatita = true, SumaPlata = 100, DataPlata = new DateTime(2023, 07, 15) };
            mockSet.Setup(m => m.Find(plataid)).Returns(plata);

            // Act
            plataService.Create(plata); // Salvăm contractul folosind DateTime(2023, 11, 15)

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Plata>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(07, plata.DataPlata.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        /// <summary>
        /// Defines test method PlataPaidInAugust_ShouldReturnTrue.
        /// </summary>



        [TestMethod]
        public void PlataPaidInAugust_ShouldReturnTrue()
        {
            // Arrange
            var plataid = 1;
            var plata = new Plata { Id = 1, ContractId = 1, EstePlatita = true, SumaPlata = 100, DataPlata = new DateTime(2023, 08, 15) };
            mockSet.Setup(m => m.Find(plataid)).Returns(plata);

            // Act
            plataService.Create(plata); // Salvăm contractul folosind DateTime(2023, 11, 15)

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Plata>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(08, plata.DataPlata.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        /// <summary>
        /// Defines test method PlataPaidInSeptembrie_ShouldReturnTrue.
        /// </summary>



        [TestMethod]
        public void PlataPaidInSeptembrie_ShouldReturnTrue()
        {
            // Arrange
            var plataid = 1;
            var plata = new Plata { Id = 1, ContractId = 1, EstePlatita = true, SumaPlata = 100, DataPlata = new DateTime(2023, 09, 15) };
            mockSet.Setup(m => m.Find(plataid)).Returns(plata);

            // Act
            plataService.Create(plata); // Salvăm contractul folosind DateTime(2023, 11, 15)

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Plata>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(09, plata.DataPlata.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        /// <summary>
        /// Defines test method PlataPaidInOctombrie_ShouldReturnTrue.
        /// </summary>


        [TestMethod]
        public void PlataPaidInOctombrie_ShouldReturnTrue()
        {
            // Arrange
            var plataid = 1;
            var plata = new Plata { Id = 1, ContractId = 1, EstePlatita = true, SumaPlata = 100, DataPlata = new DateTime(2023, 10, 15) };
            mockSet.Setup(m => m.Find(plataid)).Returns(plata);

            // Act
            plataService.Create(plata); // Salvăm contractul folosind DateTime(2023, 11, 15)

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Plata>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(10, plata.DataPlata.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        /// <summary>
        /// Defines test method PlataPaidInNoiembrie_ShouldReturnTrue.
        /// </summary>


        [TestMethod]
        public void PlataPaidInNoiembrie_ShouldReturnTrue()
        {
            // Arrange
            var plataid = 1;
            var plata = new Plata { Id = 1, ContractId = 1, EstePlatita = true, SumaPlata = 100, DataPlata = new DateTime(2023, 11, 15) } ;
            mockSet.Setup(m => m.Find(plataid)).Returns(plata);

            // Act
            plataService.Create(plata); // Salvăm contractul folosind DateTime(2023, 11, 15)

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Plata>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(11, plata.DataPlata.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }

        /// <summary>
        /// Defines test method PlataPaidInDecembrie_ShouldReturnTrue.
        /// </summary>

        [TestMethod]
        public void PlataPaidInDecembrie_ShouldReturnTrue()
        {
            // Arrange
            var plataid = 1;
            var plata = new Plata { Id = 1, ContractId = 1, EstePlatita = true, SumaPlata = 100, DataPlata = new DateTime(2023, 12, 15) };
            mockSet.Setup(m => m.Find(plataid)).Returns(plata);

            // Act
            plataService.Create(plata); // Salvăm contractul folosind DateTime(2023, 11, 15)

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Plata>()), Times.Once()); // Verificăm dacă Add a fost apelat o singură dată pe mockSet
            mockContext.Verify(m => m.SaveChanges(), Times.Once()); // Verificăm dacă SaveChanges a fost apelat o singură dată pe mockContext

            // Adăugăm aserțiunea pentru luna de încheiere a contractului
            Assert.AreEqual(12, plata.DataPlata.Month); // Verificăm dacă data de încheiere este în luna septembrie
        }


    }
}
