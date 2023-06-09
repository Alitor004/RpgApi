﻿BEGIN TRANSACTION;
GO

ALTER TABLE [Armas] ADD [PersonagemId] int NOT NULL DEFAULT 0;
GO

UPDATE [Armas] SET [PersonagemId] = 1
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Armas] SET [PersonagemId] = 2
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Armas] SET [PersonagemId] = 3
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [Armas] SET [PersonagemId] = 4
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

UPDATE [Armas] SET [PersonagemId] = 6
WHERE [Id] = 5;
SELECT @@ROWCOUNT;

GO

UPDATE [Armas] SET [PersonagemId] = 7
WHERE [Id] = 6;
SELECT @@ROWCOUNT;

GO

UPDATE [Armas] SET [PersonagemId] = 5
WHERE [Id] = 7;
SELECT @@ROWCOUNT;

GO

UPDATE [Usuarios] SET [PasswordHash] = 0x2E53D8C8EB7FDDC44640EDCFF7BAE86D0818174403215253820179CE039D2DBDBEC13475480DD3A1DD5DF16580D0E5DF00722F0DFDC776CC59AEE43CE2318D48, [PasswordSalt] = 0x9E3CCB200F40CA2C7502DEB068FADDC97D45C45827D921B81F850BE7450B4CCB43F5707EE0A83FD7F710AD51996E07BEC9C85A9C3573387B20891BA59B96E690D06C0AED65BBEE21C5914EF6B3C1D0BE1F26C9625B152B5F4349C0D4770743A7112E9A6D4904064E362E765F1B02E96771E89FEB49F8EFE8236B3CB25CD4A010
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

CREATE UNIQUE INDEX [IX_Armas_PersonagemId] ON [Armas] ([PersonagemId]);
GO

ALTER TABLE [Armas] ADD CONSTRAINT [FK_Armas_Personagens_PersonagemId] FOREIGN KEY ([PersonagemId]) REFERENCES [Personagens] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230419004501_MigracaoUmParaUm', N'7.0.4');
GO

COMMIT;
GO

