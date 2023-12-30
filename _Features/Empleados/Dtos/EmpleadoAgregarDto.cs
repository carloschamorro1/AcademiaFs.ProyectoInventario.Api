namespace AcademiaFs.ProyectoInventario.Api._Features.Empleados.Dtos
{
    public class EmpleadoAgregarDto
    {
        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public string? Direccion { get; set; }

        public int UsuarioCreacionId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; }

        public string NumeroIdentidad { get; set; }
    }
}
