-- 1. Création de la base si elle n'existe pas
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'AnnoteDb')
BEGIN
    CREATE DATABASE AnnoteDb;
END
GO

-- Utilisation de la base
USE [AnnoteDb];
GO

-- 2. Drop des tables dans l'ordre inverse des dépendances
IF OBJECT_ID('core.ContentMentions', 'U') IS NOT NULL DROP TABLE core.ContentMentions;
IF OBJECT_ID('core.Contributions', 'U') IS NOT NULL DROP TABLE core.Contributions;
IF OBJECT_ID('core.UserCollectionItems', 'U') IS NOT NULL DROP TABLE core.UserCollectionItems;
IF OBJECT_ID('core.UserCollections', 'U') IS NOT NULL DROP TABLE core.UserCollections;
IF OBJECT_ID('core.Contents', 'U') IS NOT NULL DROP TABLE core.Contents;
IF OBJECT_ID('core.Contributors', 'U') IS NOT NULL DROP TABLE core.Contributors;
IF OBJECT_ID('core.Users', 'U') IS NOT NULL DROP TABLE core.Users;
GO

-- 3. Création du schéma core si inexistant
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'core')
BEGIN
    EXEC('CREATE SCHEMA core');
END
GO