using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Entities;
using AcademiaFs.ProyectoInventario.Api.Infrastructure;
using AutoMapper;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.Api.Domain.SalidasInventarioDetalles
{
    public class SalidaInventarioDetallesDomain : ISalidaInventarioDetallesDomain
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Producto> _productoRepository;
        private readonly IRepository<ProductoLote> _productoLoteRepository;

        public SalidaInventarioDetallesDomain(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaInventario();
            _productoRepository = _unitOfWork.Repository<Producto>();
            _productoLoteRepository = _unitOfWork.Repository<ProductoLote>();
        }

    }
}
