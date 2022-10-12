using GestaoPDF.Data.Views;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Shared.Componentes;

public class ImportarArquivosBase : ComponentBase
{
    public DotNetObjectReference<ImportarArquivosBase> objRef;

    [Inject]
    private IJSRuntime JS { get; set; }

    [Inject]
    public List<ArquivoView> Arquivos { get; set; }

    [Inject]
    protected IDialogService DialogService { get; set; }

    protected string Pesquisa { get; set; }

    protected IBrowserFile fileSelecionado { get; set; }


    protected IList<IBrowserFile> files = new List<IBrowserFile>();

    protected bool _isOpen { get; set; }

    public ImportarArquivosBase()
    {
        objRef = DotNetObjectReference.Create(this);
        Arquivos = new List<ArquivoView>();
    }

    protected override void OnInitialized()
    {

    }

    protected void UploadFiles(InputFileChangeEventArgs e)
    {
        foreach (var file in e.GetMultipleFiles())
        {
            files.Add(file);

            Arquivos.Add(new ArquivoView(file.Name, 0, true, false));
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

    protected async Task VisualizarPDF(IBrowserFile file) 
    {
        var Index = files.IndexOf(file);

        await JS.InvokeVoidAsync("GerarURL", objRef, Index);
    }

    [JSInvokable]
    public async Task ExibirPDF(string URL)
    {
        var ParametersDialog = new DialogParameters();
        ParametersDialog.Add("URL", URL);

        var OptionDialog = new DialogOptions();
        OptionDialog.MaxWidth = MaxWidth.Medium;
        OptionDialog.CloseButton = true;
        OptionDialog.DisableBackdropClick = true;
        OptionDialog.Position = DialogPosition.Center;

        var Dialog = DialogService.Show<VisualizarPDF>($"Visualizar PDF", ParametersDialog, OptionDialog);
        var Result = await Dialog.Result;
    }
}
