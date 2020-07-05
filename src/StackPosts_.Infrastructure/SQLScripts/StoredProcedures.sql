CREATE PROC dbo.Post_Delete
( @Id int ) 
AS 
BEGIN 
	SET NOCOUNT ON

	DELETE
	FROM dbo.Posts WHERE PostsId = @Id
END
GO

CREATE PROC dbo.Post_Exists
( @Id int )
AS
BEGIN
	SET NOCOUNT ON

	SELECT CASE WHEN EXISTS (
		SELECT Id
		FROM dbo.Posts
		WHERE Id = @Id) 
        THEN CAST (1 AS BIT) 
        ELSE CAST (0 AS BIT) END AS Result
END
GO

CREATE PROC dbo.Post_GetAll
AS
BEGIN
	SET NOCOUNT ON

	SELECT Id, Title, Body, Score, DatePosted
	FROM dbo.Posts
END
GO

CREATE PROC dbo.Posts_GetAll_WithReplies
AS
BEGIN
	SET NOCOUNT ON

	SELECT p.Id, p.Title, p.Body, p.Score, p.DatePosted,
		r.PostId, r.Id, r.Body, r.Score, r.DateReplied
	FROM dbo.Posts p
		LEFT JOIN dbo.Replies r ON p.Id = r.PostId
END
GO