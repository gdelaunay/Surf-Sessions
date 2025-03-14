using SurfSessions;
using ElectronNET.API;
using SocketIOClient;

// Connexion EF Core et vérification que les tables existent
using AppDbContext context = new AppDbContext();
context.Database.EnsureCreated();

// Création du webApp builder
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseElectron(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Is optional, but you can use the Electron.NET API-Classes directly with DI (relevant if you want more encoupled code)
builder.Services.AddElectron();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


await app.StartAsync();

// Open the Electron-Window here
try
{
    await Electron.WindowManager.CreateWindowAsync();

}
catch (ConnectionException e)
{
    Console.BackgroundColor = ConsoleColor.White;
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("warn");
    Console.ResetColor();
    if(e.StackTrace != null){Console.Write(": " + e.StackTrace[6..].Split("(")[0] + "\n");}
    Console.WriteLine("      Failed to establish connection to Electron. \n");
}

app.WaitForShutdown();
