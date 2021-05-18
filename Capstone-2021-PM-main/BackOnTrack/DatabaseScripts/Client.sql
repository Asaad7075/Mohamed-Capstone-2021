/*
Author: Jakub Kawski
Created: 2021/25/02

Dummy client table/data
*/

print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO
/*
Author : Jakub Kawski
Created : 2021/02/19
*/
/****************************
Chantal Shirley
Updated: 2021/04/07
Adding email identifier to make
finding client id more navigable.
****************************/
print '' print '**** Creating Client Tables ****'
GO
CREATE TABLE Client
(
	[ClientID]	[int]IDENTITY(10000, 1)	NOT NULL,
	[FirstName] [nvarchar](50)			NOT NULL,
	[LastName]	[nvarchar](50)			NOT NULL,
	[Email]		[nvarchar](100)			NOT NULL,
	PasswordHash		nvarchar(100)	NOT NULL DEFAULT
	'B03DDF3CA2E714A6548E7495E2A03F5E824EAAC9837CD7F159C67B90FB4B7342',
	CONSTRAINT [pk_ClientID] PRIMARY KEY ([ClientID] ASC),
	CONSTRAINT ak_client_email UNIQUE(Email)
)
GO
/*
Author : Jakub Kawski
Created : 2021/02/19
*/
print'' print'**** adding fake client data'
GO
INSERT INTO Client
	(FirstName, LastName, email)
VALUES
	('Jakub', 'Kawski', 'jakub@backontrack.com'),
	('Bob', 'Trapp','bob@backontrack.com'),
	('Jim', 'Glasgow','jim@backontrack.com'),
	('Susan', 'Hood', 'susan@backontrack.com'),
	('Chantal', 'Shirley','chantal@backontrack.com'),
	('Josh', 'White', 'josh@backontrack.com'),
	('Ellis', 'Williams','williams@backontrack.com'),
	('Tyler', 'Simpson','simpson@backontrack.com'),
	('Max', 'Walker', 'max@backontrack.com'),
	('Harrison', 'Lawson','harrison@backontrack.com'),
	('Harper', 'Calhoun', 'harper@backontrack.com'),
	('Garbriel', 'Kline','knline@backontrack.com'),
	('Randy', 'Baird','randy@backontrack.com'),
	('Zion', 'Lawson', 'lawson@backontrack.com'),
	('Will', 'Gallagher','Gallagher@backontrack.com'),
	('Drew', 'Richards', 'drew@backontrack.com'),
	('Kris', 'Mitchell','Kris@backontrack.com'),
	('Bennie', 'King','king@backontrack.com'),
	('Addison', 'Jenkins', 'jenkins@backontrack.com'),
	('RIley', 'Cline','Riley@backontrack.com'),
	('Franky', 'Duncan', 'duncan@backontrack.com'),
	('Ashley', 'Pollard','pollard@backontrack.com'),
	('Gene', 'Preston','Preston@backontrack.com'),
	('Alexis', 'Chan', 'Chan@backontrack.com'),
	('Logan', 'Green','green@backontrack.com'),
	('Gabby', 'Bailey', 'gabby@backontrack.com'),
	('Gail', 'Mills','gail@backontrack.com'),
	('Brynn', 'Bennet','Bennet@backontrack.com'),
	('Bennie', 'Yates', 'Yates@backontrack.com'),
	('Sidney', 'Buckley','Sidney@backontrack.com'),
	('Glen', 'Suarez', 'Suarez@backontrack.com'),
	('Val', 'Mcfarland','Mcfarland@backontrack.com'),
	('Harper', 'Burt','Burt@backontrack.com')
GO

/****************************
Chantal Shirley
Created: 2021/04/07
Stored procedures: 
sp_select_client_id_by_email
****************************
Updated: 2021/04/13
sp_select_client_first_and_last_name_by_email
****************************
Updated: 2021/04/24
sp_select_client_by_email_and_password and
sp_insert_new_client_from_web
****************************/
print '' print '*** creating sp_select_client_id_by_email ***'
GO
CREATE PROCEDURE dbo.sp_select_client_id_by_email
(
	@Email	[nvarchar](100)
)
AS
	BEGIN
		SELECT
			ClientID
		FROM
			Client
		WHERE
			Email = @Email
	END
GO

-- Test record for delivery ticket web view
UPDATE 
	dbo.Client
SET 
	Email = 'chantal@backontrack.com'
WHERE
	FirstName = 'Chantal'
GO

print '' print '*** creating sp_select_client_first_and_last_name_by_email ***'
GO
CREATE PROCEDURE dbo.sp_select_client_first_and_last_name_by_email
(
	@Email	[nvarchar](100)
)
AS
	BEGIN
		SELECT
			FirstName,
			LastName
		FROM
			Client
		WHERE
			Email = @Email
	END
GO

print '' print '*** creating sp_select_client_by_email_and_password ***'
GO
CREATE PROCEDURE dbo.sp_select_client_by_email_and_password
(
	@Email [nvarchar](100),
	@Password [nvarchar](100)
)
AS
	BEGIN
		SELECT COUNT(Email)
		FROM
			Client
		WHERE
			Email = @Email AND
			PasswordHash = @Password
	END
GO

print '' print '*** creating sp_insert_new_client_from_web ***'
GO
CREATE PROCEDURE dbo.sp_insert_new_client_from_web
(
	@Email [nvarchar](100),
	@Password [nvarchar](100),
	@FirstName [nvarchar](100),
	@LastName [nvarchar](100)
)
AS
	BEGIN
		INSERT INTO [dbo].[Client]
			(
				Email
				, FirstName
				, LastName
				, PasswordHash
			)
		VALUES
			(
				@Email
				, @FirstName
				, @LastName
				, @Password
			)
		SELECT SCOPE_IDENTITY()
	END
GO