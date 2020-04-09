using CodeHubDesktop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeHubDesktop.Data.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        public async Task<T> CreateSnippet(T entity)
        {
            using (SimpleDbContext db = new SimpleDbContext())
            {
                EntityEntry<T> createdResult = await db.Set<T>().AddAsync(entity);
                await db.SaveChangesAsync();
                return createdResult.Entity;
            }
        }

        public async Task<bool> DeleteSnippet(int id)
        {
            using (SimpleDbContext db = new SimpleDbContext())
            {
                T entity = await db.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                db.Set<T>().Remove(entity);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IEnumerable<T>> GetAllSnippets()
        {
            using (SimpleDbContext db = new SimpleDbContext())
            {
                IEnumerable<T> entities = await db.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<IEnumerable<T>> GetSnippet(string filter)
        {
            using (SimpleDbContext db = new SimpleDbContext())
            {
                IEnumerable<T> entities = await db.Set<T>().Where(x => x.Title.Contains(filter)).ToListAsync();
                return entities;
            }
        }

        public async Task<T> UpdateSnippet(int id, T entity)
        {
            using (SimpleDbContext db = new SimpleDbContext())
            {
                entity.Id = id;

                db.Set<T>().Update(entity);
                await db.SaveChangesAsync();
                return entity;
            }
        }
    }
}
