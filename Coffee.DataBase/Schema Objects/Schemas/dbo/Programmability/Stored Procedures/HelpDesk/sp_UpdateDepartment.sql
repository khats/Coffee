CREATE PROCEDURE [dbo].[sp_UpdateDepartment]
	@DepartmentId UNIQUEIDENTIFIER,
	@Name NVARCHAR(256)
AS
BEGIN
	UPDATE Departments SET Name = @Name WHERE DepartmentId = @DepartmentId

	IF (@@ERROR<>0 OR @@ROWCOUNT = 0)
	BEGIN
		RETURN 0
	END

	RETURN 1
END