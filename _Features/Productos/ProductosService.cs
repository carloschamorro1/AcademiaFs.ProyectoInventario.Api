using AcademiaFs.ProyectoInventario.Api._Common.Constantes;
using AcademiaFs.ProyectoInventario.Api._Features.Productos.Dtos;
using AcademiaFs.ProyectoInventario.Api.Infrastructure;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.AuthServer.Extensions.Services;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.Api._Features.Productos
{
    public class ProductosService : IProductosService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CurrentUser _currentUser;

        public ProductosService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder, CurrentUser currentUser)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaInventario();
            _currentUser = currentUser;
        }

        public Respuesta<List<ProductoDto>> ObtenerProductos()
        {
            List<ProductoDto> productos = _unitOfWork.Repository<Producto>().AsQueryable().Select(x => new ProductoDto
            {
                Nombre = x.Nombre,
                Activo = x.Activo,
                FechaCreacion = x.FechaCreacion,
                FechaModificacion = x.FechaModificacion,
                ProductoId = x.ProductoId,
                UsuarioCreacionId = x.UsuarioCreacionId,
                UsuarioModificacionId = x.UsuarioModificacionId
            }).Where(x => x.Activo == true).ToList();
            return Respuesta<List<ProductoDto>>.Success(productos, Mensajes.DATOS_OBTENIDOS_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<ProductoDto> DesactivarProducto(int idProducto)
        {
            Producto? productoObtenido = _unitOfWork.Repository<Producto>().FirstOrDefault(p => p.ProductoId == idProducto);
            ProductoDto productoDto = _mapper.Map<ProductoDto>(productoObtenido);
            if (productoObtenido == null)
            {
                return Respuesta<ProductoDto>.Fault(Mensajes.PRODUCTO_NO_EXISTE, MensajesHttp.CODIGO400, productoDto);
            }
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            productoObtenido.Activo = false;
            productoObtenido.FechaModificacion = DateTime.Now;
            productoObtenido.UsuarioModificacionId = currentUserId;
            productoDto.ProductoId = idProducto;
            productoDto.Activo = false;
            productoDto.FechaModificacion = productoObtenido.FechaModificacion;
            productoDto.UsuarioModificacionId = productoObtenido.UsuarioModificacionId;
            _unitOfWork.SaveChanges();
            return Respuesta<ProductoDto>.Success(productoDto, Mensajes.PRODUCTO_DESACTIVADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<ProductoDto> ObtenerProductoPorId(int idProducto)
        {
            Producto? productoObtenido = _unitOfWork.Repository<Producto>().FirstOrDefault(p => p.ProductoId == idProducto);
            ProductoDto productoDto = _mapper.Map<ProductoDto>(productoObtenido);
            if (productoObtenido == null)
            {
                return Respuesta<ProductoDto>.Fault(Mensajes.PRODUCTO_NO_EXISTE, MensajesHttp.CODIGO400, productoDto);
            }
            productoDto.ProductoId = productoObtenido.ProductoId;
            return Respuesta<ProductoDto>.Success(productoDto, Mensajes.PRODUCTO_OBTENIDO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public Respuesta<ProductoAgregarDto> AgregarProducto(ProductoAgregarSolicitudDto productoDto)
        {
            ProductoAgregarDto productoAgregarRespuesta = _mapper.Map<ProductoAgregarDto>(productoDto);
            Producto nuevoProducto = _mapper.Map<Producto>(productoDto);
            if (ProductoExiste(nuevoProducto))
            {
                return Respuesta<ProductoAgregarDto>.Fault(Mensajes.PRODUCTO_YA_EXISTE, MensajesHttp.CODIGO400, productoAgregarRespuesta);
            }
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            nuevoProducto.UsuarioCreacionId = currentUserId;
            nuevoProducto.FechaCreacion = DateTime.Now;
            nuevoProducto.Activo = true;
            _unitOfWork.Repository<Producto>().Add(nuevoProducto);
            _unitOfWork.SaveChanges();
            productoAgregarRespuesta = _mapper.Map<ProductoAgregarDto>(nuevoProducto);
            return Respuesta<ProductoAgregarDto>.Success(productoAgregarRespuesta, Mensajes.PRODUCTO_AGREGADO_EXITOSAMENTE, MensajesHttp.CODIGO201);
        }

        public Respuesta<ProductoActualizarDto> ActualizarProducto(ProductoActualizarSolicitudDto productoDto, int idProducto)
        {
            ProductoActualizarDto productoActualizarRespuesta = _mapper.Map<ProductoActualizarDto>(productoDto);
            if (!IdProductoExiste(idProducto))
            {
                return Respuesta<ProductoActualizarDto>.Fault(Mensajes.PRODUCTO_NO_EXISTE, MensajesHttp.CODIGO400, productoActualizarRespuesta);
            }
            Producto producto = _mapper.Map<Producto>(productoDto);
            if (ProductoExiste(producto))
            {
                return Respuesta<ProductoActualizarDto>.Fault(Mensajes.PRODUCTO_YA_EXISTE, MensajesHttp.CODIGO400, productoActualizarRespuesta);
            }
            Producto? permisoActual = _unitOfWork.Repository<Producto>().FirstOrDefault(p => p.ProductoId == idProducto);
            if (permisoActual == null)
            {
                return Respuesta<ProductoActualizarDto>.Fault(Mensajes.PRODUCTO_NO_EXISTE, MensajesHttp.CODIGO400, productoActualizarRespuesta);
            }
            permisoActual.Nombre = productoDto.Nombre;
            int currentUserId = Convert.ToInt32(_currentUser.UserName);
            permisoActual.UsuarioModificacionId = currentUserId;
            permisoActual.FechaModificacion = DateTime.Now;
            _unitOfWork.SaveChanges();
            productoActualizarRespuesta = _mapper.Map<ProductoActualizarDto>(permisoActual);
            return Respuesta<ProductoActualizarDto>.Success(productoActualizarRespuesta, Mensajes.PRODUCTO_ACTUALIZADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
        }

        public bool IdProductoExiste(int idProducto) => _unitOfWork.Repository<Producto>().AsQueryable().Any(p => p.ProductoId == idProducto);
        public bool ProductoExiste(Producto producto) => _unitOfWork.Repository<Producto>().AsQueryable().Any(p => p.ProductoId == producto.ProductoId);
    }
}
