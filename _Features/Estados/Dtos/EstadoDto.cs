namespace AcademiaFs.ProyectoInventario.Api._Features.Estados.Dtos
{
    public class EstadoDto
    {
        public int EstadoId { get; set; }

        public string? Nombre { get; set; }

        public int UsuarioCreacionId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioModificacionId { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public bool Activo { get; set; }
    }
}
