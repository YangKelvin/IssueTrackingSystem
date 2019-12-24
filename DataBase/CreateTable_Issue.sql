USE ITS;

-- 問題種類資料表
CREATE TABLE [Issue] (
    [Id] INT PRIMARY KEY NOT NULL IDENTITY,           -- 流水號
    [IssueNumber] NVARCHAR(10),       -- 問題單號
    [IssueSummary] NVARCHAR(50),      -- 問題名稱
    [Description] NVARCHAR(255),    -- 問題描述
    [Attachments] NVARCHAR(50),       -- 附件(用附件編號串起來?)
    [AssigneeId] INT ,     -- 受讓者
    [ReporterId] INT ,     -- 回報者
    [Estimated] FLOAT,                -- 估計處理時間(給分配問題的人填寫)
    [EstimatedStartTime] DATETIME,    -- 估計開始時間(給問題處理的人填寫)
    [EstimatedEndTime] DATETIME,      -- 估計結束時間(給問題處理的人填寫)
    [ActualStartTime] DATETIME,       -- 實際開始時間(給問題處理的人填寫)
    [ActualEndTime] DATETIME,         -- 實際結束時間(給問題處理的人填寫)
    [ResolveTime] DATETIME,           -- 解決問題時間(系統帶入)
    [KindId] INT ,         -- 問題種類 Id
    [ServerityId] INT,    -- 問題嚴重性 Id
    [StatusId] INT,       -- 問題狀態 Id
    [UrgencyId] INT,      -- 問題緊急性 Id
    [ProjectId] INT,      -- 專案 Id
    [CreateTime] DATETIME,            -- 建立問題時間
    [CreateUser] INT,     -- 建立人 Id
    [ModifyTime] DATETIME,            -- 最後修改問題時間
    [ModifyUser] INT,     -- 最後修改人 Id
    FOREIGN KEY (AssigneeId) REFERENCES [User](Id),
    FOREIGN KEY (ReporterId) REFERENCES [User](Id),
    FOREIGN KEY (KindId) REFERENCES [IssueKind](Id),
    FOREIGN KEY (ServerityId) REFERENCES [IssueSeverity](Id),
    FOREIGN KEY (StatusId) REFERENCES [IssueStatus](Id),
    FOREIGN KEY (UrgencyId) REFERENCES [IssueUrgency](Id),
    FOREIGN KEY (ProjectId) REFERENCES [Project](Id),
    FOREIGN KEY (CreateUser) REFERENCES [User](Id),
    FOREIGN KEY (ModifyUser) REFERENCES [User](Id)
);
