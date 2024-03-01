## Setting the connection string to secret manager
'''powershell
$sa_password="[SA PASSWORD HERE]"

dotnet user-secrets set "ConnectionStrings:CarRentalContext" "Server=tcp:andrewkcarrental.database.windows.net,1433;Initial Catalog=CarRental;Persist Security Info=False;User ID=andrewk;Password=$sa_password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
'''