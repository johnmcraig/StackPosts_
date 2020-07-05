CREATE TABLE dbo.Posts (
	Id  int IDENTITY(1,1) NOT NULL,
    Title  VARCHAR (255) NULL,
    Body VARCHAR (255) NULL,
    Score int NOT NULL,
    Deleted bit NOT NULL,
    DatePosted datetime2(7) NOT NULL,
    PRIMARY KEY CLUSTERED (Id ASC)
)
GO

CREATE TABLE dbo.Replies (
    Id int IDENTITY(1,1) NOT NULL,
    PostId int NOT NULL,
    Body VARCHAR (255) NULL,
    Score int NOT NULL,
    Deleted bit NOT NULL,
    DateReplied datetime2(7),
    PRIMARY KEY CLUSTERED (Id ASC)
)
GO