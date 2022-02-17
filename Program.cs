var builder = WebApplication.CreateBuilder(args);

// St�d f�r MVC
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

// Inst�llningar f�r sessioner
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(600);
    options.Cookie.HttpOnly = true;
});

var app = builder.Build();

// St�d f�r statiska filer
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
