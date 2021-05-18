
ECHO off

rem batch file to run a script to create a db
rem 2020/10/26

sqlcmd -S localhost -E -i BotDBCreation.sql
sqlcmd -S localhost -E -v FullScriptDir="%CD%" -i AppImages.sql
sqlcmd -S localhost -E -i Employees.sql
sqlcmd -S localhost -E -i UserRoles.sql
sqlcmd -S localhost -E -i EmployeeRoles.sql
sqlcmd -S localhost -E -i Vehicles.sql
sqlcmd -S localhost -E -v FullScriptDir="%CD%" -i VehicleImages.sql
sqlcmd -S localhost -E -v FullScriptDir="%CD%" -i VehicleViews.sql
sqlcmd -S localhost -E -i DriversLicense.sql
sqlcmd -S localhost -E -i DriversLicenseClasses.sql
sqlcmd -S localhost -E -i InventoryItem.sql
sqlcmd -S localhost -E -i SupplyItemInventory.sql

sqlcmd -S localhost -E -i ZipCode.sql
sqlcmd -S localhost -E -v FullScriptDir="%CD%" -i Zip_Load.sql

sqlcmd -S localhost -E -i GeoLocation.sql
sqlcmd -S localhost -E -v FullScriptDir="%CD%" -i GeoLocation_Load.sql
sqlcmd -S localhost -E -i Route.sql
sqlcmd -S localhost -E -i Client.sql

sqlcmd -S localhost -E -i TicketStatus.sql
sqlcmd -S localhost -E -i TicketType.sql
sqlcmd -S localhost -E -i Ticket.sql

sqlcmd -S localhost -E -i PickUpTicket.sql
sqlcmd -S localhost -E -i RideTicket.sql
sqlcmd -S localhost -E -i UserRolesSP.sql
sqlcmd -S localhost -E -v FullScriptDir="%CD%" -i Donation.sql
sqlcmd -S localhost -E -i Order.sql
sqlcmd -S localhost -E -i DeliveryTicket.sql

sqlcmd -S localhost -E -i Donor.sql
sqlcmd -S localhost -E -i DonationForm.sql
sqlcmd -S localhost -E -i VehicleMaintenanceReport.sql
sqlcmd -S localhost -E -i VehicleMaintenanceType.sql
sqlcmd -S localhost -E -i VehicleMaintenanceStatus.sql

sqlcmd -S localhost -E -i FinancialCounseling.sql
sqlcmd -S localhost -E -i Childcare.sql
sqlcmd -S localhost -E -i RideReview.sql
sqlcmd -S localhost -E -i Service.sql
sqlcmd -S localhost -E -i Extra_Stored_Procedures.sql

sqlcmd -S localhost -E -i ProviderData.sql
sqlcmd -S localhost -E -i ServiceReview.sql
sqlcmd -S localhost -E -i ServiceProviderData.sql




ECHO .
ECHO if no error messages appear DB was created
PAUSE