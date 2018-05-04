
CREATE TABLE [dbo].[Author]
                           ( 
             [Id]        INT NOT NULL,
             [FirstName] NVARCHAR(60) NOT NULL,
             [LastName]  NVARCHAR(60) NOT NULL,
             [BookCount] INT NOT NULL,
             CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED([Id] ASC)
                           );

