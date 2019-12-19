USE ITS;

-- 專案角色資料表
CREATE TABLE [ProjectCharactor] (
    [Id] INT PRIMARY KEY NOT NULL IDENTITY,
    [Name] NVARCHAR(50)
);

INSERT INTO ProjectCharactor([Name]) VALUES ('Manager')          -- 系統管理員
INSERT INTO ProjectCharactor([Name]) VALUES ('Developer')        -- 開發人員、測試人員
INSERT INTO ProjectCharactor([Name]) VALUES ('General')          -- 一般使用者（只能提問題）
