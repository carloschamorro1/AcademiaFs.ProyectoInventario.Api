using AcademiaFs.ProyectoInventario.Api._Common.Constantes;
using AcademiaFs.ProyectoInventario.Api._Common.Enums;
using AcademiaFs.ProyectoInventario.Api._Features.Estados.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.Sucursales.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Entities;
using AcademiaFs.ProyectoInventario.Api.Domain.SalidasInventario;
using AcademiaFs.ProyectoInventario.Api.Domain.SalidasInventarioDetalles;
using AcademiaFs.ProyectoInventario.Api.Infrastructure;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.AuthServer.Extensions.Services;
using Farsiman.Domain.Core.Standard.Repositories;
using System;

namespace AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario
{
    public class SalidaInventarioService : ISalidaInventarioService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CurrentUser _currentUser; 
        private readonly ISalidaInventarioDomain _salidaInventarioDomain;
        private readonly ISalidaInventarioDetallesDomain _salidaInventarioDetallesDomain;
        private readonly ISalidaInventarioDetalleService _salidaInventarioDetalleService;
        private readonly IRepository<SalidaInventario> _salidaInventarioRepository;

        public SalidaInventarioService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder, CurrentUser currentUser, ISalidaInventarioDomain salidaInventarioDomain, ISalidaInventarioDetalleService salidaInventarioDetalleService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaInventario();
            _currentUser = currentUser;
            _salidaInventarioDomain = salidaInventarioDomain;
            _salidaInventarioDetalleService = salidaInventarioDetalleService;

            _salidaInventarioRepository = _unitOfWork.Repository<SalidaInventario>();
        }

        public bool ValidarSiEsJefe(int idUsuario)
        {
            bool esJefe = false;
            var usuarios = _unitOfWork.Repository<Usuario>().AsQueryable();
            var perfiles = _unitOfWork.Repository<Perfil>().AsQueryable();
            var perfil = (from p in perfiles
                          join user in usuarios on p.PerfilId equals user.PerfilId
                          where user.UsuarioId == idUsuario
                          select p.Nombre).FirstOrDefault();
            if (perfil.Equals(NombrePerfiles.JEFE_DE_BODEGA))
            {
                esJefe = true;
            }
            return esJefe;
        }

        public Respuesta<SalidaDto> alcanzoSucursalMaximoEnSalidas(int idSucursal, SalidaDto salidaDto, decimal totalSalidaActual)
        {
            decimal cantidadTotalPorSucursal = (from salida in _unitOfWork.Repository<SalidaInventario>().AsQueryable()
                                                where salida.SucursalId == idSucursal && salida.EstadoId == (int)EstadosEnum.EnviadaSucursal
                                                select salida.Total).Sum();
            Respuesta<SalidaDto> alcanzoMaximoSucursal = _salidaInventarioDomain.AlcanzoMaximoEnSalidas(cantidadTotalPorSucursal, salidaDto, totalSalidaActual);
            if (!alcanzoMaximoSucursal.Ok)
            {
                return alcanzoMaximoSucursal;
            }
            return alcanzoMaximoSucursal;
        }

        public bool UsuarioIdExiste(int idUsuario) => _unitOfWork.Repository<Usuario>().AsQueryable().Any(u => u.UsuarioId == idUsuario);
        public bool SucursalIdExiste(int idSucursal) => _unitOfWork.Repository<Sucursal>().AsQueryable().Any(s => s.SucursalId == idSucursal);
        public bool EstadoIdExiste(int idEstado) => _unitOfWork.Repository<Estado>().AsQueryable().Any(e => e.EstadoId == idEstado);

        public decimal ObtenerCostoProducto(int? idLote)
        {
            var productoLoteObtenido = _unitOfWork.Repository<ProductoLote>().FirstOrDefault(lote => lote.LoteId == idLote);
            if(productoLoteObtenido != null)
            {
                decimal costo = productoLoteObtenido.Costo;
                return costo;
            }
            return 0;
        }

        public int ObtenerIdProducto(int? idProducto)
        {
            var productoObtenido = _unitOfWork.Repository<ProductoLote>().FirstOrDefault(p => p.ProductoId == idProducto);
            if (productoObtenido != null)
            {
                int producto = productoObtenido.ProductoId;
                return producto;
            }
            return 0;
        }

        public Respuesta<SalidaDto> sonValidosDatosSalidaInventario(int idUsuario, int idSucursal, int idEstado, SalidaDto salidaInventarioDto)
        {
            if (!UsuarioIdExiste(idUsuario))
            {
                return Respuesta<SalidaDto>.Fault(Mensajes.USUARIO_NO_EXISTE, MensajesHttp.CODIGO400, salidaInventarioDto);
            }
            if (!ValidarSiEsJefe(idUsuario))
            {
                return Respuesta<SalidaDto>.Fault(Mensajes.USUARIO_NO_TIENE_PERFIL_JEFE, MensajesHttp.CODIGO400, salidaInventarioDto);
            }
            if (!SucursalIdExiste(idSucursal))
            {
                return Respuesta<SalidaDto>.Fault(Mensajes.SUCURSAL_NO_EXISTE, MensajesHttp.CODIGO400, salidaInventarioDto);
            }

            if (!EstadoIdExiste(idEstado))
            {
                return Respuesta<SalidaDto>.Fault(Mensajes.ESTADO_NO_EXISTE, MensajesHttp.CODIGO400, salidaInventarioDto);
            }
            return Respuesta<SalidaDto>.Success(salidaInventarioDto, Mensajes.DATOS_VALIDOS, MensajesHttp.CODIGO400);
        }

        public Respuesta<SalidaDto> AgregarSalidaInventario(SalidaDto salidaInventarioDto, int idUsuario)
        {   
            decimal costoTotal = 0;
            decimal costoProducto = 0;
            SalidaInventario salida = _mapper.Map<SalidaInventario>(salidaInventarioDto);
            salida.EstadoId = (int)EstadosEnum.EnTransito;
            salida.Total = costoTotal;
            Respuesta<SalidaDto> respuesta = sonValidosDatosSalidaInventario(idUsuario, salidaInventarioDto.SucursalId, salida.EstadoId, salidaInventarioDto);
            if (!respuesta.Ok)
                return respuesta;
            _unitOfWork.BeginTransaction();
            _salidaInventarioRepository.Add(salida);
            _unitOfWork.SaveChanges();
            foreach (SalidaInventarioDetalleAgregarDto detalle in salidaInventarioDto.salidaInventarioDetalle)
            {
                SalidaInventarioDetalleAgregarDto detalleSalidaInventario = _mapper.Map<SalidaInventarioDetalleAgregarDto>(detalle);
                detalleSalidaInventario.SalidaInventarioId = salida.SalidaInventarioId;
                var detalleAgregado = _salidaInventarioDetalleService.AgregarDetalle(detalleSalidaInventario);
                var loteActualizado = _salidaInventarioDetalleService.ActualizarInventario(detalle.LoteId, detalle.CantidadProducto);
                if (!detalleAgregado.Ok)
                {
                    _unitOfWork.RollBack();
                    respuesta = Respuesta<SalidaDto>.Fault(Mensajes.DETALLE_SALIDA_ERROR_AL_AGREGAR, MensajesHttp.CODIGO400, salidaInventarioDto);
                    return respuesta;
                }
                if (!loteActualizado.Ok)
                {
                    int cantidadEnLote = _salidaInventarioDetalleService.CantidadPorLote(detalle.LoteId);
                    _unitOfWork.RollBack();
                    respuesta = Respuesta<SalidaDto>.Fault(Mensajes.CANTIDAD_LOTE_INSUFICIENTE + detalle.LoteId + Mensajes.CANTIDAD_EN_INVENTARIO + cantidadEnLote , MensajesHttp.CODIGO400, salidaInventarioDto);
                    return respuesta;
                }
                costoProducto = ObtenerCostoProducto(detalle.LoteId);
                costoTotal += (detalleSalidaInventario.CantidadProducto * costoProducto);
            }
            respuesta = alcanzoSucursalMaximoEnSalidas(salidaInventarioDto.SucursalId, salidaInventarioDto,costoTotal);
            if (!respuesta.Ok)
            {
                return respuesta;
            }
            salida.Total = costoTotal;
            salida.EstadoId = (int)EstadosEnum.EnviadaSucursal;
            _salidaInventarioRepository.Update(salida);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
            respuesta = Respuesta<SalidaDto>.Success(salidaInventarioDto, Mensajes.SALIDA_INVENTARIO_AGREGADA_EXITOSAMENTE, MensajesHttp.CODIGO201);
            return respuesta;
        }

    }
    
}
