using System.Text.Json.Serialization;

namespace AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Dtos
{
    public class AgregarUsuarioDto
    {
        [JsonIgnore]
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = null!;

        public string Clave { get; set; } = null!;

        public int EmpleadoId { get; set; }

        public int? PerfilId { get; set; }

        public int UsuarioCreacionId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; }
    }
}
