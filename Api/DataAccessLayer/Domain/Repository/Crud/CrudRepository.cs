using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataAccessLayer.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

internal abstract class CrudRepository<T> : ICrudRepository<T> where T : class{

    private readonly CarRentalContext _carRentalContext;
    public CrudRepository(CarRentalContext carRentalContext) => _carRentalContext = carRentalContext;

    public async Task AddAsync(T model)
    {
        await _carRentalContext.AddAsync(model);
    }

    public async Task AddRangeAsync(IEnumerable<T> models)
    {
        await _carRentalContext.AddRangeAsync(models);
    }

    public void Update(T model)
    {
        _carRentalContext.Set<T>().Update(model);
    }

    public void UpdateRange(IEnumerable<T> models)
    {
        _carRentalContext.Set<T>().UpdateRange(models);
    }

    public void Remove(T model)
    {
       _carRentalContext.Remove(model);
    }

    public void RemoveRange(IEnumerable<T> models)
    {
        _carRentalContext.RemoveRange(models);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _carRentalContext.Set<T>().ToListAsync(); 
    }

    public async ValueTask<T?> GetAsync(int modelId)
    {
        return await _carRentalContext.Set<T>().FindAsync(modelId);
    }

    public IEnumerable<T?> Find(Expression<Func<T, bool>> expression)
    {
        return _carRentalContext.Set<T>().Where(expression);
    }
}
