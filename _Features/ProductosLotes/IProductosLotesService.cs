using AcademiaFs.ProyectoInventario.Api._Features.Productos.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Dtos;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes
{
    public interface IProductosLotesService
    {
        public Respuesta<List<ProductoLoteDto>> ObtenerProductosLote();
        public Respuesta<ProductoLoteDto> ObtenerProductoLotePorId(int idProductoLote);
        public Respuesta<ProductoLoteDto> DesactivarProductoLote(int idProductoLote);
        public Respuesta<List<ProductoLoteDto>> SeleccionarInventario(int idProducto, int cantidadRequerida);
        public Respuesta<List<ProductoLoteDto>> ActualizarInventario(int idProductoLote, int cantidadRequerida);
        public Respuesta<ProductoLoteAgregarDto> AgregarProductoLote(ProductoLoteAgregarSolicitudDto productoDto);
        public bool ValidarLoteInventarioPorLote(int idLote, int cantidadRequerida);
        public bool idLoteExiste(int idLote);
        public bool ValidarInventarioPorProducto(int idProducto, int cantidadRequerida);
        public bool idProductoExiste(int idProducto);
        public int CantidadPorLote(int idLote);
        public int CantidadInventario(int idProducto);
        public bool ProductoIdExiste(int idProducto);
        public bool ProductoExiste(Producto producto);
    }
}
