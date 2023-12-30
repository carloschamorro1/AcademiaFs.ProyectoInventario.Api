using AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario;
using Farsiman.Domain.Core.Standard.Repositories;
using Farsiman.Infraestructure.Core.Entity.Standard;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFs.ProyectoInventario.Api.Infrastructure
{
    public class UnitOfWorkBuilder
    {
        readonly IServiceProvider _serviceProvider;
        private IUnitOfWork _unitOfWork;

        public UnitOfWorkBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork BuilderSistemaInventario()
        {
            if (_unitOfWork != null) return _unitOfWork;

            DbContext dbContext = _serviceProvider.GetService<InventarioDbContext>() ?? throw new NullReferenceException();
            _unitOfWork = new UnitOfWork(dbContext);
            return _unitOfWork;//new UnitOfWork(dbContext);
        }
    }
}
