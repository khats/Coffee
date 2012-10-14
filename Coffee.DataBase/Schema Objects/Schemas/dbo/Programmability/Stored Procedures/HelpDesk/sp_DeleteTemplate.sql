CREATE PROCEDURE [dbo].[sp_DeleteTemplate]
	@TemplateId UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM DepartmentTemplates WHERE TemplateId = @TemplateId

	IF (@@ERROR<>0 OR @@ROWCOUNT = 0)
	BEGIN
		RETURN 0
	END

	RETURN 1
END