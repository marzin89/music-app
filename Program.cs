var builder = WebApplication.CreateBuilder(args);

// Stöd för MVC
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

// Inställningar för sessioner
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(600);
    options.Cookie.HttpOnly = true;
});

var app = builder.Build();

// Stöd för statiska filer
app.UseStaticFiles();

app.UseCookiePolicy();

// Aktiverar sessionsvariabler
app.UseSession();

// Aktiverar routing
app.UseRouting();

// Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
);

app.Run();
