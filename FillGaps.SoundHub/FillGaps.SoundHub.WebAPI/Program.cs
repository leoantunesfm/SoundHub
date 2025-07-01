using FillGaps.SoundHub.Application.Services.Implementations;
using FillGaps.SoundHub.Application.Services.Interfaces;
using FillGaps.SoundHub.Domain.Billing.Repositories;
using FillGaps.SoundHub.Domain.Catalog.Repositories;
using FillGaps.SoundHub.Domain.Identity;
using FillGaps.SoundHub.Domain.SharedKernel.Repositories;
using FillGaps.SoundHub.Domain.UserEngagement.Repositories;
using FillGaps.SoundHub.Infrastructure.Data;
using FillGaps.SoundHub.Infrastructure.Repositories.Billing;
using FillGaps.SoundHub.Infrastructure.Repositories.Catalog;
using FillGaps.SoundHub.Infrastructure.Repositories.UserEngagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SoundHubDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<Usuario, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<SoundHubDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
});

#region Camada de Infraestrutura (Repositories & UnitOfWork)

// Registra a Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registra os Repositórios do Catálogo
builder.Services.AddScoped<IArtistaRepository, ArtistaRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IMusicaRepository, MusicaRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();

// Registra os Repositórios de Engajamento do Usuário
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();

// Registra os Repositórios de Faturamento
builder.Services.AddScoped<IPlanoRepository, PlanoRepository>();
builder.Services.AddScoped<IAssinaturaRepository, AssinaturaRepository>();

#endregion

#region Camada de Aplicação (Application Services)

builder.Services.AddScoped<IArtistaService, ArtistaService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IMusicaService, MusicaService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IPlanoService, PlanoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

#endregion

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu_token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
