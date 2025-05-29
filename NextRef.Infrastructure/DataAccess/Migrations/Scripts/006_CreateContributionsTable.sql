CREATE TABLE core.Contributions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    ContributorId UNIQUEIDENTIFIER NOT NULL,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    Role NVARCHAR(MAX) NOT NULL, -- 'Author', 'Host', 'Guest', etc.
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL,

    CONSTRAINT FK_Contributions_Contributors FOREIGN KEY (ContributorId) REFERENCES core.Contributors(Id),
    CONSTRAINT FK_Contributions_Contents FOREIGN KEY (ContentId) REFERENCES core.Contents(Id)
);
