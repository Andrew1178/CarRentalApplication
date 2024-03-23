using System.Runtime.Versioning;
using DataAccessLayerAbstractions;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataAccessLayer;

// TODO:
// 1. Add validation to the DTO/Entities
// 2. Implement global error handling https://code-maze.com/global-error-handling-aspnetcore/
// - When I have Production configuration mute the default production/test appsettings.Production.json logging so we dont log the same error twice because I want everything to be logged by my global exception handler
// - Actually set the Global Exception Handler to write the logs to a database table
// 2. Setup key vault for secrets so I can make sure I can connect to the DB and write the logs
// - Also re-scaffold the entity framework context
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
// So what I have found is that Azure AD B2C does not support roles out of the box
// You can get it to work by using custom user flow policies (i.e. XML configuration on the sign up/sign in process that points to an Azure function that will check for a certain condition in the email, e.g. its my email and then return "admin" in the text of the role claim) 
// It is a lot of work and not something I would do in the real world because its more of a hack than anything.

// Alternatives so far are: Microsoft Entra (look into) and EF Core Identity

// Microsoft Entra will only allow for Microsoft account login/signup
// I need to use Microsoft Entra and Azure Roles together
// Microsoft Entra supports both Roles and Scopes. Both can be used to protect the API
// Role - Setup by the business and we determine who gets access to what
// Scope - A scope can be an individual resource. E.g. Readwrite access to files, or a database, or access to a users profile. These are granted by the user.
// Typically people use scopes when they want to gain access to a users data. E.g. Read contacts, read email, read profile picture
// Scopes are also used when you want an application to "run as" a user. E.g. Send email as a user, read files as a user
// Given this, I dont need scopes. I need roles.
// Use this video for role tutorial https://www.youtube.com/watch?v=5lRbtDSyjjs

// - Check if I actually need that scope I setup in that video
// - Implement feature that will allow users to sign up as the user role
// - Test

// I think I should handle API authentication with Azure AD B2C because #1 it suppots SPA, #2 I can leverage social networks for authentication
// 8. Setup CI/CD pipeline
// 9. Go through, review architecture and make sure everything is clean and organized
// 10. Write tests
// 11. Test
// 12. Deploy
// 13. Turn on treat warnings as errors if possible for each project
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