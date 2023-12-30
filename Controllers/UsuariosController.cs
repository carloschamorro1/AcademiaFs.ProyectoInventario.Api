using AcademiaFs.ProyectoInventario.Api._Features.Usuarios;
using AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Dtos;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFs.ProyectoInventario.Api.Controllers
{
    [Authorize]
    [Route("login")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuariosController(UsuarioService service)
        {
            _service = service;
        }
        [HttpPost("iniciarsesion")]
        public IActionResult IniciarSesion(CredencialesDto credencialesDto)
        {
            Respuesta<UsuarioLogueadoDto> resultado = _service.Login(credencialesDto);
            return Ok(resultado);
        }

        [HttpPost("agregarusuario")]
        public IActionResult AgregarUsuario(AgregarUsuarioDto usuario)
        {
             Respuesta<AgregarUsuarioCreadoDto> respuesta = _service.AgregarUsuario(usuario);
             return Ok(respuesta);   
        }
    }
}
