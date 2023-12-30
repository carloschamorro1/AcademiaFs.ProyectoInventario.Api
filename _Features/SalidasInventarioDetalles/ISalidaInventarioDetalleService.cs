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
        
    }
}
