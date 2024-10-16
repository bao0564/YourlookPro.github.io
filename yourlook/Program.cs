using Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using yourlook.MenuKid;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5089); // HTTP port
    serverOptions.ListenAnyIP(7078, listenOptions => // HTTPS port
    {
        listenOptions.UseHttps();
    });
});
builder.Services.AddDbContext<YourlookContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectedDb"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = "834180802158-c18fs2qk244bpi3349rrs84jeo9datlu.apps.googleusercontent.com";
    options.ClientSecret = "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";
});
// Add services to the container.
builder.Services.AddControllersWithViews();

//add Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//builder ViewComponent
builder.Services.AddScoped<IUploadPhoto, UploadPhoto>();
builder.Services.AddScoped<ISanPhamHot, SanPhamHot>();
builder.Services.AddScoped<IFlashSell, FlashSell>();
builder.Services.AddScoped<ICategory, Category>();
builder.Services.AddScoped<ISize, Size>();
builder.Services.AddScoped<IColor, Color>();
builder.Services.AddScoped<ISanPhamNew, SanPhamNew>();
builder.Services.AddScoped<IAds, Ads>();
builder.Services.AddScoped<IPage1, Page1>();
builder.Services.AddScoped<IPage2, Page2>();
builder.Services.AddScoped<IPage3, Page3>();
builder.Services.AddScoped<IPage4, Page4>();
//builder.Services.AddScoped<>
var app = builder.Build();

//b? ?o?n n�y n?u ch? ch?y tr�n lap
builder.Configuration.AddEnvironmentVariables();
builder.Environment.EnvironmentName = "Production";  // Thi?t l?p m�i tr??ng th�nh Production

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
app.UseSession();
//routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();