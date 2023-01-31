FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["sippedes/sippedes.csproj", "sippedes/"]
RUN dotnet restore "sippedes/sippedes.csproj"
COPY . .
WORKDIR "/src/sippedes"
RUN dotnet build "sippedes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sippedes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sippedes.dll"]
