using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.SalidasInventario
{
    public class SalidasInventarioMap : IEntityTypeConfiguration<SalidaInventario>
    {
        public void Configure(EntityTypeBuilder<SalidaInventario> builder)
        {
            builder.ToTable("SalidasInventario");
            builder.HasKey(e => e.SalidaInventarioId).HasName("PK__SalidasI__6F7AE0F90330CE89");
            builder.Property(e => e.SalidaInventarioId).HasColumnName("SalidaInventarioID");
            builder.Property(e => e.EmpleadoIdrecibe).HasColumnName("EmpleadoIDRecibe");
            builder.Property(e => e.EstadoId).HasColumnName("EstadoID");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.SucursalId).HasColumnName("SucursalID");
            builder.Property(e => e.Total).HasColumnType("decimal(10, 2)");
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");
            builder.HasOne(d => d.Estado).WithMany(p => p.SalidasInventarios)
                .HasForeignKey(d => d.EstadoId)
            .HasConstraintName("FK_Estado");
            builder.HasOne(d => d.Sucursal).WithMany(p => p.SalidasInventarios)
                .HasForeignKey(d => d.SucursalId)
                .HasConstraintName("FK__SalidasIn__Sucur__778AC167");
            builder.HasOne(d => d.Usuario).WithMany(p => p.SalidasInventarios)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__SalidasIn__Usuar__787EE5A0");
        }
    }
}
