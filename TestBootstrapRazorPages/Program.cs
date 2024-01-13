using MySql.Data.MySqlClient;
using TestBootstrapRazorPages;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccessDDO.DatabaseSettings;
using DataAccessDDO.Repositories;
using Microsoft.Extensions.Hosting;
using LogicDDO.Services;
using TestBootstrapRazorPages.AppService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddApplicationServices(builder.Configuration); //injects all services. Called AppService in this project

// Configure the database connection
builder.Services.AddTransient<MySqlConnection>(
    _ => new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
