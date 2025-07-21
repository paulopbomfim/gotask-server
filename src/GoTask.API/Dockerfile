FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app

EXPOSE 80
EXPOSE 443

ENV TZ=Etc/GMT\-3
RUN apk add --update tzdata && ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > etc/timezone

RUN apk add --no-cache \
        icu-data \
        icu-data-full \
        icu-libs 

ENV ASPNETCORE_HTTP_PORTS=80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["src/GoTask.API/GoTask.API.csproj", "src/GoTask.API/"]
COPY ["src/GoTask.Communication/GoTask.Communication.csproj", "src/GoTask.Communication/"]
COPY ["src/GoTask.Application/GoTask.Application.csproj", "src/GoTask.Application/"]
COPY ["src/GoTask.Exceptions/GoTask.Exceptions.csproj", "src/GoTask.Exceptions/"]
COPY ["src/GoTask.Domain/GoTask.Domain.csproj", "src/GoTask.Domain/"]
COPY ["GoTask.Infrastructure/GoTask.Infrastructure.csproj", "GoTask.Infrastructure/"]

RUN dotnet restore "src/GoTask.API/GoTask.API.csproj"

COPY . .

WORKDIR "/src/src/GoTask.API"

RUN dotnet build "./GoTask.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish

ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "./GoTask.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "GoTask.API.dll"]
