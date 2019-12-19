USE ITS;

-- 問題內容修改記錄資料表
CREATE TABLE [IssueHistory] (
    [Id] INT,                         -- 記錄流水號
    [IssueId] INT,                    -- 問題流水號
    [UserId] INT,                     -- 更改使用者的Id
    [WorkDescription] NVARCHAR(255),  -- 描述
    [CreateTime] DATETIME,            -- 更改紀錄時間
    FOREIGN KEY (IssueId) REFERENCES [Issue](Id),
    FOREIGN KEY (UserId) REFERENCES [User](Id)
);
