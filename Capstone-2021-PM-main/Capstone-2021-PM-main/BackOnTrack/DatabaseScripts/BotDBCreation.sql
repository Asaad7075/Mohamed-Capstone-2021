/*Check if database exists then drops it.*/

IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'backontrack_db')
	BEGIN 
		DROP DATABASE backontrack_db
		print '' print '***dropping database backontrack_db ***'
	END
GO

print'' print'*** creating backontrack_db ***'
GO
CREATE DATABASE backontrack_db
GO
