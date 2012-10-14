﻿CREATE TABLE [dbo].[SessionKeys]
(
	[Id] DECIMAL(28) NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [CodeId] TINYINT NOT NULL, 
    [Code] NVARCHAR(64) NOT NULL,
	CONSTRAINT [FK_SessionKeys_aspnet_Users] FOREIGN KEY ([UserID]) REFERENCES [aspnet_Users](UserId)
)
