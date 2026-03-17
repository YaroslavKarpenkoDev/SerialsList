using Microsoft.Extensions.Logging;
using SerialViewList.Shared.Services;
using SerialViewList.Services;
using SerialViewList.Shared.ViewModels;
using TestApp.Shared.Services.Implementations;

namespace SerialViewList;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "serials.db3");
        
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        // Add device-specific services used by the SerialViewList.Shared project
        builder.Services.AddSingleton<IFormFactor, FormFactor>();
        builder.Services.AddSingleton<IInternalStorage>(s => new InternalStorage(dbPath));
        
        builder.Services.AddTransient<MainViewModel>();
        

        builder.Services.AddMauiBlazorWebView();
        
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}