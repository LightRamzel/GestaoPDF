using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Data.Views
{
    public class ContadorPaginasview
    {
        public ContadorPaginasview() =>
            Assinaturas = new List<Assinaturas>();

        public ContadorPaginasview(string Nome, int QtdePaginas, bool Assinado, bool Ocr) : this()
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
        public List<Assinaturas> Assinaturas { get; set; }

        public MudBlazor.Color GetAssinadoColor() =>
            this.Assinado ? MudBlazor.Color.Success : MudBlazor.Color.Error;

        public MudBlazor.Color GetOcrColor() =>
            this.Ocr ? MudBlazor.Color.Success : MudBlazor.Color.Error;

        public string GetAssinado() =>
            this.Assinado ? "Sim" : "Não";

        public string GetOcr() =>
            this.Ocr ? "Sim" : "Não";
    }

    public class Assinaturas
    {
        public string Nome { get; set; }
    }
}
