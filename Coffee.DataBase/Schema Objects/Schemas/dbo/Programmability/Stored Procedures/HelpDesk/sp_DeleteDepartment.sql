CREATE PROCEDURE [dbo].[sp_DeleteDepartment]
	@DepartmentId UNIQUEIDENTIFIER
AS
BEGIN
	BEGIN TRAN

	DELETE [DepartmentTemplates] WHERE DepartmentId = @DepartmentId
	
	IF (@@ERROR<>0)
	BEGIN
		ROLLBACK TRAN
		RETURN 0
	END

	DELETE Departments WHERE DepartmentId = @DepartmentId

	IF (@@ERROR<>0 OR @@ROWCOUNT = 0)
	BEGIN
		ROLLBACK TRAN
		RETURN 0
	END

	COMMIT TRAN
	RETURN 1
END