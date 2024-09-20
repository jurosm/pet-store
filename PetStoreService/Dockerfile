FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore "./src/Web/Web.csproj"
RUN dotnet build "./src/Web/Web.csproj" -c Release -o /app/build
RUN dotnet publish "./src/Web/Web.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Web.dll"]