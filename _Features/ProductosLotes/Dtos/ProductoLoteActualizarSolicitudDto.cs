namespace AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Dtos
{
    public class ProductoLoteActualizarSolicitudDto
    {
        public int LoteId { get; set; }
        public int? ProductoId { get; set; }
        public int CantidadInicial { get; set; }
        public decimal Costo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int Inventario { get; set; }
    }
}
