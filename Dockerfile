# Используем официальный образ .NET SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем csproj файлов и восстанавливаем зависимости
COPY WebAPI/*.csproj ./WebAPI/
COPY WebAPI.BLL/*.csproj ./WebAPI.BLL/
COPY WebAPI.DB/*.csproj ./WebAPI.DB/
RUN dotnet restore ./WebAPI/WebAPI.csproj

# Теперь копируем все остальные файлы 
COPY WebAPI/. ./WebAPI/
COPY WebAPI.BLL/. ./WebAPI.BLL/
COPY WebAPI.DB/. ./WebAPI.DB/

RUN dotnet publish ./WebAPI/WebAPI.csproj -c Release -o out

# Используем официальный образ .NET Runtime для исполнения приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./

# Открываем порт приложения
EXPOSE 80

# Запускаем приложение
ENTRYPOINT ["dotnet", "WebAPI.dll"]
