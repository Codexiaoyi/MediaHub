FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["MediaHub/MediaHub.csproj", "MediaHub/"]
COPY ["MediaHub.Common/MediaHub.Common.csproj", "MediaHub.Common/"]
COPY ["MediaHub.Data/MediaHub.Data.csproj", "MediaHub.Data/"]
COPY ["MediaHub.Model/MediaHub.Model.csproj", "MediaHub.Model/"]
COPY ["MediaHub.IRespository/MediaHub.IRepository.csproj", "MediaHub.IRespository/"]
RUN dotnet restore "MediaHub/MediaHub.csproj"
COPY . .
WORKDIR "/src/MediaHub"
RUN dotnet build "MediaHub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MediaHub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaHub.dll"]