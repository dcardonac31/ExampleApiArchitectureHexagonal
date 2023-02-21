using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonasMS.Application.Configuration;
using PersonasMS.Application.Interfaces;
using PersonasMS.Application.Services;
using PersonasMS.Domain.Dto;
using PersonasMS.Domain.Interfaces.Repositories;
using PersonasMS.Infraestructure.API.Extensions;
using PersonasMS.Infraestructure.Data.DatabaseContext;
using PersonasMS.Infraestructure.Data.Repositories;
using PersonasMS.Infraestructure.Data.UnitOfWork;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ServiceExtensions.RegisterConfiguration(builder.Configuration);
//Automapper
builder.Services.RegisterAutoMapper();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Context SQL Server
// Lectura del archivo appsettings.json

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

// Inicializa DataConext con la cadena de conexión.
var connectionString = configuration.GetConnectionString("ConnPersonasMS");

builder.Services.AddDbContext<PersonasMsDbContext>(options =>
    options.UseSqlServer(connectionString));

#endregion Context SQL Server

#region Register Dependency Injection
//DataContext to database
builder.Services.AddScoped<DbContext, PersonasMsDbContext>();
builder.Services.AddScoped<ILogger, Logger<PersonasMsDbContext>>();

// CustomerRepository await UnitofWork parameter ctor explicit
builder.Services.AddScoped<UnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<IBaseService<AsignacionCreateDto, AsignacionDto>, AsignacionService>();
builder.Services.AddScoped<IBaseService<CargoCreateDto, CargoDto>, CargoService>();
builder.Services.AddScoped<IBaseService<ClienteCreateDto, ClienteDto>, ClienteService>();
builder.Services.AddScoped<IBaseService<DepartamentoCreateDto, DepartamentoDto>, DepartamentoService>();
builder.Services.AddScoped<IBaseService<HistoricoCargoCreateDto, HistoricoCargoDto>, HistoricoCargoService>();
builder.Services.AddScoped<IBaseService<IngresoRetiroCreateDto, IngresoRetiroDto>, IngresoRetiroService>();
builder.Services.AddScoped<IBaseService<MunicipioCreateDto, MunicipioDto>, MunicipioService>();
builder.Services.AddScoped<IBaseService<PaisCreateDto, PaisDto>, PaisService>();
builder.Services.AddScoped<IBaseService<PersonaCreateDto, PersonaDto>, PersonaService>();
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IBaseService<SeguimientoCreateDto, SeguimientoDto>, SeguimientoService>();

// Infraestructure
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
#endregion

#region Configure Cors
// Configure Cors

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.WithHeaders("authorization", "accept", "content-type", "origin");
            builder.SetIsOriginAllowed((_) => true);
        });
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
