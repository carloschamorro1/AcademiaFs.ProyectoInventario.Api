using AcademiaFs.ProyectoInventario.Api._Features.Empleados;
using AcademiaFs.ProyectoInventario.Api._Features.Estados;
using AcademiaFs.ProyectoInventario.Api._Features.Perfiles;
using AcademiaFs.ProyectoInventario.Api._Features.Permisos;
using AcademiaFs.ProyectoInventario.Api._Features.Productos;
using AcademiaFs.ProyectoInventario.Api._Features.ProductosLotes;
using AcademiaFs.ProyectoInventario.Api._Features.Reportes;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventario;
using AcademiaFs.ProyectoInventario.Api._Features.SalidasInventarioDetalles;
using AcademiaFs.ProyectoInventario.Api._Features.Usuarios;
using AcademiaFs.ProyectoInventario.Api.Domain.SalidasInventario;
using AcademiaFs.ProyectoInventario.Api.Domain.SalidasInventarioDetalles;
using AcademiaFs.ProyectoInventario.Api.Domain.Usuarios;
using AcademiaFs.ProyectoInventario.Api.Infrastructure;
using AcademiaFs.ProyectoInventario.Api.Infrastructure.Inventario;
using Farsiman.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerForFsIdentityServer(opt =>
{
    opt.Title = "Sistema de administración de inventario";
    opt.Description = "Api para la administración del sistema de inventario";
    opt.Version = "v1.0";
});

builder.Services.AddFsAuthService(configureOptions =>
{
    configureOptions.Username = builder.Configuration.GetFromENV("Configurations:FsIdentityServer:Username");
    configureOptions.Password = builder.Configuration.GetFromENV("Configurations:FsIdentityServer:Password");
});

var connectionString = builder.Configuration.GetConnectionString("local");
builder.Services.AddDbContext<InventarioDbContext>(opciones => opciones.UseSqlServer(connectionString));

builder.Services.AddScoped<UnitOfWorkBuilder, UnitOfWorkBuilder>();

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddTransient<IEstadosService, EstadosService>();
builder.Services.AddTransient<IEmpleadoService, EmpleadosService>();
builder.Services.AddTransient<IPerfilesService, PerfilesService>();
builder.Services.AddTransient<IPermisosService, PermisosService>();
builder.Services.AddTransient<IProductosService, ProductosService>();
builder.Services.AddTransient<IProductosLotesService, ProductosLotesService>();
builder.Services.AddTransient<ISalidaInventarioService, SalidaInventarioService>();
builder.Services.AddTransient<ISalidaInventarioDomain, SalidaInventarioDomain>();
builder.Services.AddTransient<ISalidaInventarioDetalleService, SalidaInventarioDetalleService>();
builder.Services.AddTransient<ISalidaInventarioDetallesDomain, SalidaInventarioDetallesDomain>();
builder.Services.AddTransient<IReportesService, ReportesService>();
builder.Services.AddTransient<UsuarioService>();
builder.Services.AddTransient<UsuariosDomain>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithFsIdentityServer();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
