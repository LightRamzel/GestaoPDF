using GestaoPDF.Domain.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Infra.Data.DataConfiguration
{
    public abstract class GestaoPdfDatabase
    {
        protected SQLiteAsyncConnection Database;

        protected async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await Database.CreateTableAsync<LeituraDocumento>();
            await Database.CreateTableAsync<Documento>();
            await Database.CreateTableAsync<DocumentoAssinatura>();
        }
    }
}
