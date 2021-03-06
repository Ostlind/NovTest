USE [master]
GO
/****** Object:  Database [TestDataDb]    Script Date: 2018-02-16 13:26:11 ******/
CREATE DATABASE [TestDataDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TestDataDb', FILENAME = N'S:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\TestDataDb.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TestDataDb_log', FILENAME = N'S:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\TestDataDb_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [TestDataDb] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestDataDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestDataDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestDataDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestDataDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestDataDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestDataDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestDataDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TestDataDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestDataDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestDataDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestDataDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TestDataDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestDataDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestDataDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestDataDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestDataDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TestDataDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestDataDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestDataDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestDataDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TestDataDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestDataDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TestDataDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestDataDb] SET RECOVERY FULL 
GO
ALTER DATABASE [TestDataDb] SET  MULTI_USER 
GO
ALTER DATABASE [TestDataDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TestDataDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestDataDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TestDataDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TestDataDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TestDataDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [TestDataDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = ALL, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [TestDataDb]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [TestDataDb]
GO
/****** Object:  Table [dbo].[BobbinOrders]    Script Date: 2018-02-16 13:26:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BobbinOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[CampaignName] [nvarchar](50) NULL,
	[FlsStationName] [varchar](50) NULL,
 CONSTRAINT [PK_BobbinOrders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bobbins]    Script Date: 2018-02-16 13:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bobbins](
	[Name] [nvarchar](50) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BobbinOrderId] [int] NOT NULL,
 CONSTRAINT [PK_Bobbins_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lots]    Script Date: 2018-02-16 13:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lots](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[BobbinId] [int] NOT NULL,
	[BatchNumber] [nvarchar](50) NULL,
	[IsUsed] [bit] NULL,
	[MaterialLeft] [int] NULL,
	[CoilNumber] [int] NULL,
	[Length] [int] NULL,
	[Comment] [varchar](50) NULL,
 CONSTRAINT [PK_Lots] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2018-02-16 13:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] NOT NULL,
	[ServerId] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[ClientProgram] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Weldings]    Script Date: 2018-02-16 13:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Weldings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LotId] [int] NULL,
	[WeldingType] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[WeldingPosition] [int] NULL,
	[IsFirstWelding] [bit] NULL,
	[EquipmentId] [int] NULL,
	[VisualInspectorUser] [nvarchar](50) NULL,
	[MpiUser] [varchar](50) NULL,
	[CutoutLength] [int] NULL,
	[WeldingSequenceNumber] [int] NULL,
 CONSTRAINT [PK__Weldings__FEB2A05D28434B2F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BobbinOrders] ON 

INSERT [dbo].[BobbinOrders] ([Id], [Name], [IsActive], [CampaignName], [FlsStationName]) VALUES (1005, N'1731006-212', 1, N'Demo Campaign 170210', N'KALEB1')
INSERT [dbo].[BobbinOrders] ([Id], [Name], [IsActive], [CampaignName], [FlsStationName]) VALUES (2005, N'1731006-230', 1, N'Demo Campaign 170210', N'KALEB1')
INSERT [dbo].[BobbinOrders] ([Id], [Name], [IsActive], [CampaignName], [FlsStationName]) VALUES (2007, N'1731006-228', 0, N'Demo Campaign 170210', N'KALEB1')
INSERT [dbo].[BobbinOrders] ([Id], [Name], [IsActive], [CampaignName], [FlsStationName]) VALUES (2008, N'1731006-229', 0, N'Demo Campaign 170210', N'KALEB1')
SET IDENTITY_INSERT [dbo].[BobbinOrders] OFF
SET IDENTITY_INSERT [dbo].[Bobbins] ON 

INSERT [dbo].[Bobbins] ([Name], [Id], [BobbinOrderId]) VALUES (N'KAL-EA01', 2, 2005)
INSERT [dbo].[Bobbins] ([Name], [Id], [BobbinOrderId]) VALUES (N'KAL-EA01', 6, 1005)
INSERT [dbo].[Bobbins] ([Name], [Id], [BobbinOrderId]) VALUES (N'KAL-EA01', 7, 2007)
INSERT [dbo].[Bobbins] ([Name], [Id], [BobbinOrderId]) VALUES (N'KAL-EA01', 8, 2008)
SET IDENTITY_INSERT [dbo].[Bobbins] OFF
SET IDENTITY_INSERT [dbo].[Lots] ON 

INSERT [dbo].[Lots] ([Id], [Name], [BobbinId], [BatchNumber], [IsUsed], [MaterialLeft], [CoilNumber], [Length], [Comment]) VALUES (1, N'coil-test-demo-01', 6, N'1567', 0, 0, 1, 2000, N'The First Coil')
INSERT [dbo].[Lots] ([Id], [Name], [BobbinId], [BatchNumber], [IsUsed], [MaterialLeft], [CoilNumber], [Length], [Comment]) VALUES (2, N'coil-test-demo-02', 6, N'1567', 0, 0, 2, 2000, N'The Second Coil')
INSERT [dbo].[Lots] ([Id], [Name], [BobbinId], [BatchNumber], [IsUsed], [MaterialLeft], [CoilNumber], [Length], [Comment]) VALUES (1012, N'coil-test-demo-03', 2, N'1568', 0, 0, 3, 2000, N'The Third Coil')
INSERT [dbo].[Lots] ([Id], [Name], [BobbinId], [BatchNumber], [IsUsed], [MaterialLeft], [CoilNumber], [Length], [Comment]) VALUES (1013, N'coil-test-demo-04', 7, N'1569', 0, 0, 4, 2000, N'The Fourth Coil')
INSERT [dbo].[Lots] ([Id], [Name], [BobbinId], [BatchNumber], [IsUsed], [MaterialLeft], [CoilNumber], [Length], [Comment]) VALUES (1014, N'coil-test-demo-05', 7, N'1569', 0, 0, 5, 2000, N'The Fifth Coil')
INSERT [dbo].[Lots] ([Id], [Name], [BobbinId], [BatchNumber], [IsUsed], [MaterialLeft], [CoilNumber], [Length], [Comment]) VALUES (1015, N'coil-test-demo-06', 8, N'1570', 0, 0, 6, 2000, N'The Sixth Coil')
SET IDENTITY_INSERT [dbo].[Lots] OFF
SET IDENTITY_INSERT [dbo].[Weldings] ON 

INSERT [dbo].[Weldings] ([Id], [LotId], [WeldingType], [Name], [WeldingPosition], [IsFirstWelding], [EquipmentId], [VisualInspectorUser], [MpiUser], [CutoutLength], [WeldingSequenceNumber]) VALUES (1, 1, N'WA', N'Wire Attach', 0, 1, 12345, N'af2', N'af2', 0, 1)
INSERT [dbo].[Weldings] ([Id], [LotId], [WeldingType], [Name], [WeldingPosition], [IsFirstWelding], [EquipmentId], [VisualInspectorUser], [MpiUser], [CutoutLength], [WeldingSequenceNumber]) VALUES (2, 1, N'CS', N'CS - Coilskift', 345, 0, 12345, N'af2', N'af2', 1, 2)
INSERT [dbo].[Weldings] ([Id], [LotId], [WeldingType], [Name], [WeldingPosition], [IsFirstWelding], [EquipmentId], [VisualInspectorUser], [MpiUser], [CutoutLength], [WeldingSequenceNumber]) VALUES (1006, 2, N'ECG', N'EC-nr. slebet', 450, 0, 12345, N'af2', N'af2', 1, 3)
INSERT [dbo].[Weldings] ([Id], [LotId], [WeldingType], [Name], [WeldingPosition], [IsFirstWelding], [EquipmentId], [VisualInspectorUser], [MpiUser], [CutoutLength], [WeldingSequenceNumber]) VALUES (1007, 2, N'WD', N'WD - Wiredefekt identificeret af NOV', 500, 0, 12345, N'af2', N'af2', 0, 4)
INSERT [dbo].[Weldings] ([Id], [LotId], [WeldingType], [Name], [WeldingPosition], [IsFirstWelding], [EquipmentId], [VisualInspectorUser], [MpiUser], [CutoutLength], [WeldingSequenceNumber]) VALUES (1008, 2, N'CS', N'CS - Coilskift', 600, 0, 12345, N'af2', N'af2', 0, 5)
INSERT [dbo].[Weldings] ([Id], [LotId], [WeldingType], [Name], [WeldingPosition], [IsFirstWelding], [EquipmentId], [VisualInspectorUser], [MpiUser], [CutoutLength], [WeldingSequenceNumber]) VALUES (1009, 1012, N'WA', N'Wire Attach', 0, 1, 12345, N'af2', N'af2', 0, 1)
INSERT [dbo].[Weldings] ([Id], [LotId], [WeldingType], [Name], [WeldingPosition], [IsFirstWelding], [EquipmentId], [VisualInspectorUser], [MpiUser], [CutoutLength], [WeldingSequenceNumber]) VALUES (1010, 1012, N'CS', N'CS - Coilskift', 450, 0, 12345, N'af2', N'af2', 1, 3)
INSERT [dbo].[Weldings] ([Id], [LotId], [WeldingType], [Name], [WeldingPosition], [IsFirstWelding], [EquipmentId], [VisualInspectorUser], [MpiUser], [CutoutLength], [WeldingSequenceNumber]) VALUES (1011, 1012, N'ECG', N'EC-nr. slebet', 230, 0, 12345, N'af2', N'af2', 1, 2)
SET IDENTITY_INSERT [dbo].[Weldings] OFF
ALTER TABLE [dbo].[Bobbins]  WITH CHECK ADD  CONSTRAINT [FK_Bobbins_BobbinOrders] FOREIGN KEY([BobbinOrderId])
REFERENCES [dbo].[BobbinOrders] ([Id])
GO
ALTER TABLE [dbo].[Bobbins] CHECK CONSTRAINT [FK_Bobbins_BobbinOrders]
GO
ALTER TABLE [dbo].[Lots]  WITH CHECK ADD  CONSTRAINT [FK_Lots_Lots] FOREIGN KEY([BobbinId])
REFERENCES [dbo].[Bobbins] ([Id])
GO
ALTER TABLE [dbo].[Lots] CHECK CONSTRAINT [FK_Lots_Lots]
GO
ALTER TABLE [dbo].[Weldings]  WITH CHECK ADD  CONSTRAINT [FK__Weldings__Lot__18EBB532] FOREIGN KEY([LotId])
REFERENCES [dbo].[Lots] ([Id])
GO
ALTER TABLE [dbo].[Weldings] CHECK CONSTRAINT [FK__Weldings__Lot__18EBB532]
GO
USE [master]
GO
ALTER DATABASE [TestDataDb] SET  READ_WRITE 
GO
