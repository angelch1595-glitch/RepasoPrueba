
using BackendComedor.Data;
using BackendComedor.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using BackendComedor.Services.Interfaces;
using BackendComedor.Repositories.Interfaces;
using BackendComedor.Repositories;
using BackendComedor.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<ComedorDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("ComedorDb")));
builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<ComedorDbContext>();
builder.Services.AddScoped<IProducto, RepositorioProductoSQL>();
builder.Services.AddScoped<IServicioProducto, ServicioProducto>();
builder.Services.AddScoped<ITokenServico, TokenServicio>();
builder.Services.AddScoped<IAuthServicio, AuthServicio>();
builder.Services.AddScoped<IRolServicio, RolServicio>();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Pega solo el token, sin la palabra Bearer"
    });

    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});
// Autenticación: "los usuarios se identifican con un JWT" 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,           // valida quién emitió el token 
            ValidateAudience = true,         // valida para quién es 
            ValidateLifetime = true,         // rechaza tokens vencidos 
            ValidateIssuerSigningKey = true, // valida la firma 
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
builder.Services.AddOpenApi();
// Habilita las reglas de permisos ([Authorize], roles, políticas) 
builder.Services.AddAuthorization();
// ---------- PIPELINE ---------- 
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    await IdentitySeeder.SeedAsync(scope.ServiceProvider, app.Configuration);
}
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.Redirect("/swagger"));
}
app.UseAuthentication();   // 1.º: ¿quién eres? (lee el token) 
app.UseAuthorization();    // 2.º: ¿tienes permiso? 
app.MapControllers();
app.Run();