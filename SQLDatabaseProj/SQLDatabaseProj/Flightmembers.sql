CREATE TABLE [dbo].[Flightmembers]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FlightId] INT NOT NULL, 
    [UserId] INT NOT NULL,
    CONSTRAINT [FK_Member_ToFlight] FOREIGN KEY ([FlightId]) REFERENCES [Flight]([Id]),
    CONSTRAINT [FK_Member_ToAirport] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)
