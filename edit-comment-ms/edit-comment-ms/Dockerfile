FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY edit-comment-ms.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 3020
ENTRYPOINT ["dotnet", "edit-comment-ms.dll"]
