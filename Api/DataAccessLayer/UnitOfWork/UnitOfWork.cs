using System.Runtime.Versioning;
using DataAccessLayerAbstractions;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataAccessLayer;

// TODO:
// 2. Add validation to the DTO/Entities
// - Implement the onion architecture to: https://code-maze.com/onion-architecture-in-aspnetcore/
//  - Introduce abstration csprojs for the services and repositories
//  - Move the interfaces to the abstration csprojs
//  - Separate the startup folder into another cs proj and allow it to have references to both the abstration and concrete implementations
//  - Change the other csproj's to only have access to the abstration csprojs
//  - This will: Force the developers to use abstractions and not the concrete implementations (which is good for testing and swapping out implementations)
//  1. Keep the dependencies pointing inwards
//  2. Improve testability because everything is abstracted
// 2. Implement global error handling https://code-maze.com/global-error-handling-aspnetcore/
// - When I have Production configuration mute the default production/test appsettings.Production.json logging so we dont log the same error twice because I want everything to be logged by my global exception handler
// - Actually set the Global Exception Handler to write the logs to a database table
// 2. Setup key vault for secrets so I can make sure I can connect to the DB and write the logs
// - Also re-scaffold the entity framework context
// 2. Implement logging (see if I can leverage Azure Application insights)
// - Looks like Application Insights handles logging for me in the controller. Test when I deploy it
// - Also want to log when people change an entity
// 3. Don't return error text to the client. Return a generic error message 
// 4. Setup Azure Key Vault for secrets
// - Revisit using Azure Key Vault. Looks like there is a way to use the connection string in Azure App Repositorys. Look into it
// - Write the code to get the secrets from the key vault - x
// - Actually create the key vault
// - Register the API with the Microsoft Entra  (http://localhost:5212)
// - Move connection string into Azure Key Vault 
// - See if I can pull the key
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
    public UnitOfWork(CarRentalContext carRentalContext, IVehicleMakeRepository vehicleMakeRepository, IOrderRepository orderRepository, IOrderContentRepository orderContentRepository, IVehicleRepository vehicleRepository)
    {
        _carRentalContext = carRentalContext;

        VehicleMakeRepository = vehicleMakeRepository;
        OrderRepository =  orderRepository;
        OrderContentRepository = orderContentRepository;
        VehicleRepository = vehicleRepository;
    }

    public IVehicleMakeRepository VehicleMakeRepository {get;}
    public IOrderRepository OrderRepository  {get;}
    public IOrderContentRepository OrderContentRepository  {get;}
    public IVehicleRepository VehicleRepository  {get;}

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