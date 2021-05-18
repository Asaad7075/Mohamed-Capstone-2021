print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO

/**********************************************
	Asaad Mohamed
    Created: 2021/03/01
	Description: This Table for Donor
***********************************************
	Chantal Shirley
	Updated: 2021/04/25
	Table updated to reflect donor's web credentials.
	
********************************************** */
DROP TABLE  if exists[Donor]
print '' print '*** creating Donor  table ***'
GO

CREATE TABLE [dbo].[Donor](

	  [DonorID]	           [INT] 			IDENTITY(1000000, 1)NOT NULL 
	  ,[Business]          [bit]                    NOT NULL  
	  ,[Individual]        [bit]                    NOT NULL  
	, [BusinessName]	   [nvarchar](100)		    NOT NULL 
    , [FirstName]		   [nvarchar](50)			NOT NULL  
	, [LastName]	       [nvarchar](50)		    NOT NULL  
	, [MiddleInitial]	   [nvarchar](1)		    NOT NULL 
    , [Address]            [nvarchar](100)		    NOT NULL     
    , [ZipCode]		       [nvarchar](10)		    NOT NULL   
    , [PhoneNumber]        [nvarchar](11)           NOT NULL 
	, [Email]              [nvarchar](250)          NOT NULL 
	, [SS]                 [nvarchar](11)            NULL 
	, [EIN]                [nvarchar](11)            NULL 
	 ,[Active]             [bit]		            NOT NULL DEFAULT 1
	 , [PasswordHash]		nvarchar(100)			NOT NULL DEFAULT
	'B03DDF3CA2E714A6548E7495E2A03F5E824EAAC9837CD7F159C67B90FB4B7342'
	,CONSTRAINT [pk_DonorID] PRIMARY KEY([DonorID]ASC )
	, CONSTRAINT [ak_emailAddress] UNIQUE([Email] ASC)
   
) 
GO

/**********************************************
	Asaad Mohamed
    Created: 2021/03/01
	Description: This Table for Donor

	-- This Donor test records
	
********************************************** */
print '' print '*** creating Donor test records ***'
GO
INSERT INTO [dbo].[Donor]
                   ([Business], [Individual], [BusinessName],[FirstName],[LastName],[MiddleInitial],[Address],[ZipCode],[PhoneNumber]
					   ,[Email],[SS],[EIN] )
				    VALUES
					
			 (0 , 1 ,'American Eagle','Brandon', 'Samuel','A', '1501 Skyland Blvd E','45687','5368746987','samuel22@gmail.com','','')
				 
			,(1, 0, 'Costco','Debra', 'Tyler','B', '1420 Us 231 South','15409','3248754989','tyler555@gmail.com','','')
			
			,(1, 0, 'Gap','Sophia', 'Oliver','C', '1420 Us 231 South','13339','333333989','sophia@gmail.com','','')
			
			
	GO

/**********************************************
	Asaad Mohamed
    Created: 2021/03/01
	Description: This Table for Donor

	-- Stored Procedure for select all donor by active 
	
********************************************** */
DROP PROCEDURE  if exists[sp_select_all_Donor_by_active]
print '' print '*** Creating sp_select_all_Donor_by_active'

GO

CREATE PROCEDURE [sp_select_all_Donor_by_active]
(
	@Active			[bit]
)
AS
BEGIN
	SELECT 	DonorID,  Business, Individual, BusinessName, FirstName, LastName,MiddleInitial,Address,ZipCode ,PhoneNumber
					,Email , SS, EIN,Active
	FROM 	[dbo].[Donor]
	WHERE 	[Active] = @Active
END
GO


/**********************************************
	Asaad Mohamed
    Created: 2021/04/01
	Description: This Table for Donor

	-- Stored Procedure for select all donor by id 
	
********************************************** */
DROP PROCEDURE  if exists[sp_select_all_Donor_by_id]
print '' print '*** Creating sp_select_all_Donor_by_id'

GO

CREATE PROCEDURE [sp_select_all_Donor_by_id]
(
	@DonorID			[int]
)
AS
BEGIN
	SELECT 	DonorID, Business, Individual, BusinessName, FirstName, LastName,MiddleInitial,Address,ZipCode ,PhoneNumber
					,Email , SS, EIN,Active
	FROM 	[dbo].[Donor]
	WHERE 	[DonorID] = @DonorID
END
GO

/**********************************************
	Asaad Mohamed
    Created: 2021/04/01
	Description: This Table for Donor

	-- Stored Procedure for insert donor sp
	
********************************************** */
     
DROP PROCEDURE  if exists[sp_insert_donor]
print '' print '* Creating sp_insert_donor'
GO

CREATE PROCEDURE sp_insert_donor
(
	  
	    @Business       [BIT]
	   ,@Individual     [BIT]
	   ,@BusinessName   [nvarchar](100)
	   ,@FirstName      [nvarchar](50)
	   ,@LastName       [nvarchar](50)
	   ,@MiddleInitial  [nvarchar](1)
	   ,@Address        [nvarchar](100)
	   ,@ZipCode        [nvarchar](10)
	   ,@PhoneNumber    [nvarchar](11)
	   ,@Email          [nvarchar](250)
	   ,@SS             [nvarchar](11)
	   ,@EIN            [nvarchar](11)  
	    
	
	
)
AS
BEGIN
	INSERT INTO [dbo].Donor
	(  [Business]
	 , [Individual]
	 , [BusinessName] 
	 , [FirstName] 
	 , [LastName ]
	 , [MiddleInitial]                            
	 , [Address]                         
	 , [ZipCode]         
	 , [PhoneNumber]                       
	 , [Email]                     
	 , [SS]
	 , [EIN])

	VALUES
	( 
	   @Business
	 , @Individual
	 , @BusinessName
	 , @FirstName 
	 , @LastName 
	 , @MiddleInitial                          
	 , @Address                         
	 , @ZipCode        
	 , @PhoneNumber                       
	 , @Email                    
	 , @SS
	 , @EIN)
	SELECT SCOPE_IDENTITY()
END
GO

/**********************************************
	Asaad Mohamed
    Created: 2021/04/01
	Description: This Table for Donor

	-- Stored Procedure for update donor 
	
********************************************** */
DROP PROCEDURE  if exists[sp_update_donor]
print '' print '*** Creating sp_update_donor'
GO
CREATE PROCEDURE [sp_update_donor]
(
	
	@DonorID		         [int],
	@OldBusiness	         [BIT],
	@OldIndividual	         [BIT],
	@OldBusinessName		 [nvarchar](100),
	@OldFirstName		     [nvarchar](50),
	@OldLastName		     [nvarchar](50),
	@OldMiddleInitial		 [nvarchar](1),
	@OldAddress		         [nvarchar](100),
    @OldZipCode              [nvarchar](10),
	@OldPhoneNumber			 [nvarchar](11),
	@OldEmail		         [nvarchar](250),
	@OldSS		             [nvarchar](11),
    @OldEIN		             [nvarchar](11),
	
	@NewBusiness	         [BIT],
	@NewIndividual	         [BIT],
	@NewBusinessName		 [nvarchar](100),
	@NewFirstName		     [nvarchar](50),
	@NewLastName		     [nvarchar](50),
	@NewMiddleInitial		 [nvarchar](1),
	@NewAddress		         [nvarchar](100),
    @NewZipCode              [nvarchar](10),
	@NewPhoneNumber			 [nvarchar](11),
	@NewEmail		         [nvarchar](250),
	@NewSS		             [nvarchar](11),
	@NewEIN		             [nvarchar](11)
		
	   
)
AS
BEGIN
	UPDATE [dbo].[Donor]
	SET 
	 
     [Business] = @NewBusiness,
	 [Individual] = @NewIndividual,
	 [BusinessName] = @NewBusinessName,
	 [FirstName] = @NewFirstName, 
	 [LastName] = @NewLastName,
	 [MiddleInitial] = @NewMiddleInitial,
	 [Address] = @NewAddress,
	 [ZipCode ] = @NewZipCode ,
	 [PhoneNumber] = @NewPhoneNumber,
	 [Email] = @NewEmail,
     [SS] = @NewSS,
     [EIN] = @NewEIN
		   
	 
	
	WHERE [DonorID] = @DonorID
	      
     AND  [Business] = @OldBusiness
	 AND[Individual] = @OldIndividual	  
	 AND[BusinessName] = @OldBusinessName		
	 AND[FirstName] = @OldFirstName		
	 AND[LastName] = @OldLastName		
     AND[MiddleInitial] = @OldMiddleInitial		  
     AND[Address] = @OldAddress		  
	 AND[ZipCode] = @OldZipCode
	 AND[PhoneNumber] = @OldPhoneNumber
	 AND[Email ] = @OldEmail 
     AND[SS] = @OldSS		
     AND[EIN] = @OldEIN	
     
		RETURN @@ROWCOUNT
END
GO


/**********************************************
	Asaad Mohamed
    Created: 2021/04/01
	Description: This Table for Donor

	-- Stored Procedure for remove Donor 
	
********************************************** */
DROP PROCEDURE  if exists[sp_delete_Donor]
print '' print '*** Creating sp_delete_Donor'
GO
CREATE PROCEDURE [sp_delete_Donor]
(
	@DonorID		[int]
)
AS
BEGIN
	UPDATE  	Donor
	SET Active = 0
	WHERE 	DonorID = @DonorID
	RETURN @@ROWCOUNT
END
GO

/****************************
Chantal Shirley
Created: 2021/04/25
Stored procedures: 
sp_select_donor_by_email,
sp_select_donor_by_email_and_password,
sp_insert_donor_from_web (slightly modified Asaad's code)
****************************/
print '' print '*** creating sp_select_donor_by_email ***'
GO
CREATE PROCEDURE dbo.sp_select_donor_by_email
(
	@Email	[nvarchar](100)
)
AS
	BEGIN
		SELECT
			COUNT(Email)
		FROM
			Donor
		WHERE
			Email = @Email
	END
GO

--Exec sp_select_donor_by_email 'samuel22@gmail.com'
--GO

print '' print '*** creating sp_select_donor_by_email_and_password ***'
GO
CREATE PROCEDURE dbo.sp_select_donor_by_email_and_password
(
	@Email [nvarchar](100),
	@Password [nvarchar](100)
)
AS
	BEGIN
		SELECT COUNT(Email)
		FROM
			Donor
		WHERE
			Email = @Email AND
			PasswordHash = @Password
	END
GO

--Exec sp_select_donor_by_email_and_password 'samuel22@gmail.com', 'B03DDF3CA2E714A6548E7495E2A03F5E824EAAC9837CD7F159C67B90FB4B7342'
--GO

PRINT '' PRINT '*** creating sp_insert_donor_from_web ***'
GO
CREATE PROCEDURE dbo.sp_insert_donor_from_web
(
	  
	    @Business       [BIT]
	   ,@Individual     [BIT]
	   ,@BusinessName   [nvarchar](100)
	   ,@FirstName      [nvarchar](50)
	   ,@LastName       [nvarchar](50)
	   ,@MiddleInitial  [nvarchar](1)
	   ,@Address        [nvarchar](100)
	   ,@ZipCode        [nvarchar](10)
	   ,@PhoneNumber    [nvarchar](11)
	   ,@Email          [nvarchar](250)
	   ,@SS             [nvarchar](11)
	   ,@EIN            [nvarchar](11)  
	   ,@Password		[nvarchar](100)
	    
	
	
)
AS
	BEGIN
		INSERT INTO [dbo].Donor
		(  [Business]
		 , [Individual]
		 , [BusinessName] 
		 , [FirstName] 
		 , [LastName ]
		 , [MiddleInitial]                            
		 , [Address]                         
		 , [ZipCode]         
		 , [PhoneNumber]                       
		 , [Email]                     
		 , [SS]
		 , [EIN]
		 , [PasswordHash]
		 )
		VALUES
		( 
		   @Business
		 , @Individual
		 , @BusinessName
		 , @FirstName 
		 , @LastName 
		 , @MiddleInitial                          
		 , @Address                         
		 , @ZipCode        
		 , @PhoneNumber                       
		 , @Email                    
		 , @SS
		 , @EIN
		 , @Password)
		SELECT @@ROWCOUNT
	END
GO

--EXEC sp_insert_donor_from_web
--	0
--	, 0
--	, 'Not Set'
--	, 'John'
--	, 'Doe'
--	, 'Not Set'
--	, 'Not Set'
--	, 'Not Set'
--	, 'Not Set'
--	, 'john@gmail.com'
--	, 'Not Set'
--	, 'Not Set'
--	, 'B03DDF3CA2E714A6548E7495E2A03F5E824EAAC9837CD7F159C67B90FB4B7342'
--GO