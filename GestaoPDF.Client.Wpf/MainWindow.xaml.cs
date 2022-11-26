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
using GestaoPDF.Client.Components.Extensions;

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

			serviceCollection.AddGestaoPdfComponents(AppDomain.CurrentDomain.BaseDirectory);

			serviceCollection.AddTransient<IFolderPicker, FolderPicker>();
			
			serviceCollection.AddWpfBlazorWebView();
			
			#if DEBUG
			serviceCollection.AddBlazorWebViewDeveloperTools();
			#endif

			Resources.Add("services", serviceCollection.BuildServiceProvider());
		}
	}
}
