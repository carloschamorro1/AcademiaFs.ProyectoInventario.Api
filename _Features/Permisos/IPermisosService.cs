using AcademiaFs.ProyectoInventario.Api._Features.Permisos.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Permisos.Entities;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.Api._Features.Permisos
{
    public interface IPermisosService
    {
        public Respuesta<List<PermisoDto>> ObtenerPermisos();
        public Respuesta<PermisoDto> DesactivarPermiso(int idPermiso);
        public Respuesta<PermisoDto> ObtenerPermisoPorId(int idPermiso);
        public Respuesta<PermisoAgregarDto> AgregarPermiso(PermisoAgregarSolicitudDto permisoDto);
        public Respuesta<PermisoActualizarDto> ActualizarPermiso(PermisoActualizarSolicitudDto permisoDto, int idPermiso);
        public bool IdPermisoExiste(int idPermiso);
        public bool PermisoExiste(Permiso permiso);
    }
}
