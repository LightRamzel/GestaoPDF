using GestaoPDF.Data.Views;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Shared.Componentes;

public class ImportarArquivosBase : ComponentBase
{
    [Inject]
    public List<ArquivoView> Arquivos { get; set; }

    protected string Pesquisa { get; set; }
    protected IBrowserFile fileSelecionado { get; set; }

    protected IList<IBrowserFile> files = new List<IBrowserFile>();

    protected bool _isOpen { get; set; }

    public ImportarArquivosBase()
    {
        Arquivos = new List<ArquivoView>();
    }

    protected override void OnInitialized()
    {
        GerarListaVirtual();
    }

    private void GerarListaVirtual()
    {
        for (int i = 1; i < 50; i++)
            Arquivos.Add(new ArquivoView($"Arquivo_{i}.pdf", (i * 2), true, false));
    }

    protected void UploadFiles(InputFileChangeEventArgs e)
    {
        foreach (var file in e.GetMultipleFiles())
        {
            files.Add(file);
        }
    }

    protected bool Filtro(IBrowserFile element) =>
        Filtro(element, Pesquisa);

    private bool Filtro(IBrowserFile element, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }
}
