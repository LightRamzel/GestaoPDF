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

            builder.Services.AddMudServices();
            builder.Services.AddMauiBlazorWebView();

            #if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

#if WINDOWS
            builder.Services.AddTransient<IFolderPicker, GestaoPDF.Client.Maui.Platforms.Windows.Services.FolderPicker>();
#endif
            Constants.SetDatabasePath(FileSystem.AppDataDirectory);

            builder.Services.AddSingleton<ILeituraDocumentoRepository, LeituraDocumentoService>(x => new LeituraDocumentoService(new LeituraDocumentoRepository()));
            builder.Services.AddSingleton<List<ArquivoView>>();

            return builder.Build();
        }
    }
}