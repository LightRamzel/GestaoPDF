namespace GestaoPDF.Domain.Entities
{
    public class DocumentoAssinatura
    {
        public DocumentoAssinatura()
        { }

        public DocumentoAssinatura(Guid idDocumento, string nomeAssinatura)
        {
            Id = Guid.NewGuid();
            IdDocumento = idDocumento;
            NomeAssinatura = nomeAssinatura;
        }

        public Guid Id { get; set; }
        public Guid IdDocumento { get; set; }
        public string NomeAssinatura { get; set; }
    }
}
