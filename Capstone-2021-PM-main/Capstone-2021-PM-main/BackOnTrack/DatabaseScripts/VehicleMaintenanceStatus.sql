print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Zach Stultz
Created: 2021/03/25
Description: Vehicle Maintenance Status Type Table, Index, and Test Data Creation.
***************************************/

-- Vehicle Maintenance Status Type Table, Index, and Test Data.

print '' print '*** creating vehicle maintenance status type table ***'
GO
CREATE TABLE [dbo].[VehicleMaintenanceStatusType]
(
	[MaintenanceStatusType] nvarchar(100) NOT NULL PRIMARY KEY,
	[MaintenanceStatusDescription] nvarchar(100) NOT NULL
)
GO

GO
CREATE NONCLUSTERED INDEX [maintenance_status_type_idx]
    ON [dbo].[VehicleMaintenanceStatusType]([MaintenanceStatusType] ASC);
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating vehicle maintenance status type test data ***'
GO
	INSERT INTO [dbo].[VehicleMaintenanceStatusType] ([MaintenanceStatusType], [MaintenanceStatusDescription]) VALUES (N'Active', N'An active maintenance.')
	INSERT INTO [dbo].[VehicleMaintenanceStatusType] ([MaintenanceStatusType], [MaintenanceStatusDescription]) VALUES (N'Complete', N'A complete maintenance.')
	INSERT INTO [dbo].[VehicleMaintenanceStatusType] ([MaintenanceStatusType], [MaintenanceStatusDescription]) VALUES (N'Deferred', N'A postponed maintenance.')
	INSERT INTO [dbo].[VehicleMaintenanceStatusType] ([MaintenanceStatusType], [MaintenanceStatusDescription]) VALUES (N'Merged', N'A combined maintenance.')
	INSERT INTO [dbo].[VehicleMaintenanceStatusType] ([MaintenanceStatusType], [MaintenanceStatusDescription]) VALUES (N'New', N'A new maintenance.')
	INSERT INTO [dbo].[VehicleMaintenanceStatusType] ([MaintenanceStatusType], [MaintenanceStatusDescription]) VALUES (N'Paused', N'A paused maintenance.')
	INSERT INTO [dbo].[VehicleMaintenanceStatusType] ([MaintenanceStatusType], [MaintenanceStatusDescription]) VALUES (N'Rejected', N'A rejected maintenance.')
	INSERT INTO [dbo].[VehicleMaintenanceStatusType] ([MaintenanceStatusType], [MaintenanceStatusDescription]) VALUES (N'Rework', N'A maintenance that needs to be reworked.')
	INSERT INTO [dbo].[VehicleMaintenanceStatusType] ([MaintenanceStatusType], [MaintenanceStatusDescription]) VALUES (N'Verified', N'A verified maintenance.')
GO

/**************************************
Zach Stultz
Created: 2021/03/25
Description: Vehicle Maintenance Status Table, Index, and Test Data Creation.
***************************************/

-- Vehicle Maintenance Status Table, Index, and Test Data.
print '' print '*** creating vehicle maintenance status table ***'
GO
CREATE TABLE [dbo].[VehicleMaintenanceStatus]
(
	[StatusID]    INT            IDENTITY (100000, 1) NOT NULL,
	[VinNumber] NVARCHAR(17) NOT NULL,
	[MaintenanceStatusType] NVARCHAR(100) NOT NULL,
	[HasMaintenanceReport] BIT DEFAULT ((0)) NOT NULL,
	[IdentifiedMaintenance] NVARCHAR(500) NOT NULL,
    CONSTRAINT [pk_VehicleMaintenanceStatus] PRIMARY KEY ([StatusID]),
    -- CONSTRAINT [fk_vinNumber] FOREIGN KEY ([VinNumber]) REFERENCES [dbo].[Vehicles] ([VinNumber]),
	CONSTRAINT ak_vinNumber UNIQUE(VinNumber)
)
GO

GO
CREATE INDEX [vin_number_idx] ON [dbo].[VehicleMaintenanceStatus] ([VinNumber])
GO

print '' print '*** creating vehicle maintenance status test data ***'
GO
SET IDENTITY_INSERT [dbo].[VehicleMaintenanceStatus] ON
	INSERT INTO [dbo].[VehicleMaintenanceStatus] ([StatusID], [VinNumber], [MaintenanceStatusType], [HasMaintenanceReport], [IdentifiedMaintenance]) VALUES (100000, N'2FMDK49C49BA38693', N'Active', 1, N'Tires need replacing')
	INSERT INTO [dbo].[VehicleMaintenanceStatus] ([StatusID], [VinNumber], [MaintenanceStatusType], [HasMaintenanceReport], [IdentifiedMaintenance]) VALUES (100001, N'WBACC0320VEK80642', N'Paused', 0, N'Engine')
	INSERT INTO [dbo].[VehicleMaintenanceStatus] ([StatusID], [VinNumber], [MaintenanceStatusType], [HasMaintenanceReport], [IdentifiedMaintenance]) VALUES (100002, N'19UUA56692A045248', N'Complete', 1, N'Oil')
	INSERT INTO [dbo].[VehicleMaintenanceStatus] ([StatusID], [VinNumber], [MaintenanceStatusType], [HasMaintenanceReport], [IdentifiedMaintenance]) VALUES (100004, N'4T1BK36B15U059373', N'Rejected', 0, N'Tread')
SET IDENTITY_INSERT [dbo].[VehicleMaintenanceStatus] OFF
GO

/**************************************
Zach Stultz
Created: 2021/03/26
Description: Vehicle Maintenance Status & Vehicle Maintenance Status Type Stored Procedures
***************************************/

-- Stored Procedures
print '' print '*** VEHICLE MAINTENANCE STATUS & VEHICLE MAINTENANCE STATUS TYPE STORED PROCEDURES ***'
GO


/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating sp_insert_vehicle_maintenance_status_type ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_vehicle_maintenance_status_type]
	(
		@MaintenanceStatusType nvarchar(100),
		@MaintenanceStatusDescription nvarchar(100)
	)
AS
	BEGIN
		INSERT INTO VehicleMaintenanceStatusType
			(MaintenanceStatusType , MaintenanceStatusDescription)
		VALUES
			(@MaintenanceStatusType, @MaintenanceStatusDescription)
		SELECT SCOPE_IDENTITY()
	END
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating sp_insert_vehicle_maintenance_status ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_vehicle_maintenance_status]
	(
		@VinNumber NVARCHAR(17),
		@MaintenanceStatusType NVARCHAR(100),
		@HasMaintenanceReport BIT,
		@IdentifiedMaintenance NVARCHAR(500)
	)
AS
	BEGIN
		INSERT INTO VehicleMaintenanceStatus
			(VinNumber , MaintenanceStatusType, HasMaintenanceReport, IdentifiedMaintenance)
		VALUES
			(@VinNumber, @MaintenanceStatusType, @HasMaintenanceReport, @IdentifiedMaintenance)
		SELECT SCOPE_IDENTITY()
	END
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating sp_select_all_vehicle_maintenance_status_types ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_vehicle_maintenance_status_types]
AS
	BEGIN
		SELECT		MaintenanceStatusType, MaintenanceStatusDescription
		FROM		VehicleMaintenanceStatusType
		ORDER BY 	MaintenanceStatusType ASC
	END
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating sp_select_all_vehicle_maintenance_statuses ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_vehicle_maintenance_statuses]
AS
	BEGIN
		SELECT		StatusID, VinNumber, MaintenanceStatusType, HasMaintenanceReport, IdentifiedMaintenance
		FROM		VehicleMaintenanceStatus
		ORDER BY 	StatusID ASC
	END
GO