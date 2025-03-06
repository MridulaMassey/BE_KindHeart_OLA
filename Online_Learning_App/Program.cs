//using Application.Interfaces;
using Application.Services;
//using Domain.Interfaces;
//using Infrastructure.Data;
//using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_App.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Online_Learning_APP.Application.Services;
using AuthenticationApp.Application.Services;


var builder = WebApplication.CreateBuilder(args);

// Configure database
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Online_Learning_App.Infrastructure")));


// Identity setup
builder.Services.AddIdentity<ApplicationUser, Role>(
    options => {
        options.ClaimsIdentity.UserIdClaimType = null;
        options.ClaimsIdentity.RoleClaimType = null;
    }
    )
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Cookie Authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/auth/login";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

// Register services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();