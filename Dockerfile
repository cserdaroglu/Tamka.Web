# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Proje dosyalarını kopyala ve restore et
COPY ["Tamka/Tamka.csproj", "Tamka/"]
RUN dotnet restore "Tamka/Tamka.csproj"

# Tüm kaynak kodları kopyala
COPY . .

# Uygulamayı derle
WORKDIR "/src/Tamka"
RUN dotnet build "Tamka.csproj" -c Release -o /app/build

# Publish aşaması
FROM build AS publish
RUN dotnet publish "Tamka.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Non-root kullanıcı oluştur (güvenlik için)
RUN adduser --disabled-password --gecos "" appuser

# Publish edilmiş dosyaları kopyala
COPY --from=publish /app/publish .

# Kullanıcıyı değiştir
USER appuser

# Port ayarı
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Tamka.dll"]

