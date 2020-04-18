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

USE WebSiteStore
GO

CREATE TABLE dbo.Category (
    Id int NOT NULL,
    Code NVARCHAR(256) NOT NULL,
    CONSTRAINT PK_Category PRIMARY KEY (Id)
)
GO

CREATE TABLE dbo.WebSite (
    Id int NOT NULL IDENTITY(1,1),
    [Name] NVARCHAR(256) NOT NULL,
    
    -- URLs can be 2048 characters long but I assume 768 are enough. 
    -- Also non clustered indexes have a maximum of 1700 bytes, 
    -- so we want our unique url to fit in the index
    [Url] NVARCHAR(768) NOT NULL,  
    IsDeleted BIT NOT NULL
        CONSTRAINT DF_WebSite_IsDeleted DEFAULT 0,
    CategoryId int NOT NULL,
    Email NVARCHAR(320) NOT NULL,
    [Password] NVARCHAR(256) NOT NULL,
    HasScreenshot BIT NOT NULL
        CONSTRAINT DF_WebSite_HasScreenshot DEFAULT 0,
    ScreenshotExt NVARCHAR(10),
    CONSTRAINT PK_WebSite PRIMARY KEY (Id),
    CONSTRAINT FK_WebSite_CategoryId FOREIGN KEY (CategoryId) REFERENCES dbo.Category (Id),
    CONSTRAINT UC_WebSite_Name UNIQUE([Name]),
    CONSTRAINT UC_WebSite_Url UNIQUE([Url])
)
GO

CREATE TABLE dbo.[User] (
    Id int NOT NULL IDENTITY(1, 1),
    Username NVARCHAR(256) NOT NULL,
    PasswordHash NVARCHAR(256) NOT NULL,
    Salt NVARCHAR(256) NOT NULL,
    CONSTRAINT UC_User_Username UNIQUE(Username) 
)
GO