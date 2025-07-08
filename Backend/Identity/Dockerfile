# Consultez https://aka.ms/customizecontainer pour savoir comment personnaliser votre conteneur de débogage et comment Visual Studio utilise ce Dockerfile pour générer vos images afin d’accélérer le débogage.

# En fonction du système d’exploitation du ou des ordinateurs hôtes qui génèrent ou exécutent les conteneurs, vous devrez peut-être modifier l’image spécifiée dans l’instruction FROM.
# Pour obtenir plus d’informations, veuillez consulter https://aka.ms/containercompat

# Cet index est utilisé lors de l’exécution à partir de VS en mode rapide (par défaut pour la configuration de débogage)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080


# Cette phase est utilisée pour générer le projet de service
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Identity/Identity.csproj", "Identity/"]
RUN dotnet restore "./Identity/Identity.csproj"
COPY . .
WORKDIR "/src/Identity"

RUN dotnet tool install --global NSwag.ConsoleCore && \
    echo "$HOME/.dotnet/tools" >> /etc/profile && \
    export PATH="$PATH:$HOME/.dotnet/tools"

RUN dotnet build "./Identity.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Cette étape permet de publier le projet de service à copier dans la phase finale
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Identity.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Cette phase est utilisée en production ou lors de l’exécution à partir de VS en mode normal (par défaut quand la configuration de débogage n’est pas utilisée)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.dll"]