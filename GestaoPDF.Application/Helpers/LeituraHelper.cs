using GestaoPDF.Domain.Entities;
using iText.Forms;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Pdfa;
using iText.Signatures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Application.Helpers
{
    public class LeituraHelper
    {
        public string[] CaminhosArquivos(string caminhoPasta)
        {
            return Directory.GetFiles(caminhoPasta, "*.pdf", SearchOption.AllDirectories);
        }

        public async Task FazerLeituraAsync<T>(List<T> documentos) where T : Documento
        {
            foreach (var documento in documentos)
                await FazerLeituraAsync(documento);
        }

        public async Task FazerLeituraAsync(Documento documento)
        {
            if (!documento.PdfValido)
            {
                int numeroPaginas = 0;
                bool possuiOcr = false;
                IList<string>? assinaturas = null;

                try
                {
                    await Task.Factory.StartNew(() =>
                    {
                        using PdfReader pdfReader = new(documento.CaminhoArquivoLeitura);
                        using PdfDocument pdfDocument = new(pdfReader);

                        numeroPaginas = pdfDocument.GetNumberOfPages();
                        possuiOcr = false;

                        for (int i = 1; i <= numeroPaginas; i++)
                        {
                            var pg = pdfDocument.GetPage(i);

                            if (pg == null) continue;
                            
                            try { possuiOcr = !string.IsNullOrEmpty(PdfTextExtractor.GetTextFromPage(pg)); }
                            catch { break; }

                            if (possuiOcr) break;
                        }

                        assinaturas = LeituraAssinaturas(pdfDocument);
                    });

                } catch { }
                
                documento.SetDadosPdf(numeroPaginas, possuiOcr, assinaturas);
            }
        }

        private IList<string>? LeituraAssinaturas(PdfDocument pdfDocument)
        {
            IList<string>? assinaturas = null;

            var signatureUtil = new SignatureUtil(pdfDocument);

            var signatureNames = signatureUtil.GetSignatureNames();

            if (signatureNames.Count > 0)
            {
                assinaturas = new List<string>();

                foreach (var signatureName in signatureNames)
                {

                    var subjectDN = signatureUtil.ReadSignatureData(signatureName).GetCertificates()
                        .OrderBy(x => x.SerialNumber.LongValue)
                        .LastOrDefault()
                        ?.SubjectDN?.ToString() ?? "";

                    var posicaoInicial = subjectDN.IndexOf(",CN=") + 4;

                    assinaturas.Add(subjectDN.Substring(posicaoInicial).Split(',', ':')[0]);
                }
            }

            return assinaturas;
        }
    }
}
