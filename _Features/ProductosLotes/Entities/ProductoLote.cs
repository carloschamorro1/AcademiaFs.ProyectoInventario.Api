
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Entities;

namespace AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Entities;

public class ProductoLote
{
    public int LoteId { get; set; }
    public int ProductoId { get; set; }
    public int CantidadInicial { get; set; }
    public decimal Costo { get; set; }
    public DateOnly FechaVencimiento { get; set; }
    public int Inventario { get; set; }
    public int UsuarioCreacionId { get; set; }
    public DateTime FechaCreacion { get; set; }
    public int? UsuarioModificacionId { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public bool Activo { get; set; }
    public virtual Producto? Producto { get; set; }
    public virtual ICollection<SalidaInventarioDetalle> SalidasInventarioDetalles { get; set; } = new List<SalidaInventarioDetalle>();
}
