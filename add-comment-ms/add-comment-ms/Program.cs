using Npgsql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;
using Microsoft.OpenApi.Models;

public class Program
{
    public static void Main(string[] args)
    {
        // Cargar el archivo .env
        Env.Load();

        var builder = WebApplication.CreateBuilder(args);

        // Configura la URL antes de construir la app
        builder.WebHost.UseUrls("http://0.0.0.0:3019");

        // Configuraci�n de CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        // Configuraci�n de la base de datos
        builder.Services.AddScoped<NpgsqlConnection>(sp =>
        {
            var connString = $"Host={Env.GetString("DB_HOST")};Port={Env.GetString("DB_PORT")};Username={Env.GetString("DB_USER")};Password={Env.GetString("DB_PASSWORD")};Database={Env.GetString("DB_NAME")};SSL Mode={(Env.GetString("DB_SSL") == "true" ? "Require" : "Disable")};";
            return new NpgsqlConnection(connString);
        });

        // Configuraci�n de JWT
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("JWT_SECRET")))
                };
            });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Comments API", Version = "v1" });

            // Configuraci�n de seguridad para JWT sin "Bearer"
            c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "JWT",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Introduce solo el token JWT, sin la palabra 'Bearer'."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "JWT"
                        }
                    },
                    new string[] {}
                }
            });
        });

        var app = builder.Build();

        // Middleware para agregar "Bearer " si falta en el header Authorization
        app.Use(async (context, next) =>
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var auth = context.Request.Headers["Authorization"].ToString();
                if (!auth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    context.Request.Headers["Authorization"] = $"Bearer {auth}";
                }
            }
            await next();
        });

        // Habilitar Swagger
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Comments API V1");
            c.RoutePrefix = "api-docs-add-comment";
        });

        // Configurar CORS
        app.UseCors("AllowAll");

        // Autenticaci�n y autorizaci�n
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}