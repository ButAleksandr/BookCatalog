

CREATE VIEW [dbo].[BooksView]
AS
SELECT [Id]
      ,[Name]
      ,[Rate]
      ,[PageCount]
      ,[ReleaseDate]
      ,[dbo].[GetAuthors]
       ([Id]) AS [Authors]
FROM [dbo].[Books];
    