using MySql.Data.MySqlClient;
using TestBootstrapRazorPages;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddApplicationServices(builder.Configuration); //injects all services. Called AppService in this project

// Configure the database connection
builder.Services.AddTransient<MySqlConnection>(
    _ => new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))
);


WebApplication app = builder.Build();

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
