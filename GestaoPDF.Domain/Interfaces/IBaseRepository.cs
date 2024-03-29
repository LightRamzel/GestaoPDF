﻿using GestaoPDF.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Inserir os dados de uma tabela
        /// </summary>
        /// <param name="entity">Modelo que representa a tabela a ser gravada</param>
        /// <returns><c>Id</c> gerado do modelo (Chave Primária)</returns>
        Task<Guid> InsertAsync(T entity);

        /// <summary>
        /// Alterar os registros dos dados de uma tabela
        /// </summary>
        /// <param name="entity">Modelo que representa a tabela a ser alterada</param>
        /// <returns><c>True</c> se a gravação for bem sucedida</returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Exclui o registro de uma linha da tabela
        /// </summary>
        /// <param name="id">Chave primária da linha a ser excluída</param>
        /// <returns><c>True</c> se a gravação for bem sucedida</returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Exclui todos os registros da tabela
        /// </summary>
        /// <returns><c>True</c> se a gravação for bem sucedida</returns>
        Task<bool> DeleteAllAsync();

        /// <summary>
        /// Selecionar o modelo peelo id
        /// </summary>
        /// <param name="id">Chave Primária</param>
        /// <returns>Objeto</returns>
        Task<T> SelectByIdAsync(Guid id);

        /// <summary>
        /// Seleciontar todos os itens da tabela
        /// </summary>
        /// <returns></returns>
        Task<IList<LeituraDocumento>> SelectAllAsync();
    }
}
