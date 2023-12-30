namespace AcademiaFs.ProyectoInventario.Api._Features.Reportes.Dtos
{
    public class GenerarReporteDto
    {
        public int idSucursal { get; set; }
        public string? fechaInicial { get; set; }
        public string? fechaFinal { get; set; }
    }
}
