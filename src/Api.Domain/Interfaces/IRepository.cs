using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistAsync(Guid id);
        Task<T> GetAsync(Guid id);
        Task<IList<T>> GetAllAsync();

        Task<T> InsertAsync(T entity);

        Task<T> UpdateAsync(T entity);
    }
}
