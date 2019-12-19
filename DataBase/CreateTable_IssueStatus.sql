USE ITS;

-- 問題處理狀態資料表
CREATE TABLE [IssueStatus] (
    [Id] INT PRIMARY KEY NOT NULL IDENTITY,
    [Name] NVARCHAR(50)
);

INSERT INTO IssueStatus([Name]) VALUES ('Backlog')         -- 未分配的問題
INSERT INTO IssueStatus([Name]) VALUES ('Open')            -- 以分配未開始的問題
INSERT INTO IssueStatus([Name]) VALUES ('InProgress')      -- 正在做的問題
INSERT INTO IssueStatus([Name]) VALUES ('ReOpened')        -- 完成後被重新打開的問題
INSERT INTO IssueStatus([Name]) VALUES ('Resolve')         -- 已完成的問題
INSERT INTO IssueStatus([Name]) VALUES ('Pending')         -- 待定的問題(?)
