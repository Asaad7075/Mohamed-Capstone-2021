print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Zach Stultz
Created: 2021/03/05
Description: Vehicle Maintenance Type Table Creation
***************************************/

-- Vehicle Maintenance Type Table
print '' print '*** creating vehicle maintenance type table ***'
GO
CREATE TABLE [dbo].[VehicleMaintenanceType] (
    [VehicleMaintenanceTypeName]        NVARCHAR (100) NOT NULL,
    [VehicleMaintenanceTypeDescription] NVARCHAR (300) NOT NULL,
    CONSTRAINT [pk_vehicleMaintenanceTypeName] PRIMARY KEY CLUSTERED ([VehicleMaintenanceTypeName] ASC)
)
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** creating vehicle maintenance type index ***'
GO
CREATE NONCLUSTERED INDEX [vehicle_maintenance_type_idx]
    ON [dbo].[VehicleMaintenanceType]([VehicleMaintenanceTypeName] ASC)
GO

/**************************************
Zach Stultz
Created: 2021/03/25
***************************************/
print '' print '*** inserting into vehicle maintenance type table ***'
GO
INSERT INTO [dbo].[VehicleMaintenanceType] ([VehicleMaintenanceTypeName], [VehicleMaintenanceTypeDescription]) VALUES (N'AIR FILTER
', N'Replacement of air filter.')
INSERT INTO [dbo].[VehicleMaintenanceType] ([VehicleMaintenanceTypeName], [VehicleMaintenanceTypeDescription]) VALUES (N'HEADLIGHTS, TURN SIGNALS, BRAKE, AND PARKING LIGHTS
', N'Inspection and Repair of headlights, turn signals, brakes, and parking lights.')
INSERT INTO [dbo].[VehicleMaintenanceType] ([VehicleMaintenanceTypeName], [VehicleMaintenanceTypeDescription]) VALUES (N'OIL AND COOLANT LEVELS
', N'Inspection and filling of the oil and coolant levels.')
INSERT INTO [dbo].[VehicleMaintenanceType] ([VehicleMaintenanceTypeName], [VehicleMaintenanceTypeDescription]) VALUES (N'ROTATE TIRES
', N'Rotating your tires for even usage.')
INSERT INTO [dbo].[VehicleMaintenanceType] ([VehicleMaintenanceTypeName], [VehicleMaintenanceTypeDescription]) VALUES (N'TIRE PRESSURE AND TREAD DEPTH
', N'Checking the pressure in your tires and making an overall assessment on where the tread on your tires are at.')
INSERT INTO [dbo].[VehicleMaintenanceType] ([VehicleMaintenanceTypeName], [VehicleMaintenanceTypeDescription]) VALUES (N'WAX VEHICLE
', N'Waxing the vehicle.')
GO



