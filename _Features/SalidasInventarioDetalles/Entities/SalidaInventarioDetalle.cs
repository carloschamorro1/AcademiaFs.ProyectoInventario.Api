using System;
using System.Collections.Generic;
using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Entities;

namespace AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Entities;

public class SalidaInventarioDetalle
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

    public virtual ProductoLote? Lote { get; set; }

    public virtual SalidaInventario? SalidaInventario { get; set; }
}
