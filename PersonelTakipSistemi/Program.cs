using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PersonelTakipSistemi; // For ApplicationState

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<PersonelTakipSistemi.Services.INotificationService, PersonelTakipSistemi.Services.NotificationService>();
builder.Services.AddScoped<PersonelTakipSistemi.Services.ILogService, PersonelTakipSistemi.Services.LogService>();
builder.Services.AddScoped<PersonelTakipSistemi.Services.DataSeedingService>();
builder.Services.AddHostedService<PersonelTakipSistemi.Services.NotificationBackgroundService>();

// Connection String
string connectionString = builder.Configuration.GetConnectionString("TegmPersonelTakipDB") ?? "";
Console.WriteLine($"[DB INIT] Using Connection String: {connectionString}");

builder.Services.AddDbContext<PersonelTakipSistemi.Data.TegmPersonelTakipDbContext>(options => 
    options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure(3)));

// Authentication & Data Protection
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "dp_keys")))
    .SetApplicationName("PersonelTakipSistemi");

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        
        options.Events = new CookieAuthenticationEvents
        {
            OnValidatePrincipal = async context =>
            {
                var user = context.Principal;
                if (user != null)
                {
                    // "InstanceId" claim'i var mı kontrol et
                    var instanceIdClaim = user.FindFirst("InstanceId");
                    
                    // Eğer claim varsa (demek ki RememberMe seçilmemiş),
                    // Ve bu claim şu anki sunucu InstanceId'si ile eşleşmiyorsa (demek ki sunucu restart yemiş)
                    if (instanceIdClaim != null && instanceIdClaim.Value != ApplicationState.InstanceId)
                    {
                        // Oturumu reddet (Logout)
                        context.RejectPrincipal();
                        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    }
                }
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}









// Seed Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PersonelTakipSistemi.Data.TegmPersonelTakipDbContext>();
        PersonelTakipSistemi.Data.DbSeeder.Seed(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
