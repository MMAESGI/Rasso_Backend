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
