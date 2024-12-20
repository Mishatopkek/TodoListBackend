﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TodoList.Web/TodoList.Web.csproj", "src/TodoList.Web/"]
COPY ["src/TodoList.Core/TodoList.Core.csproj", "src/TodoList.Core/"]
COPY ["src/TodoList.Infrastructure/TodoList.Infrastructure.csproj", "src/TodoList.Infrastructure/"]
COPY ["src/TodoList.UseCases/TodoList.UseCases.csproj", "src/TodoList.UseCases/"]
RUN dotnet restore "src/TodoList.Web/TodoList.Web.csproj"
COPY . .
WORKDIR "/src/src/TodoList.Web"
RUN dotnet build "TodoList.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TodoList.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
ARG PASSWORD_SALT_SECRET
ARG JWT_SECRET
ARG DEFAULT_CONNECTION
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoList.Web.dll"]
