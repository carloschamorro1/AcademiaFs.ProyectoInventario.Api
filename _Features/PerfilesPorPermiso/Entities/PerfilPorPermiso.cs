using System;
using System.Collections.Generic;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Permisos.Entities;

namespace AcademiaFs.ProyectoInventario.Api._Features.PerfilesPorPermiso.Entities;

public class PerfilPorPermiso
{
    public int PerfilId { get; set; }

    public int PermisoId { get; set; }

    public int UsuarioCreacionId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool Activo { get; set; }

    public virtual Perfil Perfil { get; set; } = null!;

    public virtual Permiso Permiso { get; set; } = null!;
}
