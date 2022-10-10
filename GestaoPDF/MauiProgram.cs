using GestaoPDF.Data;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.WebView.Maui;
using GestaoPDF.Data.Views;

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

            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton<List<ArquivoView>>();

            return builder.Build();
        }
    }
}