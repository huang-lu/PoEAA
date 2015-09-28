﻿CREATE TABLE [dbo].[Producto] (
    [ProductoId] INT			NOT NULL,
    [Nombre]   NVARCHAR (200)	NULL,
    [Tipo]    NVARCHAR (200)	NOT NULL,
    CONSTRAINT [PK_dbo.Producto] PRIMARY KEY CLUSTERED ([ProductoId] ASC)
);

CREATE TABLE [dbo].[Contrato] (
    [ContratoId] INT            NOT NULL,
	[ProductoId] INT			NOT NULL,
    [Ingreso]   DECIMAL			NULL,
    [Fecha]    DATE				NOT NULL,
    CONSTRAINT [PK_dbo.Contrato] PRIMARY KEY CLUSTERED ([ProductoId] ASC),
	CONSTRAINT [FK_dbo.Contrato_dbo.Producto_ProductoId] FOREIGN KEY ([ProductoId]) REFERENCES [dbo].[Producto] ([ProductoId]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[ReconocimientoIngreso] (
    [ContratoId] INT            NOT NULL,
	[Cantidad] DECIMAL			NOT NULL,
    [FechaReconocimiento]   DECIMAL NOT NULL,
    CONSTRAINT [PK_dbo.ReconocimientoIngreso] PRIMARY KEY CLUSTERED ([ContratoId], [FechaReconocimiento] ASC)
);

CREATE TABLE [dbo].[Persona] (
    [PersonaId]          INT            NOT NULL,
    [Nombre]             NVARCHAR (200) NULL,
    [Apellidos]          NVARCHAR (200) NULL,
    [numeroDependientes] INT            NULL,
    PRIMARY KEY CLUSTERED ([PersonaId] ASC)
);

CREATE TABLE [dbo].[Album]
(
	[AlbumId] INT NOT NULL PRIMARY KEY, 
    [Titulo] NVARCHAR(200) NULL
)
