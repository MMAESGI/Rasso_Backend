#!/bin/bash

# Obtenir le dossier du script
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# Localiser le fichier .env dans ../Backend
ENV_FILE="$SCRIPT_DIR/../Backend/.env"

# Charger les variables d�environnement
if [ -f "$ENV_FILE" ]; then
  echo "Chargement du fichier .env depuis $ENV_FILE"
  export $(grep -v '^#' "$ENV_FILE" | xargs)
else
  echo "Fichier .env non trouv� � l'emplacement : $ENV_FILE"
  exit 1
fi

# Se replacer dans le dossier du script (l� o� se trouvent liquibase.properties, lib/, etc.)
cd "$SCRIPT_DIR"

# Fichier de log
LOG_FILE="liquibase.log"

# Lancer Liquibase avec logs dans console + fichier
echo "Lancement de Liquibase (logs dans $LOG_FILE)..."
liquibase update --logLevel=debug | tee "$LOG_FILE"

# Fin
echo "Liquibase termin�. Voir $LOG_FILE pour les d�tails."


read -p "Appuyez sur Entr�e pour fermer..."


