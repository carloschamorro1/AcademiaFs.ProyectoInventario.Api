using AcademiaFs.ProyectoInventario.Api._Features.Empleados;
using AcademiaFs.ProyectoInventario.Api._Features.Empleados.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Dtos;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFs.ProyectoInventario.Api.Controllers
{
    [Authorize]
    [Route("perfiles")]
    [ApiController]
    public class PerfilesController : ControllerBase
    {
        private readonly IPerfilesService _service;
        public PerfilesController(IPerfilesService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public IActionResult ObtenerPerfiles()
        {
            Respuesta<List<PerfilDto>> resultado = _service.ObtenerPerfiles();
            return Ok(resultado);
        }

        [HttpGet("{idPerfil}")]
        public IActionResult ObtenerPerfilPorId(int idPerfil)
        {
            Respuesta<PerfilDto> resultado = _service.ObtenerPerfilPorId(idPerfil);
            return Ok(resultado);
        }

        [HttpPatch("{idPerfil}")]
        public IActionResult DesactivarPerfil(int idPerfil)
        {
            Respuesta<PerfilDto> respuesta = _service.DesactivarPerfil(idPerfil);
            return Ok(respuesta);
        }


        [HttpPut("{idPerfil}")]
        public IActionResult ActualizarPerfil(PerfilActualizarSolicitudDto perfilDto, int idPerfil)
        {
            Respuesta<PerfilActualizarDto> respuesta = _service.ActualizarPerfil(perfilDto, idPerfil);
            return Ok(respuesta);
        }

        [HttpPost("")]
        public IActionResult AgregarPerfil(PerfilAgregarSolicitudDto perfilDto)
        {
            Respuesta<PerfilAgregarDto> respuesta = _service.AgregarPerfil(perfilDto);
            return Ok(respuesta);
        }
    }
}
