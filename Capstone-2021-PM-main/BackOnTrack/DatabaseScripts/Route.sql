print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Zach Stultz
Created: 2021/03/26
Description: Route Table Creation
***************************************/

-- Route Table
print '' print '*** creating route table ***'
GO
CREATE TABLE [dbo].[Route]
(
	[RouteID]    INT            IDENTITY (100000, 1) NOT NULL,
    [DateOfRoute] DATETIME NOT NULL,
	[DriverEmployeeID] INT NULL,
	[Active] BIT DEFAULT ((1)) NOT NULL,
	[LicensePlateNumber] NVARCHAR (10),
    CONSTRAINT [pk_Route] PRIMARY KEY ([RouteID]),
    --CONSTRAINT [fk_driverEmployeeID] FOREIGN KEY ([DriverEmployeeID]) REFERENCES [dbo].[Employee]([EmployeeID]),
    --CONSTRAINT [fk_licensePlateNumber] FOREIGN KEY ([LicensePlateNumber]) REFERENCES [dbo].[Vehicles]([LicensePlateNumber])
)
GO

GO
CREATE INDEX [date_of_route_idx] ON [dbo].[Route] ([DateOfRoute])
GO

GO
CREATE INDEX [license_plate_number_idx] ON [dbo].[Route] ([LicensePlateNumber])
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating route table test data ***'
GO
SET IDENTITY_INSERT [dbo].[Route] ON
	INSERT INTO [dbo].[Route] ([RouteID], [DateOfRoute], [DriverEmployeeID], [Active], [LicensePlateNumber]) VALUES (100000, N'2021-05-02 00:00:00', 100000, 1, N'3841JD')
	INSERT INTO [dbo].[Route] ([RouteID], [DateOfRoute], [DriverEmployeeID], [Active], [LicensePlateNumber]) VALUES (100001, N'2021-05-02 00:00:00', 100000, 1, N'3841JD')
SET IDENTITY_INSERT [dbo].[Route] OFF
GO

/**************************************
Zach Stultz
Created: 2021/03/26
Description: Route Stored Procedures
***************************************/

-- Stored Procedures

print '' print '*** ROUTE STORED PROCEDURES ***'
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating sp_insert_route ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_route]
	(
		@DateOfRoute DATETIME,
		@DriverEmployeeID INT,
		@Active BIT,
		@LicensePlateNumber NVARCHAR (10)
	)
AS
	BEGIN
		INSERT INTO [Route]
			(DateOfRoute , DriverEmployeeID, Active, LicensePlateNumber)
		VALUES
			(@DateOfRoute, @DriverEmployeeID, @Active, @LicensePlateNumber)
		SELECT SCOPE_IDENTITY()
	END
GO


/**************************************
Jakub Kawski
Created: 2021/04/24
***************************************/
print '' print '*** creating sp_select_next_routeID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_next_routeID]
	(
		@DateOfRoute DATETIME
	)
AS
	BEGIN
		INSERT INTO [Route]
			(DateOfRoute)
		VALUES
			(@DateOfRoute)
		SELECT SCOPE_IDENTITY()
	END
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating sp_delete_route ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_route]
	(
		@RouteID INT
	)
AS
	BEGIN
		UPDATE Ticket
		Set RouteID = NULL
		WHERE Ticket.RouteID = @RouteID
		;
		DELETE FROM [Route]
		WHERE RouteID=@RouteID
	END
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating sp_select_route_by_date ***'
GO
CREATE PROCEDURE [dbo].[sp_select_route_by_date]
	(
		@DateOfRoute		DATETIME
	)
AS
	BEGIN
		SELECT RouteID, DateOfRoute, DriverEmployeeID, [Route].Active, LicensePlateNumber, Concat(Employee.FirstName, ' ', Employee.LastName) as 'Driver Name'
		FROM [Route]
		JOIN		Employee
			ON		[Route].DriverEmployeeID = Employee.EmployeeID
		WHERE DateOfRoute = @DateOfRoute
	END
GO
/**************************************
Jakub Kawski
Created: 2021/04/28
***************************************/
print '' print '*** creating sp_select_route_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_route_by_id]
	(
		@RouteID		INT
	)
AS
	BEGIN
		SELECT RouteID, DateOfRoute, DriverEmployeeID, [Route].Active, LicensePlateNumber, Concat(Employee.FirstName, ' ', Employee.LastName) as 'Driver Name'
		FROM [Route]
		JOIN		Employee
			ON		[Route].DriverEmployeeID = Employee.EmployeeID
		WHERE RouteID = @RouteID
	END
GO


/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating sp_select_route_by_driver_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_route_by_driver_id]
	(
		@DriverEmployeeID INT
	)
AS
	BEGIN
		SELECT RouteID, DateOfRoute, DriverEmployeeID, [Route].Active, LicensePlateNumber, Concat(Employee.FirstName, ' ', Employee.LastName) as 'Driver Name'
		FROM [Route]
		JOIN		Employee
			ON		[Route].DriverEmployeeID = Employee.EmployeeID
		WHERE DriverEmployeeID = @DriverEmployeeID
	END
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating sp_select_all_routes ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_routes]
AS
	BEGIN
		SELECT		RouteID, DateOfRoute, DriverEmployeeID, [Route].Active, [Route].LicensePlateNumber, Concat(Employee.FirstName, ' ', Employee.LastName) as 'Driver Name'
		FROM		[Route]
		JOIN		Employee
			ON		[Route].DriverEmployeeID = Employee.EmployeeID
		ORDER BY 	RouteID ASC
	END
GO

/**************************************
Zach Stultz
Created: 2021/04/08
***************************************/
print '' print '*** creating sp_update_route ***'
GO
CREATE PROCEDURE [dbo].[sp_update_route]
	(
		@RouteID    INT,
		@OldDateOfRoute DATETIME,
		@OldDriverEmployeeID INT,
		@OldActive BIT,
		@OldLicensePlateNumber NVARCHAR (10),
		@NewDateOfRoute DATETIME,
		@NewDriverEmployeeID INT,
		@NewActive BIT,
		@NewLicensePlateNumber NVARCHAR (10)
	)
AS
	BEGIN
		UPDATE [Route]
			SET DateOfRoute = @NewDateOfRoute,
				DriverEmployeeID = @NewDriverEmployeeID,
				Active = @NewActive,
				LicensePlateNumber = @NewLicensePlateNumber
			WHERE [Route].RouteID = @RouteID
			AND DateOfRoute = @OldDateOfRoute
			AND	DriverEmployeeID = @OldDriverEmployeeID
			AND	Active = @OldActive
			AND	LicensePlateNumber = @OldLicensePlateNumber
		RETURN @@ROWCOUNT
	END
GO

