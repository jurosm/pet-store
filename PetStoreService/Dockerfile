FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore "./src/PetStoreService.Web/PetStoreService.Web.csproj"
RUN dotnet build "./src/PetStoreService.Web/PetStoreService.Web.csproj" -c Release -o /app/build
RUN dotnet publish "./src/PetStoreService.Web/PetStoreService.Web.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "PetStoreService.Web.dll"]