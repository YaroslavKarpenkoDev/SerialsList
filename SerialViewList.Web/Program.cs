using SerialViewList.Web.Components;
using SerialViewList.Shared.Services;
using SerialViewList.Shared.ViewModels;
using SerialViewList.Web.Services;
using TestApp.Shared.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddTransient<MainViewModel>();

string dbPath = "serials_web.db3";
builder.Services.AddSingleton<IInternalStorage>(s => new InternalStorage(dbPath));


builder.Services.AddSingleton<IFormFactor, FormFactor>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(SerialViewList.Shared._Imports).Assembly);

app.Run();