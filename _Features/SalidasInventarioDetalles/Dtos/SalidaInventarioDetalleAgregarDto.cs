using System.Text.Json.Serialization;

namespace AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Dtos
{
    public class SalidaInventarioDetalleAgregarDto
    {
        [JsonIgnore]
        public int? SalidaInventarioId { get; set; }

        public int LoteId { get; set; }

        public int CantidadProducto { get; set; }

        public int UsuarioCreacionId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; }
    }

}
