using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp_MVC.Data;
using MyApp_MVC.Mapping;
using MyApp_MVC.Repository;
using MyApp_MVC.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnectionstring")));
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<Mappingconfig>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage(); // Always show developer page for debugging deployment

/*
if (app.Environment.IsDevelopment())
{
    // app.UseDeveloperExceptionPage(); 
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
*/

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Items}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
