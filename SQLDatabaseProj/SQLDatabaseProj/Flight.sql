CREATE TABLE [dbo].[Flight]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(MAX) NULL, 
    [Date] DATETIME NULL, 
    [Start] VARCHAR(MAX) NULL, 
    [End] VARCHAR(MAX) NULL
)
