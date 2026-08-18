CREATE TABLE [TB_USUARIOS] (
    [Id] int NOT NULL IDENTITY,
    [Username] varchar(50) NOT NULL,
    [PasswordHash] varbinary(max) NULL,
    [PasswordSalt] varbinary(max) NULL,
    [Foto] varbinary(max) NULL,
    [Latitude] float NULL,
    [Longitude] float NULL,
    [DataAcesso] datetime NULL,
    [Perfil] varchar(50) NOT NULL CONSTRAINT [DF_PERFIL] DEFAULT 'UsuarioComum',
    [Email] varchar(50) NULL,
    CONSTRAINT [PK_TB_USUARIOS] PRIMARY KEY ([Id]),
    
);