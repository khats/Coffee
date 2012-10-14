CREATE PROCEDURE [dbo].[sp_CreateDepartment]
	@Name NVARCHAR(256),
	@DepartmentId UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	DECLARE @TempDepartmentId UNIQUEIDENTIFIER = newid()

	INSERT Departments (DepartmentId, Name) VALUES (@TempDepartmentId, @Name)

	IF (@@ERROR<>0 OR @@ROWCOUNT = 0)
	BEGIN
		SET @DepartmentId = null
		RETURN 0
	END

	SET @DepartmentId = @TempDepartmentId
	RETURN 1
END
