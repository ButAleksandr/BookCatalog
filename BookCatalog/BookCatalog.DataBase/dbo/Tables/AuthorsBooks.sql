CREATE TABLE [dbo].[AuthorsBooks]
([AuthorId] INT NOT NULL,
 [BookId]   INT NOT NULL,
 CONSTRAINT [FK_AuthorsBooks_Author] FOREIGN KEY([AuthorId]) REFERENCES [dbo].[Author]([Id]),
 CONSTRAINT [FK_AuthorsBooks_Books] FOREIGN KEY([BookId]) REFERENCES [dbo].[Books]([Id])
);
GO

-- Trigger for increasing BooksCount field value after was added new book ~ author relation

CREATE TRIGGER [dbo].[Trigger_AuthorsBooks_Add] ON [dbo].[AuthorsBooks]
FOR INSERT
AS
     BEGIN
         SET NOCOUNT ON;

		 -- Get Author Id
         DECLARE @authorId INT=
(
    SELECT [AuthorId]
    FROM [inserted]
);
         
		 -- Update book count for the author
         UPDATE Author
           SET
               BookCount = BookCount + 1
         WHERE Id = @authorId;
     END;
GO

-- Trigger for decreasing BooksCount field value after was deleted a book ~ author relation

CREATE TRIGGER [dbo].[Trigger_AuthorsBooks_Delete] ON [dbo].[AuthorsBooks]
FOR DELETE
AS
     BEGIN
         SET NOCOUNT ON;

		 -- Get Author Id
         DECLARE @authorId INT=
(
    SELECT [AuthorId]
    FROM [deleted]
);
         
		 -- Update book count for the author
         UPDATE Author
           SET
               BookCount = BookCount - 1
         WHERE Id = @authorId;
     END;
GO

-- Trigger which disable actions of the table updating 
CREATE TRIGGER [dbo].[Trigger_AuthorsBooks_Update] ON [dbo].[AuthorsBooks]
INSTEAD OF UPDATE
AS
     BEGIN
         RAISERROR('AuthorsBooks table does not support UPDATE action.', 16, 1);
         ROLLBACK TRANSACTION;
     END;