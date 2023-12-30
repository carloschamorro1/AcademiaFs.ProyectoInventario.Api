using System;
using System.Collections.Generic;
using AcademiaFs.ProyectoInventario.Api._Features.Empleados.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Entities;

namespace AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Entities;

public class Usuario
{
    public int UsuarioId { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public byte[] Clave { get; set; } = null!;

    public int EmpleadoId { get; set; }

    public int? PerfilId { get; set; }

    public int UsuarioCreacionId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool Activo { get; set; }

    public int? UsuarioIdentidadFarsiman { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual Perfil? Perfil { get; set; }

    public virtual ICollection<SalidaInventario> SalidasInventarios { get; set; } = new List<SalidaInventario>();
}
