using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PeonValienteMVC.Data;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using PeonValienteMVC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Deshabilitar confirmaci�n de usuario. Configurar Identity para utilizar roles cf Practica7 p.46


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()//P7 p46
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddSession();//*

var app = builder.Build();

var supportedCultures = new[] {new CultureInfo("es-ES")};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture=new RequestCulture("es-ES"),
    SupportedCultures=supportedCultures,
    SupportedUICultures=supportedCultures
}
;
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();//*
app.UseRequestLocalization(localizationOptions);
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

// Crear los roles y el administrador predeterminados //P7 p47
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.InitializeAsync(services).Wait();
}

app.Run();
