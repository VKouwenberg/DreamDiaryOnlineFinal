using DataAccessDDO.ModelsDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcDreams.Data;
using MvcDreams.Models;
using MvcMovie.Models;

var builder = WebApplication.CreateBuilder(args);

//dbcontext for MvcDreams
/*builder.Services.AddDbContext<MvcDreamsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcDreamsContext") ?? throw new InvalidOperationException("Connection string 'MvcDreamsContext' not found.")));
*/

//Mvc data context begone!


//dbcontext for DataAccessDDO
builder.Services.AddDbContext<DataAccessDDOContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DataAccessDDOContext") ?? throw new InvalidOperationException("Connection string 'DataAccessDDOContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();