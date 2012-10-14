CREATE TABLE [dbo].[DepartmentTemplates]
(
	[TemplateId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [Template] NVARCHAR(256) NOT NULL, 
    [DepartmentId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_DepartmentTemplates_Departments] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments]([DepartmentId])
)
