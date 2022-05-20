In order to be able to run this project you require to have installed .net 6 SDK, Microsoft SQL Server.

Setup the database:
1.Create a database.
2.Run this sql script to align the required tables:

CREATE TABLE [dbo].[Students](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]

3. Update the connection string in the code

Run the application:
1. Open a terminal in the working directory
2. Run dotnet watch run

