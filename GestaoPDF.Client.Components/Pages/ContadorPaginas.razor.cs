using GestaoPDF.Client.Components.Data.Views;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Client.Components.Pages;

public class ContadorPaginasBase : ComponentBase
{
    protected string TextoDigitado { get; set; }
    protected ArquivoView ArquivoTabela { get; set; }

    [Inject]
    protected List<ArquivoView> Arquivos { get; set; }

    public ContadorPaginasBase()
    {

    }

    protected override void OnInitialized()
    {

    }

    protected bool FiltrarTabela(ArquivoView element) =>
        FiltrarTabela(element, TextoDigitado);

    private bool FiltrarTabela(ArquivoView element, string textoDigitado)
    {
        if (string.IsNullOrWhiteSpace(textoDigitado))
            return true;

        if (element.NomeArquivo.Contains(textoDigitado, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

}
