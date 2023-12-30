namespace AcademiaFs.ProyectoInventario.Api._Features.PerfilesPorPermiso.Dtos
{
    public class PerfilPorPermisoDto
    {
        public int PerfilId { get; set; }

        public int PermisoId { get; set; }

        public int UsuarioCreacionId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioModificacionId { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public bool Activo { get; set; }

    }
}
