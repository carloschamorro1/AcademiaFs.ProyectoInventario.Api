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

        

        [HttpPost("")]
        public IActionResult agregarDetalle(SalidaInventarioDetalleAgregarDto detalleSalidaInventario)
        {
            var respuesta = _service.AgregarDetalle(detalleSalidaInventario);
            return Ok(respuesta);
        }
    }
}
