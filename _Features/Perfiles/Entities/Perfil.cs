using System;
using System.Collections.Generic;
using AcademiaFs.ProyectoInventario.Api._Features.PerfilesPorPermiso.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Entities;

namespace AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Entities;

public class Perfil
{
    public int PerfilId { get; set; }

    public string? Nombre { get; set; }

    public int UsuarioCreacionId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<PerfilPorPermiso> PerfilesPorPermisos { get; set; } = new List<PerfilPorPermiso>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
