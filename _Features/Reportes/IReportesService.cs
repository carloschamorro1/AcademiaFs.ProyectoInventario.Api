using AcademiaFs.ProyectoInventario.Api._Features.Reportes.Dtos;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.Api._Features.Reportes
{
    public interface IReportesService
    {
        public bool EsFechaValida(string fecha);
        public bool EsFechaInicialMayorQueLaFechaFinal(string fechaInicial, string fechaFinal);
        public Respuesta<List<SalidasPorSucursal>> SonFechasValidas(string fechaInicial, string fechaFinal);
        public Respuesta<RegistrarRecepcionSalidaDto> RegistrarRecepcionDeSalida(int idSalidaInventario);
        public Respuesta<List<SalidasPorSucursal>> obtenerSalidasPorSucursal(GenerarReporteDto generarReporte);
    }
}
