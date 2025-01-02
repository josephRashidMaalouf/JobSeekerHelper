FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /App

# Copy everything to the container
COPY . ./

# Restore and build the project
RUN dotnet restore UserService/UserService.Api/UserService.Api.csproj
RUN dotnet publish UserService/UserService.Api/UserService.Api.csproj -c Release -o out

# Use a smaller runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /App

# Copy the published output to the runtime image
COPY --from=build-env /App/out ./

# Set environment to development
ENV ASPNETCORE_ENVIRONMENT=development

# Expose the required port
EXPOSE 4002
ENV ASPNETCORE_URLS=http://+:4002

# Use the correct path to the .dll
ENTRYPOINT ["dotnet", "UserService.Api.dll"]