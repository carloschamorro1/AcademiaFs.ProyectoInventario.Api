using System.Text.Json.Serialization;

namespace AcademiaFs.ProyectoInventario.Api._Features.Reportes.Dtos
{
    public class SalidasPorSucursal
    {
        public int IdSalidaInventario { get; set; }
        public DateTime FechaSalida { get; set; }
        public string NombreProducto { get; set; }
        public int UnidadesTotales { get; set; }      
        public decimal Total { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public int? UsuarioIdRecibe { get; set; }
        public string? NombreUsuarioRecibe { get; set; }
        public DateTime? FechaRecibido { get; set; }
    }
}
