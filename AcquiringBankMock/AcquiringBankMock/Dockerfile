#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["AcquiringBankMock/AcquiringBankMock.csproj", "AcquiringBankMock/"]
RUN dotnet restore "AcquiringBankMock/AcquiringBankMock.csproj"
COPY . .
WORKDIR "/src/AcquiringBankMock"
RUN dotnet build "AcquiringBankMock.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AcquiringBankMock.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AcquiringBankMock.dll"]