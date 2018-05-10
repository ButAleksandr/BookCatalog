

CREATE FUNCTION [dbo].[GetAuthors]
                                  ( 
                @bookId INT
                                  )
RETURNS TABLE
AS
RETURN
SELECT [Id]
      ,[FirstName]
      ,[LastName]
      ,[BookCount]
FROM [dbo].[Authors]
WHERE [Authors].[Id] IN (SELECT [AuthorId]
                         FROM [dbo].[AuthorsBooks]
                         WHERE [AuthorsBooks].[BookId] = @bookId);
