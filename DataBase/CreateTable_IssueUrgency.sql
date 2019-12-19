USE ITS;

-- 問題緊急性資料表
CREATE TABLE [IssueUrgency] (
    [Id] INT PRIMARY KEY NOT NULL IDENTITY,
    [Name] NVARCHAR(50)
);

INSERT INTO IssueUrgency([Name]) VALUES ('Urgent')             -- 緊急
INSERT INTO IssueUrgency([Name]) VALUES ('AsFastAsPossible')   -- 盡速
INSERT INTO IssueUrgency([Name]) VALUES ('Normal')             -- 普通
INSERT INTO IssueUrgency([Name]) VALUES ('NotUrgent')          -- 不急
