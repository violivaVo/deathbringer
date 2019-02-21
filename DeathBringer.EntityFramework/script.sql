IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [tabella_Categorie] (
    [Id] int NOT NULL IDENTITY,
    [DataCreazioneRecord] datetime2 NOT NULL,
    [DataUltimaModifica] datetime2 NOT NULL,
    [UtenteCreazioneRecord] nvarchar(max) NULL,
    [UtenteUltimaModificaRecord] nvarchar(max) NULL,
    [Nome] nvarchar(255) NOT NULL,
    [Descrizione] nvarchar(max) NULL,
    CONSTRAINT [PK_tabella_Categorie] PRIMARY KEY ([Id])
);

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

CREATE TABLE [tabella_Prodotti] (
    [Id] int NOT NULL IDENTITY,
    [DataCreazioneRecord] datetime2 NOT NULL,
    [DataUltimaModifica] datetime2 NOT NULL,
    [UtenteCreazioneRecord] nvarchar(max) NULL,
    [UtenteUltimaModificaRecord] nvarchar(max) NULL,
    [Nome] nvarchar(255) NOT NULL,
    [CategoriaAppartenenzaId] int NOT NULL,
    [DataProduzione] datetime2 NOT NULL,
    [Descrizione] nvarchar(max) NULL,
    [Foto] varbinary(max) NULL,
    [Brand] nvarchar(max) NULL,
    CONSTRAINT [PK_tabella_Prodotti] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_tabella_Prodotti_tabella_Categorie_CategoriaAppartenenzaId] FOREIGN KEY ([CategoriaAppartenenzaId]) REFERENCES [tabella_Categorie] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [tabella_Prezzo] (
    [Id] int NOT NULL IDENTITY,
    [DataCreazioneRecord] datetime2 NOT NULL,
    [DataUltimaModifica] datetime2 NOT NULL,
    [UtenteCreazioneRecord] nvarchar(max) NULL,
    [UtenteUltimaModificaRecord] nvarchar(max) NULL,
    [Costo] float NOT NULL,
    [Sconto] float NOT NULL,
    [DataInizio] datetime2 NOT NULL,
    [ProdottoAssociatoId] int NOT NULL,
    CONSTRAINT [PK_tabella_Prezzo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_tabella_Prezzo_tabella_Prodotti_ProdottoAssociatoId] FOREIGN KEY ([ProdottoAssociatoId]) REFERENCES [tabella_Prodotti] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_tabella_Prezzo_ProdottoAssociatoId] ON [tabella_Prezzo] ([ProdottoAssociatoId]);

GO

CREATE INDEX [IX_tabella_Prodotti_CategoriaAppartenenzaId] ON [tabella_Prodotti] ([CategoriaAppartenenzaId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190221110004_InitialMigration', N'2.2.2-servicing-10034');

GO

