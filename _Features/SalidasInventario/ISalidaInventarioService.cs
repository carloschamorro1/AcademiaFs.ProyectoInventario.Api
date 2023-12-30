using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Dtos;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario
{
    public interface ISalidaInventarioService
    {
        public bool ValidarSiEsJefe(int idUsuario);
        public Respuesta<SalidaDto> alcanzoSucursalMaximoEnSalidas(int idSucursal, SalidaDto salidaDto, decimal totalSalidaActual);
        public bool UsuarioIdExiste(int idUsuario);
        public bool SucursalIdExiste(int idSucursal);
        public bool EstadoIdExiste(int idEstado);
        public decimal ObtenerCostoProducto(int? idLote);
        public int ObtenerIdProducto(int? idProducto);
        public Respuesta<SalidaDto> sonValidosDatosSalidaInventario(int idUsuario, int idSucursal, int idEstado, SalidaDto salidaInventarioDto);
        public Respuesta<SalidaDto> AgregarSalidaInventario(SalidaDto salidaInventarioDto, int idUsuario);
    }
}
