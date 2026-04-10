using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PersonelTakipSistemi; // For ApplicationState
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "PersonelTakipSistemi.Antiforgery";
});
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<PersonelTakipSistemi.Services.INotificationService, PersonelTakipSistemi.Services.NotificationService>();
builder.Services.AddScoped<PersonelTakipSistemi.Services.ILogService, PersonelTakipSistemi.Services.LogService>();
builder.Services.AddScoped<PersonelTakipSistemi.Services.IPasswordService, PersonelTakipSistemi.Services.PasswordService>();
builder.Services.AddScoped<PersonelTakipSistemi.Services.IPersonelLookupService, PersonelTakipSistemi.Services.PersonelLookupService>();
builder.Services.AddScoped<PersonelTakipSistemi.Services.IPersonelAuthorizationService, PersonelTakipSistemi.Services.PersonelAuthorizationService>();
builder.Services.AddScoped<PersonelTakipSistemi.Services.IPersonelAssignmentService, PersonelTakipSistemi.Services.PersonelAssignmentService>();
builder.Services.AddScoped<PersonelTakipSistemi.Services.IPersonelMaintenanceService, PersonelTakipSistemi.Services.PersonelMaintenanceService>();
builder.Services.AddScoped<PersonelTakipSistemi.Services.IGorevWorkflowService, PersonelTakipSistemi.Services.GorevWorkflowService>();
builder.Services.AddScoped<PersonelTakipSistemi.Services.ICihazService, PersonelTakipSistemi.Services.CihazService>();

builder.Services.AddHostedService<PersonelTakipSistemi.Services.NotificationBackgroundService>();
builder.Services.AddScoped<PersonelTakipSistemi.Services.IExcelService, PersonelTakipSistemi.Services.ExcelService>();
builder.Services.AddScoped<PersonelTakipSistemi.Services.IFileValidationService, PersonelTakipSistemi.Services.FileValidationService>();

// Connection String
string connectionString = builder.Configuration.GetConnectionString("TegmPersonelTakipDB") ?? "";

builder.Services.AddDbContext<PersonelTakipSistemi.Data.TegmPersonelTakipDbContext>(options => 
    options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure(3)));

// Authentication & Data Protection
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "dp_keys")))
    .SetApplicationName("PersonelTakipSistemi");

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "PersonelTakipSistemi.Auth";
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(4);
        
        options.Events = new CookieAuthenticationEvents
        {
            OnValidatePrincipal = async context =>
            {
                var user = context.Principal;
                if (user != null)
                {
                    // Absolute session cap (4 hours) to avoid "always-on" sessions even with sliding expiration.
                    var loginUtcClaim = user.FindFirst("LoginUtc");
                    if (loginUtcClaim != null &&
                        DateTimeOffset.TryParse(loginUtcClaim.Value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var loginUtc) &&
                        DateTimeOffset.UtcNow - loginUtc > TimeSpan.FromHours(4))
                    {
                        context.RejectPrincipal();
                        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        return;
                    }

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


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
 
