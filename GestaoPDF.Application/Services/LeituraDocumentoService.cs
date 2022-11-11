using GestaoPDF.Application.IServices;
using GestaoPDF.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Application.Services
{
    public class LeituraDocumentoService : IServiceBase<LeituraDocumento>
    {
        public async Task<bool> DeleteAllAsync()
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await Task.FromResult(true);
        }

        public async Task<Guid> InsertAsync(LeituraDocumento entity)
        {
            return await Task.FromResult(Guid.NewGuid());
        }

        public async Task<bool> UpdateAsync(LeituraDocumento entity)
        {
            return await Task.FromResult(true);
        }
    }
}
