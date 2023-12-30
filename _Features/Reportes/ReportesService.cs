using AcademiaFs.ProyectoInventario.Api._Common.Constantes;
using AcademiaFs.ProyectoInventario.Api._Features.Estados.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Reportes.Dtos;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles.Entities;
using AcademiaFs.ProyectoInventario.Api._Features.Usuarios.Entities;
using AcademiaFs.ProyectoInventario.Api.Infrastructure;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.AuthServer.Extensions.Services;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.Api._Features.Reportes
{
    public class ReportesService : IReportesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CurrentUser _currentUser;

        public ReportesService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder, CurrentUser currentUser)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaInventario();
            _currentUser = currentUser;
        }

        public bool EsFechaValida(string fecha)
        {
            bool resultado = false;
            resultado = DateTime.TryParse(fecha, out DateTime fechaValida);
            return resultado;
        }

        public bool EsFechaInicialMayorQueLaFechaFinal(string fechaInicial, string fechaFinal)
        {
            bool resultado = false;
            DateTime fechaInicialValida = DateTime.Parse(fechaInicial);
            DateTime fechaFinalValida = DateTime.Parse(fechaFinal);
            if (fechaInicialValida > fechaFinalValida)
            {
                resultado = true;
                return resultado;
            }
            return resultado;
        }

        public Respuesta<List<SalidasPorSucursal>> SonFechasValidas(string fechaInicial, string fechaFinal)
        {
            if (!EsFechaValida(fechaInicial))
            {
                List<SalidasPorSucursal> resultado = null;
                return Respuesta<List<SalidasPorSucursal>>.Fault(Mensajes.FECHA_INICIAL_NO_VALIDA, MensajesHttp.CODIGO400, resultado);
            }

            if (!EsFechaValida(fechaFinal))
            {
                List<SalidasPorSucursal> resultado = null;
                return Respuesta<List<SalidasPorSucursal>>.Fault(Mensajes.FECHA_FINAL_NO_VALIDA, MensajesHttp.CODIGO400, resultado);
            }

            if (EsFechaInicialMayorQueLaFechaFinal(fechaInicial, fechaFinal))
            {
                List<SalidasPorSucursal> resultado = null;
                return Respuesta<List<SalidasPorSucursal>>.Fault(Mensajes.FECHA_INICIAL_MAYOR_A_LA_FINAL, MensajesHttp.CODIGO400, resultado);
            }
            return Respuesta<List<SalidasPorSucursal>>.Success();
        }

        public bool seRecibioLaSalida(SalidaInventario salida) => _unitOfWork.Repository<SalidaInventario>().AsQueryable().Any(x => x.SalidaInventarioId == salida.SalidaInventarioId && x.EmpleadoIdrecibe != null);

        public Respuesta<RegistrarRecepcionSalidaDto> RegistrarRecepcionDeSalida(int idSalidaInventario)
        {
            //int currentUserId = Convert.ToInt32(_currentUser.UserName);
            SalidaInventario salidaActual = _unitOfWork.Repository<SalidaInventario>().FirstOrDefault(s => s.SalidaInventarioId == idSalidaInventario);
            if (salidaActual == null)
                return Respuesta<RegistrarRecepcionSalidaDto>.Fault(Mensajes.SALIDA_INVENTARIO_NO_EXISTE);
            if(seRecibioLaSalida(salidaActual))
                return Respuesta<RegistrarRecepcionSalidaDto>.Fault(Mensajes.SALIDA_INVENTARIO_YA_REGISTRADA);
            //salidaActual.EmpleadoIdrecibe = currentUserId;
            //salidaActual.UsuarioModificacionId = currentUserId;
            salidaActual.EmpleadoIdrecibe = 11236;
            salidaActual.FechaRecibido = DateTime.Now;
            salidaActual.UsuarioModificacionId = 11236;
            salidaActual.FechaModificacion = DateTime.Now;
            _unitOfWork.SaveChanges();
            return Respuesta<RegistrarRecepcionSalidaDto>.Success(Mensajes.SALIDA_INVENTARIO_RECIBIDA_EXITOSAMENTE);
        }

        public Respuesta<List<SalidasPorSucursal>> obtenerSalidasPorSucursal(GenerarReporteDto generarReporte)
        {
            Respuesta<List<SalidasPorSucursal>> respuesta = SonFechasValidas(generarReporte.fechaInicial, generarReporte.fechaFinal);
            if (!respuesta.Ok)
            {
                return respuesta;
            }
            List<SalidasPorSucursal> reporte = (from salida in _unitOfWork.Repository<SalidaInventario>().AsQueryable()
                                                join detalle in _unitOfWork.Repository<SalidaInventarioDetalle>().AsQueryable()
                                                    on salida.SalidaInventarioId equals detalle.SalidaInventarioId
                                                join estado in _unitOfWork.Repository<Estado>().AsQueryable()
                                                    on salida.EstadoId equals estado.EstadoId
                                                join user in _unitOfWork.Repository<Usuario>().AsQueryable()
                                                    on salida.EmpleadoIdrecibe equals user.UsuarioIdentidadFarsiman into userJoin
                                                from user in userJoin.DefaultIfEmpty()
                                                join lote in _unitOfWork.Repository<ProductoLote>().AsQueryable()
                                                    on detalle.LoteId equals lote.LoteId
                                                join producto in _unitOfWork.Repository<Producto>().AsQueryable()
                                                    on lote.ProductoId equals producto.ProductoId
                                                where salida.SucursalId == generarReporte.idSucursal
                                                group new { salida, detalle, estado, user, producto } by new
                                                {
                                                    salida.SalidaInventarioId,
                                                    salida.FechaSalida,
                                                    salida.EstadoId,
                                                    EstadoNombre = estado.Nombre,
                                                    salida.EmpleadoIdrecibe,
                                                    salida.FechaRecibido,
                                                    salida.Total,
                                                    salida.SucursalId,
                                                    user.NombreUsuario,
                                                    producto.Nombre
                                                } into grp
                                                select new SalidasPorSucursal
                                                {
                                                    IdSalidaInventario = grp.Key.SalidaInventarioId,
                                                    FechaSalida = grp.Key.FechaSalida,
                                                    UnidadesTotales = grp.Sum(x => x.detalle.CantidadProducto),
                                                    NombreProducto = grp.Key.Nombre,
                                                    Total = grp.Key.Total,
                                                    IdEstado = grp.Key.EstadoId,
                                                    Estado = grp.Key.EstadoNombre,
                                                    UsuarioIdRecibe = grp.Key.EmpleadoIdrecibe,
                                                    NombreUsuarioRecibe = grp.Key.NombreUsuario,
                                                    FechaRecibido = grp.Key.FechaRecibido
                                                }).ToList();
            if(reporte.Count == 0)
            {
                Respuesta<List<SalidasPorSucursal>>.Fault(Mensajes.REPORTE_VACIO_SUCURSAL_SIN_SALIDAS_EN_FECHAS, MensajesHttp.CODIGO400, reporte);
                return respuesta;
            }
            respuesta = Respuesta<List<SalidasPorSucursal>>.Success(reporte, Mensajes.REPORTE_GENERADO_EXITOSAMENTE, MensajesHttp.CODIGO200);
            return respuesta;
        }
    }
}
