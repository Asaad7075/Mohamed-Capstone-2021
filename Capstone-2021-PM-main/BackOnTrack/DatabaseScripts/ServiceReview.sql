print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Chase Martin
Created: 2021/04/09
Description: Table for ServiceReviews
***************************************/
print''print'*** Creating the table for Service Reviews ***'
GO
CREATE TABLE [dbo].[ServiceReview](
	[ServiceReviewID]	[int]IDENTITY(100000, 1)	NOT NULL,
	[ServiceName]	[nvarchar](200)			NOT NULL,
	[ProviderFirstName]		[nvarchar](50)				NOT NULL,
	[ProviderLastName]		[nvarchar](50)				NOT NULL,
	[Rating]		[nvarchar](50)				NOT NULL,
	[ClientComment]	[nvarchar](500)		NOT NULL,
	CONSTRAINT [pk_serviceReview_serviceReviewID] PRIMARY KEY([ServiceReviewID] ASC)--,
	--CONSTRAINT [fk_serviceReview_serviceID] FOREIGN KEY([ServiceID])
			--REFERENCES [dbo].[Service]([ServiceID])--,
	)
GO

/**************************************
Chase Martin
Created: 2021/04/09
Description: Creating test data for SelectAllServiceReviews
from ServiceReview.
***************************************/
print '' print '*** creating service review test records ***'
GO
INSERT INTO [dbo].[ServiceReview]
		([ServiceName], [ProviderFirstName], [ProviderLastName], [Rating], [ClientComment])
	VALUES
		('Haircut', 'Bailey', 'Hauser', '3', 'Could work on cutomer service response time.'),
		('Budget Counseling', 'Steven', 'Hougse', '5', 'Excellent Counseling, great outcome.'),
		('Haircut', 'Randy', 'Hanes', '2.5', 'Poor communication with scheduling.'),
		('Babysitting', 'Mickey', 'Hines', '4.5', 'Excellent babysitter! My child had a great time.'),
		('Nanny', 'Bailey', 'Rud', '4', 'Great flexibility with scheduling.'),
		('Consulting', 'Steven', 'Goldberg', '5', 'Excellent plan, great outcome.'),
		('Home Care', 'Chase', 'Hanson', '3.5', 'Poor communication understanding my needs.'),
		('Family Care', 'Ricky', 'Bobby', '3', 'Excellent guy, will want him again soon!'),
		('Haircut', 'Charles', 'Wanston', '2', 'Bad scheduling, had to wait 30 minutes.'),
		('Credit Counseling', 'Bob', 'Martin', '4', 'Very happy with results!'),
		('Debt Counseling', 'Jim', 'Glasgow', '5', 'Outstanding outcome, learned many new things.')
GO

/**************************************
Chase Martin
Created: 2021/04/09
Description: Creates a store procedure for getting all the service reviews
***************************************/
print''print'*** Creating sp_select_all_service_reviews ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_service_reviews]
AS
	BEGIN
		SELECT ServiceReviewID, ServiceName, ProviderFirstName, ProviderLastName, Rating, ClientComment
		FROM ServiceReview
		ORDER BY ServiceReviewID ASC
	END
GO

/**************************************
Chase Martin
Created: 2021/4/16
Description: Creates a store procedure for updating 
a service review
***************************************/
print''print'*** Creating sp_update_service_review_from_client ***'
GO
CREATE PROCEDURE [dbo].[sp_update_service_review_from_client]
	(
		@ServiceReviewID	[int],
		@NewServiceName		[nvarchar](200),
		@NewProviderFirstName [nvarchar](50),
		@NewProviderLastName  [nvarchar](50),
		@NewRating			[nvarchar](50),
		@NewClientComment			[nvarchar](500),
		@OldServiceName		[nvarchar](200),
		@OldProviderFirstName [nvarchar](50),
		@OldProviderLastName  [nvarchar](50),
		@OldRating			[nvarchar](50),
		@OldClientComment			[nvarchar](500)
	)
AS
	BEGIN
		UPDATE ServiceReview
			SET ServiceName = @NewServiceName,
				ProviderFirstName = @NewProviderFirstName,
				ProviderLastName = @NewProviderLastName,
				Rating = @NewRating,
				ClientComment = @NewClientComment
			WHERE ServiceReviewID = @ServiceReviewID
			AND ServiceName = @OldServiceName
			AND ProviderFirstName = @OldProviderFirstName
			AND ProviderLastName = @OldProviderLastName
			AND Rating = @OldRating
			AND ClientComment = @OldClientComment
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Chase Martin
Created: 2021/04/09
Description: Creats a store procedure for inserting a new ServiceReview
into the database a service to review.
***************************************/
print '' print '**** Creating sp_insert_service_review ****'
GO
CREATE PROCEDURE [dbo].[sp_insert_service_review]
(
	@ServiceName	[nvarchar](200),
	@ProviderFirstName		[nvarchar](50),
	@ProviderLastName		[nvarchar](50),
	@Rating			[nvarchar](50),
	@ClientComment	[nvarchar](500)
)
AS
	BEGIN
		INSERT INTO [dbo].[ServiceReview]
			([ServiceName], [ProviderFirstName], [ProviderLastName], [Rating], [ClientComment])    
		VALUES
			(@ServiceName, @ProviderFirstName, @ProviderLastName, @Rating, @ClientComment)    
	END
GO

/**************************************
Chase Martiin
Created: 2021/04/09
Description: Creats a store procedure for getting 
the services by the serviceReviewID to use for selecting
a service to review.
***************************************/
print''print'*** Creating sp_select_service_reviews_by_service_review_id ***'
GO

CREATE PROCEDURE [dbo].[sp_select_service_reviews_by_service_review_id]
	(
		@ServiceReviewID		[int]
	)
AS
	BEGIN
		SELECT ServiceReviewID, ServiceName, ProviderFirstName, ProviderLastName, Rating, ClientComment
		FROM [dbo].ServiceReview
		WHERE ServiceReviewID = @ServiceReviewID
	END
GO

/**************************************
Chase Martin
Created: 2021/04/09
Description: Creating test data for SelectAllServiceReviews
from ServiceReview.
***************************************/
print'' print'**** creating sp_delete_service_review*****'
GO
CREATE PROCEDURE [dbo].[sp_delete_service_review]
(
	@ServiceReviewID  [int]
)
AS
	BEGIN TRY
		DELETE FROM ServiceReview
		WHERE ServiceReviewID = @ServiceReviewID
		COMMIT
	END TRY
	BEGIN CATCH
		
		ROLLBACK
		RETURN -10

	END CATCH
GO


