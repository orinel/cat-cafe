FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /

COPY . .

RUN dotnet restore src/CatService/CatService.csproj

RUN dotnet publish src/CatService/CatService.csproj \
    -c Release \
    -o /app/publish 


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CatService.dll"]
