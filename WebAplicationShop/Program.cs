

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistense.Data;
using Services.EntitiesServices.CategoryServices;
using Services.EntitiesServices.CustomerService;
using Services.EntitiesServices.OrderServices;
using Services.EntitiesServices.ProductServices;
using Services.MapServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(connection));

builder.Services.AddTransient<IProductService,ProductService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IOrderService,OrderService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddAutoMapper(typeof(EntitiesProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
