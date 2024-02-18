using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccessLayer;

public interface ICrudRepository<T> where T : class { 
    public Task<IEnumerable<T>> GetAllAsync();
    public ValueTask<T?> GetAsync(int modelId);
    public IEnumerable<T?> Find(Expression<Func<T, bool>> expression);
    public Task AddAsync(T model);
    public Task AddRangeAsync(IEnumerable<T> models);
    public void Update(T model);
    public void UpdateRange(IEnumerable<T> models);
    public void Remove(T model);
    public void RemoveRange(IEnumerable<T> models);

}