using AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.Usuarios
{
    public class UsuariosMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7985515586E");
            builder.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            builder.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.PerfilId).HasColumnName("PerfilID");
            builder.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NombreUsuario");
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");
            builder.HasOne(d => d.Empleado).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__Emplea__7C4F7684");
            builder.HasOne(d => d.Perfil).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.PerfilId)
                .HasConstraintName("FK__Usuarios__Perfil__7D439ABD");
            builder.Property(e => e.UsuarioIdentidadFarsiman).HasColumnName("UsuarioIdentidadFarsiman");
        }
    }
}
