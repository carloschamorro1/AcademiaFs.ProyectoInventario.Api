using AcademiaFs.ProyectoInventario.Api._Common.Constantes;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Entities;
using AcademiaFs.ProyectoInventario.Api.Infrastructure;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.AuthServer.Extensions.Services;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.Api._Features.Perfiles
{
    public class PerfilesService : IPerfilesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CurrentUser _currentUser;

        public PerfilesService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder, CurrentUser currentUser)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaInventario();
            _currentUser = currentUser;
        }

        public Respuesta<List<PerfilDto>> ObtenerPerfiles()
        {
            List<PerfilDto> perfiles = _unitOfWork.Repository<Perfil>().AsQueryable().Select(x => new PerfilDto
            {
               PerfilId = x.PerfilId,
               Nombre = x.Nombre,
               UsuarioCreacionId = x.UsuarioCreacionId,
               FechaCreacion = x.FechaCreacion,
               UsuarioModificacionId = x.UsuarioModificacionId,
               FechaModificacion = x.FechaModificacion,
               Activo = x.Activo
            }).Where(x => x.Activo == true).ToList();
            return Respuesta<List<PerfilDto>>.Success(perfiles, Mensajes.DATOS_OBTENIDOS_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<PerfilDto> DesactivarPerfil(int idPerfil)
        {
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            Perfil? perfilObtenido = _unitOfWork.Repository<Perfil>().FirstOrDefault(p => p.PerfilId == idPerfil);
            PerfilDto perfilDto = _mapper.Map<PerfilDto>(perfilObtenido);
            if (perfilObtenido == null)
            {
                return Respuesta<PerfilDto>.Fault(Mensajes.PERFIL_NO_EXISTE, MensajesHttp.CODIGO400, perfilDto);
            }
            perfilObtenido.Activo = false;
            perfilObtenido.FechaModificacion = DateTime.Now;
            perfilObtenido.UsuarioModificacionId = currentUserId;
            perfilDto.PerfilId = idPerfil;
            perfilDto.Activo = false;
            perfilDto.FechaModificacion = perfilObtenido.FechaModificacion;
            perfilDto.UsuarioModificacionId = perfilObtenido.UsuarioModificacionId;
            _unitOfWork.SaveChanges();
            return Respuesta<PerfilDto>.Success(perfilDto, Mensajes.PERFIL_DESACTIVADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<PerfilDto> ObtenerPerfilPorId(int idPerfil)
        {
            Perfil? perfilObtenido = _unitOfWork.Repository<Perfil>().FirstOrDefault(p => p.PerfilId == idPerfil);

            PerfilDto perfilDto = _mapper.Map<PerfilDto>(perfilObtenido);
            if (perfilObtenido == null)
            {
                return Respuesta<PerfilDto>.Fault(Mensajes.PERFIL_NO_EXISTE, MensajesHttp.CODIGO400, perfilDto);
            }
            perfilDto.PerfilId = perfilObtenido.PerfilId;
            return Respuesta<PerfilDto>.Success(perfilDto, Mensajes.PERFIL_OBTENIDO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<PerfilAgregarDto> AgregarPerfil(PerfilAgregarSolicitudDto perfilDto)
        {
            PerfilAgregarDto perfilAgregarRespuesta = _mapper.Map<PerfilAgregarDto>(perfilDto);
            Perfil nuevoPerfil = _mapper.Map<Perfil>(perfilDto);
            if (PerfilExiste(nuevoPerfil))
            {
                return Respuesta<PerfilAgregarDto>.Fault(Mensajes.PERFIL_YA_EXISTE, MensajesHttp.CODIGO400, perfilAgregarRespuesta);
            }
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            nuevoPerfil.UsuarioCreacionId = currentUserId;
            nuevoPerfil.FechaCreacion = DateTime.Now;
            nuevoPerfil.Activo = true;
            _unitOfWork.Repository<Perfil>().Add(nuevoPerfil);
            _unitOfWork.SaveChanges();
            perfilAgregarRespuesta = _mapper.Map<PerfilAgregarDto>(nuevoPerfil);
            return Respuesta<PerfilAgregarDto>.Success(perfilAgregarRespuesta, Mensajes.PERFIL_AGREGADO_EXITOSAMENTE, MensajesHttp.CODIGO201);
        }

        public Respuesta<PerfilActualizarDto> ActualizarPerfil(PerfilActualizarSolicitudDto perfilDto, int idPerfil)
        {
            PerfilActualizarDto perfilActualizarRespuesta = _mapper.Map<PerfilActualizarDto>(perfilDto);
            if (!IdPerfilExiste(idPerfil))
            {
                return Respuesta<PerfilActualizarDto>.Fault(Mensajes.PERFIL_NO_EXISTE, MensajesHttp.CODIGO400, perfilActualizarRespuesta);
            }
            Perfil perfil = _mapper.Map<Perfil>(perfilDto);
            if (PerfilExiste(perfil))
            {
                return Respuesta<PerfilActualizarDto>.Fault(Mensajes.PERFIL_YA_EXISTE, MensajesHttp.CODIGO400, perfilActualizarRespuesta);
            }
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            Perfil? perfilActual = _unitOfWork.Repository<Perfil>().FirstOrDefault(p => p.PerfilId == idPerfil);
            if (perfilActual == null)
            {
                return Respuesta<PerfilActualizarDto>.Fault(Mensajes.PERFIL_NO_EXISTE, MensajesHttp.CODIGO400, perfilActualizarRespuesta);
            }
            perfilActual.Nombre = perfilDto.Nombre;
            perfilActual.UsuarioModificacionId = currentUserId;
            perfilActual.FechaModificacion = DateTime.Now;
            _unitOfWork.SaveChanges();
            perfilActualizarRespuesta = _mapper.Map<PerfilActualizarDto>(perfilActual);
            return Respuesta<PerfilActualizarDto>.Success(perfilActualizarRespuesta, Mensajes.PERFIL_ACTUALIZADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public bool IdPerfilExiste(int idPerfil) => _unitOfWork.Repository<Perfil>().AsQueryable().Any(p => p.PerfilId == idPerfil);
        public bool PerfilExiste(Perfil perfil) => _unitOfWork.Repository<Perfil>().AsQueryable().Any(p => p.PerfilId == perfil.PerfilId);
    }
}
