USE master
GO

IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE database_id = DB_ID(N'Marvel')) BEGIN
	CREATE DATABASE Marvel
END
GO

USE Marvel
GO

IF (OBJECT_ID('FavoritesCharacters')) IS NULL BEGIN
	CREATE TABLE FavoritesCharacters (
		Id INT identity(1,1) NOT NULL PRIMARY KEY,
		Name VARCHAR(255) NOT NULL,
		DeveloperMarvelId int NOT NULL
	)
END