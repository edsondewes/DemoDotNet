FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DemoDotNet.Calculos.Api/DemoDotNet.Calculos.Api.csproj", "DemoDotNet.Calculos.Api/"]
COPY ["DemoDotNet.Calculos.Core/DemoDotNet.Calculos.Core.csproj", "DemoDotNet.Calculos.Core/"]
RUN dotnet restore "DemoDotNet.Calculos.Api/DemoDotNet.Calculos.Api.csproj"
COPY . .
WORKDIR "/src/DemoDotNet.Calculos.Api"
RUN dotnet publish "DemoDotNet.Calculos.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "DemoDotNet.Calculos.Api.dll"]