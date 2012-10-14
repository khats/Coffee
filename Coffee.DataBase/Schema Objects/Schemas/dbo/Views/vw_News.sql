CREATE VIEW [dbo].[vw_News]
	AS SELECT ROW_NUMBER() OVER (ORDER BY CreatedAt DESC) AS RN, CreatedAt, [Description],
                                          NewsId, [Subject], UpdatedAt FROM News 
