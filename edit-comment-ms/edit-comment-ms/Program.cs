using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using dotenv.net;
using dotenv.net.Utilities;
using edit_comment_ms.Data;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

// Configuración de PostgreSQL usando Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql($"Host={Environment.GetEnvironmentVariable("DB_HOST")};Port={Environment.GetEnvironmentVariable("DB_PORT")};Username={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};Database={Environment.GetEnvironmentVariable("DB_NAME")}"));

// Configuración de JWT para autenticación
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")))
        };
        // Permitir que el token venga sin "Bearer"
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault();
                if (!string.IsNullOrEmpty(token) && !token.StartsWith("Bearer "))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };
    });

// Habilitar CORS para permitir cualquier origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Agregar servicios para controlar la API
builder.Services.AddControllers();

// Configuración de Swagger para documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Edit Comment API", Version = "v1" });

    // Esquema de seguridad personalizado sin "Bearer"
    options.AddSecurityDefinition("Token", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Introduce solo el token JWT (sin 'Bearer ' delante)."
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Token"
                }
            },
            new string[] {}
        }
    });
});

// Construir la aplicación
var app = builder.Build();

// Configuración de Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Edit Comment API v1");
    c.RoutePrefix = "api-docs-comments-edit";  // URL de Swagger UI personalizada
});

app.UseCors("AllowAll");  // Activar CORS

app.UseAuthentication();  // Autenticación JWT
app.UseAuthorization();   // Autorización

app.MapControllers();  // Mapeo de controladores

// Ejecutar la aplicación en el puerto configurado
app.Run($"http://localhost:{Environment.GetEnvironmentVariable("PORT") ?? "3020"}");