using AcademiaFs.ProyectoInventario.Api._Features.Empleados.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Empleados.Entities;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.Api._Features.Empleados
{
    public interface IEmpleadoService
    {
        public Respuesta<List<EmpleadoDto>> ObtenerEmpleados();
        public Respuesta<EmpleadoDto> DesactivarEmpleado(int idEmpleado);
        public Respuesta<EmpleadoDto> ObtenerEmpleadoPorNumeroIdentidad(string numeroIdentidad);
        public Respuesta<EmpleadoAgregarDto> AgregarEmpleado(EmpleadoAgregarSolicitudDto empleadoDto);
        public Respuesta<EmpleadoActualizarDto> ActualizarEmpleado(EmpleadoActualizarSolicitudDto empleadoDto, int idEmpleado);
        public bool IdEmpleadoExiste(int idEmpleado);
        public bool EmpleadoExiste(Empleado empleado);
        public bool ContieneSoloNumeros(string numeroIdentidad);
    }
}
