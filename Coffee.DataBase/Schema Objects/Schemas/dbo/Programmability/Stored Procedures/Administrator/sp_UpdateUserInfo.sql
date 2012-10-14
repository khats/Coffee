CREATE PROCEDURE [dbo].[sp_UpdateUserInfo]
	@UserId UNIQUEIDENTIFIER,
	@Login NVARCHAR(128), 
	@Email NVARCHAR(128),
	@Fio NVARCHAR(512),
	@Phone NVARCHAR(128),
	@Address NVARCHAR(128),
	@Mobile NVARCHAR(128),
	@Country NVARCHAR(128),
	@Zip NVARCHAR(128)
AS
BEGIN
	DECLARE @lowerLogin NVARCHAR(128);
	SET @lowerLogin = LOWER(@Login);
	BEGIN TRAN;
	IF (EXISTS (SELECT 1 FROM aspnet_Users WHERE LoweredUserName = @lowerLogin)
	  OR EXISTS(SELECT 1 FROM aspnet_Membership WHERE LoweredEmail = LOWER(@Email)))
	BEGIN
		ROLLBACK TRAN;
		RETURN -1;
	END;

	UPDATE aspnet_Users SET UserName = @Login, LoweredUserName = @lowerLogin WHERE UserId = @UserId;
	IF (@@ROWCOUNT = 0 OR @@ERROR <> 0)
	BEGIN
		ROLLBACK TRAN;
		RETURN -2;
	END;

	UPDATE aspnet_Membership SET Email = @Email, LoweredEmail = LOWER(@Email) WHERE UserId = @UserId;
	BEGIN
		ROLLBACK TRAN;
		RETURN -3;
	END;

	UPDATE ClientInfo SET FIO = @Fio, Phone = @Phone, Address = @Address, Mobile = @Mobile, Country = @Country, Zip = @Zip;
	BEGIN
		ROLLBACK TRAN;
		RETURN -3;
	END;

	COMMIT TRAN;
	RETURN 1;
END;
