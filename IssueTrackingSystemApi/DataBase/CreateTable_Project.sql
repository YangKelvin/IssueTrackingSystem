USE ITS;

-- 專案資料表
CREATE TABLE Project (
    [Id] INT PRIMARY KEY,
    [Name] NVARCHAR(50)
);

INSERT INTO Project([Id], [Name]) VALUES (1, 'ProjectOne')       -- 專案1
INSERT INTO Project([Id], [Name]) VALUES (2, 'ProjectTwo')       -- 專案2
