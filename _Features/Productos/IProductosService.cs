using AcademiaFs.ProyectoInventario.Api._Features.Productos.Dtos;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.Api._Features.Productos
{
    public interface IProductosService
    {
        public Respuesta<List<ProductoDto>> ObtenerProductos();
        public Respuesta<ProductoDto> DesactivarProducto(int idProducto);
        public Respuesta<ProductoDto> ObtenerProductoPorId(int idProducto);
        public Respuesta<ProductoAgregarDto> AgregarProducto(ProductoAgregarSolicitudDto productoDto);
        public Respuesta<ProductoActualizarDto> ActualizarProducto(ProductoActualizarSolicitudDto productoDto, int idProducto);
        public bool IdProductoExiste(int idProducto);
        public bool ProductoExiste(Producto producto);
    }
}
