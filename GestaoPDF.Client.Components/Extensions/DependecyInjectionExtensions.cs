using GestaoPDF.Application.Services;
using GestaoPDF.Client.Components.Data.Views;
using GestaoPDF.Domain.Interfaces;
using GestaoPDF.Infra.Data.DataConfiguration;
using GestaoPDF.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace GestaoPDF.Client.Components.Extensions
{
	public static class DependecyInjectionExtensions
	{
		public static IServiceCollection AddGestaoPdfComponents(this IServiceCollection services, string pathDb)
		{
			services.AddMudServices();

			Constants.SetDatabasePath(pathDb);

			services.AddSingleton<ILeituraDocumentoRepository, LeituraDocumentoService>(x => new LeituraDocumentoService(new LeituraDocumentoRepository()));
			services.AddSingleton<List<ArquivoView>>();

			return services;
		}
	}
}
