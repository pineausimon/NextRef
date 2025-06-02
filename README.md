# NextRef

**NextRef** est une application née d’un besoin récurrent : retrouver facilement une œuvre mentionnée dans un podcast, une interview, une vidéo ou un article.  
Il arrive souvent qu’un livre, un film ou un auteur soit cité dans un contenu, sans que ce soit simple de le noter ou d’y revenir plus tard.  

Le projet a d’abord été pensé comme une bibliothèque personnelle de références liées à ma consommation de contenus.  
Il a depuis évolué vers l’idée de centraliser ces références dans une base collaborative, avec pour objectif à terme de proposer des recommandations de contenus, en se basant sur des références communes partagées par d’autres utilisateurs. 

Ce dépôt contient la solution **backend** du projet. Une application web en React est également en cours de développement :    
➡️ https://github.com/pineausimon/NextRefApp

---

## 🛠️ Stack technique

- **Web API REST – .NET 9 / ASP.NET Core**
- **Architecture orientée DDD**
  - Structuration en couches distinctes : `Domain`, `Application`, `Infrastructure`
  - Modélisation métier en cours : agrégats, entités, value objects, règles métiers
- **CQRS avec MediatR**
  - Pour séparer lectures/écritures et clarifier les opérations (sans base séparée pour l’instant)
- **SQL Server**
  - Base relationnelle principale, interrogée via Dapper
- **Authentification avec Identity**
  - Persistée avec EF Core, utilisation de tokens JWT côté client
- **Redis**
  - Mise en cache des résultats de recherche de contenus
- **Tests unitaires**
  - Écrits avec xUnit et Moq

---

## 🗺️ Roadmap Backend 

✅ **Structuration du projet en couches** (`Domain`, `Application`, `Infrastructure`)  
✅ **Intégration de Dapper** (accès SQL) et configuration d'EF Core (Identity)  
✅ **Authentification et inscription** avec ASP.NET Identity + JWT  
✅ **Création et recherche de contenus** (livres, films, etc.) via Web API  
✅ **Ajout de contenus à une collection personnelle d’utilisateur**  
✅ **Mise en cache Redis** de la recherche de contenus  

🛠️ **Refonte progressive des modèles métiers selon les principes DDD**  
&nbsp;&nbsp;&nbsp;&nbsp;→ Définition claire des agrégats, entités et value objects  
&nbsp;&nbsp;&nbsp;&nbsp;→ Encapsulation des règles métiers dans les agrégats  
&nbsp;&nbsp;&nbsp;&nbsp;→ Ajout de tests unitaires spécifiques à la logique métier  

🛠️ **API de gestion des collections personnelles et de leurs contenus**  
🛠️ **Gestion des références croisées entre contenus** (ex : un podcast mentionne un livre)  
🛠️ **Ouverture à la contribution collaborative** (ajout et validation de références par les utilisateurs)  
🛠️ **Élargissement de la recherche à d'autres entités** (auteurs, créateurs, etc.)  
&nbsp;&nbsp;&nbsp;&nbsp;→ Mise en place d’une vue SQL dédiée pour optimiser les jointures  
&nbsp;&nbsp;&nbsp;&nbsp;→ Intégration côté API via un endpoint de recherche enrichi  

🔜 **Système de recommandations** basé sur les références en commun entre utilisateurs  
🔜 **Gestion plus poussée des profils utilisateurs** (préférences, types de contenus suivis, personnalisation des suggestions)  
🔜 **Amélioration des tests** : couverture élargie + premiers tests d’intégration  
🔜 Versioning de l’API + documentation (Swagger, conventions, etc.)

📆 **Plus tard**  
🔸 Monitoring technique (logging enrichi, outils de supervision à définir)  
🔸 Déploiement et hébergement de l’API    

---

ℹ️ Ce projet n’est pas ouvert aux contributions externes pour le moment.
