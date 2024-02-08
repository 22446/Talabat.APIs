using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<T> GetById(int Id);
        public Task AddAsync(T Entity);

        public Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifacation<T> specifacation);
        public Task<T> GetByIdWithSpecAsync(ISpecifacation<T> specifacation);
        public Task<int> GetCountSpecAsync(ISpecifacation<T> specifacation);

    }
}
