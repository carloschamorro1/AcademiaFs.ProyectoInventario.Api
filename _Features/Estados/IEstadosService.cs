using AcademiaFs.ProyectoInventario.Api._Features.Estados.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Estados.Entities;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.Api._Features.Estados
{
    public interface IEstadosService
    {
        public Respuesta<List<EstadoDto>> ObtenerEstado();
        public Respuesta<EstadoDto> DesactivarEstado(int idEstado);
        public Respuesta<EstadoDto> ObtenerEstadoPorId(int idUsuario);
        public Respuesta<EstadoAgregarDto> AgregarEstado(EstadoAgregarSolicitudDto estadoAgregar);

        public Respuesta<EstadoActualizarDto> ActualizarEstado(EstadoActualizarSolicitudDto estadoActualizar, int idEstado);
        public bool IdEstadoExiste(int idEstado);
        public bool EstadoExiste(Estado estado);
    }
}
