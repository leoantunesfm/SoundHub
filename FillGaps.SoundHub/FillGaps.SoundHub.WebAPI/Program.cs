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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SoundHubDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<Usuario, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<SoundHubDbContext>()
    .AddDefaultTokenProviders();

#region Injeção de Dependência (DI)

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

// Registra os Application Services
builder.Services.AddScoped<IArtistaService, ArtistaService>();

#endregion

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
