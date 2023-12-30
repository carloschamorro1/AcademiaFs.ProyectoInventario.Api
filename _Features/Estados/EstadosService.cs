using AcademiaFs.ProyectoInventario.Api._Common.Constantes;
using AcademiaFs.ProyectoInventario.Api._Features.Empleados.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Estados.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Estados.Entities;
using AcademiaFs.ProyectoInventario.Api.Domain.SalidasInventario;
using AcademiaFs.ProyectoInventario.Api.Infrastructure;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.AuthServer.Extensions.Services;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.Api._Features.Estados
{
    public class EstadosService : IEstadosService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CurrentUser _currentUser;
        public EstadosService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder, CurrentUser currentUser)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaInventario();
            _currentUser = currentUser; 
        }

        public Respuesta<List<EstadoDto>> ObtenerEstado()
        {
            List<EstadoDto> estados = _unitOfWork.Repository<Estado>().AsQueryable().Select(x => new EstadoDto
            {
                EstadoId = x.EstadoId,
                Nombre = x.Nombre,
                UsuarioCreacionId = x.UsuarioCreacionId,
                FechaCreacion = x.FechaCreacion,
                UsuarioModificacionId = x.UsuarioModificacionId,
                FechaModificacion = x.FechaModificacion,
                Activo = x.Activo
            }).Where(x => x.Activo == true).ToList();
            return Respuesta<List<EstadoDto>>.Success(estados, Mensajes.DATOS_OBTENIDOS_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<EstadoDto> DesactivarEstado(int idEstado)
        {
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            Estado? estadoObtenido = _unitOfWork.Repository<Estado>().FirstOrDefault(e => e.EstadoId == idEstado);
            EstadoDto map = _mapper.Map<EstadoDto>(estadoObtenido);
            if (estadoObtenido == null)
            {
                return Respuesta<EstadoDto>.Fault(Mensajes.ESTADO_NO_EXISTE, MensajesHttp.CODIGO400, map);
            }
            estadoObtenido.Activo = false;
            estadoObtenido.FechaModificacion = DateTime.Now;
            estadoObtenido.UsuarioModificacionId = currentUserId;
            map.EstadoId = idEstado;
            map.Activo = false;
            map.FechaModificacion = estadoObtenido.FechaModificacion;
            map.UsuarioModificacionId = estadoObtenido.UsuarioModificacionId;
            _unitOfWork.SaveChanges();
            return Respuesta<EstadoDto>.Success(map, Mensajes.ESTADO_DESACTIVADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<EstadoDto> ObtenerEstadoPorId(int idEstado)
        {
            Estado? estadoObtenido = _unitOfWork.Repository<Estado>().FirstOrDefault(e => e.EstadoId == idEstado);

            EstadoDto map = _mapper.Map<EstadoDto>(estadoObtenido);
            if (estadoObtenido == null)
            {
                return Respuesta<EstadoDto>.Fault(Mensajes.ESTADO_NO_EXISTE, MensajesHttp.CODIGO400, map);
            }
            map.EstadoId = idEstado;
            return Respuesta<EstadoDto>.Success(map, Mensajes.ESTADO_OBTENIDO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<EstadoAgregarDto> AgregarEstado(EstadoAgregarSolicitudDto estadoAgregar)
        {
            EstadoAgregarDto estadoAgregarRespuesta = _mapper.Map<EstadoAgregarDto>(estadoAgregar);
            Estado nuevoEstado = _mapper.Map<Estado>(estadoAgregar);
            if (EstadoExiste(nuevoEstado))
            {
                return Respuesta<EstadoAgregarDto>.Fault(Mensajes.ESTADO_YA_EXISTE, MensajesHttp.CODIGO400, estadoAgregarRespuesta);
            }
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            nuevoEstado.UsuarioCreacionId = currentUserId;
            nuevoEstado.FechaCreacion = DateTime.Now;
            nuevoEstado.Activo = true;
            _unitOfWork.Repository<Estado>().Add(nuevoEstado);
            _unitOfWork.SaveChanges();
            estadoAgregarRespuesta = _mapper.Map<EstadoAgregarDto>(nuevoEstado);
            return Respuesta<EstadoAgregarDto>.Success(estadoAgregarRespuesta, Mensajes.ESTADO_AGREGADO_EXITOSAMENTE, MensajesHttp.CODIGO201);
        }

        public Respuesta<EstadoActualizarDto> ActualizarEstado(EstadoActualizarSolicitudDto estadoActualizar, int idEstado)
        {
            EstadoActualizarDto estadoActualizarRespuesta = _mapper.Map<EstadoActualizarDto>(estadoActualizar);
            if (!IdEstadoExiste(idEstado))
            {
                return Respuesta<EstadoActualizarDto>.Fault(Mensajes.ESTADO_NO_EXISTE, MensajesHttp.CODIGO400, estadoActualizarRespuesta);
            }
            Estado? estado = _mapper.Map<Estado>(estadoActualizar);
            if (EstadoExiste(estado))
            {
                return Respuesta<EstadoActualizarDto>.Fault(Mensajes.ESTADO_YA_EXISTE, MensajesHttp.CODIGO400, estadoActualizarRespuesta);
            }
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            Estado? estadoActual = _unitOfWork.Repository<Estado>().FirstOrDefault(e => e.EstadoId == idEstado);
            if (estadoActual == null)
            {
                return Respuesta<EstadoActualizarDto>.Fault(Mensajes.ESTADO_NO_EXISTE, MensajesHttp.CODIGO400, estadoActualizarRespuesta);
            }
            estadoActual.Nombre = estadoActualizar.Nombre;
            estadoActual.UsuarioModificacionId = currentUserId;
            estadoActual.FechaModificacion = DateTime.Now;
            _unitOfWork.SaveChanges();
            estadoActualizarRespuesta = _mapper.Map<EstadoActualizarDto>(estadoActual);
            return Respuesta<EstadoActualizarDto>.Success(estadoActualizarRespuesta, Mensajes.ESTADO_ACTUALIZADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public bool IdEstadoExiste(int idEstado) => _unitOfWork.Repository<Estado>().AsQueryable().Any(e => e.EstadoId == idEstado);
        public bool EstadoExiste(Estado estado) => _unitOfWork.Repository<Estado>().AsQueryable().Any(e => e.Nombre == estado.Nombre);

    }
}
