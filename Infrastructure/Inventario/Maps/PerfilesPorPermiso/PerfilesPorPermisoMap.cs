using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.PerfilesPorPermiso.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario.Maps.PerfilesPorPermiso
{
    public class PerfilesPorPermisoMap : IEntityTypeConfiguration<PerfilPorPermiso>
    {
        public void Configure(EntityTypeBuilder<PerfilPorPermiso> builder)
        {
            builder.ToTable("PerfilesPorPermisos");
            builder.HasKey(e => new { e.PerfilId, e.PermisoId }).HasName("PK__Perfiles__256E5716C6E56A5D");
            builder.Property(e => e.PerfilId).HasColumnName("PerfilID");
            builder.Property(e => e.PermisoId).HasColumnName("PermisoID");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");

            builder.HasOne(d => d.Perfil).WithMany(p => p.PerfilesPorPermisos)
                .HasForeignKey(d => d.PerfilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PerfilesxPermisos_Perfiles");

            builder.HasOne(d => d.Permiso).WithMany(p => p.PerfilesPorPermisos)
                .HasForeignKey(d => d.PermisoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PerfilesxPermisos_Permisos");
        }
    }
}
