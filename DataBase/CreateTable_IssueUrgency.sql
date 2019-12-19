USE ITS;

-- 問題緊急性資料表
CREATE TABLE [IssueUrgency] (
    [Id] INT PRIMARY KEY NOT NULL IDENTITY,
    [Name] NVARCHAR(50)
);

INSERT INTO IssueUrgency([Id], [Name]) VALUES (1, 'Urgent')             -- 緊急
INSERT INTO IssueUrgency([Id], [Name]) VALUES (2, 'AsFastAsPossible')   -- 盡速
INSERT INTO IssueUrgency([Id], [Name]) VALUES (3, 'Normal')             -- 普通
INSERT INTO IssueUrgency([Id], [Name]) VALUES (4, 'NotUrgent')          -- 不急
