CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [userName] VARCHAR(50) NOT NULL, 
    [password] TEXT NOT NULL, 
    [fullName] VARCHAR(50) NOT NULL, 
    [role] VARCHAR(50) NOT NULL, 
    [pending] VARCHAR(50) NOT NULL, 
    [birthDate] VARCHAR(50) NOT NULL, 
    [age] INT NOT NULL
)
