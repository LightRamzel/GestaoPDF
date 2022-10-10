using GestaoPDF.Data.Views;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Pages;

public class ContadorPaginasBase : ComponentBase
{
    protected string TextoDigitado { get; set; }
    protected ArquivoView ArquivoTabela { get; set; }

    [Inject]
    protected List<ArquivoView> Arquivos { get; set; }


    public ContadorPaginasBase()
    {
        //Arquivos = new List<ArquivoView>();
    }

    protected override void OnInitialized()
    {
        //GerarListaVirtual();
    }

    private void GerarListaVirtual()
    {
        for (int i = 1; i < 50; i++)
            Arquivos.Add(new ArquivoView($"Arquivo_{i}.pdf", (i * 2), true, false));
    }

    protected bool FiltrarTabela(ArquivoView element) =>
        FiltrarTabela(element, TextoDigitado);

    private bool FiltrarTabela(ArquivoView element, string textoDigitado)
    {
        if (string.IsNullOrWhiteSpace(textoDigitado))
            return true;

        if (element.Nome.Contains(textoDigitado, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

}
