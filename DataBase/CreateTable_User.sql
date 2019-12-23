USE ITS;

-- 用戶帳號資料表
CREATE TABLE [User] (
    [Id] INT PRIMARY KEY NOT NULL IDENTITY,
    [Account] NVARCHAR(50),
    [Password] NVARCHAR(50),
    [EMail] NVARCHAR(50),
    [CharactorId] INT,
    [Name] NVARCHAR(50),
    [LineId] NVARCHAR(50),
    FOREIGN KEY (CharactorId) REFERENCES [Charactor](Id),
	UNIQUE([Account])
);

-- 系統管理員
INSERT INTO [User](Account, [Password], Email, CharactorId, [Name]) 
VALUES ('Admin', 'admin', 't105590045@ntut.org.tw', 1, 'Kelvin')

-- 問題管理員
INSERT INTO [User](Account, [Password], Email, CharactorId, [Name]) 
VALUES ('User01', 'user01', 'f98989000@gmail.com', 1, 'Chris')

-- 一般使用者(開發人員、測試人員)
INSERT INTO [User](Account, [Password], Email, CharactorId, [Name]) 
VALUES ('User02', 'user02', 't105590017@ntut.org.tw', 1, 'Rex')
