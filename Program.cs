using BackendAPI.Configurations;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BackendAPI.Middlewares;
using BackendAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Validar las configuraciones de DbContext
var connectionStringsSettings = builder.Configuration.GetSection("ConnectionStrings");
var defaultConnection = connectionStringsSettings["DefaultConnection"];
if (string.IsNullOrEmpty(defaultConnection))
{
    throw new InvalidOperationException("La cadena de conexión por defecto ('ConnectionStrings:DefaultConnection') no está configurada en appsettings.json.");
}

// Configurar DbContext con la cadena de conexión
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(defaultConnection));

// Validar las configuraciones de JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = jwtSettings["Key"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

if (string.IsNullOrEmpty(key))
{
    throw new InvalidOperationException("La clave JWT ('Jwt:Key') no está configurada en appsettings.json.");
}

if (string.IsNullOrEmpty(issuer))
{
    throw new InvalidOperationException("El emisor JWT ('Jwt:Issuer') no está configurado en appsettings.json.");
}

if (string.IsNullOrEmpty(audience))
{
    throw new InvalidOperationException("La audiencia JWT ('Jwt:Audience') no está configurada en appsettings.json.");
}

// Configurar JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) // Leer la clave secreta desde appsettings.json
        };
    });

builder.Services.AddAuthorization();

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
    
// Reemplaza el sistema de logging predeterminado con Serilog
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackendAPI", Version = "v1" });

    // Configurar Swagger para leer los comentarios XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    // Configurar esquema de seguridad para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT recibido al autenticarse."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Register custom services
DependencyInjectionConfig.RegisterServices(builder.Services);

var app = builder.Build();

// Aplicar migraciones automáticamente al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); // Aplica las migraciones pendientes
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackendAPI v1"));
}

// Middleware de environment
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseAuthentication();
app.UseAuthorization();

// Middleware de autenticación personalizado
app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();

app.Run();