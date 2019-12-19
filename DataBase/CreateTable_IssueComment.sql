USE ITS;

-- 問題留言資料表
CREATE TABLE [IssueComment] (
    [IssueId] INT,                    -- 問題流水號
    [CommentId] INT,                  -- 留言流水號
    [CommenterId] INT,                -- 留言人
    [Content] NVARCHAR(255),		  -- 留言內容
    [CreateTime] DATETIME,            -- 留言問題時間
    [CreateUesr] INT,                 -- 留言人 Id
    [ModifyTime] DATETIME,            -- 最後修改留言時間
    [ModifyUser] INT,                 -- 最後修改人 Id
    FOREIGN KEY (IssueId) REFERENCES [Issue](Id),
    FOREIGN KEY (CommenterId) REFERENCES [User](Id),
    FOREIGN KEY (CreateUesr) REFERENCES [User](Id),
    FOREIGN KEY (ModifyUser) REFERENCES [User](Id)
);
