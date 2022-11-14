#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Ocelot_apigw/Ocelot_apigw.csproj", "Ocelot_apigw/"]
RUN dotnet restore "Ocelot_apigw/Ocelot_apigw.csproj"
COPY . .
WORKDIR "/src/Ocelot_apigw"
RUN dotnet build "Ocelot_apigw.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ocelot_apigw.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ocelot_apigw.dll"]