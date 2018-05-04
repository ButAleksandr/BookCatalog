CREATE VIEW dbo.BooksView
AS
     SELECT b.Id AS BookId,
            a.Id AS AuthorId,
            a.FirstName,
            a.LastName,
            a.BookCount
     FROM [dbo].[Books] AS b
          INNER JOIN [dbo].[Author] AS a ON a.[Id] IN(SELECT [dbo].[AuthorsBooks].[AuthorId]
                                                      FROM [dbo].[AuthorsBooks]);