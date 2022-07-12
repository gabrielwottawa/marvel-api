IF (OBJECT_ID('FavoritesCharacters')) IS NULL BEGIN
	CREATE TABLE FavoritesCharacters (
		Id INT identity(1,1) NOT NULL PRIMARY KEY,
		Name VARCHAR(255) NOT NULL,
		DeveloperMarvelId int NOT NULL
	)
END