CREATE TABLE [dbo].[ClientInfo]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FIO] NVARCHAR(512) NOT NULL, 
    [Address] NVARCHAR(512) NOT NULL, 
    [Phone] NVARCHAR(128) NOT NULL, 
    [Mobile] NVARCHAR(128) NOT NULL, 
    [Country] CHAR(2) NOT NULL, 
    [Zip] NCHAR(32) NOT NULL, 
    CONSTRAINT [FK_ClientInfo_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [aspnet_Users]([UserId])
)
