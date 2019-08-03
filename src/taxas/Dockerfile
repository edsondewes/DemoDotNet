FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DemoDotNet.Taxas.Api/DemoDotNet.Taxas.Api.csproj", "DemoDotNet.Taxas.Api/"]
COPY ["DemoDotNet.Taxas.Core/DemoDotNet.Taxas.Core.csproj", "DemoDotNet.Taxas.Core/"]
RUN dotnet restore "DemoDotNet.Taxas.Api/DemoDotNet.Taxas.Api.csproj"
COPY . .
WORKDIR "/src/DemoDotNet.Taxas.Api"
RUN dotnet publish "DemoDotNet.Taxas.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "DemoDotNet.Taxas.Api.dll"]