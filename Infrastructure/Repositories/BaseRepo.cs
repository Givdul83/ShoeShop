
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class BaseRepo<TEntity, TContext> where TEntity : class where TContext : CustomerDbContext
{
    private readonly CustomerDbContext _context;

    protected BaseRepo(CustomerDbContext context)
    {
        _context = context;
    }

    public virtual async Task <TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR CreateAsync::" + ex.Message);
        }
            return null!;
          }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR GetAllAsync::" + ex.Message);
        }
            return null!;
        }

    public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                return entity;
            }
           


        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR GetOneAsync::" + ex.Message);
        }
            return null!;
          }

    public virtual async Task <TEntity> UpdateAsync(Expression<Func<TEntity, bool >> predicate, TEntity entity)
    {
        try
        {
            var entityToUpdate = await _context.Set<TEntity>().FirstOrDefaultAsync();
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

                return entityToUpdate;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR UpdateAsync::" + ex.Message);
        }
        return null!;
    }

    public virtual async Task <bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR Delete :: " + ex.Message);
        }
            return false;
        }

    public virtual async Task <bool> ExistAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entityFound = await _context.Set<TEntity>().AnyAsync(predicate);
            return entityFound;

        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR ExistAsync:: " + ex.Message);
        }
        return false;
    }
}



    




 