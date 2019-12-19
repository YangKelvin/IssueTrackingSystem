USE ITS;

-- 問題處理狀態資料表
CREATE TABLE [IssueStatus] (
    [Id] INT PRIMARY KEY NOT NULL IDENTITY,
    [Name] NVARCHAR(50)
);

INSERT INTO IssueStatus([Id], [Name]) VALUES (1, 'Backlog')         -- 未分配的問題
INSERT INTO IssueStatus([Id], [Name]) VALUES (2, 'Open')            -- 以分配未開始的問題
INSERT INTO IssueStatus([Id], [Name]) VALUES (3, 'InProgress')      -- 正在做的問題
INSERT INTO IssueStatus([Id], [Name]) VALUES (4, 'ReOpened')        -- 完成後被重新打開的問題
INSERT INTO IssueStatus([Id], [Name]) VALUES (5, 'Resolve')         -- 已完成的問題
INSERT INTO IssueStatus([Id], [Name]) VALUES (6, 'Pending')         -- 待定的問題(?)
