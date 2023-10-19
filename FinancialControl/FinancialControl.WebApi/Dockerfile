#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FinancialControl.WebApi/FinancialControl.WebApi.csproj", "FinancialControl.WebApi/"]
RUN dotnet restore "FinancialControl.WebApi/FinancialControl.WebApi.csproj"
COPY . .
WORKDIR "/src/FinancialControl.WebApi"
RUN dotnet build "FinancialControl.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinancialControl.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinancialControl.WebApi.dll"]