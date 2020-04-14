USE WebSiteStore
GO

INSERT INTO dbo.Category (Id, Code) VALUES (1, 'Porn')
INSERT INTO dbo.Category (Id, Code) VALUES (2, 'Entertainment')
INSERT INTO dbo.Category (Id, Code) VALUES (3, 'Betting')
INSERT INTO dbo.Category (Id, Code) VALUES (4, 'Video')
INSERT INTO dbo.Category (Id, Code) VALUES (5, 'Software')
GO

INSERT INTO dbo.WebSite ([Name], [Url], CategoryId, Email, [Password], IsDeleted) 
VALUES ('Name 1', 'https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1', 1, 'asd@asd.asd', 'sladkjfh;klsajdf', 0)
INSERT INTO dbo.WebSite ([Name], [Url], CategoryId, Email, [Password], IsDeleted) 
VALUES ('Name 2', 'https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1', 2, 'asd@asd.asd', 'sladkjfh;klsajdf', 0)
INSERT INTO dbo.WebSite ([Name], [Url], CategoryId, Email, [Password], IsDeleted) 
VALUES ('Name 3', 'https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1', 3, 'asd@asd.asd', 'sladkjfh;klsajdf', 0)
INSERT INTO dbo.WebSite ([Name], [Url], CategoryId, Email, [Password], IsDeleted) 
VALUES ('Name 4', 'https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1', 4, 'asd@asd.asd', 'sladkjfh;klsajdf', 0)
INSERT INTO dbo.WebSite ([Name], [Url], CategoryId, Email, [Password], IsDeleted) 
VALUES ('Name5', 'https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1', 5, 'asd@asd.asd', 'sladkjfh;klsajdf', 0)
INSERT INTO dbo.WebSite ([Name], [Url], CategoryId, Email, [Password], IsDeleted) 
VALUES ('Meh', 'https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1', 1, 'asd@asd.asd', 'sladkjfh;klsajdf', 0)
INSERT INTO dbo.WebSite ([Name], [Url], CategoryId, Email, [Password], IsDeleted) 
VALUES ('And another', 'https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1', 2, 'asd@asd.asd', 'sladkjfh;klsajdf', 0)
INSERT INTO dbo.WebSite ([Name], [Url], CategoryId, Email, [Password], IsDeleted) 
VALUES ('zip', 'https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1', 3, 'asd@asd.asd', 'sladkjfh;klsajdf', 0)
INSERT INTO dbo.WebSite ([Name], [Url], CategoryId, Email, [Password], IsDeleted) 
VALUES ('zap', 'https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1', 4, 'asd@asd.asd', 'sladkjfh;klsajdf', 0)
INSERT INTO dbo.WebSite ([Name], [Url], CategoryId, Email, [Password], IsDeleted) 
VALUES ('zop', 'https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1', 5, 'asd@asd.asd', 'sladkjfh;klsajdf', 0)
GO