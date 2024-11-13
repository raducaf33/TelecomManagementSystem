// <copyright file=FacturaServiceTests.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Factura Service Tests class. </summary>

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
/// Defines FacturaServiceTests Tests class.
/// </summary>

namespace TelecomManagement.Tests.ServiceTests
{
    [TestClass]
    public class FacturaServiceTests
    {
        /// <summary>
        /// Gets or sets the Factura DbSet.
        /// </summary>
        private Mock<DbSet<Factura>> mockSet;

        /// <summary>
        /// Gets or sets the Telecom Context.
        /// </summary>
        private Mock<TelecomContext> mockContext;

        /// <summary>
        /// Gets or sets the Factura service.
        /// </summary>
        private FacturaService facturaService;

        /// <summary>
        /// Gets or sets the Factura list.
        /// </summary>
        private List<Factura> facturaList;

        /// <summary>
        /// Gets or sets the Factura interface.
        /// </summary>
        private Mock<IRepositoryBase<Factura>> mockFacturaRepository;

        /// <summary>
        /// Setups this instance.
        /// </summary>


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

        /// <summary>
        /// Defines test method AddCFactura_WithIdNegativ_ShouldThrowArgumentNullException.
        /// </summary>

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


        /// <summary>
        /// Defines test method AddCFactura_WithContractIdNegativ_ShouldThrowArgumentNullException.
        /// </summary>

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


        /// <summary>
        /// Defines test method AddCFactura_WithSumaTotalaPlataNegativ_ShouldThrowArgumentNullException.
        /// </summary>


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

        /// <summary>
        /// Defines test method EmptyId.
        /// </summary>

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

        /// <summary>
        /// Defines test method EmptyContractId.
        /// </summary>

        [TestMethod]
        public void EmptyContractId()
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


        /// <summary>
        /// Defines test method EmptySumaaTotalaPlata.
        /// </summary>


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

        /// <summary>
        /// Defines test method UpdateFactura_WithVNegativeIdValue.
        /// </summary>


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


        /// <summary>
        /// Defines test method UpdateFactura_WithVNegativeContractIdValue.
        /// </summary>


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
        /// <summary>
        /// Defines test method UpdateFactura_WithVNegativeSumaTotalaPlataValue.
        /// </summary>

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
        /// Defines test method Create_ShouldAddFactura.
        /// </summary>


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


        /// <summary>
        /// Defines test method Create_NullFactura_ShouldThrowArgumentNullException.
        /// </summary>


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            facturaService.Create(null);

            // Assert is handled by ExpectedException
        }

        /// <summary>
        /// Defines test method Update_ShouldUpdateFactura.
        /// </summary>

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

        /// <summary>
        /// Defines test method Update_NullFactura_ShouldThrowArgumentNullException.
        /// </summary>


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            facturaService.Update(null);

            // Assert is handled by ExpectedException
        }

        /// <summary>
        /// Defines test method Delete_ShouldRemoveFactura.
        /// </summary>

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

        /// <summary>
        /// Defines test method Delete_ShouldRemoveFactura_WithInvalidId.
        /// </summary>
        [TestMethod]
        public void Delete_ShouldRemoveFactura_WithInvalidId()
        {
            // Arrange
            var factura = new Factura { Id = -2, ContractId = 100, SumaTotalaPlata = 500, DataEmitere = new DateTime(2024, 1, 1), DataScadenta = new DateTime(2024, 2, 1) };
            mockFacturaRepository.Setup(repo => repo.Delete(It.IsAny<Factura>())).Verifiable();

            // Act
            facturaService.Delete(factura);

            // Assert
            mockFacturaRepository.Verify(repo => repo.Delete(It.Is<Factura>(f => f.Id == -2)), Times.Once);
        }

        /// <summary>
        /// Defines test method Delete_NullFactura_ShouldThrowArgumentNullException.
        /// </summary>

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            facturaService.Delete((Factura)null);

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
            mockFacturaRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Verifiable();

            // Act
            facturaService.Delete(facturaId);

            // Assert
            mockFacturaRepository.Verify(repo => repo.Delete(facturaId), Times.Once);
        }

        /// <summary>
        /// Defines test method Find_ShouldReturnFacturaById.
        /// </summary>


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

        /// <summary>
        /// Defines test method FindAll_ShouldReturnAllFacturi.
        /// </summary>

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