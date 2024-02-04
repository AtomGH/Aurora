#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Framework/Framework.csproj", "Framework/"]
RUN dotnet restore "Framework/Framework.csproj"
COPY . .
WORKDIR "/src/Framework"
RUN dotnet build "Framework.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Framework.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Framework.dll"]