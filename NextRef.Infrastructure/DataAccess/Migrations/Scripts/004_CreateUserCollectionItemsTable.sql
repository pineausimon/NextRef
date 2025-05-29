CREATE TABLE core.UserCollectionItems (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    CollectionId UNIQUEIDENTIFIER NOT NULL,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    Status NVARCHAR(MAX) NOT NULL, -- 'ToWatch', 'Watched', 'NotInterested'
    AddedAt DATETIME NOT NULL,

    CONSTRAINT FK_UserCollectionItems_Collections FOREIGN KEY (CollectionId) REFERENCES core.UserCollections(Id),
    CONSTRAINT FK_UserCollectionItems_Contents FOREIGN KEY (ContentId) REFERENCES core.Contents(Id),

    CONSTRAINT UQ_UserCollectionItem UNIQUE (CollectionId, ContentId)
);
