using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContadorPaginasPDF.Entidade
{
    public class DocumentoAssinatura
    {
        public DocumentoAssinatura(Guid idDocumento, string nomeAssinatura)
        {
            Id = Guid.NewGuid();
            IdDocumento = idDocumento;
            NomeAssinatura = nomeAssinatura;
        }

        public Guid Id { get; private set; }
        public Guid IdDocumento { get; private set; }
        public Documento? Documento { get; set; }
        public string NomeAssinatura { get; private set; }
    }
}
