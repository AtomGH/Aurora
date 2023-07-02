#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Operators/PerforceOperator/PerforceOperator.csproj", "Operators/PerforceOperator/"]
RUN dotnet restore "Operators/PerforceOperator/PerforceOperator.csproj"
COPY . .
WORKDIR "/src/Operators/PerforceOperator"
RUN dotnet build "PerforceOperator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PerforceOperator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PerforceOperator.dll"]