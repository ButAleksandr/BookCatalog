CREATE TABLE [dbo].[AuthorsBooks] (
    [AuthorId] INT NOT NULL,
    [BookId]   INT NOT NULL,
    CONSTRAINT [FK_AuthorsBooks_Author] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Author] ([Id]),
    CONSTRAINT [FK_AuthorsBooks_Books] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([Id])
);

