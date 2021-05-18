print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Zach Stultz
Created: 2021/03/05
Description: Vehicle Maintenance Report Table Creation
***************************************/

-- Vehicle Maintenance Report Table

print '' print '*** creating vehicle maintenance report table ***'
GO
CREATE TABLE [dbo].[VehicleMaintenanceReport] (
    [VehicleMaintenanceReportID]    INT            IDENTITY (100000, 1) NOT NULL,
    [VinNumber]                     NVARCHAR (17)  NOT NULL,
    [VehicleMaintenanceTypeName]    NVARCHAR (100) NOT NULL,
    [VehicleMaintenanceServiceDate] DATETIME       NOT NULL,
    [MaintenanceFinished]           BIT            DEFAULT ((0)) NOT NULL,
    [VehicleMaintenanceNotes]       NVARCHAR (250) NULL,
    [isQueued]                      BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [pk_vehicleMaintenanceReportID] PRIMARY KEY CLUSTERED ([VehicleMaintenanceReportID] ASC),
   -- CONSTRAINT [fk_vinNumber] FOREIGN KEY ([VinNumber]) REFERENCES [dbo].[Vehicles] ([VinNumber])
)
GO

/**************************************
Zach Stultz
Created: 2021/03/12
Description: Vehicle Maintenance Report Test Data
***************************************/

-- Test Data
print '' print '*** inserting vehicle maintenance report data ***'
GO
SET IDENTITY_INSERT [dbo].[VehicleMaintenanceReport] ON
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100000, N'0D537YGQFKPFM4JOO', N'OIL AND COOLANT LEVELS', N'2021-03-05 00:00:00', 0, N'Almost done.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100001, N'5SX43TNWCO0Q4Y2N6', N'ROTATE TIRES', N'2021-03-04 00:00:00', 1, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100002, N'IDS43TNWCO0Q4Y2N6', N'TIRE PRESSURE AND TREAD DEPTH', N'2021-03-03 00:00:00', 0, N'Nope, not even close.', 1)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100003, N'PDIE7YGQFKPFM4JOO', N'WAX VEHICLE', N'2021-03-02 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100004, N'1GTEC14W4VZ530220', N'WAX VEHICLE', N'2021-03-03 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100005, N'5UXKR0C52F0K25095', N'TIRE PRESSURE AND TREAD DEPTH', N'2021-03-04 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100006, N'4T1BF1FK1CU630530', N'ROTATE TIRES', N'2021-03-05 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100007, N'1FAFP53U82G136295', N'OIL AND COOLANT LEVELS', N'2021-03-06 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100008, N'2T3BF4DV5AW023615', N'AIR FILTER', N'2021-03-07 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100009, N'5J6YH28533L096582', N'WAX VEHICLE', N'2021-03-08 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100010, N'KM8SC13E25U904313', N'ROTATE TIRES', N'2021-03-09 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100011, N'1FMRU15W93LA29218', N'HEADLIGHTS, TURN SIGNALS, BRAKE, AND PARKING LIGHTS', N'2021-03-10 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100012, N'1HGCM56435A156397', N'AIR FILTER', N'2021-03-11 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100013, N'ZKHAFEHBX8V406823', N'WAX VEHICLE', N'2021-03-12 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100014, N'JN8AZ18W99W156450', N'TIRE PRESSURE AND TREAD DEPTH', N'2021-03-13 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100015, N'5XXGR4A62DG137993', N'OIL AND COOLANT LEVELS', N'2021-03-14 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100016, N'5TFHY5F14EX377521', N'AIR FILTER', N'2021-03-15 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100017, N'3VWRZ71K48M055322', N'WAX VEHICLE', N'2021-03-16 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100018, N'1GKS2MEF9BR256745', N'AIR FILTER', N'2021-03-17 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100019, N'JN8HD17Y0SW143222', N'ROTATE TIRES', N'2021-03-18 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100020, N'1G1PC5SH4C7103383', N'HEADLIGHTS, TURN SIGNALS, BRAKE, AND PARKING LIGHTS', N'2021-03-19 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100021, N'1ZVFT82HX65216868', N'AIR FILTER', N'2021-03-20 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100022, N'WBA3B3G55ENR69469', N'HEADLIGHTS, TURN SIGNALS, BRAKE, AND PARKING LIGHTS', N'2021-03-21 00:00:00', 0, N'Dont even ask.', 0)
INSERT INTO [dbo].[VehicleMaintenanceReport]
		([VehicleMaintenanceReportID], [VinNumber], [VehicleMaintenanceTypeName], [VehicleMaintenanceServiceDate], [MaintenanceFinished], [VehicleMaintenanceNotes], [isQueued])
			VALUES (100023, N'2B3KA43R46H314909', N'WAX VEHICLE', N'2021-03-22 00:00:00', 0, N'Dont even ask.', 0)
SET IDENTITY_INSERT [dbo].[VehicleMaintenanceReport] OFF
GO

/**************************************
Zach Stultz
Created: 2021/03/12
Description: Vehicle Maintenance Report Stored Procedures
***************************************/

-- Stored Procedures

print '' print '*** VEHICLE MAINTENANCE REPORT STORED PROCEDURES ***'
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating sp_insert_vehicle_maintenance_report ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_vehicle_maintenance_report]
	(
		@VinNumber                     nvarchar (17),
		@VehicleMaintenanceTypeName    nvarchar (100),
		@VehicleMaintenanceServiceDate datetime,
		@MaintenanceFinished           bit,
		@VehicleMaintenanceNotes       nvarchar (250),
		@isQueued                      bit
	)
AS
	BEGIN
		INSERT INTO VehicleMaintenanceReport
			(VinNumber , VehicleMaintenanceTypeName, VehicleMaintenanceServiceDate, MaintenanceFinished, VehicleMaintenanceNotes, isQueued)
		VALUES
			(@VinNumber, @VehicleMaintenanceTypeName, @VehicleMaintenanceServiceDate, @MaintenanceFinished, @VehicleMaintenanceNotes, @isQueued)
		SELECT SCOPE_IDENTITY()
	END
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating sp_retrieve_vehicle_maintenance_report_by_vin ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_vehicle_maintenance_report_by_vin]
	(
		@VinNumber		[nvarchar](17)
	)
AS
	BEGIN
		SELECT VehicleMaintenanceReportID, VinNumber, VehicleMaintenanceTypeName, VehicleMaintenanceServiceDate, MaintenanceFinished, VehicleMaintenanceNotes, isQueued
		FROM VehicleMaintenanceReport
		WHERE VinNumber = @VinNumber
	END
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating sp_retrieve_all_active_vehicle_maintenance_reports ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_active_vehicle_maintenance_reports]
(
	@isQueued [int] = 1
)
AS
	BEGIN
		SELECT VehicleMaintenanceReportID, VinNumber, VehicleMaintenanceTypeName, VehicleMaintenanceServiceDate, MaintenanceFinished, VehicleMaintenanceNotes, isQueued
		FROM VehicleMaintenanceReport
		WHERE isQueued = @isQueued
	END
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating sp_update_vehicle_maintenance_report ***'
GO
CREATE PROCEDURE [dbo].[sp_update_vehicle_maintenance_report]
	(
		@VehicleMaintenanceReportID	  	  int,
		@OldVinNumber                     nvarchar (17),
		@OldVehicleMaintenanceTypeName    nvarchar (100),
		@OldVehicleMaintenanceServiceDate datetime,
		@OldMaintenanceFinished           bit,
		@OldVehicleMaintenanceNotes       nvarchar (250),
		@OldisQueued                      bit,
		@NewVinNumber                     nvarchar (17),
		@NewVehicleMaintenanceTypeName    nvarchar (100),
		@NewVehicleMaintenanceServiceDate datetime,
		@NewMaintenanceFinished           bit,
		@NewVehicleMaintenanceNotes       nvarchar (250),
		@NewisQueued                      bit
	)
AS
	BEGIN
		UPDATE VehicleMaintenanceReport
			SET VinNumber = @NewVinNumber,
				VehicleMaintenanceTypeName = @NewVehicleMaintenanceTypeName,
				VehicleMaintenanceServiceDate = @NewVehicleMaintenanceServiceDate,
				MaintenanceFinished = @NewMaintenanceFinished,
				VehicleMaintenanceNotes = @NewVehicleMaintenanceNotes,
				isQueued = @NewisQueued
			WHERE VehicleMaintenanceReportID = @VehicleMaintenanceReportID
			AND VinNumber = @OldVinNumber
			AND	VehicleMaintenanceTypeName = @OldVehicleMaintenanceTypeName
			AND	VehicleMaintenanceServiceDate = @OldVehicleMaintenanceServiceDate
			AND	MaintenanceFinished = @OldMaintenanceFinished
			AND VehicleMaintenanceNotes = @OldVehicleMaintenanceNotes
			AND isQueued = @OldisQueued
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating sp_select_all_vehicle_maintenance_report ***'
GO
CREATE PROCEDURE [dbo].sp_select_all_vehicle_maintenance_report
AS
	BEGIN
		SELECT		VehicleMaintenanceReportID, VinNumber, VehicleMaintenanceTypeName, VehicleMaintenanceServiceDate, MaintenanceFinished, VehicleMaintenanceNotes, isQueued
		FROM		VehicleMaintenanceReport
		ORDER BY 	VehicleMaintenanceReportID ASC
	END
GO
