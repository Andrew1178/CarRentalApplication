using System.Linq.Expressions;
using AutoMapper;
using DataAccessLayer;

namespace BusinessLayer;

internal abstract class CrudService<Tdto, TEntity> : ICrudService<Tdto, TEntity> where Tdto: class where TEntity : class
{
    public CrudService(IUnitOfWork unitOfWork, ICrudRepository<TEntity> crudRepository, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Repository = crudRepository;
        Mapper = mapper;
    }
    public IUnitOfWork UnitOfWork {get; }

    public ICrudRepository<TEntity> Repository {get; }
    
    public IMapper Mapper {get; }

    public async Task<IEnumerable<Tdto>> GetAllAsync(){
        var items = await Repository.GetAllAsync();
        return Mapper.Map<IEnumerable<TEntity>, IEnumerable<Tdto>>(items);
    }
    public async ValueTask<Tdto?> GetAsync(int modelId){
        var item = await Repository.GetAsync(modelId);
        return Mapper.Map<TEntity?, Tdto?>(item);
    }
    
    public IEnumerable<Tdto?> Find(Expression<Func<Tdto, bool>> expression)
    {
        var modelExpression = Mapper.Map<Expression<Func<Tdto, bool>>, Expression<Func<TEntity, bool>>>(expression);
        var items = Repository.Find(modelExpression);
        return Mapper.Map<IEnumerable<TEntity?>, IEnumerable<Tdto?>>(items);
     }

    public async Task AddAsync(Tdto model){
        var item = Mapper.Map<Tdto, TEntity>(model);
        await Repository.AddAsync(item);
        await UnitOfWork.SaveChangesAsync();
    }
    public async Task AddRangeAsync(IEnumerable<Tdto> models){
        var items = Mapper.Map<IEnumerable<Tdto>, IEnumerable<TEntity>>(models);
        await Repository.AddRangeAsync(items);
        await UnitOfWork.SaveChangesAsync();
    }
    public async Task<int> UpdateAsync(Tdto model){
        var item = Mapper.Map<Tdto, TEntity>(model);
        Repository.Update(item);
        return await UnitOfWork.SaveChangesAsync();
    }

    public async Task<int> UpdateRangeAsync(IEnumerable<Tdto> models){
        var items = Mapper.Map<IEnumerable<Tdto>, IEnumerable<TEntity>>(models);
        Repository.UpdateRange(items);
        return await UnitOfWork.SaveChangesAsync();
    }
    public async Task<int> RemoveAsync(Tdto model){
        var item = Mapper.Map<Tdto, TEntity>(model);
        Repository.Remove(item);
        return await UnitOfWork.SaveChangesAsync();
    }
    public async Task<int> RemoveRangeAsync(IEnumerable<Tdto> models){
        var items = Mapper.Map<IEnumerable<Tdto>, IEnumerable<TEntity>>(models);
        Repository.RemoveRange(items);
        return await UnitOfWork.SaveChangesAsync();
    }

}
