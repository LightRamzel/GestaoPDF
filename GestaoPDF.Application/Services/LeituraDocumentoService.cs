using GestaoPDF.Domain.Entities;
using GestaoPDF.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Application.Services
{
    public class LeituraDocumentoService : ILeituraDocumentoRepository
    {
        private readonly ILeituraDocumentoRepository _repo;
        public LeituraDocumentoService(ILeituraDocumentoRepository repo) =>
            _repo = repo;
        
        public async Task<bool> DeleteAllAsync() =>
            await _repo.DeleteAllAsync();

        public async Task<bool> DeleteAsync(Guid id) =>
            await _repo.DeleteAsync(id);
        
        public async Task<Guid> InsertAsync(LeituraDocumento entity) =>
            await _repo.InsertAsync(entity);

        public async Task<bool> UpdateAsync(LeituraDocumento entity) =>
            await _repo.UpdateAsync(entity);

        public async Task<LeituraDocumento> SelectByIdAsync(Guid id) =>
            await _repo.SelectByIdAsync(id);

        public async Task<IList<LeituraDocumento>> SelectAllAsync() =>
            await _repo.SelectAllAsync();
    }
}
