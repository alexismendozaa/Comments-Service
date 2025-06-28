FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY delete-comment-ms.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 3021
ENTRYPOINT ["dotnet", "delete-comment-ms.dll"]
