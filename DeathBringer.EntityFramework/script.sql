IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [tabella_Utenti] (
    [Id] int NOT NULL IDENTITY,
    [DataCreazioneRecord] datetime2 NOT NULL,
    [DataUltimaModifica] datetime2 NOT NULL,
    [UtenteCreazioneRecord] nvarchar(max) NULL,
    [UtenteUltimaModificaRecord] nvarchar(max) NULL,
    [Username] nvarchar(max) NULL,
    [Nome] nvarchar(max) NULL,
    [Cognome] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Indirizzo] nvarchar(max) NULL,
    [Civico] nvarchar(max) NULL,
    [Cap] int NOT NULL,
    [Citta] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [IsAdministrator] bit NOT NULL,
    CONSTRAINT [PK_tabella_Utenti] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190219112619_InitialMigration', N'2.2.2-servicing-10034');

GO

