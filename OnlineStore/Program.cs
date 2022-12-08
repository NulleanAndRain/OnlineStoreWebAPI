using Microsoft.EntityFrameworkCore;
using Nullean.OnlineStore.BllInterfaceUsers;
using Nullean.OnlineStore.DalInterfaceProducts;
using Nullean.OnlineStore.DalInterfaceUsers;
using Nullean.OnlineStore.EFContext;
using Nullean.OnlineStore.ProductsDaoEF;
using Nullean.OnlineStore.UserDaoEF;
using Nullean.OnlineStore.UsersLogic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductsDao, ProductsDaoEF>();
builder.Services.AddScoped<IUserDao, UserDaoEF>();

builder.Services.AddScoped<IUserBll, UserLogic>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();