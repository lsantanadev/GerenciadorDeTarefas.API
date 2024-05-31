using GerenciadorDeTarefas.API.Auth;
using GerenciadorDeTarefas.API.Commands;
using GerenciadorDeTarefas.API.Repositories;
using GerenciadorDeTarefas.API.DataBase;
using GerenciadorDeTarefas.API.Queries.ObterUsuario;
using GerenciadorDeTarefas.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyHeader()
    .AllowCredentials()
    .AllowAnyMethod()
    .WithExposedHeaders(new[] {
        "X-Custom-Header",
        "Location",
        "Content-Disposition",
        "Content-Length",
    })
    .SetIsOriginAllowed(origin => true);
}));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var configuration = builder.Configuration;

builder.Services.AddDbContext<GerenciadorDbContext>(options =>
    options.UseSqlite(configuration.GetConnectionString("TarefasCs") ?? "DataSource=banco.db;Cache=Shared"));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(CriarTarefaCommand).Assembly); });

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GerenciadorDeTarefas.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "LSdev",
            Email = "larasantanadev@gmail.com"
        }
    });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer."
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },

        new string[] {}
    }
    });

});

// Configuração de AUTENTICAÇÃO JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GerenciadorDeTarefas.API v1");
    });
}

app.UseCors("MyPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
