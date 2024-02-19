using System.Runtime.Versioning;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataAccessLayer;

// TODO:
// 1. Finish making other repositories - x
// Figure out how to leverage DTOs with entity framework - x
// - Want to do this to hide the database schema from the client - x
//  - Use automapper in the Repository - x
// 2. Add validation to the DTO/Entities
// - Implement the onion architecture to: https://code-maze.com/onion-architecture-in-aspnetcore/
//  1. Keep the dependencies pointing inwards
//  2. Improve testability because everything is abstracted
// 2. Implement global error handling https://code-maze.com/global-error-handling-aspnetcore/
// - When I have Production configuration mute the default production/test appsettings.Production.json logging so we dont log the same error twice because I want everything to be logged by my global exception handler
// - Actually set the Global Exception Handler to write the logs to a database table
// 2. Implement logging (see if I can leverage Azure Application insights)
// - Looks like Application Insights handles logging for me in the controller. Test when I deploy it
// - Also want to log when people change an entity
// 3. Don't return error text to the client. Return a generic error message 
// 3. Implement Repository layer - x
// 4. Setup Azure Key Vault for secrets
// - Revisit using Azure Key Vault. Looks like there is a way to use the connection string in Azure App Repositorys. Look into it
// - Write the code to get the secrets from the key vault - x
// - Actually create the key vault
// - Register the API with the Microsoft Entra  (http://localhost:5212)
// - Move connection string into Azure Key Vault 
// - See if I can pull the key
// 5. Implement controllers - x
// 6. Run the app and ensure I can hit an end point - x
//  - Fix the circular reference issue where the Unit Of Work expects a Repository and the Repository expects a unit of work
//   - I need to change the unit of work and make it so its just the repository. Repositorys should be injected into the controller and Repositorys will leverage the unit of work
//    - Remember the unit of work is a design pattern that is used to manage transactions and we will call one one Repository method to do the X number of things using the unit of work and will then call save changes to commit the transaction
// 7. Implement authentication 
// https://learn.microsoft.com/en-us/azure/active-directory-b2c/overview 
// https://learn.microsoft.com/en-us/azure/active-directory-b2c/configure-authentication-sample-spa-app
// I think I should handle API authentication with Azure AD B2C because #1 it suppots SPA, #2 I can leverage social networks for authentication
// 8. Setup CI/CD pipeline
// 9. Go through, review architecture and make sure everything is clean and organized
// 10. Write tests
// 11. Test
// 12. Deploy
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