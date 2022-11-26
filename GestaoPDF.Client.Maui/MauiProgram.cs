using MudBlazor.Services;
using GestaoPDF.Client.Components.Data.Views;
using GestaoPDF.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GestaoPDF.Application.Services;
using GestaoPDF.Client.Components.Data;
using GestaoPDF.Domain.Entities;
using GestaoPDF.Infra.Data.DataConfiguration;
using GestaoPDF.Infra.Data.Repository;
using GestaoPDF.Client.Components.Extensions;

namespace GestaoPDF.Client.Maui
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

            builder.Services.AddGestaoPdfComponents(FileSystem.AppDataDirectory);

            #if WINDOWS
            builder.Services.AddTransient<IFolderPicker, GestaoPDF.Client.Maui.Platforms.Windows.Services.FolderPicker>();
            #endif

			builder.Services.AddMauiBlazorWebView();

            #if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }
    }
}