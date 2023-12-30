using AcademiaFs.ProyectoInventario.Api._Common.Constantes;
using AcademiaFs.ProyectoInventario.Api._Features.Empleados.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Empleados.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Dtos;
using AcademiaFs.ProyectoInventario.Api.Infrastructure;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.AuthServer.Extensions.Services;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.Api._Features.Empleados
{
    public class EmpleadosService : IEmpleadoService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CurrentUser _currentUser;

        public EmpleadosService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder, CurrentUser currentUser)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaInventario();
            _currentUser = currentUser;
        }

        public Respuesta<List<EmpleadoDto>> ObtenerEmpleados()
        {
            List<EmpleadoDto> empleados = _unitOfWork.Repository<Empleado>().AsQueryable().Select(x => new EmpleadoDto
            {
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Direccion = x.Direccion,
                FechaCreacion = x.FechaCreacion,
                UsuarioCreacionId = x.UsuarioCreacionId,
                EmpleadoId = x.EmpleadoId,
                FechaModificacion = x.FechaModificacion,
                NumeroIdentidad = x.NumeroIdentidad,
                UsuarioModificacionId = x.UsuarioModificacionId,
                Activo = x.Activo
            }).Where(x => x.Activo == true).ToList();
            return Respuesta<List<EmpleadoDto>>.Success(empleados, Mensajes.DATOS_OBTENIDOS_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<EmpleadoDto> DesactivarEmpleado(int idEmpleado)
        {
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            Empleado? empleadoObtenido = _unitOfWork.Repository<Empleado>().FirstOrDefault(e => e.EmpleadoId == idEmpleado);
            EmpleadoDto empleadoDto = _mapper.Map<EmpleadoDto>(empleadoObtenido);
            if (empleadoObtenido == null)
            {
                return Respuesta<EmpleadoDto>.Fault(Mensajes.EMPLEADO_NO_EXISTE, MensajesHttp.CODIGO400, empleadoDto);
            }
            empleadoObtenido.Activo = false;
            empleadoObtenido.FechaModificacion = DateTime.Now;
            empleadoObtenido.UsuarioModificacionId = currentUserId;
            empleadoDto.EmpleadoId = idEmpleado;
            empleadoDto.Activo = false;
            empleadoDto.FechaModificacion = empleadoObtenido.FechaModificacion;
            empleadoDto.UsuarioModificacionId = empleadoObtenido.UsuarioModificacionId;
            _unitOfWork.SaveChanges();
            return Respuesta<EmpleadoDto>.Success(empleadoDto, Mensajes.EMPLEADO_DESACTIVADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public bool ContieneSoloNumeros(string numeroIdentidad) => numeroIdentidad.All(char.IsDigit);

        public Respuesta<EmpleadoDto> ObtenerEmpleadoPorNumeroIdentidad(string numeroIdentidad)
        {
            if(numeroIdentidad.Length != Limites.LIMITE_LONGITUD_NUMERO_IDENTIDAD)
            {
                return Respuesta<EmpleadoDto>.Fault(Mensajes.LONGITUD_NUMERO_IDENTIDAD_NO_PERMITIDA);
            }
            if (!ContieneSoloNumeros(numeroIdentidad))
            {
                return Respuesta<EmpleadoDto>.Fault(Mensajes.CARACTERES_NO_PERMITIDOS_EN_NUMERO_IDENTIDAD);
            }
            Empleado? empleadoObtenido = _unitOfWork.Repository<Empleado>().FirstOrDefault(e => e.NumeroIdentidad == numeroIdentidad);

            EmpleadoDto empleadoDto = _mapper.Map<EmpleadoDto>(empleadoObtenido);
            if (empleadoObtenido == null)
            {
                return Respuesta<EmpleadoDto>.Fault(Mensajes.EMPLEADO_NO_EXISTE, MensajesHttp.CODIGO400, empleadoDto);
            }
            empleadoDto.EmpleadoId = empleadoObtenido.EmpleadoId;
            return Respuesta<EmpleadoDto>.Success(empleadoDto, Mensajes.EMPLEADO_OBTENIDO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<EmpleadoAgregarDto> AgregarEmpleado(EmpleadoAgregarSolicitudDto empleadoDto)
        {
            EmpleadoAgregarDto empleadoAgregarRespuesta = _mapper.Map<EmpleadoAgregarDto>(empleadoDto);
            Empleado nuevoEmpleado = _mapper.Map<Empleado>(empleadoDto);
            if (EmpleadoExiste(nuevoEmpleado))
            {
                return Respuesta<EmpleadoAgregarDto>.Fault(Mensajes.EMPLEADO_YA_EXISTE, MensajesHttp.CODIGO400, empleadoAgregarRespuesta);
            }
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            nuevoEmpleado.UsuarioCreacionId = currentUserId;
            nuevoEmpleado.FechaCreacion = DateTime.Now;
            nuevoEmpleado.Activo = true;
            _unitOfWork.Repository<Empleado>().Add(nuevoEmpleado);
            _unitOfWork.SaveChanges();
            empleadoAgregarRespuesta = _mapper.Map<EmpleadoAgregarDto>(nuevoEmpleado);
            return Respuesta<EmpleadoAgregarDto>.Success(empleadoAgregarRespuesta, Mensajes.EMPLEADO_AGREGADO_EXITOSAMENTE, MensajesHttp.CODIGO201);
        }

        public Respuesta<EmpleadoActualizarDto> ActualizarEmpleado(EmpleadoActualizarSolicitudDto empleadoDto, int idEmpleado)
        {
            EmpleadoActualizarDto empleadoActualizarRespuesta = _mapper.Map<EmpleadoActualizarDto>(empleadoDto);
            if (!IdEmpleadoExiste(idEmpleado))
            {
                return Respuesta<EmpleadoActualizarDto>.Fault(Mensajes.EMPLEADO_NO_EXISTE, MensajesHttp.CODIGO400, empleadoActualizarRespuesta);
            }
            Empleado empleado = _mapper.Map<Empleado>(empleadoDto);
            if (EmpleadoExiste(empleado))
            {
                return Respuesta<EmpleadoActualizarDto>.Fault(Mensajes.EMPLEADO_YA_EXISTE, MensajesHttp.CODIGO400, empleadoActualizarRespuesta);
            }
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            Empleado? empleadoActual = _unitOfWork.Repository<Empleado>().FirstOrDefault(e => e.EmpleadoId == idEmpleado);
            if (empleadoActual == null)
            {
                return Respuesta<EmpleadoActualizarDto>.Fault(Mensajes.EMPLEADO_NO_EXISTE, MensajesHttp.CODIGO400, empleadoActualizarRespuesta);
            }
            empleadoActual.Nombre = empleadoDto.Nombre;
            empleadoActual.Apellido = empleadoDto.Apellido;
            empleadoActual.Direccion = empleadoDto.Direccion;
            empleadoActual.NumeroIdentidad = empleadoDto.NumeroIdentidad;
            empleadoActual.UsuarioModificacionId = currentUserId;
            empleadoActual.FechaModificacion = DateTime.Now;
            _unitOfWork.SaveChanges();
            empleadoActualizarRespuesta = _mapper.Map<EmpleadoActualizarDto>(empleadoActual);
            return Respuesta<EmpleadoActualizarDto>.Success(empleadoActualizarRespuesta, Mensajes.ESTADO_ACTUALIZADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }
        public bool IdEmpleadoExiste(int idEmpleado) => _unitOfWork.Repository<Empleado>().AsQueryable().Any(e => e.EmpleadoId == idEmpleado);
        public bool EmpleadoExiste(Empleado empleado) => _unitOfWork.Repository<Empleado>().AsQueryable().Any(e => e.EmpleadoId== empleado.EmpleadoId);
    }
}
