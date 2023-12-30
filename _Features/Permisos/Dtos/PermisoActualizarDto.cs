namespace AcademiaFs.ProyectoInventario.Api._Features.Permisos.Dtos
{
    public class PermisoActualizarDto
    {
        public int PermisosId { get; set; }
        public string? Nombre { get; set; }
        public int? UsuarioModificacionId { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool Activo { get; set; }
    }
}
