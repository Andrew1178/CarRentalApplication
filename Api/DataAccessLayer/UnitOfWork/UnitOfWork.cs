using System.Runtime.Versioning;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataAccessLayer;

// TODO:
// 1. Finish making other repositories - x
// Figure out how to leverage DTOs with entity framework - x
// - Want to do this to hide the database schema from the client - x
//  - Use automapper in the Repository - x
// 2. Implement logging (see if I can leverage Azure Application insights)
// - Looks like Application Insights handles logging for me in the controller. Test when I deploy it
// 3. Implement Repository layer - x
// 4. Setup Azure Key Vault for secrets
// - Revisit using Azure Key Vault. Looks like there is a way to use the connection string in Azure App Repositorys. Look into it
// - Write the code to get the secrets from the key vault - x
// - Actually create the key vault
// - Register the API with the Microsoft Entra  (http://localhost:5212)
// - Move connection string into Azure Key Vault 
// - See if I can pull the key
// 5. Implement controllers - x
// 6. Run the app and ensure I can hit an end point
//  - Fix the circular reference issue where the Unit Of Work expects a Repository and the Repository expects a unit of work
//   - I need to change the unit of work and make it so its just the repository. Repositorys should be injected into the controller and Repositorys will leverage the unit of work
//    - Remember the unit of work is a design pattern that is used to manage transactions and we will call one one Repository method to do the X number of things using the unit of work and will then call save changes to commit the transaction
// 7. Implement authentication https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-8.0
// 8. Setup CI/CD pipeline
// 9. Go through, review architecture and make sure everything is clean and organized
public class UnitOfWork : IUnitOfWork
{
    private readonly CarRentalContext _carRentalContext;
    public UnitOfWork(CarRentalContext carRentalContext, IVehicleMakeRepository vehicleMakeRepository, IOrderRepository orderRepository, IOrderContentRepository orderContentRepository, IVehicleRepository vehicleRepository, IKeyVaultRepository keyVaultRepository)
    {
        _carRentalContext = carRentalContext;

        VehicleMakeRepository = vehicleMakeRepository;
        OrderRepository =  orderRepository;
        OrderContentRepository = orderContentRepository;
        VehicleRepository = vehicleRepository;
        KeyVaultRepository = keyVaultRepository;
    }


    public IVehicleMakeRepository VehicleMakeRepository {get;}
    public IOrderRepository OrderRepository  {get;}
    public IOrderContentRepository OrderContentRepository  {get;}
    public IVehicleRepository VehicleRepository  {get;}
    public IKeyVaultRepository KeyVaultRepository {get;}

    public async ValueTask DisposeAsync()
    {
        await _carRentalContext.DisposeAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
      return await _carRentalContext.SaveChangesAsync();
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await _carRentalContext.DisposeAsync(); 
    }
}