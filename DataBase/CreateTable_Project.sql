USE ITS;

-- 專案資料表
CREATE TABLE [Project] (
    [Id] INT PRIMARY KEY NOT NULL IDENTITY,
    [Name] NVARCHAR(50)
);

INSERT INTO Project([Name]) VALUES ('ProjectOne')       -- 專案1
INSERT INTO Project([Name]) VALUES ('ProjectTwo')       -- 專案2
