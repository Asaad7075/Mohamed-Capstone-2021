print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO

/**********************************************
	Asaad Mohamed
    Created: 2021/03/01
	Description: This Table for Donation
********************************************** */
DROP TABLE  if exists[Donation]
print '' print '*** creating Donationtable ***'
GO


CREATE TABLE [dbo].[Donation](
	  [DonationID]	      [INT ]		    IDENTITY(1000000, 1)NOT NULL   
     , [DonorID]		  [INT]			            NOT NULL  
	 , [NameOfItem]	      [nvarchar](250)			NOT NULL  
     , [Description]      [nvarchar](250)		    NOT NULL  
     , [EstValue] 		  [DECIMAL](9, 2)		    NOT NULL  
     , [AgeofItem]        [INT]                     NOT NULL 
	 , [DropOff]          [BIT]                     NOT NULL  
	 , [PickUp]           [BIT]                     NOT NULL
	 , [PickUpDateTime]   [DATETIME]                 NOT NULL
	 , [DonatedImage]	  [varbinary](MAX)	         NULL
	 , [MailReceipt]      [BIT]                     NOT NULL 
	 , [EmailReceipt]     [BIT]                     NOT NULL 
	 , [DonationStatus]   [nvarchar] (50)                    NOT  NULL
	 ,CONSTRAINT [pk_DonationID] PRIMARY KEY([DonationID] ASC)
	 /*, CONSTRAINT [ak_donarID] UNIQUE([DonorID] ASC)*/
  
) 
GO

/**********************************************
	Asaad Mohamed
    Created: 2021/03/01
	Description: This Table for Donation

	-- This Donation test records
	
********************************************** */

print '' print '*** creating Donation test records ***'

GO

INSERT INTO [dbo].[Donation]
                   ([DonorID],[NameOfItem],[Description],[EstValue],[AgeofItem],[DropOff],[PickUp],[PickUpDateTime],[MailReceipt],[EmailReceipt],[DonationStatus])
				    VALUES
					(1000001, 'Wheelchair', 		'Medical supplies', 99.90, 		2, 0, 1, '2018-09-12', 0, 1, 'denied'),
					(1000002, 'Operating Room', 	'Medical Supplies', 200000.90,	5, 1, 0, '2018-09-12', 1, 0, 'pending'),
					(1000003, 'Brown side table',	'Office Supplies',  50.00,		4, 0, 1, '2021-02-12', 0, 1, 'approved'),
					(1000004, 'Couch', 				'Furniture',  		450.00,		1, 1, 0, '2021-02-12', 0, 1, 'pending'),
					(1000005, 'Suit jacket', 		'Clothing',			80.00,		6, 0, 1, '2021-02-12', 0, 1, 'approved'),
					(1000006, 'Necktie', 			'Clothing',  		30.00,		5, 0, 1, '2021-02-12', 0, 1, 'denied'),
					(1000007, 'Frying Pan', 		'Kitchenware', 		40.00,		2, 0, 1, '2021-02-12', 0, 1, 'approved'),
					(1000008, 'Knife Set', 			'Kitchenware',  	150.00,		4, 0, 1, '2021-02-12', 0, 1, 'approved'),
					(1000009, 'Black Dress Shoes',  'Clothing',  		75.00,		3, 0, 1, '2021-02-12', 0, 1, 'pending'),
					(1000010, 'Red Dress', 			'Clothing',			350.00,		1, 0, 1, '2021-02-12', 0, 1, 'denied'),
					(1000011, 'Sunglasses', 		'Accessories',  	100.00,		1, 0, 1, '2021-02-12', 0, 1, 'approved'),
					(1000013, 'Blue Dress Shirt', 	'Clothing',  		20.00,		2, 0, 1, '2021-02-12', 0, 1, 'pending'),
					(1000014, 'Slacks', 			'Clothing',  		40.00,		2, 0, 1, '2021-02-12', 0, 1, 'denied'),
					(1000015, 'Computer', 			'Electronic',  		200.00,		8, 0, 1, '2021-02-12', 0, 1, 'approved'),
					(1000016, 'Monitor', 			'Electronic',  		60.00,		8, 0, 1, '2021-02-12', 0, 1, 'approved')
GO

/**********************************************
	Asaad Mohamed
    Created: 2021/03/01
	Description: This Table for Donation

	-- Stored Procedure for select all donation 
	
********************************************** */
DROP PROCEDURE  if exists[sp_select_all_Donation]
print '' print '*** creating sp_select_all_Donation ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_Donation]
AS
	BEGIN
		SELECT		[DonationID],[DonorID],[NameOfItem],[Description],[EstValue],[AgeofItem],[DropOff]
				   ,[PickUp],[PickUpDateTime],[MailReceipt],[EmailReceipt],[DonationStatus]
		FROM		Donation 
		ORDER BY 	DonationID ASC
	END
GO
/**********************************************
	Asaad Mohamed
    Created: 2021/03/01
	Description: This Table for Donation

	-- Stored Procedure for update donation Item
	
********************************************** */
DROP PROCEDURE  if exists[sp_update_donation_item]
print '' print '*** Creating sp_update_donation_item'
GO
CREATE PROCEDURE [sp_update_donation_item]
(
	@DonationID              [int] ,
	@DonorID		         [int],
	
	@NewNameOfItem	         [nvarchar](250),
	@NewDescription		     [nvarchar](250),
	@NewEstValue		     [DECIMAL](9, 2),
	@NewAgeofItem		     [INT],
	@NewDropOff		         [BIT],
	@NewPickUp		         [BIT],
	@NewPickUpDateTime		 [DATETIME],
	--@NewDonatedImage            [varbinary](MAX),
	@NewMailReceipt		     [BIT],
	@NewEmailReceipt		 [BIT],
	@NewDonationStatus		 [nvarchar](50),
	
	
	
	@OldNameOfItem	         [nvarchar](250),
	@OldDescription		     [nvarchar](250),
	@OldEstValue		     [DECIMAL](9, 2),
	@OldAgeofItem		     [INT],
	@OldDropOff		         [BIT],
	@OldPickUp		         [BIT],
	@OldPickUpDateTime		 [DATETIME],
	--@OldDonatedImage      [varbinary](MAX),
	@OldMailReceipt		     [BIT],
	@OldEmailReceipt		 [BIT],
	@OldDonationStatus		 [nvarchar](50)
	   
)
AS
BEGIN
	UPDATE [dbo].[Donation]
	SET 
	 
     [NameOfItem] = @NewNameOfItem,
	 [Description] = @NewDescription,
	 [EstValue] = @NewEstValue,
	 [AgeofItem] = @NewAgeofItem, 
	 [DropOff] = @NewDropOff,
	 [PickUp] = @NewPickUp,
	 [PickUpDateTime] = @NewPickUpDateTime,
	 --[DonatedImage ] = @NewDonatedImage ,
	 [MailReceipt] = @NewMailReceipt,
     [EmailReceipt] = @NewEmailReceipt,
     [DonationStatus] = @NewDonationStatus
		   
	 
	
	WHERE [DonationID] = @DonationID
	      
     AND  [DonorID] = @DonorID
	 AND[NameOfItem] = @OldNameOfItem	  
	 AND[Description] = @OldDescription		
	 AND[EstValue] = @OldEstValue		
	 AND[AgeofItem] = @OldAgeofItem		
     AND[DropOff] = @OldDropOff		  
     AND[PickUp] = @OldPickUp		  
	 AND[PickUpDateTime] = @OldPickUpDateTime
	 --AND[DonatedImage ] = @OldDonatedImage 
     AND[MailReceipt] = @OldMailReceipt		
     AND[EmailReceipt] = @OldEmailReceipt	
     AND[DonationStatus] = @OldDonationStatus
    	   
	
	
	
		RETURN @@ROWCOUNT
END
GO

/**********************************************
	Asaad Mohamed
    Created: 2021/03/15
	Description: This Table for Donation

	-- Stored Procedure for remove donation Item
	
********************************************** */
DROP PROCEDURE  if exists[sp_delete_Donation]
print '' print '*** Creating sp_delete_Donation'
GO
CREATE PROCEDURE sp_delete_Donation
(
	@DonationID		[int]
)
AS
BEGIN
	
	DELETE FROM
  	Donation
	
	WHERE 	DonationID = @DonationID
	RETURN @@ROWCOUNT
END
GO
/**********************************************
	Asaad Mohamed
    Created: 2021/03/24
	Description: This Table for Donation

	-- Stored Procedure for insert donation sp
	
********************************************** */
     
DROP PROCEDURE  if exists[sp_insert_donation]
print '' print '* Creating sp_insert_donation'
GO

CREATE PROCEDURE sp_insert_donation
(
	  
	   @DonorID		       [INT]			            
	 , @NameOfItem	       [nvarchar](250)			  
     , @Description        [nvarchar]	(250)	    
	 , @EstValue 		   [DECIMAL](9, 2)		     
     , @AgeofItem          [INT]                     
	 , @DropOff            [BIT]                       
	 , @PickUp             [BIT]                     
	 , @PickUpDateTime     [DATETIME] 
	 --, @DonatedImage	   [varbinary](MAX)	     
	 , @MailReceipt        [BIT]                      
	 , @EmailReceipt       [BIT]                     
	 , @DonationStatus     [nvarchar](50)            
	
	
)
AS
BEGIN
	INSERT INTO [dbo].Donation
	(  [DonorID]
	 , [NameOfItem]
	 , [Description] 
	 , [EstValue] 
	 , [AgeofItem ]
	 , [DropOff]                            
	 , [PickUp]                         
	 , [PickUpDateTime]    	
	 --, [DonatedImage] 	        
	 , [MailReceipt]                       
	 , [EmailReceipt]                     
	 , [DonationStatus])

	VALUES
	(  @DonorID
	 , @NameOfItem
	 , @Description  
	 , @EstValue 
	 , @AgeofItem 
	 , @DropOff                                
	 , @PickUp                               
	 , @PickUpDateTime
	 --, @DonatedImage	   	        
	 , @MailReceipt                            
	 , @EmailReceipt                          
	 , @DonationStatus )
	SELECT SCOPE_IDENTITY()
END
GO

/**************************************
Chantal Shirley
Created: 2021/04/08
Description: Donation Item 
Stored Procedure Creation (
sp_update_image_by_donation_id )
**************************************
Chantal Shirley
Created: 2021/04/14
Adding additional stored procedure for getting
donated item by donationId 
sp_select_donation_by_donation_id
**************************************/
print '' print '*** sp_update_image_by_donation_id ***'
GO
CREATE PROCEDURE sp_update_image_by_donation_id
(
	@DonationID			int,
	@DonatedImage		varbinary(max)
)
AS
	BEGIN
		UPDATE
			Donation
		SET
			DonatedImage = @DonatedImage
		WHERE
			DonationID = @DonationID
	END
GO

print '' print '*** sp_select_donation_image_by_donation_id ***'
GO
CREATE PROCEDURE dbo.sp_select_donation_image_by_donation_id
(
	@DonationID int
)
AS
	BEGIN
		SELECT
			DonatedImage
		FROM
			Donation
		WHERE
			DonationID = @DonationID
	END
GO

print '' print '*** sp_select_donation_by_donation_id ***'
GO
CREATE PROCEDURE dbo.sp_select_donation_by_donation_id
(
	@DonationID int
)
AS
	BEGIN
		SELECT *
		FROM
			Donation
		WHERE
			DonationID = @DonationID
	END
GO
		
/**************************************
Chantal Shirley
Created: 2021/04/08
Description: Donation Images Stored
Procedure Tests
**************************************
Chantal Shirley
Updated: 2021/04/12
Adding more images to populate data with.
***************************************/
-- Stored Procedure Test Records
--/**************************************
-- Comment the line above and below, 
-- if you would like to test drivers 
-- license data 

DECLARE @DonatedImage varbinary(max)
Set @DonatedImage = (SELECT * FROM OPENROWSET(BULK '$(FullScriptDir)\Resources\Blue-Wheelchair.jpg', SINGLE_BLOB) as img) -- Image made accessible via Creative Commons License, https://commons.wikimedia.org/wiki/File:Blue-lightweight-wheelchair.jpg
EXEC sp_update_image_by_donation_id
	1000000,
	@DonatedImage
GO

DECLARE @DonatedImage varbinary(max)
Set @DonatedImage = (SELECT * FROM OPENROWSET(BULK '$(FullScriptDir)\Resources\OperatingRoomTent.jpg', SINGLE_BLOB) as img) -- Image made accessible via Creative Commons Licensae, https://commons.wikimedia.org/wiki/File:20060422_Aufblasbares_Sanitaetszelt_30qm_1219.jpg
EXEC sp_update_image_by_donation_id
	1000001,
	@DonatedImage
GO
DECLARE @DonatedImage varbinary(max)
Set @DonatedImage = (SELECT * FROM OPENROWSET(BULK '$(FullScriptDir)\Resources\PicnicTable.jpg', SINGLE_BLOB) as img) -- Image made accessible via Creative Commons License, https://www.flickr.com/photos/bdesham/1129464710/
EXEC sp_update_image_by_donation_id
	1000002,
	@DonatedImage
GO

--EXEC sp_select_donation_by_donation_id 1000000
--***************************************/


/**********************************************
	Richard Schroeder
    Created: 2021/04/14
	Description: Selects donation by donationID

	-- Stored Procedure for select donation by ID
	
********************************************** */
DROP PROCEDURE  if exists[sp_select_donation_by_id]
print '' print '*** creating sp_select_donation_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_donation_by_id]
	(
		@DonationID				[INT]
	)
AS
	BEGIN
		SELECT [DonationID],[DonorID],[NameOfItem],[Description],[EstValue],[AgeofItem],[DropOff],[PickUp],[PickUpDateTime],[MailReceipt],[EmailReceipt],[DonationStatus]
		FROM Donation 
		WHERE DonationID = @DonationID
		RETURN @@ROWCOUNT
	END
GO
