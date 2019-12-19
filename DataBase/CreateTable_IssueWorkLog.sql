USE ITS;

-- 問題工作記錄資料表
CREATE TABLE [IssueWorkLog] (
    [IssueId] INT NOT NULL,           -- 問題流水號
    [LogId] INT NOT NULL,             -- 記錄流水號
    [SpentTime] FLOAT,                -- 花費時間 (小時)
    [DateStart] DATETIME,             -- 開始時間
    [WorkDescription] NVARCHAR(255),  -- 描述
    [CreateTime] DATETIME,            -- 建立問題時間
    [CreateUesr] INT,                 -- 建立人 Id
    [ModifyTime] DATETIME,            -- 最後修改問題時間
    [ModifyUser] INT,                 -- 最後修改人 Id
    FOREIGN KEY (IssueId) REFERENCES [Issue](Id),
    FOREIGN KEY (CreateUesr) REFERENCES [User](Id),
    FOREIGN KEY (ModifyUser) REFERENCES [User](Id)
);
