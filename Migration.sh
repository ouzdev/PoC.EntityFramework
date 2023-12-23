#!/bin/bash

echo please wait...

poc_migration () {
   dotnet ef migrations add "$1" -c PoCDbContext -s src/Api/PoC.Api -p src/PoC.Infrastructure/PoC.Infrastructure.csproj --output-dir EfCore/Migrations
   read
}

poc () {
    poc_migration $1
}

"$@"