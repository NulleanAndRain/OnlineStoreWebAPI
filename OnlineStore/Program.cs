using Microsoft.EntityFrameworkCore;
using Nullean.OnlineStore.BllInterfaceProducts;
using Nullean.OnlineStore.BllInterfaceUsers;
using Nullean.OnlineStore.DalInterfaceProducts;
using Nullean.OnlineStore.DalInterfaceUsers;
using Nullean.OnlineStore.EFContext;
using Nullean.OnlineStore.ProductsDaoEF;
using Nullean.OnlineStore.UserDaoEF;
using Nullean.OnlineStore.UsersLogic;
using ProductsLogic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductsDao, ProductsDaoEF>();
builder.Services.AddScoped<IUserDao, UserDaoEF>();

builder.Services.AddScoped<IUserBll, UserLogic>();
builder.Services.AddScoped<IOrderBll, OrderLogic>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc();
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(ep =>
{
    ep.MapDefaultControllerRoute();
});
//app.MapControllers();

app.Run();