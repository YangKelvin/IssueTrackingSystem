USE ITS;

-- 問題種類資料表
CREATE TABLE [IssueKind] (
    [Id] INT PRIMARY KEY NOT NULL IDENTITY,
    [Name] NVARCHAR(50)
);

INSERT INTO IssueKind([Id], [Name]) VALUES (1, 'Trobleshooting')        -- 疑難排解
INSERT INTO IssueKind([Id], [Name]) VALUES (2, 'FunctionDevelopment')   -- 功能開發
INSERT INTO IssueKind([Id], [Name]) VALUES (3, 'GraphicDesign')         -- 平面設計
INSERT INTO IssueKind([Id], [Name]) VALUES (4, 'Other')                 -- 其他
