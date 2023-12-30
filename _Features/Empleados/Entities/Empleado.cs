using System;
using System.Collections.Generic;
using AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Entities;

namespace AcademiaFs.ProyectoInventario.Api._Features.Empleados.Entities;

public class Empleado
{
    public int EmpleadoId { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Direccion { get; set; }

    public int UsuarioCreacionId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool Activo { get; set; }

    public string NumeroIdentidad { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
