using System;
using System.Collections.Generic;
using AcademiaFs.ProyectoInventario.Api._Features.Estados.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Sucursales.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Entities;

namespace AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Entities;

public class SalidaInventario
{
    public int SalidaInventarioId { get; set; }

    public int SucursalId { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime FechaSalida { get; set; }

    public decimal Total { get; set; }

    public DateTime? FechaRecibido { get; set; }

    public int? EmpleadoIdrecibe { get; set; }

    public int EstadoId { get; set; }

    public int UsuarioCreacionId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool Activo { get; set; }

    public virtual Estado? Estado { get; set; }

    public virtual ICollection<SalidaInventarioDetalle> SalidasInventarioDetalles { get; set; } = new List<SalidaInventarioDetalle>();

    public virtual Sucursal? Sucursal { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
