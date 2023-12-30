using AcademiaFs.ProyectoInventario.Api._Common.Constantes;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Permisos.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Permisos.Entities;
using AcademiaFs.ProyectoInventario.Api.Infrastructure;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.AuthServer.Extensions.Services;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.Api._Features.Permisos
{
    public class PermisosService : IPermisosService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CurrentUser _currentUser;

        public PermisosService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder, CurrentUser currentUser)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaInventario();
            _currentUser = currentUser;
        }

        public Respuesta<List<PermisoDto>> ObtenerPermisos()
        {
            List<PermisoDto> permisos = _unitOfWork.Repository<Permiso>().AsQueryable().Select(x => new PermisoDto
            {
                Nombre = x.Nombre,
                Activo = x.Activo,
                FechaCreacion = x.FechaCreacion,
                FechaModificacion = x.FechaModificacion,
                PermisosId = x.PermisosId,
                UsuarioCreacionId = x.UsuarioCreacionId,
                UsuarioModificacionId = x.UsuarioModificacionId
            }).Where(x => x.Activo == true).ToList();
            return Respuesta<List<PermisoDto>>.Success(permisos, Mensajes.DATOS_OBTENIDOS_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<PermisoDto> DesactivarPermiso(int idPermiso)
        {
            Permiso? permisoObtenido = _unitOfWork.Repository<Permiso>().FirstOrDefault(p => p.PermisosId == idPermiso);
            PermisoDto permisoDto = _mapper.Map<PermisoDto>(permisoObtenido);
            if (permisoObtenido == null)
            {
                return Respuesta<PermisoDto>.Fault(Mensajes.PERMISO_NO_EXISTE, MensajesHttp.CODIGO400, permisoDto);
            }
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            permisoObtenido.Activo = false;
            permisoObtenido.FechaModificacion = DateTime.Now;
            permisoObtenido.UsuarioModificacionId = currentUserId;
            permisoDto.PermisosId = idPermiso;
            permisoDto.Activo = false;
            permisoDto.FechaModificacion = permisoObtenido.FechaModificacion;
            permisoDto.UsuarioModificacionId = permisoObtenido.UsuarioModificacionId;
            _unitOfWork.SaveChanges();
            return Respuesta<PermisoDto>.Success(permisoDto, Mensajes.PERFIL_DESACTIVADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }
        public Respuesta<PermisoDto> ObtenerPermisoPorId(int idPermiso)
        {
            Permiso? permisoObtenido = _unitOfWork.Repository<Permiso>().FirstOrDefault(p => p.PermisosId == idPermiso);
            PermisoDto perfilDto = _mapper.Map<PermisoDto>(permisoObtenido);
            if (permisoObtenido == null)
            {
                return Respuesta<PermisoDto>.Fault(Mensajes.PERMISO_NO_EXISTE, MensajesHttp.CODIGO400, perfilDto);
            }
            perfilDto.PermisosId = permisoObtenido.PermisosId;
            return Respuesta<PermisoDto>.Success(perfilDto, Mensajes.PERMISO_OBTENIDO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<PermisoAgregarDto> AgregarPermiso(PermisoAgregarSolicitudDto permisoDto)
        {
            PermisoAgregarDto permisoAgregarRespuesta = _mapper.Map<PermisoAgregarDto>(permisoDto);
            Permiso nuevoPermiso = _mapper.Map<Permiso>(permisoDto);
            if (PermisoExiste(nuevoPermiso))
            {
                return Respuesta<PermisoAgregarDto>.Fault(Mensajes.PERMISO_YA_EXISTE, MensajesHttp.CODIGO400, permisoAgregarRespuesta);
            }
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            nuevoPermiso.UsuarioCreacionId = currentUserId;
            nuevoPermiso.FechaCreacion = DateTime.Now;
            nuevoPermiso.Activo = true;
            _unitOfWork.Repository<Permiso>().Add(nuevoPermiso);
            _unitOfWork.SaveChanges();
            permisoAgregarRespuesta = _mapper.Map<PermisoAgregarDto>(nuevoPermiso);
            return Respuesta<PermisoAgregarDto>.Success(permisoAgregarRespuesta, Mensajes.PERMISO_AGREGADO_EXITOSAMENTE, MensajesHttp.CODIGO201);
        }

        public Respuesta<PermisoActualizarDto> ActualizarPermiso(PermisoActualizarSolicitudDto permisoDto, int idPermiso)
        {
            PermisoActualizarDto permisoActualizarRespuesta = _mapper.Map<PermisoActualizarDto>(permisoDto);
            if (!IdPermisoExiste(idPermiso))
            {
                return Respuesta<PermisoActualizarDto>.Fault(Mensajes.PERMISO_NO_EXISTE, MensajesHttp.CODIGO400, permisoActualizarRespuesta);
            }
            Permiso permiso = _mapper.Map<Permiso>(permisoDto);
            if (PermisoExiste(permiso))
            {
                return Respuesta<PermisoActualizarDto>.Fault(Mensajes.PERMISO_YA_EXISTE, MensajesHttp.CODIGO400, permisoActualizarRespuesta);
            }
            Permiso? permisoActual = _unitOfWork.Repository<Permiso>().FirstOrDefault(p => p.PermisosId == idPermiso);
            if (permisoActual == null)
            {
                return Respuesta<PermisoActualizarDto>.Fault(Mensajes.PERMISO_NO_EXISTE, MensajesHttp.CODIGO400, permisoActualizarRespuesta);
            }
            permisoActual.Nombre = permisoDto.Nombre;
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            permisoActual.UsuarioModificacionId = currentUserId;
            permisoActual.FechaModificacion = DateTime.Now;
            _unitOfWork.SaveChanges();
            permisoActualizarRespuesta = _mapper.Map<PermisoActualizarDto>(permisoActual);
            return Respuesta<PermisoActualizarDto>.Success(permisoActualizarRespuesta, Mensajes.PERMISO_ACTUALIZADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public bool IdPermisoExiste(int idPermiso) => _unitOfWork.Repository<Permiso>().AsQueryable().Any(p => p.PermisosId == idPermiso);
        public bool PermisoExiste(Permiso permiso) => _unitOfWork.Repository<Permiso>().AsQueryable().Any(p => p.PermisosId == permiso.PermisosId);
    }
}
