

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json.Serialization;
using Tickets.Application.Services;
using Tickets.Application.Services.HistoryTicketCase;
using Tickets.Domain.Interfaces;
using Tickets.Domain.Interfaces.Repositories;
using Tickets.Infrastructure.Identity;
using Tickets.Infrastructure.Persistence.Repostiories;
using Tickets.Infrastructure.Pesistence;
using Tickets.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

});

builder.Services.AddIdentity<AppIdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
}); 
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Estamos inyectando servicios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IRolePository, RoleRepository>();
builder.Services.AddScoped<ITickesRepository, TicketsRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TicketsCase>();
builder.Services.AddScoped<HistoryService>();
builder.Services.AddScoped<IHistoryTicketRepository, HistoryTicketRepository>();

    

builder.Services.AddAuthentication(options=>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme =    JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtIssuer"],
            ValidAudience = builder.Configuration["JwtAudience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Creando data por defecto
using (var scope = app.Services.CreateScope())
{
    var roleRepository = scope.ServiceProvider.GetRequiredService<IRolePository>();
    var roles = new[] { "Admin", "User" };

    foreach(var role in roles) 
    {
        if (!await roleRepository.RoleExistsAsync(role))
        {
            await roleRepository.CreateRole(role);
        }
    }

    var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

    if(!await userRepository.UserExists("admin@admin.com"))
    {
        var result = userRepository.CreateUser(
            new Tickets.Domain.Entities.Usuario()
            {
               Email = "admin@admin.com",
               Password ="Admin123!",
               FirtsName= "Admin",
               LastName ="Admin"
            }).Result;

       var resultUserToRole= userRepository.AddToRoleAsync(result, "Admin").Result;
    }
}


app.Run();
