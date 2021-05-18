print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO



/**************************************
Chase Martin
Created: 2021/03/09
Description: Created FinancialCounseling table.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating FinancialCounseling table ***'
GO
CREATE TABLE [dbo].[FinancialCounseling] (
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
Created: 2021/03/09 
Description: Created FinancialCounseling test records.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating FinancialCounseling test records ***'
GO
INSERT INTO [dbo].[FinancialCounseling]
		([ServiceID], [ServiceProviderID], [ServiceName], [Available], [ScheduleRequired], [ServiceDescription]) 
	VALUES
		('1', '001', 'Budget', '1', '1', 'Type of counseling'),
		('2', '002', 'Credit', '1', '1', 'Type of counseling'),
		('3', '003', 'Debt', '1', '1', 'Types of counseling')
		
GO

/**************************************
Chase Martin
Created: 2021/03/09 
Description: Created sp_select_all_financial_counseling_types.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating sp_select_all_financial_counseling_types ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_financial_counseling_types]
AS
	BEGIN
		SELECT ServiceID, ServiceProviderID, ServiceName, Available, ScheduleRequired, ServiceDescription
		FROM FinancialCounseling
	END
GO
