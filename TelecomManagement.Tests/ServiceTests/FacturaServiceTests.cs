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

namespace TelecomManagement.Tests.ServiceTests
{
    [TestClass]
    public class FacturaServiceTests
    {
        private Mock<DbSet<Factura>> mockSet;
        private Mock<TelecomContext> mockContext;
        private FacturaService facturaService;
        private List<Factura> facturaList;
        private Mock<IRepositoryBase<Factura>> mockFacturaRepository;


        [TestInitialize]
        public void Setup()
        {
            // Inițializare lista de facturi
            facturaList = new List<Factura>
    {
        new Factura { Id = 1, ContractId = 1, SumaTotalaPlata = 100, DataEmitere = DateTime.Now, DataScadenta = DateTime.Now.AddDays(30) },
        new Factura { Id = 2, ContractId = 2, SumaTotalaPlata = 150, DataEmitere = DateTime.Now.AddDays(-10),  DataScadenta = DateTime.Now.AddDays(30) }
    };

            var queryable = facturaList.AsQueryable();

            // Configurare mockSet pentru DbSet<Factura>
            mockSet = new Mock<DbSet<Factura>>();

            // Configurare mockFacturaRepository pentru IRepositoryBase<Factura>
            mockFacturaRepository = new Mock<IRepositoryBase<Factura>>();

            // Inițializare FacturaService cu mock-ul
            facturaService = new FacturaService(mockFacturaRepository.Object);

            // Configurare mockSet pentru IQueryable<Factura>
            mockSet.As<IQueryable<Factura>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Factura>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Factura>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Factura>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => facturaList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Factura>())).Callback<Factura>((entity) => facturaList.Remove(entity));
            mockSet.Setup(m => m.Add(It.IsAny<Factura>())).Callback<Factura>((entity) =>
            {
                if (entity.ContractId <= 0 || entity.Id <= 0 || entity.SumaTotalaPlata < 0)
                {
                    throw new ArgumentException("ContractID trebuie să fie mai mare ca zero.");
                }

                facturaList.Add(entity);
            });
        }

            [TestMethod]
        public void AddCFactura_WithIdNegativ_ShouldThrowArgumentNullException()
        {
            // Arrange
            var factura = new Factura
            {
                Id = -1,
                ContractId = 1,
                SumaTotalaPlata = 200,
                DataEmitere = DateTime.Now,
                DataScadenta = DateTime.Today.AddDays(-30)

            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(factura));
        }

        [TestMethod]
        public void AddCFactura_WithContractIdNegativ_ShouldThrowArgumentNullException()
        {
            // Arrange
            var factura = new Factura
            {
                Id = 1,
                ContractId = -1,
                SumaTotalaPlata = 200,
                DataEmitere = DateTime.Now,
                DataScadenta = DateTime.Today.AddDays(-30)

            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(factura));
        }


        [TestMethod]
        public void AddCFactura_WithSumaTotalaPlataNegativ_ShouldThrowArgumentNullException()
        {
            // Arrange
            var factura = new Factura
            {
                Id = 1,
                ContractId = 1,
                SumaTotalaPlata = -200,
                DataEmitere = DateTime.Now,
                DataScadenta = DateTime.Today.AddDays(-30)

            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockSet.Object.Add(factura));
        }
        

        [TestMethod]
        public void EmptyId()
        {
            // Arrange
            var factura = new Factura
            {
                Id = 0, // Set to an empty value
                        // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (factura.Id == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        [TestMethod]
        public void EmptyCntractId()
        {
            // Arrange
            var factura = new Factura
            {
                ContractId = 0, // Set to an empty value
                        // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (factura.ContractId == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        [TestMethod]
        public void EmptySumaaTotalaPlata()
        {
            // Arrange
            var factura = new Factura
            {
                SumaTotalaPlata = 0, // Set to an empty value
                        // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (factura.SumaTotalaPlata == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        [TestMethod]
        public void UpdateFactura_WithVNegativeIdValue()
        {
            // Arrange
            var factura = new Factura
            {
                Id = 1,
                ContractId = 1,
                SumaTotalaPlata = 200,
                DataEmitere = DateTime.Now,
                DataScadenta = DateTime.Today.AddDays(-30)

            };

           

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(factura);

            // Modificați numele clientului
            factura.Id = -1;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }


        [TestMethod]
        public void UpdateFactura_WithVNegativeContractIdValue()
        {
            // Arrange
            var factura = new Factura
            {
                Id = 1,
                ContractId = 1,
                SumaTotalaPlata = 200,
                DataEmitere = DateTime.Now,
                DataScadenta = DateTime.Today.AddDays(-30)

            };



            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(factura);

            // Modificați numele clientului
            factura.ContractId = -1;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void UpdateFactura_WithVNegativeSumaTotalaPlataValue()
        {
            // Arrange
            var factura = new Factura
            {
                Id = 1,
                ContractId = 1,
                SumaTotalaPlata = 200,
                DataEmitere = DateTime.Now,
                DataScadenta = DateTime.Today.AddDays(-30)

            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(factura);

            // Modificați numele clientului
            factura.ContractId = -200;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }


        [TestMethod]
        public void DeleteFactura_WithValidId_ShouldRemoveFromRepository()
        {
            // Arrange
            var bonus = facturaList.First();

            // Act
            mockSet.Object.Remove(bonus);
            mockContext.Object.SaveChanges();

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<Factura>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual(6, facturaList.Count);
        }

        [TestMethod]
        public void GetBPlataById_WithValidId_ShouldReturnFactura()
        {
            // Arrange
            var Id = 9;

            // Act
            var plata = mockSet.Object.Find(Id);

            // Assert
             
            Assert.AreEqual(Id, plata.Id);
        }

        [TestMethod]
        public void GetPlataById_WithInvalidId_ShouldReturnNull()
        {
            // Act
            var plata = mockSet.Object.Find(999);

            // Assert
            Assert.IsNull(plata);
        }

        [TestMethod]
        public void Create_ShouldAddFactura()
        {
            // Arrange
            var factura = new Factura { Id = 1, ContractId = 100, SumaTotalaPlata = 500, DataEmitere = new DateTime(2024, 1, 1), DataScadenta = new DateTime(2024, 2, 1) };
            mockFacturaRepository.Setup(repo => repo.Create(It.IsAny<Factura>())).Verifiable();

            // Act
            facturaService.Create(factura);

            // Assert
            mockFacturaRepository.Verify(repo => repo.Create(It.Is<Factura>(f => f.Id == 1)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            facturaService.Create(null);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void Update_ShouldUpdateFactura()
        {
            // Arrange
            var factura = new Factura { Id = 1, ContractId = 100, SumaTotalaPlata = 500, DataEmitere = new DateTime(2024, 1, 1), DataScadenta = new DateTime(2024, 2, 1) };
            mockFacturaRepository.Setup(repo => repo.Update(It.IsAny<Factura>())).Verifiable();

            // Act
            factura.SumaTotalaPlata = 600;
            facturaService.Update(factura);

            // Assert
            mockFacturaRepository.Verify(repo => repo.Update(It.Is<Factura>(f => f.SumaTotalaPlata == 600)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            facturaService.Update(null);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void Delete_ShouldRemoveFactura()
        {
            // Arrange
            var factura = new Factura { Id = 1, ContractId = 100, SumaTotalaPlata = 500, DataEmitere = new DateTime(2024, 1, 1), DataScadenta = new DateTime(2024, 2, 1) };
            mockFacturaRepository.Setup(repo => repo.Delete(It.IsAny<Factura>())).Verifiable();

            // Act
            facturaService.Delete(factura);

            // Assert
            mockFacturaRepository.Verify(repo => repo.Delete(It.Is<Factura>(f => f.Id == 1)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            facturaService.Delete((Factura)null);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void DeleteById_ShouldRemoveFacturaById()
        {
            // Arrange
            int facturaId = 1;
            mockFacturaRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Verifiable();

            // Act
            facturaService.Delete(facturaId);

            // Assert
            mockFacturaRepository.Verify(repo => repo.Delete(facturaId), Times.Once);
        }

        [TestMethod]
        public void Find_ShouldReturnFacturaById()
        {
            // Arrange
            var factura = new Factura { Id = 1, ContractId = 100, SumaTotalaPlata = 500, DataEmitere = new DateTime(2024, 1, 1), DataScadenta = new DateTime(2024, 2, 1) };
            mockFacturaRepository.Setup(repo => repo.Find(1)).Returns(factura);

            // Act
            var result = facturaService.Find(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public void FindAll_ShouldReturnAllFacturi()
        {
            // Arrange
            var facturiList = new List<Factura>
            {
                new Factura { Id = 1, ContractId = 100, SumaTotalaPlata = 500, DataEmitere = new DateTime(2024, 1, 1), DataScadenta = new DateTime(2024, 2, 1) },
                new Factura { Id = 2, ContractId = 200, SumaTotalaPlata = 1000, DataEmitere = new DateTime(2024, 2, 1), DataScadenta = new DateTime(2024, 3, 1) }
            };
            mockFacturaRepository.Setup(repo => repo.FindAll()).Returns(facturiList.AsQueryable());

            // Act
            var result = facturaService.FindAll();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result.First().Id);
        }
    }

}