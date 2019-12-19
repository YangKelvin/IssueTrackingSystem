USE ITS;

-- 資料表
CREATE TABLE [Charactor] (
    [Id] INT PRIMARY KEY NOT NULL IDENTITY,
    [Name] NVARCHAR(50)
);

INSERT INTO Charactor([Id], [Name]) VALUES (1, 'Admin')          -- 系統管理員
INSERT INTO Charactor([Id], [Name]) VALUES (2, 'User')           -- 一般使用者
