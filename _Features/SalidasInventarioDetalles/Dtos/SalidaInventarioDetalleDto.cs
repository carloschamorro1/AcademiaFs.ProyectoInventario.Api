namespace AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Dtos
{
    public class SalidaInventarioDetalleDto
    {
        public int DetalleId { get; set; }

        public int? SalidaInventarioId { get; set; }

        public int? LoteId { get; set; }

        public int CantidadProducto { get; set; }

        public int UsuarioCreacionId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioModificacionId { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public bool Activo { get; set; }
    }
}
