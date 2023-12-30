using AcademiaFs.ProyectoInventario.Api._Common.Constantes;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Entities;
using AcademiaFs.ProyectoInventario.Api.Infrastructure;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.Api.Domain.SalidasInventario
{
    public class SalidaInventarioDomain : ISalidaInventarioDomain
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SalidaInventarioDomain(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaInventario();
        }

        public Respuesta<SalidaDto> AlcanzoMaximoEnSalidas(decimal cantidadTotalPorSucursal, SalidaDto salidaDto, decimal totalSalidaActual)
        {
            if (cantidadTotalPorSucursal > Limites.LIMITE_ENVIO_A_SUCURSAL || totalSalidaActual > Limites.LIMITE_ENVIO_A_SUCURSAL)
            {
                return Respuesta<SalidaDto>.Fault(Mensajes.LIMITE_EN_SUCURSAL_ALCANZADO, MensajesHttp.CODIGO400, salidaDto);
            }
            return Respuesta<SalidaDto>.Success();
        }
    }
}
