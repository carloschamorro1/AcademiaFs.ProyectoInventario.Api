using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFs.ProyectoInventario.Api.Controllers
{
    [Authorize]
    [Route("salidadetalle")]
    [ApiController]
    public class SalidaInventarioDetallesController : ControllerBase
    {
        private readonly ISalidaInventarioDetalleService _service;
        private readonly IMapper _mapper;

        public SalidaInventarioDetallesController(ISalidaInventarioDetalleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("seleccionarlote/{idProducto}/{cantidad}")]
        public IActionResult SeleccionarInventario(int idProducto, int cantidad)
        {
            var resultado = _service.SeleccionarInventario(idProducto, cantidad);
            return Ok(resultado);
        }

        [HttpPut("{idProductoLote}/{cantidadRequerida}")]
        public IActionResult actualizarInventario(int idProductoLote, int cantidadRequerida)
        {
            var respuesta = _service.ActualizarInventario(idProductoLote, cantidadRequerida);
            return Ok(respuesta);

        }

        [HttpPost("")]
        public IActionResult agregarDetalle(SalidaInventarioDetalleAgregarDto detalleSalidaInventario)
        {
            var respuesta = _service.AgregarDetalle(detalleSalidaInventario);
            return Ok(respuesta);
        }
    }
}
