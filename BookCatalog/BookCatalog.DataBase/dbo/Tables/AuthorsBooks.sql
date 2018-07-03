

CREATE TABLE [dbo].[AuthorsBooks]
                                 ( 
             [AuthorId] INT NOT NULL,
             [BookId]   INT NOT NULL,
             CONSTRAINT [FK_AuthorsBooks_Author] FOREIGN KEY([AuthorId]) REFERENCES [dbo].[Authors]([Id]),
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
    DECLARE @authorId INT = (SELECT [AuthorId]
                             FROM [inserted]);
         
	-- Update book count for the author
    UPDATE [Authors]
    SET [BookCount] = [BookCount] + 1
    WHERE [Id] = @authorId;
END;
GO

-- Trigger for decreasing BooksCount field value after was deleted a book ~ author relation

CREATE TRIGGER [dbo].[Trigger_AuthorsBooks_Delete] ON [dbo].[AuthorsBooks]
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

	-- Get Author Id
    DECLARE @authorId TABLE
                           ( 
                            [Ids] INT NOT NULL
                           );
    Declare @bookId bigint = (Select TOP(1) [BookId] From [deleted]);

    IF @bookId > 0 
    BEGIN
        INSERT INTO TestTable 
        SELECT @bookId, 2

	    -- Update book count for the author
        UPDATE [Authors]
        SET [BookCount] = [BookCount] - 1    
        WHERE (SELECT COUNT(*)
        FROM [AuthorsBooks]
        WHERE [BookId] = @bookId and [AuthorId] = [Authors].[Id]) > 0;

        DELETE [AuthorsBooks] WHERE [BookId] = @bookId
    END
END;
GO

-- Trigger which disable actions of the table updating 

CREATE TRIGGER [dbo].[Trigger_AuthorsBooks_Update] ON [dbo].[AuthorsBooks]
INSTEAD OF UPDATE
AS
BEGIN
    RAISERROR('AuthorsBooks table does not support UPDATE action.',16,1);
    ROLLBACK TRANSACTION;
END;