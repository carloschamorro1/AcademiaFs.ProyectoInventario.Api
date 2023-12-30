using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.SalidasInventarioDetalles
{
    public class SalidasInventarioDetalleMap : IEntityTypeConfiguration<SalidaInventarioDetalle>
    {
        public void Configure(EntityTypeBuilder<SalidaInventarioDetalle> builder)
        {
            builder.ToTable("SalidasInventarioDetalle");
            builder.HasKey(e => e.DetalleId).HasName("PK__SalidasI__6E19D6FAD47CED83");
            builder.Property(e => e.DetalleId).HasColumnName("DetalleID");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.LoteId).HasColumnName("LoteID");
            builder.Property(e => e.SalidaInventarioId).HasColumnName("SalidaInventarioID");
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");
            builder.HasOne(d => d.Lote).WithMany(p => p.SalidasInventarioDetalles)
                .HasForeignKey(d => d.LoteId)
                .HasConstraintName("FK__SalidasIn__LoteI__7A672E12");
            builder.HasOne(d => d.SalidaInventario).WithMany(p => p.SalidasInventarioDetalles)
                .HasForeignKey(d => d.SalidaInventarioId)
                .HasConstraintName("FK__SalidasIn__Salid__7B5B524B");
        }
    }
}
