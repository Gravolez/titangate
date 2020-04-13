USE master  
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'WebSiteStore'
)
CREATE DATABASE WebSiteStore
GO


CREATE TABLE dbo.Category (
    Id int NOT NULL,
    Code NVARCHAR(256) NOT NULL,
    CONSTRAINT PK_Category PRIMARY KEY (Id)
)
GO


CREATE TABLE dbo.WebSite (
    Id int NOT NULL IDENTITY(1,1),
    Name NVARCHAR(256) NOT NULL,
    Url NVARCHAR(MAX) NOT NULL,
    IsDeleted BIT NOT NULL
        CONSTRAINT DF_WebSite_IsDeleted DEFAULT 0,
    CategoryId int NOT NULL,
    Email NVARCHAR(320) NOT NULL,
    Pass NVARCHAR(256) NOT NULL,
    CONSTRAINT PK_WebSite PRIMARY KEY (Id),
    CONSTRAINT FK_WebSite_CategoryId FOREIGN KEY (CategoryId) REFERENCES dbo.Category (Id)
)
GO
