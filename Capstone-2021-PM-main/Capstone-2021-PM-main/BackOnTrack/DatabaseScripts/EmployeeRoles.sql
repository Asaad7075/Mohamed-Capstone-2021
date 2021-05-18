print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Chantal Shirley
Created: 2021/02/25
Description: Employee Role Table Creation
***************************************/

-- Employee Role Table

print '' print '*** creating employee role table ***'
GO
CREATE TABLE dbo.EmployeeRole
(
	EmployeeID		int NOT NULL,
	RoleName		nvarchar(50) NOT NULL
	CONSTRAINT pk_empployeeRole PRIMARY KEY(EmployeeID,RoleName),
	CONSTRAINT fk_employeeIDEmployeeRole FOREIGN KEY(EmployeeID	)
		REFERENCES dbo.Employee(EmployeeID),
	CONSTRAINT fk_roleNameEmployeeRole FOREIGN KEY(RoleName)
		REFERENCES dbo.Role(RoleName)
)
GO

print '' print '*** creating sp_select_employeeroles_by_employeeid ***'
GO
CREATE PROCEDURE dbo.sp_select_employeeroles_by_employeeid
	(
		@EmployeeID				[int]
	)
AS
	BEGIN
		SELECT RoleName
		FROM EmployeeRole
		WHERE EmployeeID = @EmployeeID
	END
GO

/**************************************
Chantal Shirley
Created: 2021/02/25
Description: Test employee roles
Procedure Tests
***************************************
Updater: Zach Stultz
Updated: 04-30-21
Remarks: Added additional roles.
***************************************/
-- Stored Procedure Test Records
--/**************************************
-- Comment the line above and below,
-- if you would like to test drivers
-- license data

print '' print '*** Stored Procedure Tests ***'
INSERT INTO dbo.EmployeeRole
(
	EmployeeID,
	RoleName
)
VALUES
(
	100001,
	"Logistics Maintenance"
),
(
	100000,
	"Admin"
),
(
	100002,
	"Manager"
),
(
	100003,
	"Logistics Manager"
),
(
	100004,
	"Logistics Admin"
),
(
	100005,
	"Logistics Driver"
),
(
	100006,
	"Inventory Admin"
),
(
	100007,
	"Inventory Maintenance"
),
(
	100008,
	"Inventory Manager"
),
(
	100009,
	"Inventory Specialist"
),
(
	100010,
	"Material Handling Admin"
),
(
	100011,
	"Material Handling Maintenance"
),
(
	100012,
	"Material Handling Manager"
),
(
	100013,
	"Material Handling Item Inspector"
),
(
	100014,
	"Material Handling Inventory Auditor"
),
(
	100015,
	"Material Handling Donor"
),
(
	100016,
	"Services Provision Admin"
),
(
	100017,
	"Service Provision Manager"
),
(
	100018,
	"Service Provision Provider"
)

GO

--***************************************/