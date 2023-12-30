namespace AcademiaFs.ProyectoInventario.Api._Features.Estados.Dtos
{
    public class EstadoAgregarDto
    {
        public string? Nombre { get; set; }

        public int UsuarioCreacionId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; }
    }
}
