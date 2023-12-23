FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PoC.EntityFramework/PoC.EntityFramework.csproj", "PoC.EntityFramework/"]
RUN dotnet restore "PoC.EntityFramework/PoC.EntityFramework.csproj"
COPY . .
WORKDIR "/src/PoC.EntityFramework"
RUN dotnet build "PoC.EntityFramework.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PoC.EntityFramework.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PoC.EntityFramework.dll"]
