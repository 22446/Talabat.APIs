using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            
            return await _dbContext.Set<T>().ToListAsync();
        }


        public async Task<T> GetById(int Id)
        {
            return await _dbContext.Set<T>().FindAsync(Id);
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifacation<T> specifacation)
        {
            return await SpecificationEvaluter<T>.GetQuery(_dbContext.Set<T>(), specifacation).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecifacation<T> specifacation)
        {
            return await SpecificationEvaluter<T>.GetQuery(_dbContext.Set<T>(), specifacation).FirstOrDefaultAsync();

        }

        public async Task<int> GetCountSpecAsync(ISpecifacation<T> specifacation)
        {
           return await SpecificationEvaluter<T>.GetQuery(_dbContext.Set<T>(),specifacation).CountAsync();
        }

        public async Task AddAsync(T Entity)
        {
            await _dbContext.Set<T>().AddAsync(Entity);
        }
    }
}
