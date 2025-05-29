# Projet NextRef - Résumé et Guide

## Contexte général  
- Projet backend ASP.NET Core C# structuré selon les principes DDD (Domain Driven Design).  
- Couche Domain distincte de la couche Infrastructure.  
- Utilisation de Dapper pour l’accès aux données métiers (pas EF Core).  
- Identity séparé dans un projet Infrastructure distinct, géré via EF Core uniquement pour les migrations.  
- Gestion de la concurrence via champ `rowversion` SQL Server dans Infrastructure (non exposé dans Domain).  
- Migrations métiers gérées par scripts SQL exécutés au démarrage via DbUp.  

## Domaines et entités  
- Entités principales : User, Content, UserCollection, UserCollectionItem, Contributor, Contribution, ContentMention.  
- Entités dotées de constructeur privé, méthodes statiques `Create` et `Rehydrate` pour contrôle des invariants.  
- Champs `CreatedAt` et `UpdatedAt` uniquement en Infrastructure (pour traçabilité base).  

## Repositories  
- Interfaces repositories définies dans couche Domain ou Application selon besoin.  
- Implémentations en Infrastructure, utilisant Dapper.  
- Méthodes AddAsync et UpdateAsync utilisant paramètres Dapper explicites (pas de création d’entités Infrastructure arbitraires).  

## Couche Application  
- Handlers MediatR pour CQRS, tests unitaires avec xUnit et Moq.  
- Exemples fournis pour handlers utilisateurs (Register, Login, Update, Get), contenus (GetById, Delete).  
- Tests unitaires avec mocks déclarés dans constructeur des classes tests (meilleure réutilisation).  

## Authentification et Sécurité  
- Identity ASP.NET Core utilisé uniquement côté Infrastructure, lié à AppUser.  
- Tests unitaires pour handlers avec mocks spécialisés pour UserManager et SignInManager (car difficiles à moquer).  
- Gestion de JWT ou autre token non détaillée (à implémenter).  

## Tests et qualité  
- Couverture de code recommandée via outils comme Coverlet ou Visual Studio Coverage.  
- Recommandation de structurer les tests unitaires en initialisant mocks dans constructeurs pour éviter duplication.  
- Usage de `Rehydrate` dans tests unitaires pour construire entités domaine valides sans passer par constructeurs publics.  

---
