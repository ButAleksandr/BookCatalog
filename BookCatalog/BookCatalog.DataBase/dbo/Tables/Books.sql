
CREATE TABLE [dbo].[Books]
                          ( 
             [Id]          INT NOT NULL,
             [Name]        NVARCHAR(60) NOT NULL,
             [PageCount]   INT NOT NULL,
             [ReleaseDate] DATE NOT NULL,
             [Rate]        INT NOT NULL,
             CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED([Id] ASC)
                          );

