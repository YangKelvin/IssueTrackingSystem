USE ITS;

-- 角色資料表
CREATE TABLE [UserProjectRelation] (
    [UserId] INT NOT NULL,
    [ProjectId] INT NOT NULL,
    [ProjectCharactorId] INT,
    FOREIGN KEY (UserId) REFERENCES [User](Id),
    FOREIGN KEY (ProjectId) REFERENCES [Project](Id),
    FOREIGN KEY (ProjectCharactorId) REFERENCES [ProjectCharactor](Id)
);
