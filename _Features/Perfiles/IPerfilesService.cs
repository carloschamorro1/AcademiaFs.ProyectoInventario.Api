using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Entities;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.Api._Features.Perfiles
{
    public interface IPerfilesService
    {
        public Respuesta<List<PerfilDto>> ObtenerPerfiles();
        public Respuesta<PerfilDto> DesactivarPerfil(int idPerfil);
        public Respuesta<PerfilDto> ObtenerPerfilPorId(int idPerfil);
        public Respuesta<PerfilAgregarDto> AgregarPerfil(PerfilAgregarSolicitudDto perfilDto);
        public Respuesta<PerfilActualizarDto> ActualizarPerfil(PerfilActualizarSolicitudDto perfilDto, int idPerfil);
        public bool IdPerfilExiste(int idPerfil);
        public bool PerfilExiste(Perfil perfil);
    }
}
