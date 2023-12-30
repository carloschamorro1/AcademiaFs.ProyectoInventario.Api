using AcademiaFs.ProyectoInventario.Api._Features.Permisos;
using AcademiaFs.ProyectoInventario.Api._Features.Permisos.Dtos;
using Farsiman.Application.Core.Standard.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFs.ProyectoInventario.Api.Controllers
{
    [Authorize]
    [Route("permisos")]
    [ApiController]
    public class PermisosController : ControllerBase
    {
        private readonly IPermisosService _service;
        public PermisosController(IPermisosService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public IActionResult ObtenerPermisos()
        {
            Respuesta<List<PermisoDto>> resultado = _service.ObtenerPermisos();
            return Ok(resultado);
        }

        [HttpGet("{idPermiso}")]
        public IActionResult ObtenerPermisoPorId(int idPermiso)
        {
            Respuesta<PermisoDto> resultado = _service.ObtenerPermisoPorId(idPermiso);
            return Ok(resultado);
        }

        [HttpPatch("{idPermiso}")]
        public IActionResult DesactivarPermiso(int idPermiso)
        {
            Respuesta<PermisoDto> respuesta = _service.DesactivarPermiso(idPermiso);
            return Ok(respuesta);
        }

        [HttpPut("{idPermiso}")]
        public IActionResult ActualizarPermiso(PermisoActualizarSolicitudDto permisoDto, int idPermiso)
        {
            Respuesta<PermisoActualizarDto> respuesta = _service.ActualizarPermiso(permisoDto, idPermiso);
            return Ok(respuesta);
        }

        [HttpPost("")]
        public IActionResult AgregarPermiso(PermisoAgregarSolicitudDto permisoDto)
        {
            Respuesta<PermisoAgregarDto> respuesta = _service.AgregarPermiso(permisoDto);
            return Ok(respuesta);
        }

    }
}
