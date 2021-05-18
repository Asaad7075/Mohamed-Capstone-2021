print '' print '*** using database backontrack_db ***'
GO
USE backontrack_db
GO


/**************************************
Chase Martin
Created: 2021/03/4 
Description: Created Childcare table.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating Childcare  table ***'
GO
CREATE TABLE [dbo].[Childcare](
	[ServiceID] [int]	NOT NULL,
	[ServiceProviderID] [int] NOT NULL,
	[ServiceName] [nvarchar](200)	NOT NULL,
	[Available]   [bit]			NOT NULL DEFAULT 0,
	[ScheduleRequired]  [bit]	NOT NULL,
    [ServiceDescription]  [nvarchar](300) NOT NULL
)
GO

/**************************************
Chase Martin
Created: 2021/02/19 
Description: Created Childcare test records.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating childcare test records ***'
GO
INSERT INTO [dbo].[Childcare]
		([ServiceID], [ServiceProviderID], [ServiceName], [Available], [ScheduleRequired], [ServiceDescription]) 
	VALUES
		('1', '001', 'Nanny', '1', '1', 'Types of childcare'),
		('2', '002', 'Daycare', '1', '1', 'Types of childcare'),
		('3', '003', 'Babysitter', '1', '1', 'Types of childcare')
		
GO

/**************************************
Chase Martin
Created: 2021/02/19 
Description: Created sp_select_all_childcare_types.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating sp_select_all_childcare_types ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_childcare_types]
AS
	BEGIN
		SELECT ServiceID, ServiceProviderID, ServiceName, Available, ScheduleRequired, ServiceDescription
		FROM Childcare
	END
GO
