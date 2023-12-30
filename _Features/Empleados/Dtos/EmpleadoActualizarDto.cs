using System.Text.Json.Serialization;

namespace AcademiaFs.ProyectoInventario.Api._Features.Empleados.Dtos
{
    public class EmpleadoActualizarDto
    {
        public int EmpleadoId { get; set; }
        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public string? Direccion { get; set; }

        public int? UsuarioModificacionId { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string NumeroIdentidad { get; set; }
    }
}
