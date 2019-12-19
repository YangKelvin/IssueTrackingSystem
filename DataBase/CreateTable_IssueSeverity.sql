USE ITS;

-- 問題嚴重性資料表
CREATE TABLE [IssueSeverity] (
    [Id] INT PRIMARY KEY NOT NULL IDENTITY,
    [Name] NVARCHAR(50)
);

INSERT INTO IssueSeverity([Id], [Name]) VALUES (1, 'Unknown')       -- 未知
INSERT INTO IssueSeverity([Id], [Name]) VALUES (2, 'Trival')        -- 不重要的
INSERT INTO IssueSeverity([Id], [Name]) VALUES (3, 'Minor')         -- 次要的
INSERT INTO IssueSeverity([Id], [Name]) VALUES (4, 'Critical')      -- 危急的
INSERT INTO IssueSeverity([Id], [Name]) VALUES (5, 'Major')         -- 重要的
