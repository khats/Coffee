CREATE PROCEDURE [dbo].[sp_CreateUser]
	@Login nvarchar(256),
	@Password nvarchar(128),
	@PasswordSalt nvarchar(128),
	@Email nvarchar(256),
	@Fio nvarchar(512),
	@Address nvarchar(512),
	@Phone nvarchar(128),
	@Mobile nvarchar(128),
	@Country char(2),
	@Zip nchar(32)
AS
BEGIN
	DECLARE @Now DATETIME;
	SET @Now = GETUTCDATE();

	DECLARE @UserId uniqueidentifier;
	SET @UserId = NEWID();

	BEGIN TRAN;
	EXEC aspnet_Membership_CreateUser
			@ApplicationName = '/',
			@UserName = @Login,
			@Password = @Password,
			@PasswordSalt = @PasswordSalt,
			@Email = @Email,
			@PasswordQuestion = null,
			@PasswordAnswer = null,
			@IsApproved = 1,
			@CurrentTimeUtc = @Now,
			@CreateDate = @Now,
			@UniqueEmail = 1,
			@PasswordFormat = 1,
			@UserId = @UserId;

	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRAN;
		RETURN -1;
	END;

	INSERT INTO ClientInfo (UserId, FIO, Address, Phone, Mobile, Country, Zip)
		VALUES(@UserId, @Fio, @Address, @Phone, @Mobile, @Country, @Zip);
	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRAN;
		RETURN -2;
	END;

	EXEC aspnet_UsersInRoles_AddUsersToRoles 
		@ApplicationName= '/', 
		@UserNames = @Login,
		@RoleNames = 'User', 
		@CurrentTimeUtc = @Now;
	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRAN;
		RETURN -3;
	END;

	COMMIT TRAN;
	RETURN 1;
END;
