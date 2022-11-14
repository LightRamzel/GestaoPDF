using GestaoPDF.Domain.Entities;
using GestaoPDF.Domain.Interfaces;
using GestaoPDF.Infra.Data.DataConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Infra.Data.Repository
{
    public class LeituraDocumentoRepository : GestaoPdfDatabase, ILeituraDocumentoRepository
    {
        public async Task<bool> DeleteAllAsync()
        {
            await Init();
            await Database.DeleteAllAsync<LeituraDocumento>();
            await Database.DeleteAllAsync<Documento>();
            await Database.DeleteAllAsync<DocumentoAssinatura>();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await Init();

            var entity = await SelectByIdAsync(id);

            await Database.DeleteAsync(entity);

            foreach (var doc in entity.Documentos)
            {
                foreach (var docAssinado in doc.AssinaturasDigitais)
                    await Database.DeleteAsync(docAssinado);
                
                await Database.DeleteAsync(doc);
            }

            return true;
        }

        public async Task<Guid> InsertAsync(LeituraDocumento entity)
        {
            await Init();
            await Database.InsertAsync(entity);
            await Database.InsertAllAsync(entity.Documentos);
            await Database.InsertAllAsync(entity.Documentos.SelectMany(x => x.AssinaturasDigitais).ToArray());

            return entity.Id;
        }

        public async Task<bool> UpdateAsync(LeituraDocumento entity)
        {
            await Init();
            await Database.UpdateAsync(entity);

            return true;
        }

        public async Task<LeituraDocumento> SelectByIdAsync(Guid id)
        {
            await Init();

            var leitura = await Database.Table<LeituraDocumento>().FirstOrDefaultAsync(x => x.Id == id);
            leitura.SetDocumentos(await Database.Table<Documento>().Where(x => x.IdLeituraDocumento == id).ToListAsync());

            foreach (var doc in leitura.Documentos.Where(x => x.AssinadoCertificado))
                doc.SetAssinaturasDigitais(
                    await Database
                        .Table<DocumentoAssinatura>()
                        .Where(x => x.IdDocumento == doc.Id)
                        .ToListAsync());
            
            return leitura;
        }

        public async Task<IList<LeituraDocumento>> SelectAllAsync()
        {
            await Init();

            IList<Guid> ids = (await Database.Table<LeituraDocumento>().ToArrayAsync())
                .Select(x => x.Id)
                .ToArray();

            var listaRetorno = new List<LeituraDocumento>();

            foreach (var id in ids)
                listaRetorno.Add(await SelectByIdAsync(id));

            return listaRetorno;
        }
    }
}
