using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFs.ProyectoInventario.Api.Controllers
{
    [Authorize]
    [Route("salidainventario")]
    [ApiController]
    public class SalidaInventarioController : ControllerBase
    {
        private readonly ISalidaInventarioService _service;

        public SalidaInventarioController(ISalidaInventarioService service)
        {
            _service = service;
        }

        [HttpPost("{idUsuario}")]
        public IActionResult agregarSalidaInventario(SalidaDto salidaInventario, int idUsuario)
        {
             var respuesta = _service.AgregarSalidaInventario(salidaInventario, idUsuario);
             return Ok(respuesta);
        }


    }
}
