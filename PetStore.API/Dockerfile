FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app
COPY PetStore.API.csproj PetStore.API.csproj
RUN dotnet restore "./PetStore.API.csproj"
COPY . .
RUN dotnet build "PetStore.API.csproj" -c Release -o /app/build
RUN dotnet publish "PetStore.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "PetStore.API.dll"]