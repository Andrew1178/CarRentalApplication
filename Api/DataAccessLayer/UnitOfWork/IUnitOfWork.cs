namespace DataAccessLayer;

public interface IUnitOfWork : IAsyncDisposable
{
    public IVehicleMakeRepository VehicleMakeRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IOrderContentRepository OrderContentRepository { get; }    
    public IVehicleRepository VehicleRepository { get;  }
    public Task<int> SaveChangesAsync();
}
