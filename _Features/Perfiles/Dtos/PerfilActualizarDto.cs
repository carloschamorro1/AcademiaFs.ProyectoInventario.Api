using System.Text.Json.Serialization;

namespace AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Dtos
{
    public class PerfilActualizarDto
    {    
        public int PerfilId { get; set; }

        public string? Nombre { get; set; }
        public int? UsuarioModificacionId { get; set; }
        public DateTime? FechaModificacion { get; set; }

    }
}
