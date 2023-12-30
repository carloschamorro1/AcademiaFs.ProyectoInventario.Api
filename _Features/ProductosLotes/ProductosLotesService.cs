using AcademiaFs.ProyectoInventario.Api._Common.Constantes;
using AcademiaFs.ProyectoInventario.Api._Features.Productos.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Entities;
using AcademiaFs.ProyectoInventario.Api.Infrastructure;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.AuthServer.Extensions.Services;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes
{
    public class ProductosLotesService : IProductosLotesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CurrentUser _currentUser;
        private readonly IRepository<ProductoLote> _productoLoteRepository;
        private readonly IRepository<SalidaInventarioDetalle> _salidaInventarioRepository;
        private readonly IRepository<Producto> _productoRepository;

        public ProductosLotesService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder, CurrentUser currentUser)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaInventario();
            _currentUser = currentUser;
            _productoRepository = _unitOfWork.Repository<Producto>();
            _productoLoteRepository = _unitOfWork.Repository<ProductoLote>();
            _salidaInventarioRepository = _unitOfWork.Repository<SalidaInventarioDetalle>();
        }

        public Respuesta<List<ProductoLoteDto>> ObtenerProductosLote()
        {
            List<ProductoLoteDto> productos = _unitOfWork.Repository<ProductoLote>().AsQueryable().Select(x => new ProductoLoteDto
            {
                CantidadInicial = x.CantidadInicial,
                Costo = x.Costo,
                FechaVencimiento = x.FechaVencimiento,
                Inventario = x.Inventario,
                LoteId = x.LoteId,
                Activo = x.Activo,
                FechaCreacion = x.FechaCreacion,
                FechaModificacion = x.FechaModificacion,
                ProductoId = x.ProductoId,
                UsuarioCreacionId = x.UsuarioCreacionId,
                UsuarioModificacionId = x.UsuarioModificacionId
            }).Where(x => x.Activo == true).ToList();
            return Respuesta<List<ProductoLoteDto>>.Success(productos, Mensajes.DATOS_OBTENIDOS_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }
        public Respuesta<ProductoLoteDto> ObtenerProductoLotePorId(int idProductoLote)
        {
            ProductoLote? productoLoteObtenido = _unitOfWork.Repository<ProductoLote>().FirstOrDefault(lote => lote.LoteId == idProductoLote);
            ProductoLoteDto productoLoteDto = _mapper.Map<ProductoLoteDto>(productoLoteObtenido);
            if (productoLoteObtenido == null)
            {
                return Respuesta<ProductoLoteDto>.Fault(Mensajes.PRODUCTO_LOTE_NO_EXISTE, MensajesHttp.CODIGO400, productoLoteDto);
            }
            productoLoteDto.ProductoId = productoLoteObtenido.ProductoId;
            return Respuesta<ProductoLoteDto>.Success(productoLoteDto, Mensajes.PRODUCTO_LOTE_OBTENIDO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<ProductoLoteDto> DesactivarProductoLote(int idProductoLote)
        {
            ProductoLote? productoLoteObtenido = _unitOfWork.Repository<ProductoLote>().FirstOrDefault(lote => lote.LoteId == idProductoLote);
            ProductoLoteDto productoDto = _mapper.Map<ProductoLoteDto>(productoLoteObtenido);
            if (productoLoteObtenido == null)
            {
                return Respuesta<ProductoLoteDto>.Fault(Mensajes.PRODUCTO_NO_EXISTE, MensajesHttp.CODIGO400, productoDto);
            }
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            productoLoteObtenido.Activo = false;
            productoLoteObtenido.FechaModificacion = DateTime.Now;
            productoLoteObtenido.UsuarioModificacionId = currentUserId;
            productoDto.ProductoId = idProductoLote;
            productoDto.Activo = false;
            productoDto.FechaModificacion = productoLoteObtenido.FechaModificacion;
            productoDto.UsuarioModificacionId = productoLoteObtenido.UsuarioModificacionId;
            _unitOfWork.SaveChanges();
            return Respuesta<ProductoLoteDto>.Success(productoDto, Mensajes.PRODUCTO_DESACTIVADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<ProductoLoteAgregarDto> AgregarProductoLote(ProductoLoteAgregarSolicitudDto productoDto)
        {
            ProductoLoteAgregarDto productoAgregarRespuesta = _mapper.Map<ProductoLoteAgregarDto>(productoDto);
            Producto producto = _mapper.Map<Producto>(productoDto);
            ProductoLote nuevoProductoLote = _mapper.Map<ProductoLote>(productoDto);
            if (!ProductoExiste(producto))
            {
                return Respuesta<ProductoLoteAgregarDto>.Fault(Mensajes.PRODUCTO_NO_EXISTE, MensajesHttp.CODIGO400, productoAgregarRespuesta);
            }
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            nuevoProductoLote.UsuarioCreacionId = currentUserId;
            nuevoProductoLote.FechaCreacion = DateTime.Now;
            nuevoProductoLote.Activo = true;
            _unitOfWork.Repository<ProductoLote>().Add(nuevoProductoLote);
            _unitOfWork.SaveChanges();
            productoAgregarRespuesta = _mapper.Map<ProductoLoteAgregarDto>(nuevoProductoLote);
            return Respuesta<ProductoLoteAgregarDto>.Success(productoAgregarRespuesta, Mensajes.PRODUCTO_LOTE_AGREGADO_EXITOSAMENTE, MensajesHttp.CODIGO201);
        }

        public bool ProductoExiste(Producto producto) => _unitOfWork.Repository<Producto>().AsQueryable().Any(p => p.ProductoId == producto.ProductoId);

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
            IRepository<ProductoLote> productosLotes = _unitOfWork.Repository<ProductoLote>();
            List<ProductoLote> productosOrdenadosPorFecha = productosLotes.AsQueryable()
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
            ProductoLote? loteObtenido = _productoLoteRepository.FirstOrDefault(l => l.LoteId == idProductoLote);
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
            IQueryable<Producto> productos = _productoRepository.AsQueryable();
            IQueryable<ProductoLote> productosLotes = _productoLoteRepository.AsQueryable();
            int cantidadInventarioPorProducto = (from p in productos
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
            IQueryable<Producto> productos = _productoRepository.AsQueryable();
            IQueryable<ProductoLote> productosLotes = _unitOfWork.Repository<ProductoLote>().AsQueryable();
            int cantidadInventarioPorProducto = (from p in productos
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
            IQueryable<Producto> productos = _unitOfWork.Repository<Producto>().AsQueryable();
            IQueryable<ProductoLote> productosLotes = _productoLoteRepository.AsQueryable();
            int cantidadInventarioPorProducto = (from p in productos
                                                 join pl in productosLotes on p.ProductoId equals pl.ProductoId
                                                 where pl.LoteId == idLote
                                                 select pl.Inventario).Sum();
            return cantidadInventarioPorProducto;
        }
        public int CantidadInventario(int idProducto)
        {
            IQueryable<Producto> productos = _unitOfWork.Repository<Producto>().AsQueryable();
            IQueryable<ProductoLote> productosLotes = _productoLoteRepository.AsQueryable();
            int cantidadInventarioPorProducto = (from p in productos
                                                 join pl in productosLotes on p.ProductoId equals pl.ProductoId
                                                 where p.ProductoId == idProducto
                                                 select pl.Inventario).Sum();
            return cantidadInventarioPorProducto;
        }

        public bool ProductoIdExiste(int idProducto) => _unitOfWork.Repository<Producto>().AsQueryable().Any(p => p.ProductoId == idProducto);
    }
}
