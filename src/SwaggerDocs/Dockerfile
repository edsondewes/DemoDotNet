FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["./SwaggerDocs.csproj", "."]
RUN dotnet restore "SwaggerDocs.csproj"
COPY . .
RUN dotnet publish "SwaggerDocs.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SwaggerDocs.dll"]