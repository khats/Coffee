CREATE PROCEDURE [dbo].[sp_UpdateTemplate]
	@TemplateId UNIQUEIDENTIFIER,
	@Template NVARCHAR(256)
AS
BEGIN
	UPDATE DepartmentTemplates SET Template = @Template WHERE TemplateId = @TemplateId

	IF (@@ERROR<>0 OR @@ROWCOUNT = 0)
	BEGIN
		RETURN 0
	END

	RETURN 1
END