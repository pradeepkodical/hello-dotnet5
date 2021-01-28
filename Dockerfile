FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["HelloDotnet5.csproj", "./"]
RUN dotnet restore "HelloDotnet5.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "HelloDotnet5.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelloDotnet5.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloDotnet5.dll"]
