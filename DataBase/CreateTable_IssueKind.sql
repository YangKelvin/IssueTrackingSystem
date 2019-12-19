USE ITS;

-- 問題種類資料表
CREATE TABLE [IssueKind] (
    [Id] INT PRIMARY KEY NOT NULL IDENTITY,
    [Name] NVARCHAR(50)
);

INSERT INTO IssueKind([Name]) VALUES ('Trobleshooting')        -- 疑難排解
INSERT INTO IssueKind([Name]) VALUES ('FunctionDevelopment')   -- 功能開發
INSERT INTO IssueKind([Name]) VALUES ('GraphicDesign')         -- 平面設計
INSERT INTO IssueKind([Name]) VALUES ('Other')                 -- 其他
