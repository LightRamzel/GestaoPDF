using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Domain.Entities
{
    public class Documento
    {
        public Documento(string caminhoArquivoLeitura)
        {
            Id = Guid.NewGuid();

            CaminhoArquivoLeitura = caminhoArquivoLeitura;

            SetDadosLeituraArquivo();

            AssinaturasDigitais = new List<DocumentoAssinatura>();
        }

        public Guid Id { get; private set; }
        public DateTime DataLeitura { get; private set; }
        public string? NomeArquivo { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public string CaminhoArquivoLeitura { get; private set; }
        public long TamanhoArquivo { get; private set; }
        public int QuantidadePaginas { get; private set; }
        public bool Ocr { get; private set; }
        public bool AssinadoCertificado { get; private set; }
        public bool CaminhoValido { get; private set; }
        public bool PdfValido { get; private set; }

        public IList<DocumentoAssinatura> AssinaturasDigitais { get; set; }

        private void SetDadosLeituraArquivo()
        {
            DataLeitura = DateTime.Now;
            CaminhoValido = File.Exists(CaminhoArquivoLeitura);

            if (!CaminhoValido) return;

            NomeArquivo = Path.GetFileNameWithoutExtension(CaminhoArquivoLeitura);
            DataCriacao = File.GetCreationTime(CaminhoArquivoLeitura);

            var file = new FileInfo(CaminhoArquivoLeitura);

            TamanhoArquivo = file.Length;
        }

        public void SetDadosPdf(int quantidadePaginas, bool ocr, IList<string>? assinaturas)
        {
            PdfValido = quantidadePaginas > 0;
            QuantidadePaginas = quantidadePaginas;
            Ocr = ocr;
            AssinadoCertificado = assinaturas != null && assinaturas.Count > 0;

            if (AssinadoCertificado)
                assinaturas?
                    .Select(assinatura => 
                    { 
                        AssinaturasDigitais.Add(new(Id, assinatura)); 
                        return assinatura; 
                    })
                    .ToArray();
        }

        public override string ToString() => NomeArquivo ?? "-";
    }
}
