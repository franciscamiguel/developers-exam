using Domain.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using FluentValidation;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Validation;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


services.AddControllersWithViews();

// Register DbContext
services.AddDbContext<SqlDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    }));

// Register Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register Services
services.AddScoped<IEmailService, EmailService>();
services.AddScoped<IUserService, UserService>();

// Register Validators
services.AddTransient<IValidator<UserCreateDto>, UserCreateDtoValidator>();
services.AddTransient<IValidator<UserUpdateDto>, UserUpdateDtoValidator>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();