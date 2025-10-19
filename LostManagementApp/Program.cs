using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LostManagementApp.DatabaseContext;
using LostManagementApp.Dao;
using LostManagementApp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DBˆË‘¶ŠÖŒW’Ç‰Á
builder.Services.AddDbContext<LostContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ILostDao, LostDao>();
builder.Services.AddScoped<ILoginDao, LoginDao>();
builder.Services.AddScoped<LostService>();
builder.Services.AddScoped<LoginService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "lost",
    pattern: "{controller=Home}/{action=Lost}/{id?}")
    .WithStaticAssets();


app.Run();
