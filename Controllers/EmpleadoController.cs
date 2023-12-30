using AcademiaFs.ProyectoInventario.Api._Features.Empleados;
using AcademiaFs.ProyectoInventario.Api._Features.Empleados.Dtos;
using Farsiman.Application.Core.Standard.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFs.ProyectoInventario.Api.Controllers
{
    [Authorize]
    [Route("empleados")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _service;
        public EmpleadoController(IEmpleadoService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public IActionResult ObtenerEmpleados()
        {
            Respuesta<List<EmpleadoDto>> resultado = _service.ObtenerEmpleados();
            return Ok(resultado);
        }

        [HttpGet("{numeroIdentidad}")]
        public IActionResult ObtenerEmpleadoPorNumeroIdentidad(string numeroIdentidad)
        {
            Respuesta<EmpleadoDto> resultado = _service.ObtenerEmpleadoPorNumeroIdentidad(numeroIdentidad);
            return Ok(resultado);
        }

        [HttpPatch("{idEmpleado}")]
        public IActionResult DesactivarEmpleado(int idEmpleado)
        {
            Respuesta<EmpleadoDto> respuesta = _service.DesactivarEmpleado(idEmpleado);
            return Ok(respuesta);
        }


        [HttpPut("{idEmpleado}")]
        public IActionResult ActualizarEmpleado(EmpleadoActualizarSolicitudDto empleadoDto, int idEmpleado)
        {
            Respuesta<EmpleadoActualizarDto> respuesta = _service.ActualizarEmpleado(empleadoDto, idEmpleado);
            return Ok(respuesta);
        }

        [HttpPost("")]
        public IActionResult AgregarEmpleado(EmpleadoAgregarSolicitudDto empleadoDto)
        {
            Respuesta<EmpleadoAgregarDto> respuesta = _service.AgregarEmpleado(empleadoDto);
            return Ok(respuesta);
        }
    }
    
}
