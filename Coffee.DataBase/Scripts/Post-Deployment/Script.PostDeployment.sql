--INSERT aspnet_Applications VALUES ('/',	'/', '89E78EB1-51FF-4C33-97AB-A5FA4EA89B39', NULL);

--EXEC aspnet_Roles_CreateRole @ApplicationName = '/', @RoleName = 'Administrator';
--EXEC aspnet_Roles_CreateRole @ApplicationName = '/', @RoleName = 'User';