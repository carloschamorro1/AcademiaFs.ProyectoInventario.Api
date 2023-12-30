namespace AcademiaFs.ProyectoInventario.Api._Features.Productos.Dtos
{
    public class ProductoActualizarDto
    {
        public int ProductoId { get; set; }
        public string? Nombre { get; set; } 
        public int? UsuarioModificacionId { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool Activo { get; set; }
    }
}
