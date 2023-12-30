using AcademiaFs.ProyectoInventario.Api._Common.Constantes;
using AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Entities;
using AcademiaFs.ProyectoInventario.Api.Domain.Usuarios;
using AcademiaFs.ProyectoInventario.Api.Infrastructure;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.Api._Features.Usuarios
{
    public class UsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UsuariosDomain _usuariosDomain;

        public UsuarioService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder, UsuariosDomain usuariosDomain)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaInventario();
            _usuariosDomain = usuariosDomain;
        }

        public Respuesta<UsuarioLogueadoDto> Login(CredencialesDto credencialesDto)
        {
            string resultado = "";
            bool camposVacios = _usuariosDomain.EstaVacio(credencialesDto.nombreUsuario, credencialesDto.password);
            UsuarioLogueadoDto usuarioLogueadoDto = new UsuarioLogueadoDto()
            {
                NombreUsuario = credencialesDto.nombreUsuario
            };
            if (camposVacios)
            {
                return Respuesta<UsuarioLogueadoDto>.Fault(Mensajes.CAMPOS_VACIOS, MensajesHttp.CODIGO400, usuarioLogueadoDto);
            }

            Usuario? usuario = _unitOfWork.Repository<Usuario>().AsQueryable().FirstOrDefault(u => u.NombreUsuario == credencialesDto.nombreUsuario);
            try
            {
                bool validacion = _usuariosDomain.ValidarPassword(usuario.Clave, credencialesDto.password);
                if(validacion)
                {
                    return Respuesta<UsuarioLogueadoDto>.Success(usuarioLogueadoDto, $"{Mensajes.LOGIN_EXITOSO} {credencialesDto.nombreUsuario}", MensajesHttp.CODIGO200);
                }
            }
            catch (Exception ex)
            {
                return Respuesta<UsuarioLogueadoDto>.Fault(MensajesHttp.CODIGO400, $"{Mensajes.EXCEPCION_HA_OCURRIDO} {ex.Message}", usuarioLogueadoDto);
            }
            return Respuesta<UsuarioLogueadoDto>.Fault(MensajesHttp.CODIGO400, Mensajes.CREDENCIALES_NO_COINCIDEN, usuarioLogueadoDto);
        }

        public Respuesta<AgregarUsuarioCreadoDto> AgregarUsuario(AgregarUsuarioDto usuario)
        {
            AgregarUsuarioCreadoDto usuarioCreadoDto = _mapper.Map<AgregarUsuarioCreadoDto>(usuario);
            byte[] encriptarClave = _usuariosDomain.ObtenerBytes(usuario.Clave);
            Usuario nuevoUsuario = new Usuario()
            {
                Activo = usuario.Activo,
                NombreUsuario = usuario.NombreUsuario,
                Clave = encriptarClave,
                EmpleadoId = usuario.EmpleadoId,
                FechaCreacion = usuario.FechaCreacion,
                UsuarioCreacionId = usuario.UsuarioCreacionId,
                PerfilId = usuario.PerfilId,   
            };
           
            if (UsuarioExiste(nuevoUsuario))
            {
                return Respuesta<AgregarUsuarioCreadoDto>.Fault(MensajesHttp.CODIGO400, Mensajes.USUARIO_YA_EXISTE, usuarioCreadoDto);
            }
            _unitOfWork.Repository<Usuario>().Add(nuevoUsuario);
            _unitOfWork.SaveChanges();
            return Respuesta<AgregarUsuarioCreadoDto>.Success(usuarioCreadoDto, Mensajes.USUARIO_AGREGADO_EXITOSAMENTE, MensajesHttp.CODIGO201);
        }

        public bool UsuarioExiste(Usuario usuario) => _unitOfWork.Repository<Usuario>().AsQueryable().Any(x => x.NombreUsuario == usuario.NombreUsuario);
   
    }
}
