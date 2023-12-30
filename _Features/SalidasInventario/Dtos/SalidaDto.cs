using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Dtos;
using System.Text.Json.Serialization;

namespace AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Dtos
{
    public class SalidaDto
    {
        public int SucursalId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaSalida { get; set; }
        [JsonIgnore]
        public decimal Total { get; set; }
        public DateTime? FechaRecibido { get; set; }
        [JsonIgnore]
        public int EstadoId { get; set; }
        public int UsuarioCreacionId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }

        public List<SalidaInventarioDetalleAgregarDto> salidaInventarioDetalle { get; set; }


    }
}
