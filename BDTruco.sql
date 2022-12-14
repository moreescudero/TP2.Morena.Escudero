USE [master]
GO
/****** Object:  Database [truco]    Script Date: 11/20/2022 7:29:33 PM ******/
CREATE DATABASE [truco]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'truco', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\truco.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'truco_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\truco_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [truco] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [truco].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [truco] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [truco] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [truco] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [truco] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [truco] SET ARITHABORT OFF 
GO
ALTER DATABASE [truco] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [truco] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [truco] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [truco] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [truco] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [truco] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [truco] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [truco] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [truco] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [truco] SET  DISABLE_BROKER 
GO
ALTER DATABASE [truco] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [truco] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [truco] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [truco] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [truco] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [truco] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [truco] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [truco] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [truco] SET  MULTI_USER 
GO
ALTER DATABASE [truco] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [truco] SET DB_CHAINING OFF 
GO
ALTER DATABASE [truco] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [truco] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [truco] SET DELAYED_DURABILITY = DISABLED 
GO
USE [truco]
GO
/****** Object:  Table [dbo].[Partidas]    Script Date: 11/20/2022 7:29:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partidas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ganador] [varchar](50) NOT NULL,
	[Perdedor] [varchar](50) NOT NULL,
	[Fecha] [date] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 11/20/2022 7:29:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Contraseña] [varchar](50) NOT NULL,
	[PartidasGanadas] [int] NULL,
	[PartidasPerdidas] [int] NULL,
	[AnchosEspadaObtenidos] [int] NULL,
	[CantFaltaEnvidoJugados] [int] NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Partidas] ON 

INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (1, N'cielmonster', N'lobe', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (2, N'more', N'lupput', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (3, N'cielmonster', N'luki', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (4, N'addq', N'mzarate07', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (5, N'lachancha', N'romal', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (6, N'facuarg16', N'lupput', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (7, N'more', N'romal', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (8, N'nicpichi', N'cielmonster', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (9, N'lobe', N'mishh30', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (20, N'nicpichi', N'lupput', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (21, N'lobe', N'luki', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (22, N'nicpichi', N'cielmonster', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (23, N'lupput', N'lachancha', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (24, N'mishh30', N'facuarg16', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (25, N'lobe', N'more', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (26, N'mishh30', N'luki', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (27, N'more', N'lachancha', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (28, N'facuarg16', N'lobe', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (29, N'romal', N'mzarate07', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (30, N'nicpichi', N'addq', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (31, N'lupput', N'facuarg16', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (32, N'mishh30', N'cielmonster', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (33, N'luki', N'more', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (34, N'mzarate07', N'lobe', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (35, N'facuarg16', N'luki', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (36, N'addq', N'cielmonster', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (37, N'romal', N'mishh30', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (38, N'addq', N'lachancha', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (51, N'romal', N'addq', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (52, N'facuarg16', N'luki', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (53, N'cielmonster', N'nicpichi', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (54, N'lupput', N'facuarg16', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (55, N'mzarate07', N'asd', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (56, N'lachancha', N'mishh30', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (57, N'luki', N'asd', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (58, N'more', N'asd', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (10, N'luki', N'cielmonster', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (11, N'romal', N'addq', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (12, N'lupput', N'more', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (13, N'facuarg16', N'lachancha', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (14, N'romal', N'lachancha', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (15, N'more', N'mzarate07', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (16, N'facuarg16', N'lupput', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (17, N'mishh30', N'luki', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (18, N'more', N'cielmonster', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (19, N'mzarate07', N'romal', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (39, N'lobe', N'romal', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (40, N'luki', N'asd', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (41, N'mzarate07', N'addq', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (42, N'lupput', N'facuarg16', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (43, N'asd', N'mishh30', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (44, N'nicpichi', N'mzarate07', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (45, N'romal', N'luki', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (46, N'lobe', N'facuarg16', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (47, N'mzarate07', N'lachancha', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (48, N'lobe', N'facuarg16', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (49, N'asd', N'cielmonster', CAST(N'2022-11-20' AS Date))
INSERT [dbo].[Partidas] ([Id], [Ganador], [Perdedor], [Fecha]) VALUES (50, N'nicpichi', N'lupput', CAST(N'2022-11-20' AS Date))
SET IDENTITY_INSERT [dbo].[Partidas] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id], [Usuario], [Contraseña], [PartidasGanadas], [PartidasPerdidas], [AnchosEspadaObtenidos], [CantFaltaEnvidoJugados]) VALUES (1, N'asd', N'asd', 2, 3, 3, 6)
INSERT [dbo].[Usuarios] ([Id], [Usuario], [Contraseña], [PartidasGanadas], [PartidasPerdidas], [AnchosEspadaObtenidos], [CantFaltaEnvidoJugados]) VALUES (2, N'more', N'more', 5, 3, 9, 2)
INSERT [dbo].[Usuarios] ([Id], [Usuario], [Contraseña], [PartidasGanadas], [PartidasPerdidas], [AnchosEspadaObtenidos], [CantFaltaEnvidoJugados]) VALUES (3, N'addq', N'1234', 3, 4, 7, 2)
INSERT [dbo].[Usuarios] ([Id], [Usuario], [Contraseña], [PartidasGanadas], [PartidasPerdidas], [AnchosEspadaObtenidos], [CantFaltaEnvidoJugados]) VALUES (4, N'luki', N'1234', 5, 7, 6, 6)
INSERT [dbo].[Usuarios] ([Id], [Usuario], [Contraseña], [PartidasGanadas], [PartidasPerdidas], [AnchosEspadaObtenidos], [CantFaltaEnvidoJugados]) VALUES (5, N'lachancha', N'1234', 2, 6, 8, 4)
INSERT [dbo].[Usuarios] ([Id], [Usuario], [Contraseña], [PartidasGanadas], [PartidasPerdidas], [AnchosEspadaObtenidos], [CantFaltaEnvidoJugados]) VALUES (6, N'mzarate07', N'maxi', 5, 4, 9, 3)
INSERT [dbo].[Usuarios] ([Id], [Usuario], [Contraseña], [PartidasGanadas], [PartidasPerdidas], [AnchosEspadaObtenidos], [CantFaltaEnvidoJugados]) VALUES (7, N'lobe', N'lombi', 6, 4, 9, 4)
INSERT [dbo].[Usuarios] ([Id], [Usuario], [Contraseña], [PartidasGanadas], [PartidasPerdidas], [AnchosEspadaObtenidos], [CantFaltaEnvidoJugados]) VALUES (8, N'facuarg16', N'facu', 6, 7, 5, 4)
INSERT [dbo].[Usuarios] ([Id], [Usuario], [Contraseña], [PartidasGanadas], [PartidasPerdidas], [AnchosEspadaObtenidos], [CantFaltaEnvidoJugados]) VALUES (9, N'mishh30', N'mishka', 4, 4, 5, 3)
INSERT [dbo].[Usuarios] ([Id], [Usuario], [Contraseña], [PartidasGanadas], [PartidasPerdidas], [AnchosEspadaObtenidos], [CantFaltaEnvidoJugados]) VALUES (10, N'nicpichi', N'nico', 7, 1, 5, 6)
INSERT [dbo].[Usuarios] ([Id], [Usuario], [Contraseña], [PartidasGanadas], [PartidasPerdidas], [AnchosEspadaObtenidos], [CantFaltaEnvidoJugados]) VALUES (11, N'lupput', N'lupi', 6, 6, 4, 5)
INSERT [dbo].[Usuarios] ([Id], [Usuario], [Contraseña], [PartidasGanadas], [PartidasPerdidas], [AnchosEspadaObtenidos], [CantFaltaEnvidoJugados]) VALUES (12, N'romal', N'1234', 6, 4, 4, 6)
INSERT [dbo].[Usuarios] ([Id], [Usuario], [Contraseña], [PartidasGanadas], [PartidasPerdidas], [AnchosEspadaObtenidos], [CantFaltaEnvidoJugados]) VALUES (13, N'cielmonster', N'12345', 3, 7, 7, 1)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
USE [master]
GO
ALTER DATABASE [truco] SET  READ_WRITE 
GO
