using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.ProductosLotes
{
    public class ProductosLotesMap : IEntityTypeConfiguration<ProductoLote>
    {
        public void Configure(EntityTypeBuilder<ProductoLote> builder)
        {
            builder.ToTable("ProductosLotes");
            builder.HasKey(e => e.LoteId).HasName("PK__Producto__E6EAE6F862A5C13A");
            builder.Property(e => e.LoteId).HasColumnName("LoteID");
            builder.Property(e => e.Costo).HasColumnType("decimal(10, 2)");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.ProductoId).HasColumnName("ProductoID");
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");
            builder.HasOne(d => d.Producto).WithMany(p => p.ProductosLotes)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__Productos__Produ__76969D2E");
        }
    }
}
