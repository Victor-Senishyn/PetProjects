FROM mcr.microsoft.com/dotnet/sdk:8.0  AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore OfficeControlSystemApi.csproj
RUN dotnet publish OfficeControlSystemApi.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "OfficeControlSystemApi.dll"]