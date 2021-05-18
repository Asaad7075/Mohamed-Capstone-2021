/**************************************
FirstName LastName
Created: YYYY/MM/DD
Description: Description of sql code.
**************************************
Chantal Shirley
Updated: 2021/02/25
Description: Inserting data relevant to
logistics login for testing.
***************************************/

print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO

print''print'***create role table***'
GO
CREATE TABLE [dbo].[Role](
	[RoleName]			[nvarchar](50)					NOT NULL,
	[Description]		[nvarchar](300)					NOT NULL,
	CONSTRAINT [pk_RoleName] PRIMARY KEY ([RoleName] ASC)
)
GO

print'' print'*** creating sp_Select_All_Roles ***'
GO
CREATE PROCEDURE sp_Select_All_Roles
AS
	BEGIN
		SELECT Role.RoleName, Role.Description
		FROM Role
	END
GO

print'' print'*** creating sp_Insert_Role ***'
GO
CREATE PROCEDURE [dbo].[sp_Insert_Role]
(
	@RoleName 		[nvarchar](50),
	@Description 	[nvarchar](300)
)
AS
	BEGIN
		INSERT INTO Role
			(RoleName, Description)
		VALUES
			(@RoleName, @Description)
		RETURN @@ROWCOUNT
	END
GO


EXEC sp_Insert_Role
	@RoleName = "Admin",
	@Description = "Unlimited Power"
;
EXEC sp_Insert_Role
	@RoleName = "Manager",
	@Description = "Mr.Mrs.InCharge"
;
EXEC sp_Insert_Role
	@RoleName = "General Employee",
	@Description = "Here for the benefits"
;

EXEC sp_Insert_Role
	@RoleName = "Logistics Manager",
	@Description = "Managing maintenance related operations."

EXEC sp_Insert_Role
	@RoleName = "Logistics Admin",
	@Description = "Managing maintenance related operations."

EXEC sp_Insert_Role
	@RoleName = "Logistics Maintenance",
	@Description = "Managing maintenance related operations."

EXEC sp_Insert_Role
	@RoleName = "Logistics Driver",
	@Description = "Managing maintenance related operations."

EXEC sp_Insert_Role
	@RoleName = "Inventory Admin",
	@Description = "Managing inventory related operations."

EXEC sp_Insert_Role
	@RoleName = "Inventory Maintenance",
	@Description = "Managing inventory related operations."

EXEC sp_Insert_Role
	@RoleName = "Inventory Manager",
	@Description = "Managing inventory related operations."

EXEC sp_Insert_Role
	@RoleName = "Inventory Specialist",
	@Description = "Managing inventory related operations."

EXEC sp_Insert_Role
	@RoleName = "Material Handling Admin",
	@Description = "Managing material-handling-related operations."

EXEC sp_Insert_Role
	@RoleName = "Material Handling Maintenance",
	@Description = "Managing material-handling-related operations."

EXEC sp_Insert_Role
	@RoleName = "Material Handling Manager",
	@Description = "Managing material-handling-related operations."

EXEC sp_Insert_Role
	@RoleName = "Material Handling Item Inspector",
	@Description = "Managing material-handling-related operations."

EXEC sp_Insert_Role
	@RoleName = "Material Handling Inventory Auditor",
	@Description = "Managing material-handling-related operations."

EXEC sp_Insert_Role
	@RoleName = "Material Handling Donor",
	@Description = "Managing material-handling-related operations."

EXEC sp_Insert_Role
	@RoleName = "Services Provision Admin",
	@Description = "Managing service-provision-related operations."

EXEC sp_Insert_Role
	@RoleName = "Service Provision Manager",
	@Description = "Managing service-provision-related operations."

EXEC sp_Insert_Role
	@RoleName = "Service Provision Provider",
	@Description = "Managing service-provision-related operations."