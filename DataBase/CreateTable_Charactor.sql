USE ITS;

-- 資料表
CREATE TABLE [Charactor] (
    [Id] INT PRIMARY KEY NOT NULL IDENTITY,
    [Name] NVARCHAR(50)
);

INSERT INTO Charactor([Name]) VALUES ('Admin')          -- 系統管理員
INSERT INTO Charactor([Name]) VALUES ('User')           -- 一般使用者
