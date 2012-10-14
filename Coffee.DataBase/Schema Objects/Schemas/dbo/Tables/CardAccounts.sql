CREATE TABLE [dbo].[CardAccounts]
(
	[Number] BIGINT NOT NULL PRIMARY KEY , 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Balance] MONEY NOT NULL, 
    [CurrencyCode] NCHAR(3) NOT NULL, 
    CONSTRAINT [FK_CardAccounts_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [aspnet_Users]([UserId]), 
    CONSTRAINT [FK_CardAccounts_Currencies] FOREIGN KEY ([CurrencyCode]) REFERENCES [Currencies]([CurrencyCode])
)
