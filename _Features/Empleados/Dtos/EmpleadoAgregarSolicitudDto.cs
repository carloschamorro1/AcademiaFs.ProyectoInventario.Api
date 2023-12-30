namespace AcademiaFs.ProyectoInventario.Api._Features.Empleados.Dtos
{
    public class EmpleadoAgregarSolicitudDto
    {
        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public string? Direccion { get; set; }

        public bool Activo { get; set; }

        public string NumeroIdentidad { get; set; }
    }
}
