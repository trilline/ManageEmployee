## ManageEmployees
Ce projet est une application de gestion des employés développée en utilisant ASP.NET Core pour le backend et Blazor WebAssembly pour le frontend.

# Prérequis
Avant de lancer l'application, assurez-vous d'avoir les outils suivants installés sur votre machine :

- .NET SDK (version 5.0 ou supérieure)
- Visual Studio 2022(de préférence) ou Code ou un autre éditeur de code de votre choix
- PostgreSQL (ou un autre système de gestion de base de données relationnelle)


# Installation
  
1.Clonez ce dépôt sur votre machine :

  git clone [https://github.com/votre-utilisateur/ManageEmployees.git](https://github.com/trilline/ManageEmployee/)

2.Accédez au répertoire du projet :
  cd ManageEmployees
  Ouvrez le fichier appsettings.json dans le répertoire ManageEmployees.Api et configurez la chaîne de connexion à votre base de données PostgreSQL.

3.Installez les dépendances nécessaires pour le backend et le frontend :

cd ManageEmployees.Api
dotnet restore
cd ../ManageEmployees.Front
dotnet restore

# Lancement
  Dans les propriété de la solution chosissez "Multiple start projet" et designez le projet web et l'api en tant que projet de démarrage. Puis exécutez la solution.
