
CREATE PROCEDURE [dbo].[Delete_Author] 
                 @authorId INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        DELETE FROM [dbo].[AuthorsBooks]
        WHERE [AuthorsBooks].[AuthorId] = @authorId;

        DELETE FROM [dbo].[Authors]
        WHERE [Authors].[Id] = @authorId;
        COMMIT;
    END TRY
    BEGIN CATCH
		SELECT ERROR_MESSAGE()
        ROLLBACK;
    END CATCH;

    RETURN 0;
END;