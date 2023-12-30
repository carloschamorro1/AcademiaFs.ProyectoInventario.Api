using AcademiaFs.ProyectoInventario.Api._Features.Reportes;
using AcademiaFs.ProyectoInventario.Api._Features.Reportes.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFs.ProyectoInventario.Api.Controllers
{
    [Authorize]
    [Route("reportes")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IReportesService _service;

        public ReportesController(IReportesService service)
        {
            _service = service;
        }

        [HttpPost("obtenerporsucursal")]
        public IActionResult ObtenerReportePorSucursal(GenerarReporteDto generarReporte)
        {
            var respuesta = _service.obtenerSalidasPorSucursal(generarReporte);
            return Ok(respuesta);
        }

        [HttpPut("recepcion")]
        public IActionResult RegistrarRecepcionDeSalida(int idSalidaInventario)
        {
            var respuesta = _service.RegistrarRecepcionDeSalida(idSalidaInventario);
            return Ok(respuesta);
        }
    }
}
