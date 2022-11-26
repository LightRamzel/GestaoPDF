using GestaoPDF.Application.Helpers;
using GestaoPDF.Application.Services;
using GestaoPDF.Client.Components.Data.Views;
using GestaoPDF.Domain.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Client.Components.Shared.Componentes;

public class ImportarArquivosBase : ComponentBase
{
    public DotNetObjectReference<ImportarArquivosBase> objRef;

    [Inject]
    private IFolderPicker _folderPicker { get; set; }

    [Inject]
    private IJSRuntime JS { get; set; }

    [Inject]
    protected IDialogService DialogService { get; set; }

    [Inject]
    private ILeituraDocumentoRepository AppService { get; set; }

    [Inject]
    protected List<ArquivoView> Arquivos { get; set; }

    protected string Pesquisa { get; set; }

    protected FileView fileSelecionado { get; set; }


    protected List<FileView> files = new List<FileView>();

    protected bool _isOpen { get; set; }

    private string DirectoryPath { get; set; }

    private readonly LeituraHelper _leituraHelper;

    public ImportarArquivosBase()
    {
        _leituraHelper = new LeituraHelper();
        objRef = DotNetObjectReference.Create(this);
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
        DirectoryPath = await _folderPicker.PickFolder();

        if (string.IsNullOrEmpty(DirectoryPath)) 
            return;

        var arquivos = _leituraHelper.CaminhosArquivos(DirectoryPath).Select(x => new ArquivoView(x)).ToList();

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

            files.Add(NovoObjeto);
            //Arquivos.Add(new ArquivoView(file.Name, 0, true, false, NovoObjeto));
        }

        StateHasChanged();
    }

    protected bool Filtro(FileView element) =>
        Filtro(element, Pesquisa);

    private bool Filtro(FileView element, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.File.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

    protected async Task RemoverPDF(FileView file)
    {
        var Arquivo = Arquivos.Where(x => x.FileId == file.Id).FirstOrDefault();

        if (!Equals(Arquivo, null)) 
        {
            var Index = files.IndexOf(file);

            files.Remove(file);
            Arquivos.Remove(Arquivo);

            await JS.InvokeVoidAsync("RemoverFile", Index);

            StateHasChanged();
        }
    }

    protected async Task VisualizarPDF(FileView file)
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
