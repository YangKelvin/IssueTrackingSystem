USE ITS;

-- 問題嚴重性資料表
CREATE TABLE [IssueSeverity] (
    [Id] INT PRIMARY KEY NOT NULL IDENTITY,
    [Name] NVARCHAR(50)
);

INSERT INTO IssueSeverity([Name]) VALUES ('Unknown')       -- 未知
INSERT INTO IssueSeverity([Name]) VALUES ('Trival')        -- 不重要的
INSERT INTO IssueSeverity([Name]) VALUES ('Minor')         -- 次要的
INSERT INTO IssueSeverity([Name]) VALUES ('Critical')      -- 危急的
INSERT INTO IssueSeverity([Name]) VALUES ('Major')         -- 重要的
