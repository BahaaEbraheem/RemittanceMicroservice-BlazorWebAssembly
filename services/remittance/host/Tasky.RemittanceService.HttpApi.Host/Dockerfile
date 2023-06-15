#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/remittance/NuGet.Config", "services/remittance/"]
COPY ["services/currency/NuGet.Config", "services/currency/"]
COPY ["services/customer/NuGet.Config", "services/customer/"]
COPY ["services/remittance/host/Tasky.RemittanceService.HttpApi.Host/Tasky.RemittanceService.HttpApi.Host.csproj", "services/remittance/host/Tasky.RemittanceService.HttpApi.Host/"]
COPY ["shared/Tasky.Hosting.Shared/Tasky.Hosting.Shared.csproj", "shared/Tasky.Hosting.Shared/"]
COPY ["services/remittance/src/Tasky.RemittanceService.HttpApi/Tasky.RemittanceService.HttpApi.csproj", "services/remittance/src/Tasky.RemittanceService.HttpApi/"]
COPY ["services/remittance/src/Tasky.RemittanceService.Application.Contracts/Tasky.RemittanceService.Application.Contracts.csproj", "services/remittance/src/Tasky.RemittanceService.Application.Contracts/"]
COPY ["shared/Tasky.Microservice.Shared/Tasky.Microservice.Shared.csproj", "shared/Tasky.Microservice.Shared/"]
COPY ["services/currency/src/Tasky.CurrencyService.Application.Contracts/Tasky.CurrencyService.Application.Contracts.csproj", "services/currency/src/Tasky.CurrencyService.Application.Contracts/"]
COPY ["services/currency/src/Tasky.CurrencyService.Domain.Shared/Tasky.CurrencyService.Domain.Shared.csproj", "services/currency/src/Tasky.CurrencyService.Domain.Shared/"]
COPY ["services/customer/src/Tasky.CustomerService.Application.Contracts/Tasky.CustomerService.Application.Contracts.csproj", "services/customer/src/Tasky.CustomerService.Application.Contracts/"]
COPY ["services/customer/src/Tasky.CustomerService.Domain.Shared/Tasky.CustomerService.Domain.Shared.csproj", "services/customer/src/Tasky.CustomerService.Domain.Shared/"]
COPY ["services/remittance/src/Tasky.RemittanceService.Domain.Shared/Tasky.RemittanceService.Domain.Shared.csproj", "services/remittance/src/Tasky.RemittanceService.Domain.Shared/"]
COPY ["services/remittance/src/Tasky.RemittanceService.Application/Tasky.RemittanceService.Application.csproj", "services/remittance/src/Tasky.RemittanceService.Application/"]
COPY ["services/currency/src/Tasky.CurrencyService.HttpApi.Client/Tasky.CurrencyService.HttpApi.Client.csproj", "services/currency/src/Tasky.CurrencyService.HttpApi.Client/"]
COPY ["services/customer/src/Tasky.CustomerService.HttpApi.Client/Tasky.CustomerService.HttpApi.Client.csproj", "services/customer/src/Tasky.CustomerService.HttpApi.Client/"]
COPY ["services/remittance/src/Tasky.RemittanceService.Domain/Tasky.RemittanceService.Domain.csproj", "services/remittance/src/Tasky.RemittanceService.Domain/"]
COPY ["services/remittance/src/Tasky.RemittanceService.EntityFrameworkCore/Tasky.RemittanceService.EntityFrameworkCore.csproj", "services/remittance/src/Tasky.RemittanceService.EntityFrameworkCore/"]
RUN dotnet restore "services/remittance/host/Tasky.RemittanceService.HttpApi.Host/Tasky.RemittanceService.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/remittance/host/Tasky.RemittanceService.HttpApi.Host"
RUN dotnet build "Tasky.RemittanceService.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasky.RemittanceService.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasky.RemittanceService.HttpApi.Host.dll"]