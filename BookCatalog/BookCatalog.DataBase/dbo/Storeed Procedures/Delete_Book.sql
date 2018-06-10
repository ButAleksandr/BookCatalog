
CREATE PROCEDURE [dbo].[Delete_Book] 
                 @bookId INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        DELETE FROM [dbo].[AuthorsBooks]
        WHERE [AuthorsBooks].[BookId] = @bookId;

        DELETE FROM [dbo].[Books]
        WHERE [Books].[Id] = @bookId;
        COMMIT;
    END TRY
    BEGIN CATCH
		SELECT ERROR_MESSAGE()
        ROLLBACK;
    END CATCH;

    RETURN 0;
END;