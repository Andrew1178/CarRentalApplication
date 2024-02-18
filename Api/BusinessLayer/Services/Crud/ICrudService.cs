using System.Linq.Expressions;
using AutoMapper;
using DataAccessLayer;

namespace BusinessLayer;

// TODO: See if I can restrict the generics a bit more to enforce DTO and Model to be related
public interface ICrudService<Tdto, TEntity> where Tdto: class where TEntity : class { 
    public IUnitOfWork UnitOfWork { get;}
    public ICrudRepository<TEntity> Repository { get; } // Don't include in the unit of work because I don't want to exposre the DB Access Layer to the UI Layer. Want to force the dev to go through the service layer.
    public IMapper Mapper { get;}
    public Task<IEnumerable<Tdto>> GetAllAsync();
    public ValueTask<Tdto?> GetAsync(int modelId);
    public IEnumerable<Tdto?> Find(Expression<Func<Tdto, bool>> expression);
    public Task AddAsync(Tdto model);
    public Task AddRangeAsync(IEnumerable<Tdto> models);
    public Task<int> UpdateAsync(Tdto model);
    public Task<int> UpdateRangeAsync(IEnumerable<Tdto> models);
    public Task<int> RemoveAsync(Tdto model);
    public Task<int> RemoveRangeAsync(IEnumerable<Tdto> models);
}