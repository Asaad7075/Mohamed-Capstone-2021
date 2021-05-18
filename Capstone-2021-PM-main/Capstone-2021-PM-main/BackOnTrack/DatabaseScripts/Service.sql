print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Thomas Stout
Created: 2021/04/01 
Description: Created Category table.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print''print'***create Category table***'
GO
CREATE TABLE [dbo].[Category](
	[ServiceCategoryName]			[nvarchar](50)				NOT NULL,
	[ServiceCategoryDescription]	[nvarchar](500)				NOT NULL,
	CONSTRAINT [pk_serviceCategoryName] Primary KEY([ServiceCategoryName])
)
print''print'***creating Categories***'
GO
INSERT INTO Category (ServiceCategoryName, ServiceCategoryDescription)
VALUES ("Self Care/Hygiene", "Haircuts, Places to shower, etc."),
	("Childcare", "Childcare services"),
	("Rehabilitation", "Drug & alcohol abuse, addiction, etc."),
	("Food", "Food services"),
	("Financial Counseling", "Money management, investing, etc.")
	
/**************************************
Thomas Stout
Created: 2021/04/01
Description: Stored procedure. Selects
all Service Categories
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_select_all_categories ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_categories]
AS
	BEGIN
		SELECT 	ServiceCategoryName, ServiceCategoryDescription
		FROM 	Category
		ORDER BY ServiceCategoryName ASC
	END
GO

/**************************************
Chase Martin
Created: 2021/03/26 
Description: Created Provider table.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print''print'***create Provider table***'
GO
CREATE TABLE [dbo].[Provider](
	[ServiceProviderID]	 [int] IDENTITY(100000, 1)	NOT NULL,
	[BusinessName]	[nvarchar](200)				NOT NULL,
	[FirstName]		[nvarchar](50)				NOT NULL,
	[LastName]		[nvarchar](50)				NOT NULL,
	[MiddleInitial]	[char](1)					NULL,
	[Address]		[nvarchar](100)				NOT NULL,
	[PhoneNumber]	[nvarchar](11)				NOT NULL,
	[Email]			[nvarchar](100)				NOT NULL,
	[ZipCode]			[nvarchar](50)			NOT NULL,
	[EIN]		[nvarchar](50)					NOT NULL,
	[Activated]		[bit]						NOT NULL,
	[Schedule]		[bit]						NOT NULL
	CONSTRAINT [pk_serviceProviderID] PRIMARY KEY ([ServiceProviderID] ASC)
)
/**************************************
Thomas Stout
Created: 2021/04/07
Description: Stored procedure. Selects
all businesses.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_select_all_businesses ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_businesses]
AS
	BEGIN
		SELECT  DISTINCT BusinessName
		FROM 	Provider
		ORDER BY BusinessName ASC
	END
GO

/**************************************
Thomas Stout
Created: 2021/04/07
Description: Stored procedure. Selects
all Service Provider First and Last Names
by their business.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_select_all_provider_names_by_business ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_provider_names_by_business]
	(
		@BusinessName	[nvarchar](200)
	)
AS
	BEGIN
		SELECT CONCAT(FirstName, ' ', LastName)
		FROM 	Provider
		WHERE	BusinessName = @BusinessName
		ORDER BY LastName ASC
	END
GO

/**************************************
Thomas Stout
Created: 2021/03/24 
Description: Created Services table.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print''print'***create Service table***'
GO
CREATE TABLE [dbo].[Service](
	[ServiceID]				[int] IDENTITY(100000, 1)	NOT NULL,
	[BusinessName]			[nvarchar](200)				NOT NULL,
	[ServiceName]			[nvarchar](200)				NOT NULL,
	[ServiceCategoryName]	[nvarchar](50)				NOT NULL,
	[Available]				[bit]						NOT NULL,
	[ScheduleRequired]		[bit]						NOT NULL,
	[ServiceDescription]	[nvarchar](300)				NOT NULL,
	[ServiceProvider]		[nvarchar](500)				NOT NULL,
	[ServiceScheduleStart]		[datetime], /*depracated, delete when clientsaveshecdule works*/
	[ServiceScheduleEnd]		[datetime] /*depracated, delete when clientsaveshecdule works*/
	CONSTRAINT [pk_serviceID] PRIMARY KEY ([ServiceID] ASC)
)

/**************************************
Chase Martin
Created: 2021/03/26 
Description: Created Service data.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating Service test data ***'
GO
INSERT INTO [dbo].[Service]
		([BusinessName], [ServiceName], [ServiceCategoryName], [Available], [ScheduleRequired], [ServiceDescription], [ServiceProvider], [ServiceScheduleStart], [ServiceScheduleEnd])
	VALUES
		('Slicked Back', 'Haircut', 'Self Care/Hygiene', '1', '1', 'Slicked Back haircuts are known to give the best "Slicked Back" cuts!', 'Mary Kyte', '2021-04-22 06:30:00', '2021-04-22 07:00:00'),
		('Waverly', 'Daycare', 'Childcare', '1', '1', 'Waverly Child Care and Preschool was founded as a preschool program in 1970 with 7 children.', 'Joanne Williams', '2021-04-20 08:00:00', '2021-04-20 17:00:00'),
		('Ameriprise', 'Savings', 'Financial Counseling', '1', '1', 'Diversified financial services company and bank holding company.', 'Steve Thompson', '2021-04-10 10:30:00', '2021-04-10 11:30:00'),
		('Alcoholics Anonymous', 'A.A. Meetings', 'Rehabilitation', '1', '1', 'International mutual aid fellowship that helps its members stay sober/achieve alcohol sobriety', 'Kelly Crosby', '2021-05-07 20:00:00', '2021-05-07 21:00:00'),
		('First Presbyterian Church', 'Can Drives', 'Food', '1', '1', 'Community can drives for anyone needing food','Presbyterian Church','2021-05-07 20:00:00','2021-05-07 21:00:00'),
		('Narcotics Anonymous', 'N.A. Meetings', 'Rehabilitation', '1', '1', 'International mutual aid fellowship that helps its member to stay sober/achieve sobriety', 'James Maxwell', '2021-05-07 20:00:00', '2021-05-07 21:00:00'),
		('Pilot Flying J Travel Center', 'Free Shower', 'Self Care/Hygiene', '1', '1', 'Down on your luck? Come get a free shower at our truck stop!', 'Jimmy Haslam', '2021-05-07 20:00:00', '2021-05-07 21:00:00')
GO

/**************************************
Thomas Stout
Created: 2021/04/16 
Description: Created ServiceSchedule data.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print''print'***create ServiceSchedule table***'
GO
CREATE TABLE [dbo].[ServiceSchedule](
	[BusinessName]				[nvarchar](200),
	[ServiceName]				[nvarchar](200),
	[ServiceScheduleStart]		[datetime],
	[ServiceScheduleEnd]		[datetime]
)

/**************************************
Thomas Stout
Created: 2021/04/16 
Description: Created ServiceSchedule data.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating ServiceSchedule test data ***'
GO
INSERT INTO [dbo].[ServiceSchedule]
		([BusinessName], [ServiceName], [ServiceScheduleStart], [ServiceScheduleEnd])
	VALUES
		('Slicked Back', 'Haircut', '2021-04-22 06:30:00', '2021-04-22 07:00:00'),
		('Slicked Back', 'Haircut', '2021-04-24 06:30:00', '2021-04-24 06:30:00'),
		('Waverly', 'Daycare', '2021-04-20 08:00:00', '2021-04-20 17:00:00'),
		('Ameriprise', 'Savings', '2021-04-10 10:30:00', '2021-04-10 11:30:00')
GO

/**************************************
Thomas Stout
Created: 2021/04/16
Description: Stored procedure. Selects
all ServiceSchedules
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_Select_All_Services_Schedules ***'
GO
CREATE PROCEDURE [dbo].[sp_Select_All_Services_Schedules]
AS
	BEGIN
		SELECT 	BusinessName, ServiceName, CONCAT(ServiceScheduleStart, " - ", ServiceScheduleEnd) AS Schedule
		FROM 	ServiceSchedule
		ORDER BY ServiceName ASC
	END
GO

/**************************************
Thomas Stout
Created: 2021/03/24 
Description: Created ServiceProvider table.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating serviceProvider table ***'
GO
CREATE TABLE [dbo].[serviceProvider] (
	[ServiceID]				[int]			NOT NULL,
	[ServiceProviderID]		[nvarchar](25)	NOT NULL,
	CONSTRAINT [pk_serviceID_serviceProviderID] PRIMARY KEY([ServiceID], [ServiceProviderID]),
	CONSTRAINT [fk_serviceID] FOREIGN KEY([ServiceID])
		REFERENCES [dbo].[Service]([ServiceID])
)
GO

/**************************************
Chase Martin
Created: 2021/03/26
Description: Stored procedure. Inserts
a new Provider
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_insert_provider ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_provider]
(
	@BusinessName	[nvarchar](200),
	@FirstName		[nvarchar](50),
	@LastName		[nvarchar](50),
	@Address	[nvarchar](100),
	@PhoneNumber	[nvarchar](11),
	@Email		[nvarchar](100),
	@ZipCode		[nvarchar](50),
	@EIN	[nvarchar](50),
	@Activated	[bit],
	@Schedule	[bit]
)
AS
	BEGIN
		INSERT INTO Provider
			(BusinessName, FirstName, LastName, Address, PhoneNumber, Email, ZipCode, EIN, Activated, Schedule)--ServiceCategory, 
		VALUES
			(@BusinessName, @FirstName, @LastName, @Address, @PhoneNumber, @Email, @ZipCode, @EIN, @Activated, @Schedule)--@ServiceCategory
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Chase Martin
Created: 2021/03/26
Description: Stored procedure. Deletes
a Provider
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_delete_provider ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_provider]
(
	@ServiceProviderID		[int]
)
AS
	BEGIN		
		DELETE FROM Provider
			WHERE ServiceProviderID = @ServiceProviderID
		RETURN @@ROWCOUNT
	END
GO


/**************************************
Chase Martin
Created: 2021/03/26
Description: Stored procedure. Selects
all Providers
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_select_all_providers ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_providers]
AS
	BEGIN
		SELECT 	ServiceProviderID, BusinessName, FirstName, LastName, Address, PhoneNumber, Email, ZipCode, EIN, Activated, Schedule--ServiceCategory,
		FROM 	Provider
		ORDER BY ServiceProviderID ASC
	END
GO

/**************************************
Chase Martin
Created: 2021/03/26
Description: Stored procedure. Updates
a Provider
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating sp_update_provider ***'
GO
CREATE PROCEDURE [dbo].[sp_update_provider]
	(
		@ServiceProviderID		[int],
		@BusinessName			[nvarchar](200),
		@FirstName				[nvarchar](50),
		@LastName				[nvarchar](50),
		@Address				[nvarchar](100),
		@PhoneNumber			[nvarchar](11),
		@Email					[nvarchar](100),
		@ZipCode					[nvarchar](50),
		@EIN				[nvarchar](50),
		@Activated				[bit],
		@Schedule				[bit]
	)
AS
	BEGIN
		UPDATE Provider
			SET BusinessName = @BusinessName,
				FirstName = @FirstName,
				LastName = @LastName,
				Address = @Address,
				PhoneNumber = @PhoneNumber,
				Email = @Email,
				ZipCode = @ZipCode,
				EIN = @EIN,
				Activated = @Activated,
				Schedule = @Schedule
			WHERE ServiceProviderID = @ServiceProviderID
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Chase Martin
Created: 2021/03/26
Description: Stored procedure. 
Selects providers by zip code.
**************************************/
print '' print '*** creating sp_select_providers_by_zip_code ***'
GO
CREATE PROCEDURE [dbo].[sp_select_providers_by_zip_code]
	(
		@ZipCode		[nvarchar](50)
	)
AS
	BEGIN
		SELECT		ServiceProviderID, BusinessName, FirstName, LastName, Address, PhoneNumber, Email, ZipCode, EIN, Activated, Schedule--ServiceCategory,
		FROM		Provider 
		WHERE		ZipCode = @ZipCode
		ORDER BY 	ServiceProviderID ASC
	END
GO

/**************************************
Thomas Stout
Created: 2021/03/24
Description: Stored procedure. Inserts
a new Service
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_Insert_Service ***'
GO
CREATE PROCEDURE [dbo].[sp_Insert_Service]
(
	@BusinessName			[nvarchar](200),
	@ServiceName			[nvarchar](200),
	@ServiceCategoryName	[nvarchar](50),
	@Available				[bit],
	@ScheduleRequired		[bit],
	@ServiceDescription		[nvarchar](300),
	@ServiceProvider		[nvarchar](500)
)
AS
	BEGIN
		INSERT INTO Service
			(BusinessName, ServiceName, ServiceCategoryName, Available, ScheduleRequired, ServiceDescription, ServiceProvider)
		VALUES
			(@BusinessName, @ServiceName, @ServiceCategoryName, @Available, @ScheduleRequired, @ServiceDescription, @ServiceProvider)
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Thomas Stout
Created: 2021/03/24
Description: Stored procedure. Deletes
a Service
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_Delete_Service ***'
GO
CREATE PROCEDURE [dbo].[sp_Delete_Service]
(
	@ServiceID		[int]
)
AS
	BEGIN		
		DELETE FROM Service
			WHERE ServiceID = @ServiceID
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Thomas Stout
Created: 2021/03/24
Description: Stored procedure. Selects
all Services
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_Select_All_Services ***'
GO
CREATE PROCEDURE [dbo].[sp_Select_All_Services]
AS
	BEGIN
		SELECT 	ServiceID, BusinessName, ServiceName, ServiceCategoryName, Available, ScheduleRequired, ServiceDescription, ServiceProvider
		FROM 	Service
		ORDER BY ServiceID ASC
	END
GO

/**************************************
Thomas Stout
Created: 2021/03/24
Description: Stored procedure. Updates
a Service
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating sp_Update_Service ***'
GO
CREATE PROCEDURE [dbo].[sp_Update_Service]
	(
		@ServiceID				[int],
		@BusinessName			[nvarchar](200),			
		@ServiceName			[nvarchar](200),
		@ServiceCategoryName	[nvarchar](50),
		@Available				[bit],
		@ScheduleRequired		[bit],
		@ServiceDescription		[nvarchar](300),
		@ServiceProvider		[nvarchar](500)
	)
AS
	BEGIN
		UPDATE Service
			SET BusinessName = @BusinessName,
				ServiceName = @ServiceName,
				ServiceCategoryName = @ServiceCategoryName,
				Available = @Available,
				ScheduleRequired = @ScheduleRequired,
				ServiceDescription = @ServiceDescription,
				ServiceProvider = @ServiceProvider
			WHERE ServiceID = @ServiceID
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Thomas Stout
Created: 2021/04/21
Description: Created ClientSavedSchedules
table.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print''print'***create ClientSavedSchedules table***'
GO
CREATE TABLE [dbo].[ClientSavedSchedules](
	[ScheduleID]				[int] IDENTITY(100000, 1)	NOT NULL,
	[ClientID]					[int]						NULL,
	[ServiceID]					[int]						NOT NULL,
	[ServiceScheduleStart]		[datetime]					NOT NULL,
	[ServiceScheduleEnd]		[datetime]					NOT NULL,
	CONSTRAINT [pk_scheduleID] PRIMARY KEY ([ScheduleID] ASC),
	CONSTRAINT [fk_clientID] FOREIGN KEY([ClientID])
		REFERENCES [dbo].[Client]([ClientID]),
	CONSTRAINT [fk_serviceID_clientsavedschedules] FOREIGN KEY([ServiceID])
		REFERENCES [dbo].[Service]([ServiceID])
)

/**************************************
Thomas Stout
Created: 2021/04/23 
Description: Created Schedule Data.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating Service test data ***'
GO
INSERT INTO [dbo].[ClientSavedSchedules]
		([ServiceID], [ServiceScheduleStart], [ServiceScheduleEnd])
	VALUES
		(100000, '2021-05-08 06:30:00', '2021-05-08 07:00:00'),
		(100000, '2021-05-08 07:30:00', '2021-05-08 08:00:00'),
		(100000, '2021-05-22 08:00:00', '2021-05-22 08:30:00'),
		(100001, '2021-05-09 10:00:00', '2021-05-09 10:30:00'),
		(100001, '2021-05-09 10:30:00', '2021-05-09 11:00:00'),
		(100002, '2021-05-12 09:30:00', '2021-05-12 10:00:00'),
		(100002, '2021-05-14 09:30:00', '2021-05-14 10:00:00'),
		(100003, '2021-05-07 20:00:00', '2021-05-07 21:00:00'),
		(100003, '2021-05-14 20:00:00', '2021-05-14 21:00:00'),
		(100003, '2021-05-21 20:00:00', '2021-05-21 21:00:00'),
		(100003, '2021-05-28 20:00:00', '2021-05-28 21:00:00'),
		(100004, '2021-05-08 10:00:00', '2021-05-08 11:00:00'),
		(100004, '2021-05-29 10:00:00', '2021-05-29 11:00:00'),
		(100004, '2021-06-19 10:00:00', '2021-06-19 11:00:00'),
		(100004, '2021-07-10 10:00:00', '2021-07-10 11:00:00'),
		(100005, '2021-05-07 20:00:00', '2021-05-07 21:00:00'),
		(100005, '2021-05-14 20:00:00', '2021-05-14 21:00:00'),
		(100005, '2021-05-21 20:00:00', '2021-05-21 21:00:00'),
		(100005, '2021-05-28 20:00:00', '2021-05-28 21:00:00'),
		(100006, '2021-05-14 8:00:00', '2021-05-14 8:30:00'),
		(100006, '2021-05-21 8:00:00', '2021-05-21 8:30:00'),
		(100006, '2021-05-28 8:30:00', '2021-05-14 9:00:00'),
		(100006, '2021-05-28 8:30:00', '2021-05-14 9:00:00')
		
GO

/**************************************
Thomas Stout
Created: 2021/04/15
Description: Stored procedure. Selects
all Schedules
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_Select_All_Service_Schedules_By_ID ***'
GO
CREATE PROCEDURE [dbo].[sp_Select_All_Service_Schedules_By_ID]
(
	@ServiceID [int]
)
AS
	BEGIN
		SELECT 	BusinessName AS "Business", ServiceName AS "Service", ClientSavedSchedules.ServiceScheduleStart AS "Start Date/Time", 
			ClientSavedSchedules.ServiceScheduleEnd AS "End Date/Time", ClientSavedSchedules.ScheduleID, ClientSavedSchedules.ClientID
		FROM 	Service
		JOIN ClientSavedSchedules 
			ON ClientSavedSchedules.ServiceID = Service.ServiceID
		WHERE Service.ServiceID = @ServiceID
	END
GO

/**************************************
Thomas Stout
Created: 2021/04/21
Description: Stored procedure. Inserts
a client saved schedule.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_Insert_Client_Schedule ***'
GO
CREATE PROCEDURE [dbo].[sp_Insert_Client_Schedule]
(
	@ServiceScheduleStart	[datetime],
	@ServiceScheduleEnd		[datetime]
)
AS
	BEGIN
		INSERT INTO ClientSavedSchedules
			(ServiceScheduleStart, ServiceScheduleEnd)
		VALUES
			(@ServiceScheduleStart, @ServiceScheduleEnd)
		RETURN @@ROWCOUNT
	END
GO

/* NEED EDIT CLIENT SAVED SCHEDULES, TO ADD CLIENT ID TO ROW with @clientID and @scheduleID*/

/**************************************
Thomas Stout
Created: 2021/04/23
Description: Stored procedure. Updates
a Saved Schedule
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating sp_Update_Client_Schedule ***'
GO
CREATE PROCEDURE [dbo].[sp_Update_Client_Schedule]
	(
		@ClientID				[int],
		@ScheduleID				[int]
	)
AS
	BEGIN
		UPDATE ClientSavedSchedules
			SET ClientID = @ClientID
			WHERE ScheduleID = @ScheduleID
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Nate Hepker
Created: 2021/04/30
Description: Stored procedure. Selects all the selected clients service schedules
a Saved Schedule
**************************************/
print'' print '*** creating sp_select_all_client_schedules NHNH***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_client_schedules]
(
	@ClientID	[int]
)
AS
	BEGIN
		SELECT ClientSavedSchedules.ScheduleID, Provider.BusinessName, Service.ServiceName, ClientSavedSchedules.ServiceScheduleStart, 
		ClientSavedSchedules.ServiceScheduleEnd, ClientSavedSchedules.ClientID, ClientSavedSchedules.ServiceID
		
		FROM ClientSavedSchedules
		JOIN Service
			ON ClientSavedSchedules.ServiceID = Service.ServiceID
		JOIN Provider
			ON Service.BusinessName = Provider.BusinessName
		JOIN ServiceSchedule
			ON Provider.BusinessName = ServiceSchedule.BusinessName
		WHERE ClientSavedSchedules.ClientID = @ClientID
	END
GO
/**************************************
Thomas Stout
Created: 2021/04/27
Description: Stored procedure. Selects
all Client Saved Schedules by their Client
ID.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_select_all_saved_schedules_by_client_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_saved_schedules_by_client_id]
(
	@ClientID [int]
)
AS
	BEGIN
		SELECT 	BusinessName AS "Business", ServiceName AS "Service", ClientSavedSchedules.ServiceScheduleStart AS "Start Date/Time", 
			ClientSavedSchedules.ServiceScheduleEnd AS "End Date/Time"
		FROM 	Service
		JOIN ClientSavedSchedules 
			ON ClientSavedSchedules.ServiceID = Service.ServiceID
		WHERE ClientSavedSchedules.ClientID = @ClientID
	END
GO