using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rent.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IQueryable<TEntity>> QueryAsync();
        Task<TEntity> FindByIdAsync(long id);
        Task CreateAsync(TEntity item);
        Task UpdateAsync(TEntity item);
        Task RemoveAsync(TEntity item);
    }
}
