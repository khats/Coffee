CREATE PROCEDURE [dbo].[sp_AssociateUserWithCodeCard]
	@UserId UNIQUEIDENTIFIER,
	@Codes tbl_Codes READONLY
AS
BEGIN
	BEGIN TRAN;
	DELETE FROM SessionKeys WHERE UserId = @UserId;
	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRAN;
		RETURN -1;
	END;

	INSERT INTO SessionKeys(UserId, CodeId, Code) (SELECT @UserId, CodeNumber, CodeValue FROM @Codes);
	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRAN;
		RETURN -4;
	END;

	COMMIT TRAN;
	RETURN 1
END;