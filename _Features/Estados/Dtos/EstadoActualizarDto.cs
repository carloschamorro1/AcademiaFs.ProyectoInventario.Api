namespace AcademiaFs.ProyectoInventario.Api._Features.Estados.Dtos
{
    public class EstadoActualizarDto
    {
        public int EstadoId { get; set; }
        public string? Nombre { get; set; }

        public int? UsuarioModificacionId { get; set; }

        public DateTime? FechaModificacion { get; set; }

    }
}
