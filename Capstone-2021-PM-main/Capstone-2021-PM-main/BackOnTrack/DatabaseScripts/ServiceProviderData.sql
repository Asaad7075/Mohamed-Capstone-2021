print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Chase Martin
Created: 2021/03/26 
Description: Created Provider data.
**************************************/
print '' print '*** creating Provider test data ***'
GO
INSERT INTO [dbo].[Provider]
		([BusinessName], [FirstName], [LastName], [MiddleInitial], [Address], [PhoneNumber], [Email], [ZipCode], [EIN], [Activated], [Schedule])
	VALUES
		('Slicked Back', 'Martin', 'Benish', 'T', '220 Redwood Dr.', '3197376560', 'martin@company.com', '52315', '1230', '1', '1'),
		('Fishers', 'Chase', 'Barnhart', 'L', '240 Angel Dr.', '3198957563', 'chase@company.com', '52314', '1231', '1', '1'),
		('Pearsons', 'Aaron', 'Martin', 'G', '2001 Cherry St.', '3197473561', 'aaron@company.com', '52315', '1232', '1', '1'),
		('WillowWay', 'Bridget', 'Bardo', 'S', '20 Willow Creek Dr.', '3194570774', 'bridget@company.com', '52404', '1233', '1', '1'),
		('Wells Stine', 'Alex', 'Sentons', 'A', '620 Blueberry St.', '3197774525', 'alex@company.com', '52315', '1234', '1', '1'),
		('Veritiv', 'Shela', 'Blanco', 'C', '450 Oak Ave.', '3191116002', 'shela@company.com', '52314', '1235', '0', '1'),
		('Westlake', 'Martinez', 'Carrera', 'Y', '670 Ingle Dr.', '3193374465', 'martinez@company.com', '52404', '1236', '1', '1'),
		('budgetCrunch', 'Mark', 'Guzman', 'U', '210 Deer Rd.', '3197726005', 'mark@company.com', '52315', '1237', '0', '1'),
		('NannyNeighbors', 'Steve', 'Walsh', 'W', '920 Humboldt Dr.', '3196179965', 'steve@company.com', '52404', '1238', '1', '1'),
		('GulfStream', 'Wilson', 'Bane', 'R', '370 Weverly Dr.', '3197676584', 'wilson@company.com', '52314', '1239', '0', '1')
GO