print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Nate Hepker
Created: 2021/02/26
Description: Adds and employee role sp_add_employeerole
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating sp_add_employee_role ***'
GO
CREATE PROCEDURE [dbo].[sp_add_employee_role]
	(
		@EmployeeID			[int]
		,@RoleName			[nvarchar](50)
	)
AS
	BEGIN
		INSERT INTO EmployeeRole
				(EmployeeID, RoleName)
			VALUES
				(@EmployeeID, @RoleName)
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Nate Hepker
Created: 2021/02/26
Description: Removes and employee role sp_remove_employeerole
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating sp_remove_employee_role ***'
GO
CREATE PROCEDURE [dbo].[sp_remove_employee_role]
	(
		@EmployeeID	[int],
		@RoleName	[nvarchar](50)
	)
AS
	BEGIN
		DECLARE @Admins int
	
		SELECT @Admins = COUNT(RoleName)
		FROM EmployeeRole
		WHERE RoleName = 'Admin'
	
		IF @RoleName = 'Admin' AND @Admins = 1
			BEGIN
				RETURN 0
			END
		ELSE
			BEGIN
				DELETE FROM EmployeeRole
					WHERE EmployeeID = @EmployeeID
					  AND RoleName = @RoleName
				RETURN @@ROWCOUNT
			END
	END
GO

/**************************************
Nate Hepker
Created: 2021/02/26
Description: gets the employee roles by an employeeId sp_select_employeeroles_by_employeeid
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating sp_select_employee_roles_by_employeeid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_roles_by_employeeid]
	(
		@EmployeeID		[int]
	)
AS
	BEGIN
		SELECT RoleName
		FROM EmployeeRole
		WHERE EmployeeID = @EmployeeID
	END
GO