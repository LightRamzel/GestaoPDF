using GestaoPDF.Application.Helpers;
using GestaoPDF.Client.Components.Data.Views;
using GestaoPDF.Domain.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;

namespace GestaoPDF.Client.Components.Shared.Componentes;

public class ImportarArquivosBase : ComponentBase
{
    private readonly DotNetObjectReference<ImportarArquivosBase> _objRef;

    [Inject]
    private IFolderPicker FolderPicker { get; set; } = null!;

    [Inject]
    private IJSRuntime JS { get; set; } = null!;

    [Inject]
    protected IDialogService DialogService { get; set; } = null!;

    [Inject]
    private ILeituraDocumentoRepository AppService { get; set; } = null!;

    [Inject]
    protected List<ArquivoView> Arquivos { get; set; } = null!;

    protected string? Pesquisa { get; set; }

    protected FileView? FileSelecionado;

    protected List<FileView> Files = new();

    protected bool IsOpen { get; set; }

    private string? _directoryPath;

    private readonly LeituraHelper _leituraHelper;

    public ImportarArquivosBase()
    {
        _leituraHelper = new LeituraHelper();
        _objRef = DotNetObjectReference.Create(this);
    }

    //protected async override Task OnInitializedAsync()
    //{
    //    //Testar leitura da tabela no banco de dados
    //    var leituras = await AppService.SelectAllAsync();

    //    //Testar seleção por um único item
    //    if (leituras != null && leituras.Count > 0)
    //    {
    //        var idLeitura = leituras.Select(x => x.Id).FirstOrDefault();

    //        var leitura = await AppService.SelectByIdAsync(idLeitura);
    //    }
    //}

    protected async void GetDirectoryPath() 
    {
        _directoryPath = await FolderPicker.PickFolder();

        if (string.IsNullOrEmpty(_directoryPath)) 
            return;

        var arquivos = _leituraHelper.CaminhosArquivos(_directoryPath).Select(x => new ArquivoView(x)).ToList();

        await _leituraHelper.FazerLeituraAsync(arquivos);

        Arquivos.AddRange(arquivos);

        // Teste de salvar leitura
        //await SalvarLeitura();

        StateHasChanged();
    }

    protected async Task SalvarLeitura()
    {
        var leitura = new Domain.Entities.LeituraDocumento($"Leitura {DateTime.Now:dd-MM-yyyy HH:mm:ss}"
            , ""//DeviceInfo.Name
            , "" /*DeviceInfo.Current.Model*/);
        leitura.SetDocumentos(Arquivos);

        await AppService.InsertAsync(leitura);
    }

    protected void UploadFiles(InputFileChangeEventArgs e)
    {
        foreach (var file in e.GetMultipleFiles())
        {
            var NovoObjeto = new FileView(file);

            Files.Add(NovoObjeto);
            //Arquivos.Add(new ArquivoView(file.Name, 0, true, false, NovoObjeto));
        }

        StateHasChanged();
    }

    protected bool Filtro(FileView element) 
    {
        if (string.IsNullOrWhiteSpace(Pesquisa))
            return true;
        if (element.File.Name.Contains(Pesquisa, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

    protected async Task RemoverPDF(FileView file)
    {
        var Arquivo = Arquivos.Where(x => x.FileId == file.Id).FirstOrDefault();

        if (!Equals(Arquivo, null)) 
        {
            var Index = Files.IndexOf(file);

            Files.Remove(file);
            Arquivos.Remove(Arquivo);

            await JS.InvokeVoidAsync("RemoverFile", Index);

            StateHasChanged();
        }
    }

    protected async Task VisualizarPDF(FileView file)
    {
        var Index = Files.IndexOf(file);

        await JS.InvokeVoidAsync("GerarURL", _objRef, Index);
    }

    [JSInvokable]
    public async Task ExibirPDF(string URL)
    {
        var ParametersDialog = new DialogParameters
        {
            { "URL", URL }
        };

        var OptionDialog = new DialogOptions
        {
            MaxWidth = MaxWidth.Medium,
            CloseButton = true,
            DisableBackdropClick = true,
            Position = DialogPosition.Center
        };

        var Dialog = DialogService.Show<VisualizarPDF>($"Visualizar PDF", ParametersDialog, OptionDialog);
        var Result = await Dialog.Result;
    }
}
