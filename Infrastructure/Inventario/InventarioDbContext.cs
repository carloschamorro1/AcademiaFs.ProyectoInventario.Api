using System;
using System.Collections.Generic;
using AcademiaFs.ProyectoInventario.Api._Features.Empleados.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Estados.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.PerfilesPorPermiso.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Permisos.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Sucursales.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Entities;
using AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.Empleados;
using AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.Estados;
using AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.Perfiles;
using AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.PerfilesPorPermiso;
using AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.Permisos;
using AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.Productos;
using AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.ProductosLotes;
using AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.SalidasInventario;
using AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.SalidasInventarioDetalles;
using AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.Sucursales;
using AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario;

public class InventarioDbContext : DbContext
{

    public InventarioDbContext(DbContextOptions<InventarioDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Perfil> Perfiles { get; set; }

    public virtual DbSet<PerfilPorPermiso> PerfilesPorPermisos { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoLote> ProductosLotes { get; set; }

    public virtual DbSet<SalidaInventario> SalidasInventarios { get; set; }

    public virtual DbSet<SalidaInventarioDetalle> SalidasInventarioDetalles { get; set; }

    public virtual DbSet<Sucursal> Sucursales { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmpleadosMap());
        modelBuilder.ApplyConfiguration(new EstadosMap());
        modelBuilder.ApplyConfiguration(new PerfilesMap());
        modelBuilder.ApplyConfiguration(new PerfilesPorPermisoMap());
        modelBuilder.ApplyConfiguration(new PermisosMap());
        modelBuilder.ApplyConfiguration(new ProductosMap());
        modelBuilder.ApplyConfiguration(new ProductosLotesMap());
        modelBuilder.ApplyConfiguration(new SalidasInventarioMap());
        modelBuilder.ApplyConfiguration(new SalidasInventarioDetalleMap());
        modelBuilder.ApplyConfiguration(new SucursalesMap());
        modelBuilder.ApplyConfiguration(new UsuariosMap());
    }

}
