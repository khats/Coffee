CREATE PROCEDURE [dbo].[sp_CreateTemplate]
	@DepartmentId UNIQUEIDENTIFIER,
	@Template NVARCHAR(256),
	@TemplateId UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	DECLARE @TempTemplateId UNIQUEIDENTIFIER = newid()

	BEGIN TRAN

	IF (NOT EXISTS(SELECT 1 FROM Departments WHERE DepartmentId = @DepartmentId))
	BEGIN
		ROLLBACK TRAN
		RETURN 0
	END

	INSERT DepartmentTemplates (TemplateId, Template, DepartmentId) 
	VALUES (@TempTemplateId, @Template, @DepartmentId)

	IF (@@ERROR<>0 OR @@ROWCOUNT = 0)
	BEGIN
		ROLLBACK TRAN
		RETURN 0
	END

	COMMIT TRAN
	SET @TemplateId = @TempTemplateId
	RETURN 1
END