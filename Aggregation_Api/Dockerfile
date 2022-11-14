#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Aggregation_Api/Aggregation_Api.csproj", "Aggregation_Api/"]
RUN dotnet restore "Aggregation_Api/Aggregation_Api.csproj"
COPY . .
WORKDIR "/src/Aggregation_Api"
RUN dotnet build "Aggregation_Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aggregation_Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aggregation_Api.dll"]