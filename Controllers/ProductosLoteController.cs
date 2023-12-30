using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes;
using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Dtos;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFs.ProyectoInventario.Api.Controllers
{
    [Route("lotes")]
    [ApiController]
    public class ProductosLoteController : ControllerBase
    {

        private readonly IProductosLotesService _service;
        private readonly IMapper _mapper;

        public ProductosLoteController(IProductosLotesService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult ObtenerProductosLotes()
        {
            Respuesta<List<ProductoLoteDto>> resultado = _service.ObtenerProductosLote();
            return Ok(resultado);
        }

        [HttpGet("{idProductoLote}")]
        public IActionResult ObtenerProductoLotePorId(int idProductoLote)
        {
            Respuesta<ProductoLoteDto> resultado = _service.ObtenerProductoLotePorId(idProductoLote);
            return Ok(resultado);
        }

        [HttpPatch("{idProductoLote}")]
        public IActionResult DesactivarPerfil(int idProductoLote)
        {
            Respuesta<ProductoLoteDto> respuesta = _service.DesactivarProductoLote(idProductoLote);
            return Ok(respuesta);
        }

        [HttpPost("")]
        public IActionResult AgregarProductoLote(ProductoLoteAgregarSolicitudDto productoLoteDto)
        {
            Respuesta<ProductoLoteAgregarDto> respuesta = _service.AgregarProductoLote(productoLoteDto);
            return Ok(respuesta);
        }

        [HttpGet("seleccionarlote/{idProducto}/{cantidad}")]
        public IActionResult SeleccionarInventario(int idProducto, int cantidad)
        {
            Respuesta<List<ProductoLoteDto>> resultado = _service.SeleccionarInventario(idProducto, cantidad);
            return Ok(resultado);
        }

        [HttpPut("{idProductoLote}/{cantidadRequerida}")]
        public IActionResult ActualizarInventario(int idProductoLote, int cantidadRequerida)
        {
            Respuesta<List<ProductoLoteDto>> respuesta = _service.ActualizarInventario(idProductoLote, cantidadRequerida);
            return Ok(respuesta);

        }
    }
}
