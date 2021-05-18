USE backontrack_db
GO

exec sp_insert_provider @BusinessName = "Waverly", @FirstName = "Joanne ", @LastName = "Williams", @Address = "112 E 8th st", @PhoneNumber = "3194722222",
	@Email = "waverly@waverly.com", @ZipCode = "52349", @EIN = "123123123", @Activated = true, @Schedule = true;

exec sp_insert_provider @BusinessName = "Slicked Back", @FirstName = "Mary ", @LastName = "Kyte", @Address = "1111 b ave", @PhoneNumber = "3194722221",
	@Email = "sb@slickedback.com", @ZipCode = "52233", @EIN = "123123124", @Activated = true, @Schedule = true;

exec sp_insert_provider @BusinessName = "Waverly", @FirstName = "Steve ", @LastName = "Thompson", @Address = "11 E 55th st", @PhoneNumber = "3194722223",
	@Email = "ameriprise@aac.com", @ZipCode = "42404", @EIN = "123123125", @Activated = true, @Schedule = true;
