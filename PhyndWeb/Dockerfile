FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-stretch AS build
WORKDIR /src
COPY ["PhyndWeb/PhyndWeb.csproj", "PhyndWeb/"]
COPY ["PhyndLogic/PhyndLogic.csproj", "PhyndLogic/"]
COPY ["PhyndData/PhyndData.csproj", "PhyndData/"]
RUN dotnet restore "PhyndWeb/PhyndWeb.csproj"
COPY . .
WORKDIR "/src/PhyndWeb"
RUN dotnet build "PhyndWeb.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PhyndWeb.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PhyndWeb.dll"]