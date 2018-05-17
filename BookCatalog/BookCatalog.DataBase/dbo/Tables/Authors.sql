
CREATE TABLE [dbo].[Authors]
                           ( 
             [Id]        INT NOT NULL  IDENTITY(1,1),
             [FirstName] NVARCHAR(60) NOT NULL,
             [LastName]  NVARCHAR(60) NOT NULL,
             [BookCount] INT NOT NULL,
             CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED([Id] ASC)
                           );

