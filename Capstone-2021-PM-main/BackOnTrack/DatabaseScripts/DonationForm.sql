print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO


/**********************************************
	Asaad Mohamed
    Created: 2021/03/01
	Description: This donation form table
	
********************************************** */
DROP TABLE  if exists[DonationForm]
print '' print '*** creating DonationForm table ***'
GO

CREATE TABLE [dbo].[DonationForm](

	  [DonorFormID]	       [INT] 			IDENTITY(1000000, 1)NOT NULL 
	, [DonorID]	           [INT]	            NOT NULL 
    , [DateCreated]		   [DATETIME]			NOT NULL  
	, [Status]	       [nvarchar](50)		    NOT NULL 
	,CONSTRAINT [pk_DonorFormID] PRIMARY KEY([DonorFormID] ASC)
	--,CONSTRAINT [fk_donor_donorID] FOREIGN KEY ([DonorID])
		--REFERENCES [Donor] ([DonorID]) ON UPDATE CASCADE
  
)
			
GO


/**********************************************
	Asaad Mohamed
    Created: 2021/03/01
	Description: This Table for Donation form

	-- Stored Procedure for insert donation form 
	
********************************************** */

print '' print '*** creating DonationForm insert records ***'
GO
INSERT INTO [dbo].[DonationForm]
                   ([DonorID],[DateCreated],[Status] )
				    VALUES
					
			 (1000000, GETDATE(),'Pending')
				 
			,(1000001, GETDATE(),'Inprocessing')
			
			,(1000002, GETDATE(),'Pending')
GO



/**********************************************
	Asaad Mohamed
    Created: 2021/03/01
	Description: This Table for Donor

	-- Stored Procedure for This  select donation Form
	
********************************************** */

DROP PROCEDURE  if exists[sp_select_all_DonationForm]
print '' print '*** creating sp_select_all_DonationForm ***'
GO

CREATE PROCEDURE [dbo].[sp_select_all_DonationForm]
AS
	BEGIN
	SELECT		DonorFormID, DonorID, DateCreated, Status
		FROM		DonationForm 
		ORDER BY 	DonorID ASC
	END
GO

/**********************************************
	Asaad Mohamed
    Created: 2021/03/01
	Description: This Table for Donor

	-- Stored Procedure for insert donation sp
	
********************************************** */

DROP PROCEDURE  if exists[sp_insert_donation_form]
print '' print '* Creating sp_insert_donation_form'
GO

CREATE PROCEDURE [sp_insert_donation_form]
(
	@DonorID		    [int],
	@DateCreated 	    [DATETIME],
	@Status 			[nvarchar](50)
	
)
AS
BEGIN
	INSERT INTO [dbo].[DonationForm]
		([DonorID],[DateCreated],[Status])
	VALUES
		(@DonorID,@DateCreated, @Status)
	SELECT SCOPE_IDENTITY()
END
GO


/**********************************************
	Asaad Mohamed
    Created: 2021/03/01
	Description: This Table for Donor

	-- Stored Procedure for his  update donation form
	
********************************************** */
DROP PROCEDURE  if exists[sp_update_donation_form]
print '' print '*** Creating sp_update_donation_form'
GO
CREATE PROCEDURE [sp_update_donation_form]
(
	@DonorFormID         [int] ,
	@DonorID		     [int],
	
	@NewDateCreated	     [DATETIME],
	@NewStatus		     [nvarchar](50),
	
	
	@OldDateCreated		[DATETIME],
	@OldStatus		    [nvarchar](50)
	   
)
AS
BEGIN
	UPDATE [dbo].[DonationForm]
	SET 
	 
     [DateCreated] = @NewDateCreated,
	 [Status] = @NewStatus
	 
	
	WHERE [DonorFormID] = @DonorFormID
	      
    AND  [DonorID] = @DonorID
	AND [DateCreated] = @OldDateCreated
	AND [Status] = @OldStatus
	
	
		RETURN @@ROWCOUNT
END

GO
	

