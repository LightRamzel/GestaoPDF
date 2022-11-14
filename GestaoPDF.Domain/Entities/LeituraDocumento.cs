namespace GestaoPDF.Domain.Entities
{
    public class LeituraDocumento
    {
        public LeituraDocumento()
        {
            NomeLoteLeitura = 
            NomeUsuarioMaquina =
            NomeMaquina = string.Empty;
            Id = Guid.NewGuid();
            DataGravacao = DateTime.Now;
            
            Documentos = new List<Documento>();
        }

        public LeituraDocumento(string nomeLoteLiteura, string nomeUsuarioMaquina, string nomeMaquina)
        {
            NomeLoteLeitura = nomeLoteLiteura;
            NomeUsuarioMaquina = nomeUsuarioMaquina;
            NomeMaquina = nomeMaquina;
            Id = Guid.NewGuid();
            DataGravacao = DateTime.Now;

            Documentos = new List<Documento>();
        }

        public Guid Id { get; set; }
        public string NomeLoteLeitura { get; set; }
        public DateTime DataGravacao { get; set; }
        public string NomeUsuarioMaquina { get; set; }
        public string NomeMaquina { get; set; }
        public IList<Documento> Documentos { get; private set; }

        public void SetDocumentos<TDocumento>(IList<TDocumento> documentos) where TDocumento : Documento
        {
            documentos.Select(x => x.IdLeituraDocumento = Id).ToList();

            Documentos = documentos.Select(x => new Documento(x)).ToList();
        }
    }
}
