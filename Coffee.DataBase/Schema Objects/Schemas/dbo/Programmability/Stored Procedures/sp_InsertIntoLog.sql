CREATE PROCEDURE [dbo].[sp_InsertIntoLog]
	@Component NVARCHAR(64),
	@Type int,
	@Message NVARCHAR(MAX),
	@Exception VARBINARY(MAX)
AS
BEGIN
	INSERT [Log] (Component, [Type], [Message], Exception) 
		VALUES (@Component, @Type, @Message, @Exception)
END