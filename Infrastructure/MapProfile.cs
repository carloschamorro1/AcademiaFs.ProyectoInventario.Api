using AcademiaFs.ProyectoInventario.Api._Features.Empleados.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Empleados.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Estados.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Estados.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.PerfilesPorPermiso.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.PerfilesPorPermiso.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Permisos.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Permisos.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Productos.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Sucursales.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Sucursales.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Entities;
using AutoMapper;

namespace AcademiaFs.ProyectoInventario.Api.Infrastructure
{
    public class MapProfile : Profile
    {
        public MapProfile() 
        { 
            CreateMap<EmpleadoDto, Empleado>().ReverseMap(); 
            CreateMap<EmpleadoAgregarDto, Empleado>().ReverseMap();
            CreateMap<EmpleadoAgregarSolicitudDto, Empleado>().ReverseMap();
            CreateMap<EmpleadoAgregarDto, EmpleadoAgregarSolicitudDto>().ReverseMap(); 
            CreateMap<EmpleadoActualizarDto, Empleado>().ReverseMap();
            CreateMap<EmpleadoActualizarSolicitudDto, Empleado>().ReverseMap();
            CreateMap<EmpleadoActualizarDto, EmpleadoActualizarSolicitudDto>().ReverseMap();

            CreateMap<EstadoDto, Estado>().ReverseMap();
            CreateMap<EstadoAgregarDto, Estado>().ReverseMap();
            CreateMap<EstadoAgregarSolicitudDto, Estado>().ReverseMap();
            CreateMap<EstadoAgregarDto, EstadoAgregarSolicitudDto>().ReverseMap();
            CreateMap<EstadoActualizarDto, Estado>().ReverseMap();
            CreateMap<EstadoActualizarSolicitudDto, Estado>().ReverseMap();
            CreateMap<EstadoActualizarDto, EstadoActualizarSolicitudDto>().ReverseMap();

            CreateMap<PerfilDto, Perfil>().ReverseMap();
            CreateMap<PerfilAgregarDto, Perfil>().ReverseMap();
            CreateMap<PerfilAgregarSolicitudDto, Perfil>().ReverseMap();
            CreateMap<PerfilAgregarDto, PerfilAgregarSolicitudDto>().ReverseMap();
            CreateMap<PerfilActualizarDto, Perfil>().ReverseMap();
            CreateMap<PerfilActualizarSolicitudDto, Perfil>().ReverseMap();
            CreateMap<PerfilActualizarDto, PerfilActualizarSolicitudDto>().ReverseMap();

            CreateMap<PerfilPorPermisoDto, PerfilPorPermiso>().ReverseMap();

            CreateMap<PermisoDto, Permiso>().ReverseMap();
            CreateMap<PermisoAgregarDto, Permiso>().ReverseMap();
            CreateMap<PermisoAgregarSolicitudDto, Permiso>().ReverseMap();
            CreateMap<PermisoAgregarDto, PermisoAgregarSolicitudDto>().ReverseMap();
            CreateMap<PermisoActualizarDto, Permiso>().ReverseMap();
            CreateMap<PermisoActualizarSolicitudDto, Permiso>().ReverseMap();
            CreateMap<PermisoActualizarDto, PermisoActualizarSolicitudDto>().ReverseMap();

            CreateMap<ProductoDto, Producto>().ReverseMap();
            CreateMap<ProductoAgregarDto, Producto>().ReverseMap();
            CreateMap<ProductoAgregarSolicitudDto, Producto>().ReverseMap();
            CreateMap<ProductoAgregarDto, ProductoAgregarSolicitudDto>().ReverseMap();
            CreateMap<ProductoActualizarDto, Producto>().ReverseMap();
            CreateMap<ProductoActualizarSolicitudDto, Producto>().ReverseMap();
            CreateMap<ProductoActualizarDto, ProductoActualizarSolicitudDto>().ReverseMap();
            CreateMap<ProductoLoteAgregarSolicitudDto, Producto>().ReverseMap();

            CreateMap<ProductoLoteDto, ProductoLote>().ReverseMap();
            CreateMap<ProductoLoteAgregarDto, ProductoLote>().ReverseMap();
            CreateMap<ProductoLoteAgregarSolicitudDto, ProductoLote>().ReverseMap();
            CreateMap<ProductoLoteAgregarDto, ProductoLoteAgregarSolicitudDto>().ReverseMap();
            CreateMap<ProductoLoteActualizarDto, ProductoLote>().ReverseMap();
            CreateMap<ProductoLoteActualizarSolicitudDto, ProductoLote>().ReverseMap();
            CreateMap<ProductoLoteActualizarDto, ProductoLoteActualizarSolicitudDto>().ReverseMap();

            CreateMap<AgregarUsuarioDto, AgregarUsuarioCreadoDto>().ReverseMap();

            CreateMap<SalidaDto, SalidaInventario>().ReverseMap();


            CreateMap<SalidaInventarioDetalleDto, SalidaInventarioDetalle>().ReverseMap();
            CreateMap<SalidaInventarioDetalleAgregarDto, SalidaInventarioDetalle>().ReverseMap();
            CreateMap<SalidaInventarioDetalleAgregarDto, SalidaInventarioDetalleDto>().ReverseMap();
            CreateMap<SucursalDto, Sucursal>().ReverseMap();
            CreateMap<UsuarioDto, Usuario>().ReverseMap();
        }
    }
}
