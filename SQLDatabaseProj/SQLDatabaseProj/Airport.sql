
CREATE TABLE [dbo].[Airport] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (MAX) NOT NULL, 
    [FsCode] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_Airport] PRIMARY KEY ([Id])
);


