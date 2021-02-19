USE [master]
GO
CREATE DATABASE [Point_Markers]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Point_Markers', FILENAME = N'D:\Programs\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Point_Markers.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Point_Markers_log', FILENAME = N'D:\Programs\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Point_Markers_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Point_Markers] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Point_Markers].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Point_Markers] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Point_Markers] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Point_Markers] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Point_Markers] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Point_Markers] SET ARITHABORT OFF 
GO
ALTER DATABASE [Point_Markers] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Point_Markers] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Point_Markers] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Point_Markers] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Point_Markers] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Point_Markers] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Point_Markers] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Point_Markers] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Point_Markers] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Point_Markers] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Point_Markers] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Point_Markers] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Point_Markers] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Point_Markers] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Point_Markers] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Point_Markers] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Point_Markers] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Point_Markers] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Point_Markers] SET  MULTI_USER 
GO
ALTER DATABASE [Point_Markers] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Point_Markers] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Point_Markers] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Point_Markers] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Point_Markers] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Point_Markers] SET QUERY_STORE = OFF
GO
USE [Point_Markers]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Place](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[X] [float] NOT NULL,
	[Y] [float] NOT NULL,
	[Place] [nvarchar](50) NULL,
 CONSTRAINT [PK_Place] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Place] ON 

INSERT [dbo].[Place] ([ID], [X], [Y], [Place]) VALUES (2, -23.322080011378432, -58.55712890625, N'Парагвай')
INSERT [dbo].[Place] ([ID], [X], [Y], [Place]) VALUES (3, 57.515822865538816, -101.865234375, N'Канада')
INSERT [dbo].[Place] ([ID], [X], [Y], [Place]) VALUES (4, 56.016130892, 92.8960478, N'Красноярск')
INSERT [dbo].[Place] ([ID], [X], [Y], [Place]) VALUES (6, 1.4390576608077481, 103.86474609375, N'Сингапур')
SET IDENTITY_INSERT [dbo].[Place] OFF
GO
USE [master]
GO
ALTER DATABASE [Point_Markers] SET  READ_WRITE 
GO
