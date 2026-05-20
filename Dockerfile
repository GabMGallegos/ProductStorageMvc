# ---------- Frontend build stage ----------
FROM node:22-alpine AS frontend-build
WORKDIR /src

COPY package*.json ./
COPY vite.config.js ./
COPY frontend ./frontend

RUN npm config set registry https://registry.npmjs.org/ \
    && npm install \
    && npm run build

# ---------- .NET publish stage ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ProductStorageMvc.csproj ./
RUN dotnet restore "ProductStorageMvc.csproj"

COPY . ./
COPY --from=frontend-build /src/wwwroot/dist ./wwwroot/dist

RUN dotnet publish "ProductStorageMvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ---------- Runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ProductStorageMvc.dll"]
