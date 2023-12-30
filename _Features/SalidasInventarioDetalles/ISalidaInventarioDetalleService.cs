using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Dtos;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles
{
    public interface ISalidaInventarioDetalleService
    {
        public Respuesta<List<SalidaInventarioDetalleDto>> ObtenerSalidasDetalle();
        public Respuesta<SalidaInventarioDetalleAgregarDto> AgregarDetalle(SalidaInventarioDetalleAgregarDto detalleSalidaInventario);
        public Respuesta<SalidaInventarioDetalleDto> DesactivarSalidaDetalle(int idSalida);
        public bool ProductoIdExiste(int idProducto);
        public Respuesta<List<ProductoLoteDto>> SeleccionarInventario(int idProducto, int cantidadRequerida);
        public Respuesta<List<ProductoLoteDto>> ActualizarInventario(int idProductoLote, int cantidadRequerida);
        public bool ValidarLoteInventarioPorLote(int idLote, int cantidadRequerida);
        public bool idLoteExiste(int idLote);
        public bool ValidarInventarioPorProducto(int idProducto, int cantidadRequerida);
        public bool idProductoExiste(int idProducto);
        public int CantidadPorLote(int idLote);
        public int CantidadInventario(int idProducto);
    }
}
