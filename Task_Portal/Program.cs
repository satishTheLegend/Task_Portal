using DeviceId;
using Microsoft.EntityFrameworkCore;
using Task_Portal.AppConfig;
using Task_Portal.Database;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
string Connection = string.Empty;
string deviceId = new DeviceIdBuilder().AddMachineName().ToString();
if (AppConfig.PersonalPCUniqueId == deviceId)
{
    Connection = "DefaultConnection";
}
else
{
    Connection = "OfficeDefaultConnection";
}
builder.Services.AddDbContext<ApplicationDBContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString(Connection)));
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
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
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
