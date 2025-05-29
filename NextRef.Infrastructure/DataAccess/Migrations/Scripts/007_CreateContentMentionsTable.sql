CREATE TABLE core.ContentMentions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    SourceContentId UNIQUEIDENTIFIER NOT NULL,
    TargetContentId UNIQUEIDENTIFIER NOT NULL,
    Context NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL,

    CONSTRAINT FK_ContentMentions_Source FOREIGN KEY (SourceContentId) REFERENCES core.Contents(Id),
    CONSTRAINT FK_ContentMentions_Target FOREIGN KEY (TargetContentId) REFERENCES core.Contents(Id)
);
