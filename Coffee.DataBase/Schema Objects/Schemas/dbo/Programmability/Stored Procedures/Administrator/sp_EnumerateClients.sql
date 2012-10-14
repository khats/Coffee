CREATE PROCEDURE [dbo].[sp_EnumerateClients]
	@FIO NVARCHAR(512),
	@Login NVARCHAR(258),
	@PageNumber int,
	@CountPerPage int
AS
BEGIN
	DECLARE @startNumber int;
	SET @startNumber = @PageNumber * @CountPerPage;
	WITH tempAllUser AS (
		SELECT c.UserId, c.FIO, m.CreateDate as RegistrationDate, u.LoweredUserName AS Login, 
				1 AS CounOfCardAccounts, ROW_NUMBER() OVER (ORDER BY m.CreateDate ASC) AS num
		FROM ClientInfo AS c
		JOIN aspnet_Users AS u ON c.UserId = u.UserId
		JOIN aspnet_Membership AS m ON u.UserId = m.UserId
		WHERE u.LoweredUserName = CASE WHEN u.LoweredUserName IS NULL THEN u.LoweredUserName ELSE @Login END
			AND c.FIO = CASE WHEN c.FIO IS NULL THEN c.FIO ELSE @FIO + '%' END
	)
	SELECT UserId, FIO, RegistrationDate, Login, CounOfCardAccounts
	FROM tempAllUser
	WHERE num >= @startNumber AND num < (@startNumber + @CountPerPage)
END