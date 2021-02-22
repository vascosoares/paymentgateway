using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentApi.Data.Repository.v1
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        public IEnumerable<TEntity> GetAll();

        Task<TEntity> AddAsync(TEntity entity);
    }
}
