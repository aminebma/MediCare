CREATE TABLE [dbo].RendezVous
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [IdPatient] INT NOT NULL, 
    [Jour] TINYINT NOT NULL, 
    [Mois] TINYINT NOT NULL, 
    [Annee] SMALLINT NOT NULL, 
    [Heure] TINYINT NOT NULL, 
    [Minutes] TINYINT NOT NULL, 
    [IdMedecin] TINYINT NOT NULL
)
