using GestaoPDF.Data;
using MudBlazor.Services;
using GestaoPDF.Data.Views;
using GestaoPDF.Data.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoPDF
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMudServices();
            builder.Services.AddMauiBlazorWebView();

            #if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            #endif

            #if WINDOWS
            builder.Services.AddTransient<IFolderPicker, GestaoPDF.Platforms.Windows.Services.FolderPicker>();
            #endif

            builder.Services.AddSingleton<List<ArquivoView>>();

            return builder.Build();
        }
    }
}