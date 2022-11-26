using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.Logging;
using GestaoPDF.Application.Services;
using GestaoPDF.Client.Components.Data.Views;
using GestaoPDF.Domain.Interfaces;
using GestaoPDF.Infra.Data.DataConfiguration;
using GestaoPDF.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using GestaoPDF.Client.Wpf.Services;

namespace GestaoPDF.Client.Wpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			var serviceCollection = new ServiceCollection();

			serviceCollection.AddWpfBlazorWebView();
			serviceCollection.AddMudServices();

			#if DEBUG
			serviceCollection.AddBlazorWebViewDeveloperTools();
			#endif

			serviceCollection.AddTransient<IFolderPicker, FolderPicker>();

			Constants.SetDatabasePath(AppDomain.CurrentDomain.BaseDirectory);

			serviceCollection.AddSingleton<ILeituraDocumentoRepository, LeituraDocumentoService>(x => new LeituraDocumentoService(new LeituraDocumentoRepository()));
			serviceCollection.AddSingleton<List<ArquivoView>>();
			

			Resources.Add("services", serviceCollection.BuildServiceProvider());
		}
	}
}
