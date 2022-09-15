using ContadorPaginasPDF.Aplicacao.Helper;
using ContadorPaginasPDF.Entidade;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace ContadorPaginasPDF
{
    public partial class MainWindow : Window
    {
        private readonly LeituraHelper _leituraHelper;
        public MainWindow()
        {
            InitializeComponent();

            _leituraHelper = new LeituraHelper();
        }

        private void CaminhoPasta()
        {
            var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            
            
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string caminho = @"C:\Users\lucia\Desktop\Documentos Assinados";

            var arquivosCaminho = _leituraHelper.CaminhosArquivos(caminho);
            ObservableCollection<Documento> documentos = new ObservableCollection<Documento>();

            await _leituraHelper.FazerLeituraAsync(arquivosCaminho);
        }
    }
}
