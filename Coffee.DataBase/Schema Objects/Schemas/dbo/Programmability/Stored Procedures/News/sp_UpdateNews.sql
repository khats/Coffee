CREATE PROCEDURE [dbo].[sp_UpdateNews]
	@NewsId UNIQUEIDENTIFIER,
	@Subject NVARCHAR(256),
	@Description NVARCHAR(1024),
	@Content NVARCHAR(MAX)
AS
BEGIN
	UPDATE News SET [Subject] = @Subject, [Description] = @Description, Content = @Content WHERE NewsId = @NewsId

	IF (@@ERROR <> 0 OR @@ROWCOUNT = 0)
	BEGIN
		ROLLBACK TRAN
		RETURN 0
	END

	RETURN 1
END