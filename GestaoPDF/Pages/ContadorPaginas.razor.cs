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
    protected ContadorPaginasview ArquivoTabela { get; set; }
    protected List<ContadorPaginasview> Arquivos { get; set; }

    public ContadorPaginasBase()
    {
        Arquivos = new List<ContadorPaginasview>();
    }

    protected override void OnInitialized()
    {
        GerarListaVirtual();
    }

    private void GerarListaVirtual()
    {
        for (int i = 1; i < 50; i++)
            Arquivos.Add(new ContadorPaginasview($"Arquivo_{i}.pdf", (i * 2), true, false));
    }

    protected bool FiltrarTabela(ContadorPaginasview element) =>
        FiltrarTabela(element, TextoDigitado);

    private bool FiltrarTabela(ContadorPaginasview element, string textoDigitado)
    {
        if (string.IsNullOrWhiteSpace(textoDigitado))
            return true;

        if (element.Nome.Contains(textoDigitado, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

}
