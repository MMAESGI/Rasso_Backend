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

Dossier contenants les fichiers nécessaires à la mise en place de la base de données.

Database
	/changes        : Dossier contenant les scripts de modifications de la base de données par ordre chronologique. 
	   001-init.sql : Fichier de création de la base de données initial. Nomenclature à respecter est 'XXX-nomModification.sql' avec XXX le numéro d'ordre de passage du script.
	/lib            : Contient le fichier .jar du driver MySQL : mysql-connector-java-X.Y.Z.jar   (8.3.0 à l'heure actuelle).
	changelog.sql   : Fichier sql incluant les scripts à passer


