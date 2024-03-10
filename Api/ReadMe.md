## General Comments

Given the simplicity of this application, if I were to develop it in the real world I wouldn't necessarily take the same approaches. I opted for more complex design patterns because I wanted to try out new things and I wanted to demonstrate possible patterns in a "what if" scenario. E.g. What if I was tasked with building this application for an established company with thousands of users and was expected to grow and add more features.

## Design Patterns

1. Onion architecture - The Onion architecture is a form of layered architecture and can be visualized as concentric circles (i.e. onion). I saw this pattern while researching online and I liked it because the Controllers, BusinessLayer, and DataAccessLayer projects only have references to the Abstractions projects. This way we force developers to use unit testable interfaces and not rely on concrete implementations. It also allows us to switch out the implementation at runtime.


## Setup Guide

## Setting the connection string to secret manager
'''powershell
$sa_password="[SA PASSWORD HERE]"

dotnet user-secrets set "ConnectionStrings:CarRentalContext" "Server=tcp:andrewkcarrental.database.windows.net,1433;Initial Catalog=CarRental;Persist Security Info=False;User ID=andrewk;Password=$sa_password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
'''