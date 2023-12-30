using AcademiaFs.ProyectoInventario.Api._Features.Estados;
using AcademiaFs.ProyectoInventario.Api._Features.Estados.Dtos;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFs.ProyectoInventario.Api.Controllers
{
    [Authorize]
    [Route("estados")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly IEstadosService _service;

        public EstadosController(IEstadosService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public IActionResult obtenerEstados()
        {
            Respuesta<List<EstadoDto>> resultado = _service.ObtenerEstado();
            return Ok(resultado);
        }

        [HttpGet("{idEstado}")]
        public IActionResult obtenerEstadoPorId(int idEstado)
        {
            Respuesta<EstadoDto> resultado = _service.ObtenerEstadoPorId(idEstado);
            return Ok(resultado);
        }

        [HttpPatch("desactivar/{idEstado}")]
        public IActionResult desactivarEstado(int idEstado)
        {
            Respuesta<EstadoDto> respuesta = _service.DesactivarEstado(idEstado);
            return Ok(respuesta);
        }

        [HttpPut("{idEstado}")]
        public IActionResult actualizarEstado(EstadoActualizarSolicitudDto estado, int idEstado)
        {
            Respuesta<EstadoActualizarDto> respuesta = _service.ActualizarEstado(estado, idEstado);
            return Ok(respuesta);
        }

        [HttpPost("")]
        public IActionResult agregarEstado(EstadoAgregarSolicitudDto estado)
        {
            Respuesta<EstadoAgregarDto> respuesta = _service.AgregarEstado(estado);
            return Ok(respuesta);
        }
    }

    
}
