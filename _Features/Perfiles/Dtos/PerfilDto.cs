namespace AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Dtos
{
    public class PerfilDto
    {
        public int PerfilId { get; set; }

        public string? Nombre { get; set; }

        public int UsuarioCreacionId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioModificacionId { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public bool Activo { get; set; }
    }
}
