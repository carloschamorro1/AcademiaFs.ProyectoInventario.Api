using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Entities;
using System;
using System.Collections.Generic;

namespace AcademiaFs.ProyectoInventario.Api;

public class Producto
{
    public int ProductoId { get; set; }

    public string? Nombre { get; set; }

    public int UsuarioCreacionId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<ProductoLote> ProductosLotes { get; set; } = new List<ProductoLote>();
}
