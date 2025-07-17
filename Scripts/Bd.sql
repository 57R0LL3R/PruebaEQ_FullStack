USE [master]
GO

-- Crear la base de datos
CREATE DATABASE [EQDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EQDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EQDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EQDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EQDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [EQDB] SET COMPATIBILITY_LEVEL = 160
GO

-- Opcionales
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
EXEC [EQDB].[dbo].[sp_fulltext_database] @action = 'enable'
GO

-- Configuraciones de la base de datos (opcional pero útil)
ALTER DATABASE [EQDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EQDB] SET RECOVERY FULL 
GO
ALTER DATABASE [EQDB] SET MULTI_USER 
GO
ALTER DATABASE [EQDB] SET PAGE_VERIFY CHECKSUM  
GO

-- Usar la base de datos
USE [EQDB]
GO

-- Crear tablas
CREATE TABLE [dbo].[DocKey](
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[DocName] NVARCHAR(200) NOT NULL,
	[Key] NVARCHAR(200) NOT NULL
)
GO

CREATE TABLE [dbo].[LogProcess](
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[OriginalFileName] NVARCHAR(200) NOT NULL,
	[Status] NVARCHAR(200) NOT NULL,
	[NewFileName] NVARCHAR(200) NULL,
	[DateProcces] DATETIME2(7) NOT NULL
)
GO

CREATE TABLE [dbo].[User](
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[EmailAddress] NVARCHAR(100) NOT NULL,
	[ContrasenaHash] NVARCHAR(256) NOT NULL
)
GO

-- Insertar datos base

SET IDENTITY_INSERT [dbo].[DocKey] ON
INSERT [dbo].[DocKey] ([Id], [DocName], [Key]) VALUES 
(1, N'docil', N'paratelano'),
(2, N'keydocument', N'key'),
(3, N'aturnedocument', N'aturne'),
(6, N'cotkeysfile', N'cotkeys')
SET IDENTITY_INSERT [dbo].[DocKey] OFF
GO

SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [EmailAddress], [ContrasenaHash]) VALUES 
(1, N'Admin@email.com', N'FihBbBjzn8XNxZoev5gc7vjEA1HiEkISWEEe8IXa1W8='),
(2, N'Service@email.com', N'UmqyzrvvtxmFCp292Sl7vM5FrI5xAeOipkGyR9rSFLw=')
SET IDENTITY_INSERT [dbo].[User] OFF
GO

-- Cambiar de nuevo a master y dejar la base de datos lista para escritura
USE [master]
GO
ALTER DATABASE [EQDB] SET READ_WRITE
GO
