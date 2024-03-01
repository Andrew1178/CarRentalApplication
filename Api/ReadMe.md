## Read before running application

To have the Swagger UI show automatically when running the app, we must write to the the Console and SQL Server. This is because Swagger UI looks for a line in the log to tell it to open the UI. If I write the logs to SQL server, then the line doesnt show up and Swagger UI has to be manually run. I believe this is a bug with Serilog. https://www.reddit.com/r/dotnet/comments/u3kuii/browser_not_launching_on_start_after_serilog_has/



## Setting the connection string to secret manager
'''powershell
$sa_password="[SA PASSWORD HERE]"

dotnet user-secrets set "ConnectionStrings:CarRentalContext" "Server=tcp:andrewkcarrental.database.windows.net,1433;Initial Catalog=CarRental;Persist Security Info=False;User ID=andrewk;Password=$sa_password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
'''