# Backend


## Structure du projet :
  - Backend  : Contients les microservices et le projet docker.
  - Database : Contients les fichiers relatifs à la mise en place / mise à jour de la base de données.


## Backend
### Identity

Microservice d'identité : celcui-ci gènère un JWT permettant d'autoriser l'accès aux autres microservices


### RassoApi

Api principale pour la gestion de nos événements 


### Database 

EntityFramework 

Pour réaliser une migration, se rendre dans le package "Identity" ou "RassoApi" et exécuter la commande :

``` cmd
 dotnet ef migrations add MyMigration
```

La mise à jour de la base devrait s'appliquer automatiquement au lancement du microservice.
Pour appliquer les changements manuellement, dans un terminal exécuter la commande :

``` cmd
dotnet ef database update
```

# Clients

Le backend fournit des clients auto-générés grâce à NSwag.

- **RassoApi.Client** : contient un client C# `RassoApiClient.cs` et un client TypeScript `client-ts/rasso-api-client.ts`  
- **Identity.Client** : contient un client Rust `identity-client.rs`

## Publication automatique

Les clients TypeScript sont automatiquement déployés en tant que package npm, permettant ainsi d'être installés depuis le front.  
Cela simplifie l'utilisation en encapsulant les requêtes HTTP. De plus, cela permet de manipuler directement les objets.

## Utilisation locale

Pour travailler localement en développement avec la version locale, il faut :

- Dans le backend, dossier `/client-ts` du projet souhaité :  
  - `npm run build`  
  - `npm link @mmaesgi/rassoapi-client`

- Dans le frontend :  
  - `npm link @mmaesgi/rassoapi-client`  (prérequis : `.npmrc` configuré)

