using AppDeCastracao.Components;
using AppDeCastracao.Db;
using AppDeCastracao.Entities;
using AppDeCastracao.Services;
using Radzen;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//UI Radzen
builder.Services.AddRadzenComponents();
builder.Services.AddServerSideBlazor();

//Config Repository

//Config Services
builder.Services.AddControllers();
builder.Services.AddScoped<GatoService>();
builder.Services.AddScoped<DonoService>();
builder.Services.AddScoped<StatusService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/acesso-negado";
        options.ExpireTimeSpan = TimeSpan.FromDays(7); // Login persistente
        options.SlidingExpiration = true;
    });
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

//Config Database
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.UseStaticFiles();

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    if (!context.Usuarios.Any())
//    {
//        var senhaHash = BCrypt.Net.BCrypt.HashPassword("Flores08.");
//        var superAdmin = new Usuario
//        {
//            Nome = "Guilherme Flores",
//            Email = "guilherme.flores@rede.ulbra.br",
//            SenhaHash = senhaHash,
//            Role = "SuperAdministrador"
//        };

//        context.Usuarios.Add(superAdmin);
//        context.SaveChanges();
//    }
//}

app.Run();




