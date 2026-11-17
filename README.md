# Suivi des livreurs — SOPAL

Application de suivi des livraisons réalisée pendant mon stage de fin d'études chez **SOPAL** (2024).
Elle gère les voyages de livraison entre entrepôts et demandeurs : création d'un voyage,
affectation automatique au transporteur le plus proche, suivi GPS en temps réel et
notifications push.

Le dépôt contient l'**API ASP.NET Core** et l'**application mobile Flutter**.
Le back-office Vue.js 3 n'est pas publié dans ce dépôt.

## Fonctionnalités

- **Authentification JWT et rôles** (ASP.NET Core Identity) : `Admin`, `Magasinier`,
  `Demandeur`, `Transporteur`. Chaque endpoint est restreint aux rôles concernés.
- **Gestion des utilisateurs** : inscription, connexion, modification et suppression, avec un
  profil propre à chaque rôle (entrepôt rattaché au magasinier, véhicule rattaché au transporteur).
- **Gestion des entrepôts et des véhicules** : CRUD, positions GPS des entrepôts, liste des
  véhicules disponibles.
- **Voyages** : création d'un voyage (poids, nombre de palettes, type de voyage), consultation
  par transporteur, par magasinier ou par demandeur, et suivi de l'état du voyage.
- **Affectation automatique** : à la création d'un voyage, l'API calcule la distance
  (formule de Haversine) entre le point de départ et chaque transporteur actif, et choisit le
  plus proche.
- **Suivi GPS** : l'application mobile du transporteur envoie sa position toutes les
  10 secondes. Quand il arrive à moins de 200 m du destinataire, le voyage passe à l'état
  « Terminé » et le destinataire reçoit une notification.
- **Notifications push** avec Firebase Cloud Messaging (Firebase Admin SDK côté API).
- **Application mobile** : connexion, liste et détail des voyages, carte avec l'itinéraire
  (OpenStreetMap + routage OSRM), scan d'un QR code pour ouvrir un voyage.
- **Hub SignalR** (`/LocalisationHub`) pour diffuser des positions en temps réel.

## Architecture

```
Application mobile Flutter ──HTTP/JSON──▶ API ASP.NET Core ──EF Core──▶ SQL Server
                                               │
                                               └──▶ Firebase Cloud Messaging
```

L'API est organisée en couches, avec injection de dépendances :

- **Controllers** : endpoints REST et contrôle des rôles (`[Authorize(Roles = ...)]`)
- **Service** : logique métier (affectation, calcul de distance, notifications), derrière des
  interfaces (`IVoyageService`, …)
- **Repository** : accès aux données avec Entity Framework Core, derrière des interfaces
  (`IVoyageRepo`, …)
- **Dtos / Mappers** : objets d'échange et conversion depuis et vers les entités
- **Models / Data** : entités et `DbContext` (migrations EF Core)

## Stack

| Partie | Technologies |
|---|---|
| API | C#, .NET 8, ASP.NET Core Web API, ASP.NET Core Identity, JWT Bearer, SignalR, Swagger |
| Données | SQL Server, Entity Framework Core 8 (migrations) |
| Notifications | Firebase Admin SDK, Firebase Cloud Messaging |
| Mobile | Flutter (Dart), flutter_map, geolocator, firebase_messaging, flutter_secure_storage, barcode_scan2 |

## Structure du dépôt

```
backend/                 API ASP.NET Core (.NET 8)
├── Controllers/         Utilisateur, Entrepôt, Véhicule, Voyage
├── Service/             Logique métier
├── Repository/          Accès aux données (EF Core)
├── Interfaces/          Contrats des services et des repositories
├── Models/  Dtos/  Mappers/
├── Data/                ApplicationDBContext (rôles initialisés par HasData)
├── Hubs/                LocalisationHub (SignalR)
└── Migrations/          Migrations EF Core
frontend_mobile/         Application Flutter
└── lib/
    ├── pages/           Connexion, fonctionnalités, détail et infos d'un voyage
    ├── components/      Carte, liste des voyages, scan QR, champs de saisie
    └── service/         Appels à l'API, mise à jour de la position, Firebase
```

## Lancer le projet en local

### Prérequis

- [.NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (Express ou LocalDB)
- [Flutter](https://docs.flutter.dev/get-started/install) (Dart ≥ 3.3.4) et un émulateur Android

### API

1. Dans `backend/appsettings.json`, adaptez `ConnectionStrings:DefaultConnection` à votre
   instance SQL Server. La valeur actuelle pointe vers la machine de développement d'origine.
   Pour LocalDB, par exemple :
   ```
   Server=(localdb)\\MSSQLLocalDB;Database=sopal2;Trusted_Connection=True;TrustServerCertificate=True
   ```
2. Définissez la clé de signature JWT, un secret d'au moins 64 caractères (HMAC-SHA512).
   Elle n'est pas versionnée : l'API refuse de démarrer sans elle.
   ```bash
   cd backend
   dotnet user-secrets set "JWT:SigningKey" "$(openssl rand -base64 64)"
   ```
   Hors développement, utilisez la variable d'environnement `JWT__SigningKey`.
3. Créez la base et lancez l'API :
   ```bash
   dotnet tool restore
   dotnet ef database update
   dotnet run
   ```
4. L'API écoute sur `http://localhost:5000`. Swagger est disponible sur
   `http://localhost:5000/swagger`.
5. Créez un premier utilisateur avec `POST /api/user/register`, puis connectez-vous avec
   `POST /api/user/login` (`matriculeSopal` + `password`). Les quatre rôles sont créés par
   la migration.

Les notifications push ont besoin d'un fichier de compte de service Firebase. Il n'est pas
versionné. Sans lui, l'API fonctionne mais n'envoie pas de notification : l'erreur est
seulement journalisée. Pour les activer, placez votre propre fichier dans le dossier de sortie
(`backend/bin/Debug/net8.0/`), sous le nom attendu par `Service/VoyageService.cs`.

### Application mobile

1. L'adresse de l'API est écrite en dur (`http://20.56.21.171:5000`) dans
   `lib/components/maps.dart`, `lib/service/location_update.dart`,
   `lib/service/utilisateur_service.dart` et `lib/service/voyage.dart`.
   Sur un émulateur Android, remplacez-la par `http://10.0.2.2:5000`.
2. Lancez l'application :
   ```bash
   cd frontend_mobile
   flutter pub get
   flutter run
   ```
3. Connectez-vous avec un compte `Transporteur` pour activer l'envoi de la position.

## Captures d'écran

<!-- À compléter : back-office, application mobile (connexion, liste des voyages, carte). -->

| Connexion | Liste des voyages | Carte et itinéraire |
|---|---|---|
| _à venir_ | _à venir_ | _à venir_ |
