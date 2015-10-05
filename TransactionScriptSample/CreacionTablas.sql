CREATE TABLE [dbo].[Producto] (
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

CREATE TABLE [dbo].[Album] (
    [AlbumId]   INT            NOT NULL,
    [ArtistaId] INT            NOT NULL,
    [Titulo]    NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([AlbumId] ASC),
    CONSTRAINT [fk_ArtistaAlbum] FOREIGN KEY ([ArtistaId]) REFERENCES [dbo].[Artista] ([ArtistaId])
);

CREATE TABLE [dbo].[Artista]
(
	[ArtistaId] INT NOT NULL PRIMARY KEY, 
    [Nombre] NVARCHAR(200) NULL
);

CREATE TABLE [dbo].[Empleado]
(
	[EmpleadoId] INT NOT NULL PRIMARY KEY, 
    [Nombre] NVARCHAR(200) NULL
);

CREATE TABLE [dbo].[Habilidad]
(
	[HabilidadId] INT NOT NULL PRIMARY KEY, 
    [Nombre] NVARCHAR(200) NULL
);

CREATE TABLE [dbo].[HabilidadesEmpleados]
(
	[EmpleadoId] INT NOT NULL , 
    [HabilidadId] INT NOT NULL, 
    PRIMARY KEY ([EmpleadoId],[HabilidadId]),
	FOREIGN KEY ([EmpleadoId]) REFERENCES [dbo].[Empleado] ([EmpleadoId]),
	FOREIGN KEY ([HabilidadId]) REFERENCES [dbo].[Habilidad] ([HabilidadId]),
);

CREATE TABLE [dbo].[Cancion] (
    [CancionId] INT            NOT NULL,
	[AlbumId] INT NOT NULL,
    [Titulo]    NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([CancionId] ASC),
	FOREIGN KEY ([AlbumId]) REFERENCES [dbo].[Album] ([AlbumId])
);