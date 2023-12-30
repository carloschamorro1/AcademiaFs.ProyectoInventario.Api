using AcademiaFs.ProyectoInventario.Api._Features.Productos;
using AcademiaFs.ProyectoInventario.Api._Features.Productos.Dtos;
using Farsiman.Application.Core.Standard.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFs.ProyectoInventario.Api.Controllers
{
    [Authorize]
    [Route("productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosService _service;
        public ProductosController(IProductosService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public IActionResult ObtenerProductos()
        {
            Respuesta<List<ProductoDto>> resultado = _service.ObtenerProductos();
            return Ok(resultado);
        }

        [HttpGet("{idProducto}")]
        public IActionResult ObtenerProductoPorId(int idProducto)
        {
            Respuesta<ProductoDto> resultado = _service.ObtenerProductoPorId(idProducto);
            return Ok(resultado);
        }

        [HttpPatch("{idProducto}")]
        public IActionResult DesactivarProducto(int idProducto)
        {
            Respuesta<ProductoDto> respuesta = _service.DesactivarProducto(idProducto);
            return Ok(respuesta);
        }


        [HttpPut("{idProducto}")]
        public IActionResult ActualizarPerfil(ProductoActualizarSolicitudDto productoDto, int idProducto)
        {
            Respuesta<ProductoActualizarDto> respuesta = _service.ActualizarProducto(productoDto, idProducto);
            return Ok(respuesta);
        }

        [HttpPost("")]
        public IActionResult AgregarPerfil(ProductoAgregarSolicitudDto productoDto)
        {
            Respuesta<ProductoAgregarDto> respuesta = _service.AgregarProducto(productoDto);
            return Ok(respuesta);
        }
    }
}
