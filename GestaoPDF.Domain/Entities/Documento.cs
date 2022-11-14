namespace GestaoPDF.Domain.Entities
{
    public class Documento
    {
        public Documento()
        { }

        public Documento(string caminhoArquivoLeitura)
        {
            Id = Guid.NewGuid();

            CaminhoArquivoLeitura = caminhoArquivoLeitura;

            SetDadosLeituraArquivo();

            AssinaturasDigitais = new List<DocumentoAssinatura>();
        }

        public Documento(Documento documento)
        {
            Id = documento.Id;
            IdLeituraDocumento = documento.IdLeituraDocumento;
            DataCriacao= documento.DataCriacao;
            DataLeitura = documento.DataLeitura;
            NomeArquivo = documento.NomeArquivo;
            CaminhoArquivoLeitura = documento.CaminhoArquivoLeitura;
            TamanhoArquivo = documento.TamanhoArquivo;
            QuantidadePaginas = documento.QuantidadePaginas;
            Ocr = documento.Ocr;
            AssinadoCertificado = documento.AssinadoCertificado;
            CaminhoValido= documento.CaminhoValido;
            PdfValido= documento.PdfValido;
            AssinaturasDigitais = documento.AssinaturasDigitais;

            if (AssinaturasDigitais == null)
                AssinaturasDigitais = new List<DocumentoAssinatura>();
        }

        public Guid Id { get; set; }
        public Guid IdLeituraDocumento { get; set; }
        public DateTime DataLeitura { get; set; }
        public string? NomeArquivo { get; set; }
        public DateTime DataCriacao { get; set; }
        public string CaminhoArquivoLeitura { get; set; }
        public long TamanhoArquivo { get; set; }
        public int QuantidadePaginas { get; set; }
        public bool Ocr { get; set; }
        public bool AssinadoCertificado { get; set; }
        public bool CaminhoValido { get; set; }
        public bool PdfValido { get; set; }

        public IList<DocumentoAssinatura> AssinaturasDigitais { get; private set; }

        public void SetAssinaturasDigitais(IList<DocumentoAssinatura> assinaturasDigitais) =>
            AssinaturasDigitais = assinaturasDigitais;

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
