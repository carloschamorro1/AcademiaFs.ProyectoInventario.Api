using System;
using System.Collections.Generic;
using AcademiaFs.ProyectoInventario.Api._Features.PerfilesPorPermiso.Entities;

namespace AcademiaFs.ProyectoInventario.Api._Features.Permisos.Entities;

public class Permiso
{
    public int PermisosId { get; set; }

    public string? Nombre { get; set; }

    public int UsuarioCreacionId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<PerfilPorPermiso> PerfilesPorPermisos { get; set; } = new List<PerfilPorPermiso>();
}
