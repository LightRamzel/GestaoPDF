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
    protected Auxiliar ArquivoTabela { get; set; }
    protected List<Auxiliar> Arquivos { get; set; }

    public ContadorPaginasBase()
    {
        Arquivos = new List<Auxiliar>();
    }

    protected override void OnInitialized()
    {
        GerarListaVirtual();
    }

    private void GerarListaVirtual()
    {
        for (int i = 1; i < 50; i++)
            Arquivos.Add(new Auxiliar($"Arquivo {i}", (i * 2), true, true));
    }

    protected bool FiltrarTabela(Auxiliar element) =>
        FiltrarTabela(element, TextoDigitado);

    private bool FiltrarTabela(Auxiliar element, string textoDigitado)
    {
        if (string.IsNullOrWhiteSpace(textoDigitado))
            return true;

        if (element.Nome.Contains(textoDigitado, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

}

public class Auxiliar
{
    public Auxiliar() { }

    public Auxiliar(string Nome, int QtdePaginas, bool Assinado, bool Ocr)
    {
        this.Nome = Nome;
        this.QtdePaginas = QtdePaginas;
        this.Assinado = Assinado;
        this.Ocr = Ocr;
    }

    public string Nome { get; set; }
    public int QtdePaginas { get; set; }
    public bool Assinado { get; set; }
    public bool Ocr { get; set; }

    public string GetAssinado() =>
        this.Assinado ? "Sim" : "Não";

    public string GetOcr() =>
        this.Ocr ? "Sim" : "Não";
}
