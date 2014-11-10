CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(MAX) NOT NULL, 
    [Email] VARCHAR(MAX) NOT NULL, 
    [Token] VARCHAR(MAX) NULL
)
