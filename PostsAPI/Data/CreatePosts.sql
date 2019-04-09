CREATE TABLE [dbo].[Posts] (
	[Id]  uniqueidentifier NOT NULL DEFAULT NEWSEQUENTIALID() ,
    [Title]  VARCHAR (255) NULL,
    [Body] VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);