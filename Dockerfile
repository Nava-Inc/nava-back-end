## # Use the official ASP.NET Core runtime image as a base
## FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
## WORKDIR /app
## EXPOSE 8080
## EXPOSE 8081
## 
## # Use the official ASP.NET Core SDK image as a build stage
## FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
## WORKDIR /src
## COPY ["Nava.csproj", "Nava/"]
## RUN dotnet restore "Nava/Nava.csproj"
## COPY . .
## WORKDIR "/src/Nava"
## RUN dotnet build "Nava.csproj" -c Release -o /app/build
## 
## # Build the application
## FROM build AS publish
## RUN dotnet publish "Nava.csproj" -c Release -o /app/publish
## 
## # Final stage/image
## FROM base AS final
## WORKDIR /app
## COPY --from=publish /app/publish .
## ENTRYPOINT ["dotnet", "Nava.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App
EXPOSE 5001
EXPOSE 5002

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Nava.dll"]

