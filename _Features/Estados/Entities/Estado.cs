using System;
using System.Collections.Generic;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Entities;

namespace AcademiaFs.ProyectoInventario.Api._Features.Estados.Entities;

public class Estado
{
    public int EstadoId { get; set; }

    public string? Nombre { get; set; }

    public int UsuarioCreacionId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<SalidaInventario> SalidasInventarios { get; set; } = new List<SalidaInventario>();
}
