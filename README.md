# POC_JWT

Ceci est une démonstration de l'utilisation d'un JWT.

Le projet est décomposé en deux parties :
  - JWTApi  : Microservice d'identité (à l'heure où je rédige ce README, la vérification de l'existence de l'utilisateur n'est pas complète).
  - BasicApi : Microservice de test qui permet de récupérer quelques informations en base de données.

L'accès à BasicApi est donc ""protégé"" par un JWT.

Il n'est pas utilisable dans un vrai contexte, il manque des parties comme :
- la vérification de l'existence de l'utilisateur lors de la demande de JWT.
- les rôles (Administrateur etc) à renseigner dans le JWT.
- le refresh token permettant d'accéder à un nouveau token par exemple.
- d'autres aspects de sécurité qui vont de paire avec la "la vérification de l'existence de l'utilisateur" comme le hashage, le nombre de tentatives de connexion pour bloquer un utilisateur etc.
