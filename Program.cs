using Microsoft.EntityFrameworkCore;
using s17_l3.Models;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi i servizi al contenitore.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ScarpeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configura il pipeline delle richieste HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
