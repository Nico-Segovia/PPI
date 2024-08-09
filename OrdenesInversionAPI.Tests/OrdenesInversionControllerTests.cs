using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NuGet.ContentModel;
using OrdenesInversionAPI.Controllers;
using OrdenesInversionAPI.Models;
using OrdenesInversionAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OrdenesInversionAPI.Tests
{
    public class OrdenesInversionControllerTests
    {
        [Fact]
        public async Task PostOrdenInversion_CalculaMontoTotalCorrectamente_ParaAccion_Compra()
        {
            // Arrange (Preparar)
            var options = new DbContextOptionsBuilder<OrdenesInversionContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            var mockService = new Mock<IOrdenInversionService>();
            mockService.Setup(s => s.PostOrdenInversion(It.IsAny<OrdenInversion>()))
                       .ReturnsAsync(new ActionResult<OrdenInversion>(new CreatedAtActionResult("GetOrdenInversion", "OrdenInversions", new { id = 1 },
                           new OrdenInversion
                           {
                               Id = 1,
                               IdCuentaInversion = 12345,
                               NombreActivo = "Apple",
                               Cantidad = 100,
                               Operacion = 'C',
                               MontoTotal = 18004.21m
                           })));

            var controller = new OrdenInversionsController(mockService.Object);
            var nuevaOrden = new OrdenInversion
            {
                IdCuentaInversion = 12345,
                NombreActivo = "Apple",
                Cantidad = 100,
                Operacion = 'C'
            };

            // Act (Actuar)
            var result = await controller.PostOrdenInversion(nuevaOrden);

            // Assert (Verificar)
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var ordenCreada = Assert.IsType<OrdenInversion>(createdResult.Value);
            Assert.Equal(18004.21m, ordenCreada.MontoTotal); // 177.97 * 100 + 0.6% + 21% de 0.6%
        }

        [Fact]
        public async Task PostOrdenInversion_CalculaMontoTotalCorrectamente_ParaAccion_Venta()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OrdenesInversionContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Nombre de base de datos único
                .Options;

            // No es necesario agregar el activo financiero aquí

            var mockService = new Mock<IOrdenInversionService>();
            mockService.Setup(s => s.PostOrdenInversion(It.IsAny<OrdenInversion>()))
                       .ReturnsAsync(new ActionResult<OrdenInversion>(new CreatedAtActionResult("GetOrdenInversion", "OrdenInversions", new { id = 1 },
                           new OrdenInversion
                           {
                               Id = 1,
                               IdCuentaInversion = 12345,
                               NombreActivo = "Apple",
                               Cantidad = 100,
                               Operacion = 'V',
                               MontoTotal = 17692.79m
                           })));

            var controller = new OrdenInversionsController(mockService.Object);
            var nuevaOrden = new OrdenInversion
            {
                IdCuentaInversion = 12345,
                NombreActivo = "Apple",
                Cantidad = 100,
                Operacion = 'V'
            };

            // Act
            var result = await controller.PostOrdenInversion(nuevaOrden);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var ordenCreada = Assert.IsType<OrdenInversion>(createdResult.Value);
            Assert.Equal(17692.79m, ordenCreada.MontoTotal); // 177.97 * 100 - 0.6% - 21% de 0.6%
        }


        [Fact]
        public async Task PostOrdenInversion_CalculaMontoTotalCorrectamente_ParaBono_Compra()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OrdenesInversionContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new OrdenesInversionContext(options))
            {
                context.ActivosFinancieros.Add(new ActivoFinanciero { Id = 6, Nombre = "BONOS ARGENTINA USD 2030 L.A", TipoActivo = 2, PrecioUnitario = 307.4m, Ticker = "AL30" });
                context.SaveChanges();
            }

            var mockService = new Mock<IOrdenInversionService>();
            mockService.Setup(s => s.PostOrdenInversion(It.IsAny<OrdenInversion>()))
                       .ReturnsAsync(new ActionResult<OrdenInversion>(new CreatedAtActionResult("GetOrdenInversion", "OrdenInversions", new { id = 1 },
                           new OrdenInversion
                           {
                               Id = 1,
                               IdCuentaInversion = 12345,
                               NombreActivo = "BONOS ARGENTINA USD 2030 L.A",
                               Cantidad = 50,
                               Precio = 305m,
                               Operacion = 'C',
                               MontoTotal = 15253.01515m
                           })));

            var controller = new OrdenInversionsController(mockService.Object);
            var nuevaOrden = new OrdenInversion
            {
                IdCuentaInversion = 12345,
                NombreActivo = "BONOS ARGENTINA USD 2030 L.A",
                Cantidad = 50,
                Precio = 305m, 
                Operacion = 'C'
            };

            // Act
            var result = await controller.PostOrdenInversion(nuevaOrden);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var ordenCreada = Assert.IsType<OrdenInversion>(createdResult.Value);
            Assert.Equal(15253.01515m, ordenCreada.MontoTotal); // 305 * 50 + 0.2% + 21% de 0.2%
        }

        [Fact]
        public async Task PostOrdenInversion_CalculaMontoTotalCorrectamente_ParaBono_Venta()
        {
            var options = new DbContextOptionsBuilder<OrdenesInversionContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            var mockService = new Mock<IOrdenInversionService>();
            mockService.Setup(s => s.PostOrdenInversion(It.IsAny<OrdenInversion>()))
                       .ReturnsAsync(new ActionResult<OrdenInversion>(new CreatedAtActionResult("GetOrdenInversion", "OrdenInversions", new { id = 1 },
                           new OrdenInversion
                           {
                               Id = 1,
                               IdCuentaInversion = 12345,
                               NombreActivo = "BONOS ARGENTINA USD 2030 L.A",
                               Cantidad = 50,
                               Precio = 305m,
                               Operacion = 'V',
                               MontoTotal = 15246.98485m
                           })));

            var controller = new OrdenInversionsController(mockService.Object);
            var nuevaOrden = new OrdenInversion
            {
                IdCuentaInversion = 12345,
                NombreActivo = "BONOS ARGENTINA USD 2030 L.A",
                Cantidad = 50,
                Precio = 305m, 
                Operacion = 'V'
            };

            // Act
            var result = await controller.PostOrdenInversion(nuevaOrden);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var ordenCreada = Assert.IsType<OrdenInversion>(createdResult.Value);
            Assert.Equal(15246.98485m, ordenCreada.MontoTotal); // 305 * 50 - 0.2% - 21% de 0.2%
        }


        [Fact]
        public async Task PostOrdenInversion_CalculaMontoTotalCorrectamente_ParaFCI_Compra()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OrdenesInversionContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new OrdenesInversionContext(options))
            {
                context.ActivosFinancieros.Add(new ActivoFinanciero { Id = 8, Nombre = "Delta Pesos Clase A", TipoActivo = 3, PrecioUnitario = 0.0181m, Ticker = "Delta.Pesos" });
                context.SaveChanges();
            }

            var mockService = new Mock<IOrdenInversionService>();
            mockService.Setup(s => s.PostOrdenInversion(It.IsAny<OrdenInversion>()))
                       .ReturnsAsync(new ActionResult<OrdenInversion>(new CreatedAtActionResult("GetOrdenInversion", "OrdenInversions", new { id = 1 },
                           new OrdenInversion
                           {
                               Id = 1,
                               IdCuentaInversion = 12345,
                               NombreActivo = "Delta Pesos Clase A",
                               Cantidad = 2000,
                               Precio = 0.018m, 
                               Operacion = 'C',
                               MontoTotal = 36.2m
                           })));

            var controller = new OrdenInversionsController(mockService.Object);
            var nuevaOrden = new OrdenInversion
            {
                IdCuentaInversion = 12345,
                NombreActivo = "Delta Pesos Clase A",
                Cantidad = 2000,
                Precio = 0.018m, 
                Operacion = 'C'
            };

            var result = await controller.PostOrdenInversion(nuevaOrden);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var ordenCreada = Assert.IsType<OrdenInversion>(createdResult.Value);
            Assert.Equal(36.2m, ordenCreada.MontoTotal); // 0.0181 (precio desde la BD) * 2000
        }



        [Fact]
        public async Task PostOrdenInversion_AsignaEstadoInicialCorrectamente()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OrdenesInversionContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            var mockService = new Mock<IOrdenInversionService>();
            mockService.Setup(s => s.PostOrdenInversion(It.IsAny<OrdenInversion>()))
                       .ReturnsAsync(new ActionResult<OrdenInversion>(new CreatedAtActionResult("GetOrdenInversion", "OrdenInversions", new { id = 1 },
                           new OrdenInversion
                           {
                               Id = 1,
                               IdCuentaInversion = 12345,
                               NombreActivo = "Apple",
                               Cantidad = 100,
                               Operacion = 'C',
                               Estado = 0, 
                               MontoTotal = 18004.21m
                           })));

            var controller = new OrdenInversionsController(mockService.Object);
            var nuevaOrden = new OrdenInversion
            {
                IdCuentaInversion = 12345,
                NombreActivo = "Apple",
                Cantidad = 100,
                Operacion = 'C'
            };

            // Act
            var result = await controller.PostOrdenInversion(nuevaOrden);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var ordenCreada = Assert.IsType<OrdenInversion>(createdResult.Value);
            Assert.Equal(0, ordenCreada.Estado); // "En proceso"
        }

        [Fact]
        public async Task PostOrdenInversion_DevuelveBadRequest_SiOperacionEsInvalida()
        {
            var mockService = new Mock<IOrdenInversionService>();

            mockService.Setup(s => s.PostOrdenInversion(It.Is<OrdenInversion>(o => o.Operacion != 'C' && o.Operacion != 'V')))
                       .ReturnsAsync(new ActionResult<OrdenInversion>(new BadRequestObjectResult("La operación debe ser 'C' (Compra) o 'V' (Venta).")));

            var controller = new OrdenInversionsController(mockService.Object);
            var nuevaOrden = new OrdenInversion
            {
                IdCuentaInversion = 12345,
                NombreActivo = "Apple",
                Cantidad = 100,
                Operacion = 'X' // Operación inválida
            };

            var result = await controller.PostOrdenInversion(nuevaOrden);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("La operación debe ser 'C' (Compra) o 'V' (Venta).", badRequestResult.Value);
        }



        [Fact]
        public async Task PostOrdenInversion_DevuelveBadRequest_SiActivoNoExiste()
        {
            var mockService = new Mock<IOrdenInversionService>();

            mockService.Setup(s => s.PostOrdenInversion(It.IsAny<OrdenInversion>()))
                       .ReturnsAsync(new ActionResult<OrdenInversion>(new BadRequestObjectResult("Activo financiero no encontrado.")));

            var controller = new OrdenInversionsController(mockService.Object);
            var nuevaOrden = new OrdenInversion
            {
                IdCuentaInversion = 12345,
                NombreActivo = "ActivoNoExistente",
                Cantidad = 100,
                Operacion = 'C'
            };

            var result = await controller.PostOrdenInversion(nuevaOrden);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Activo financiero no encontrado.", badRequestResult.Value);
        }


        [Fact]
        public async Task PostOrdenInversion_DevuelveBadRequest_SiCantidadEsInvalida()
        {
            var mockService = new Mock<IOrdenInversionService>();

            mockService.Setup(s => s.PostOrdenInversion(It.Is<OrdenInversion>(o => o.Cantidad <= 0)))
                       .ReturnsAsync(new ActionResult<OrdenInversion>(new BadRequestObjectResult("La cantidad debe ser mayor que cero.")));

            var controller = new OrdenInversionsController(mockService.Object);
            var nuevaOrden = new OrdenInversion
            {
                IdCuentaInversion = 12345,
                NombreActivo = "Apple",
                Cantidad = 0, // Cantidad inválida
                Operacion = 'C'
            };

            var result = await controller.PostOrdenInversion(nuevaOrden);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("La cantidad debe ser mayor que cero.", badRequestResult.Value);
        }

        [Fact]
        public async Task PostOrdenInversion_DevuelveBadRequest_SiPrecioEsInvalido_ParaBono()
        {
            var options = new DbContextOptionsBuilder<OrdenesInversionContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            var mockService = new Mock<IOrdenInversionService>();

            mockService.Setup(s => s.PostOrdenInversion(It.Is<OrdenInversion>(o =>
                    o.NombreActivo == "BONOS ARGENTINA USD 2030 L.A" && o.Precio <= 0)))
                       .ReturnsAsync(new ActionResult<OrdenInversion>(new BadRequestObjectResult("El precio debe ser mayor que cero para Bonos y FCIs.")));

            var controller = new OrdenInversionsController(mockService.Object);
            var nuevaOrden = new OrdenInversion
            {
                IdCuentaInversion = 12345,
                NombreActivo = "BONOS ARGENTINA USD 2030 L.A",
                Cantidad = 50,
                Precio = 0, // Precio inválido
                Operacion = 'C'
            };

            var result = await controller.PostOrdenInversion(nuevaOrden);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("El precio debe ser mayor que cero para Bonos y FCIs.", badRequestResult.Value);
        }


        [Fact]
        public async Task PostOrdenInversion_DevuelveInternalServerError_SiErrorAlGuardarEnLaBaseDeDatos()
        {
            var mockService = new Mock<IOrdenInversionService>();

            // Simula un error al guardar los cambios
            mockService.Setup(s => s.PostOrdenInversion(It.IsAny<OrdenInversion>()))
                       .ThrowsAsync(new DbUpdateException("Error simulado"));

            var controller = new OrdenInversionsController(mockService.Object);
            var nuevaOrden = new OrdenInversion
            {
                IdCuentaInversion = 12345,
                NombreActivo = "Apple",
                Cantidad = 100,
                Operacion = 'C'
            };

            var exception = await Assert.ThrowsAsync<DbUpdateException>(() => controller.PostOrdenInversion(nuevaOrden));
            Assert.Equal("Error simulado", exception.Message);
        }

        [Fact]
        public async Task PutOrdenInversion_ActualizaEstadoCorrectamente()
        {
            var mockService = new Mock<IOrdenInversionService>();

            mockService.Setup(s => s.PutOrdenInversion(1, It.IsAny<OrdenInversion>()))
                       .ReturnsAsync(new NoContentResult());

            var controller = new OrdenInversionsController(mockService.Object);
            var ordenActualizada = new OrdenInversion { Id = 1, Estado = 1 };

            var result = await controller.PutOrdenInversion(1, ordenActualizada);

            var noContentResult = Assert.IsType<NoContentResult>(result);

            mockService.Verify(s => s.PutOrdenInversion(1, ordenActualizada), Times.Once);
        }


        [Fact]
        public async Task PutOrdenInversion_DevuelveBadRequest_SiIdNoCoincide()
        {
            var mockService = new Mock<IOrdenInversionService>();

            mockService.Setup(s => s.PutOrdenInversion(2, It.IsAny<OrdenInversion>()))
                       .ReturnsAsync(new BadRequestResult());

            var controller = new OrdenInversionsController(mockService.Object);
            var ordenActualizada = new OrdenInversion { Id = 1, Estado = 1 }; // Nuevo estado: "Ejecutada"

            var result = await controller.PutOrdenInversion(2, ordenActualizada); // IDs no coinciden

            var badRequestResult = Assert.IsType<BadRequestResult>(result);
        }


        [Fact]
        public async Task PutOrdenInversion_DevuelveNotFound_SiOrdenNoExiste()
        {
            var mockService = new Mock<IOrdenInversionService>();

            mockService.Setup(s => s.PutOrdenInversion(1, It.IsAny<OrdenInversion>()))
                       .ReturnsAsync(new NotFoundResult());

            var controller = new OrdenInversionsController(mockService.Object);
            var ordenActualizada = new OrdenInversion { Id = 1, Estado = 1 };

            var result = await controller.PutOrdenInversion(1, ordenActualizada);

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PutOrdenInversion_DevuelveBadRequest_SiSeIntentanActualizarOtrosCampos()
        {
            var mockService = new Mock<IOrdenInversionService>();

            mockService.Setup(s => s.PutOrdenInversion(1, It.IsAny<OrdenInversion>()))
                       .ReturnsAsync(new BadRequestObjectResult("Solo se puede actualizar el estado de la orden."));

            var controller = new OrdenInversionsController(mockService.Object);
            var ordenActualizada = new OrdenInversion { Id = 1, Estado = 1, Cantidad = 200 }; // Intenta actualizar la cantidad

            var result = await controller.PutOrdenInversion(1, ordenActualizada);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Solo se puede actualizar el estado de la orden.", badRequestResult.Value);
        }

    }
}
