using AcademiaFs.ProyectoInventario.Api._Common.Constantes;
using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Entities;
using AcademiaFs.ProyectoInventario.Api.Domain.SalidasInventarioDetalles;
using AcademiaFs.ProyectoInventario.Api.Infrastructure;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.AuthServer.Extensions.Services;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles
{
    public class SalidaInventarioDetalleService : ISalidaInventarioDetalleService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CurrentUser _currentUser; 
        private readonly IRepository<SalidaInventarioDetalle> _salidaInventarioRepository;

        public SalidaInventarioDetalleService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder, CurrentUser currentUser)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaInventario();
            _currentUser = currentUser;     
            _salidaInventarioRepository = _unitOfWork.Repository<SalidaInventarioDetalle>();
        }

        public Respuesta<List<SalidaInventarioDetalleDto>> ObtenerSalidasDetalle()
        {
            var estados = _unitOfWork.Repository<SalidaInventarioDetalle>().AsQueryable().Select(x => new SalidaInventarioDetalleDto
            {
                DetalleId = x.DetalleId,
                SalidaInventarioId = x.SalidaInventarioId,
                LoteId = x.LoteId,
                CantidadProducto = x.CantidadProducto,
                UsuarioCreacionId = x.UsuarioCreacionId,
                FechaCreacion = x.FechaCreacion,
                UsuarioModificacionId = x.UsuarioModificacionId,
                FechaModificacion = x.FechaModificacion,
                Activo = x.Activo
            }).Where(x => x.Activo == true).ToList();
            return Respuesta<List<SalidaInventarioDetalleDto>>.Success(estados, Mensajes.DATOS_OBTENIDOS_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<SalidaInventarioDetalleAgregarDto> AgregarDetalle(SalidaInventarioDetalleAgregarDto detalleSalidaInventario)
        {
            if (detalleSalidaInventario.CantidadProducto <= 0)
            {
                return Respuesta<SalidaInventarioDetalleAgregarDto>.Fault(Mensajes.CANTIDAD_DE_PRODUCTOS_NO_PERMITIDA, MensajesHttp.CODIGO400, detalleSalidaInventario);
            }
            var map = _mapper.Map<SalidaInventarioDetalle>(detalleSalidaInventario);
            _salidaInventarioRepository.Add(map);
            _unitOfWork.SaveChanges();
            return Respuesta<SalidaInventarioDetalleAgregarDto>.Success(detalleSalidaInventario, Mensajes.DETALLE_SALIDA_AGREGADO_EXITOSAMENTE, MensajesHttp.CODIGO201);
        }

        public Respuesta<SalidaInventarioDetalleDto> DesactivarSalidaDetalle(int idSalida)
        {
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            var salidaObtenida = _unitOfWork.Repository<SalidaInventarioDetalle>().FirstOrDefault(d => d.SalidaInventarioId == idSalida);
            var map = _mapper.Map<SalidaInventarioDetalleDto>(salidaObtenida);
            if (salidaObtenida == null)
            {
                return Respuesta<SalidaInventarioDetalleDto>.Fault(Mensajes.DETALLE_SALIDA_NO_EXISTE, MensajesHttp.CODIGO400, map);
            }
            salidaObtenida.Activo = false;
            salidaObtenida.FechaModificacion = DateTime.Now;
            salidaObtenida.UsuarioModificacionId = currentUserId;
            map.SalidaInventarioId = idSalida;
            map.Activo = false;
            map.FechaModificacion = salidaObtenida.FechaModificacion;
            map.UsuarioModificacionId = salidaObtenida.UsuarioModificacionId;
            _unitOfWork.SaveChanges();
            return Respuesta<SalidaInventarioDetalleDto>.Success(map, Mensajes.DETALLE_SALIDA_DESACTIVADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public bool ProductoIdExiste(int idProducto) => _unitOfWork.Repository<Producto>().AsQueryable().Any(p => p.ProductoId == idProducto);

        

    }
}
