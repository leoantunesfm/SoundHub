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
using FillGaps.SoundHub.WebAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var webAppUrl = builder.Configuration["WebAppUrl"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("WebAppPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7018/", webAppUrl)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SoundHubDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<Usuario, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
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

// Registra os Reposit�rios do Cat�logo
builder.Services.AddScoped<IArtistaRepository, ArtistaRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IMusicaRepository, MusicaRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();

// Registra os Reposit�rios de Engajamento do Usu�rio
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();

// Registra os Reposit�rios de Faturamento
builder.Services.AddScoped<IPlanoRepository, PlanoRepository>();
builder.Services.AddScoped<IAssinaturaRepository, AssinaturaRepository>();

#endregion

#region Camada de Aplica��o (Application Services)

builder.Services.AddScoped<IArtistaService, ArtistaService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IMusicaService, MusicaService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IPlanoService, PlanoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAssinaturaService, AssinaturaService>();

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SoundHubDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrations.");
    }
}

await DbInitializer.SeedAsync(app);

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SoundHub API V1");

    options.RoutePrefix = string.Empty;
});

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors("WebAppPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
