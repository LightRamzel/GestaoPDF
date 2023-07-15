using GestaoPDF.Client.Components.Data.Views;
using Microsoft.AspNetCore.Components;

namespace GestaoPDF.Client.Components.Pages;

public class ContadorPaginasBase : ComponentBase
{
    protected string? TextoDigitado { get; set; }
    protected ArquivoView? ArquivoTabela { get; set; }

    [Inject]
    protected List<ArquivoView> Arquivos { get; set; } = null!;

    protected bool FiltrarTabela(ArquivoView? element)
    {
        if (element == null || string.IsNullOrWhiteSpace(TextoDigitado))
            return true;

        if (!string.IsNullOrEmpty(element.NomeArquivo) && element.NomeArquivo.Contains(TextoDigitado, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}
