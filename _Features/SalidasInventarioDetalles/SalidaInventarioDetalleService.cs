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
        private readonly ISalidaInventarioDetallesDomain _salidaInventarioDetallesDomain;
        private readonly IRepository<ProductoLote> _productoLoteRepository;
        private readonly IRepository<SalidaInventarioDetalle> _salidaInventarioRepository;
        private readonly IRepository<Producto> _productoRepository;

        public SalidaInventarioDetalleService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder, CurrentUser currentUser, ISalidaInventarioDetallesDomain salidaInventarioDetallesDomain)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaInventario();
            _currentUser = currentUser;
            _salidaInventarioDetallesDomain = salidaInventarioDetallesDomain;
            _productoRepository = _unitOfWork.Repository<Producto>();
            _productoLoteRepository = _unitOfWork.Repository<ProductoLote>();
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

        public Respuesta<List<ProductoLoteDto>> SeleccionarInventario(int idProducto, int cantidadRequerida)
        {
            List<ProductoLoteDto> lista = new List<ProductoLoteDto>();
            if (!ProductoIdExiste(idProducto))
            {
                return Respuesta<List<ProductoLoteDto>>.Fault(Mensajes.PRODUCTO_NO_EXISTE, MensajesHttp.CODIGO400, lista);
            }
            if (!ValidarInventarioPorProducto(idProducto, cantidadRequerida))
            {
                int cantidadInventario = CantidadInventario(idProducto);
                return Respuesta<List<ProductoLoteDto>>.Fault(Mensajes.CANTIDAD_INVENTARIO_INSUFICIENTE + cantidadInventario, MensajesHttp.CODIGO400, lista);
            }
            var productosLotes = _unitOfWork.Repository<ProductoLote>();
            var productosOrdenadosPorFecha = productosLotes.AsQueryable()
                                                           .Where(pl => pl.ProductoId == idProducto && pl.Activo)
                                                           .OrderBy(pl => pl.FechaVencimiento)
                                                           .ToList();
            int cantidadAcumulada = 0;
            foreach (var producto in productosOrdenadosPorFecha)
            {
                if (cantidadAcumulada < cantidadRequerida)
                {
                    int cantidadDisponible = Math.Min(producto.Inventario, cantidadRequerida - cantidadAcumulada);
                    ProductoLoteDto productoLoteDto = _mapper.Map<ProductoLoteDto>(producto);
                    productoLoteDto.Inventario = cantidadDisponible;
                    cantidadAcumulada += cantidadDisponible;
                    lista.Add(productoLoteDto);
                }
            }
            return Respuesta<List<ProductoLoteDto>>.Success(lista, Mensajes.INVENTARIO_SELECCIONADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<List<ProductoLoteDto>> ActualizarInventario(int idProductoLote, int cantidadRequerida)
        {
            List<ProductoLoteDto> lista = new List<ProductoLoteDto>();
            if (!ValidarLoteInventarioPorLote(idProductoLote, cantidadRequerida))
            {
                int cantidadInventario = CantidadPorLote(idProductoLote);
                return Respuesta<List<ProductoLoteDto>>.Fault(Mensajes.CANTIDAD_INVENTARIO_INSUFICIENTE + cantidadInventario, MensajesHttp.CODIGO400, lista);
            }
            var loteObtenido = _productoLoteRepository.FirstOrDefault(l => l.LoteId == idProductoLote);
            int cantidadAcumulada = 0;

            if (cantidadAcumulada < cantidadRequerida)
                {
                    int cantidadDisponible = Math.Min(loteObtenido.Inventario, cantidadRequerida - cantidadAcumulada);
                    loteObtenido.Inventario -= cantidadDisponible;
                    cantidadAcumulada += cantidadDisponible;
                    if (loteObtenido.Inventario == 0)
                    {
                    loteObtenido.Activo = false;
                    }
                    _productoLoteRepository.Update(loteObtenido);
                    ProductoLoteDto productoLoteDto = _mapper.Map<ProductoLoteDto>(loteObtenido); 
                    lista.Add(productoLoteDto);
                }

            _unitOfWork.SaveChanges();
            return Respuesta<List<ProductoLoteDto>>.Success(lista, Mensajes.INVENTARIO_SELECCIONADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public bool ValidarLoteInventarioPorLote(int idLote, int cantidadRequerida)
        {
            bool existeInventarioSuficiente = false;
            if (!idLoteExiste(idLote))
            {
                return existeInventarioSuficiente;
            }
            var productos = _productoRepository.AsQueryable();
            var productosLotes = _productoLoteRepository.AsQueryable();

            var cantidadInventarioPorProducto = (from p in productos
                                                 join pl in productosLotes on p.ProductoId equals pl.ProductoId
                                                 where pl.LoteId == idLote
                                                 select pl.Inventario).Sum();
            if (cantidadRequerida <= cantidadInventarioPorProducto)
            {
                existeInventarioSuficiente = true;
            }
            return existeInventarioSuficiente;
        }

        public bool idLoteExiste(int idLote) => _productoLoteRepository.AsQueryable().Any(l => l.LoteId == idLote);

        public bool ValidarInventarioPorProducto(int idProducto, int cantidadRequerida)
        {
            bool existeInventarioSuficiente = false;
            if (!idProductoExiste(idProducto))
            {
                return existeInventarioSuficiente;
            }
            var productos = _productoRepository.AsQueryable();
            var productosLotes = _unitOfWork.Repository<ProductoLote>().AsQueryable();

            var cantidadInventarioPorProducto = (from p in productos
                                                 join pl in productosLotes on p.ProductoId equals pl.ProductoId
                                                 where p.ProductoId == idProducto
                                                 select pl.Inventario).Sum();
            if (cantidadRequerida <= cantidadInventarioPorProducto)
            {
                existeInventarioSuficiente = true;
            }
            return existeInventarioSuficiente;
        }

        public bool idProductoExiste(int idProducto) => _productoRepository.AsQueryable().Any(p => p.ProductoId == idProducto);

        public int CantidadPorLote(int idLote)
        {
            var productos = _unitOfWork.Repository<Producto>().AsQueryable();

            var productosLotes = _productoLoteRepository.AsQueryable();

            var cantidadInventarioPorProducto = (from p in productos
                                                 join pl in productosLotes on p.ProductoId equals pl.ProductoId
                                                 where pl.LoteId == idLote
                                                 select pl.Inventario).Sum();
            return cantidadInventarioPorProducto;
        }
        public int CantidadInventario(int idProducto)
        {
            var productos = _unitOfWork.Repository<Producto>().AsQueryable();

            var productosLotes = _productoLoteRepository.AsQueryable();

            var cantidadInventarioPorProducto = (from p in productos
                                                 join pl in productosLotes on p.ProductoId equals pl.ProductoId
                                                 where p.ProductoId == idProducto
                                                 select pl.Inventario).Sum();
            return cantidadInventarioPorProducto;
        }

    }
}
