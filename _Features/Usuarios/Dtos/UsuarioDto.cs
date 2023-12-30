namespace AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Dtos
{
    public class UsuarioDto
    {
        public int UsuarioId { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public byte[] Clave { get; set; } = null!;

        public int EmpleadoId { get; set; }

        public int? PerfilId { get; set; }

        public int UsuarioCreacionId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioModificacionId { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public bool Activo { get; set; }
    }
}
