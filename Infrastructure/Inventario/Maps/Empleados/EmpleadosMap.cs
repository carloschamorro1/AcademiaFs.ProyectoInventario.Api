using AcademiaFs.ProyectoInventario.Api._Features.Empleados.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.Empleados
{
    public class EmpleadosMap : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.ToTable("Empleados");
            builder.HasKey(e => e.EmpleadoId).HasName("PK__Empleado__958BE6F0EA7FE150");

            builder.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            builder.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");
            builder.Property(e => e.NumeroIdentidad).HasColumnName("NumeroIdentidad").HasMaxLength(13);
        }
    }
}
