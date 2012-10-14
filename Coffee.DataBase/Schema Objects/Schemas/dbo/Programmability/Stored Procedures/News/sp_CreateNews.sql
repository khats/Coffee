CREATE PROCEDURE [dbo].[sp_CreateNews]
	@Subject NVARCHAR(256),
	@Description NVARCHAR(1024),
	@Content NVARCHAR(MAX),
	@NewsId UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	DECLARE @NewsIdTemp UNIQUEIDENTIFIER = NEWID()
	DECLARE @Now DATETIME = GETDATE()
	
	INSERT News (NewsId, [Subject], [Description], [Content], CreatedAt, UpdatedAt)
	VALUES (@NewsId, @Subject, @Description, @Content, @Now, @Now) 

	IF (@@ERROR <> 0 OR @@ROWCOUNT = 0)
	BEGIN
		RETURN 0
	END

	SET @NewsId = @NewsIdTemp
	RETURN 1
END