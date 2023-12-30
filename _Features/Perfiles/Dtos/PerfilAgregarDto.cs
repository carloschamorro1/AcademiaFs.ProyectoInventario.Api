using System.Text.Json.Serialization;

namespace AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Dtos
{
    public class PerfilAgregarDto
    {

        public string? Nombre { get; set; }  
        public int UsuarioCreacionId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}
