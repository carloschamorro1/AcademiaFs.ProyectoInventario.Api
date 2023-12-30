﻿namespace AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Dtos
{
    public class ProductoLoteAgregarDto
    {
        public int LoteId { get; set; }
        public int? ProductoId { get; set; }
        public int CantidadInicial { get; set; }
        public decimal Costo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int Inventario { get; set; }
        public int UsuarioCreacionId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}
