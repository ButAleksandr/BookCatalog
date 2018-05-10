

CREATE FUNCTION [dbo].[GetAuthors]
                                  ( 
                @bookId INT
                                  )
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @authorNames TABLE
                              ( 
                               [AuthorFullName] NVARCHAR(120)
                              );
    DECLARE @fullname      NVARCHAR(120),
            @authorsString NVARCHAR(MAX) = '';

    INSERT INTO @authorNames
    SELECT [FirstName]+' '+[LastName] AS [AuthorFullName]
    FROM [dbo].[Authors]
    WHERE [Authors].[Id] IN (SELECT [AuthorId]
                            FROM [dbo].[AuthorsBooks]
                            WHERE [AuthorsBooks].[BookId] = @bookId);

    DECLARE fullname_cursor CURSOR
    FOR SELECT [AuthorFullName] FROM @authorNames;

	OPEN fullname_cursor;
    FETCH NEXT FROM fullname_cursor INTO @fullname;
 
	WHILE @@FETCH_STATUS = 0  
	BEGIN  		
		IF LEN(@fullname) > 0 
		    BEGIN
				SET @authorsString = @authorsString + @fullname;

				FETCH NEXT FROM fullname_cursor  
				INTO @fullname;  

				IF @@FETCH_STATUS > -1 
				SET @authorsString = @authorsString + ', ';
			END
		ELSE 
			BEGIN
				FETCH NEXT FROM fullname_cursor  
				INTO @fullname; 
			END
		
	END  

    CLOSE fullname_cursor;
    DEALLOCATE fullname_cursor;

    RETURN @authorsString;
END;
