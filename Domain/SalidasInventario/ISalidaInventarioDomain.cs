using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Dtos;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.Api.Domain.SalidasInventario
{
    public interface ISalidaInventarioDomain
    {
        public Respuesta<SalidaDto> AlcanzoMaximoEnSalidas(decimal cantidadTotalPorSucursal, SalidaDto salidaDto, decimal totalSalidaActual);
    }
}
