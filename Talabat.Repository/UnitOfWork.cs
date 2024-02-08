using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repository;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private Hashtable Repo;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Repo = new Hashtable();

        }
        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        
          => await _dbContext.DisposeAsync();
        

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var Key = typeof(TEntity).Name; 
            if (!Repo.Contains(Key))
            {
                var repostory = new GenericRepository<TEntity>(_dbContext);
                Repo.Add(Key, repostory);
            }
            return (IGenericRepository<TEntity>)Repo[Key];
        }
    }
}
