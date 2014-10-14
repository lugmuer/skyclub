CREATE TABLE [dbo].[Flight]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(MAX) NOT NULL, 
    [Date] DATETIME NOT NULL, 
    [Start] VARCHAR(MAX) NULL, 
    [End] VARCHAR(MAX) NULL, 
    [SourceId] INT NOT NULL, 
    [TargetId] INT NOT NULL, 
    CONSTRAINT [FK_Flightsource_ToAirport] FOREIGN KEY ([SourceId]) REFERENCES [Airport]([Id]),
    CONSTRAINT [FK_Flighttarget_ToAirport] FOREIGN KEY ([TargetId]) REFERENCES [Airport]([Id])
)
