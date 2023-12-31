USE [master]
GO
/****** Object:  Database [Department]    Script Date: 07.12.2023 17:19:13 ******/
CREATE DATABASE [Department]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Department', FILENAME = N'D:\SSEI\MSSQL16.SQLEXPRESS\MSSQL\DATA\Department.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Department_log', FILENAME = N'D:\SSEI\MSSQL16.SQLEXPRESS\MSSQL\DATA\Department_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Department] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Department].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Department] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Department] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Department] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Department] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Department] SET ARITHABORT OFF 
GO
ALTER DATABASE [Department] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Department] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Department] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Department] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Department] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Department] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Department] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Department] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Department] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Department] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Department] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Department] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Department] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Department] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Department] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Department] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Department] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Department] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Department] SET  MULTI_USER 
GO
ALTER DATABASE [Department] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Department] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Department] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Department] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Department] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Department] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Department] SET QUERY_STORE = OFF
GO
USE [Department]
GO
/****** Object:  User [Ingener]    Script Date: 07.12.2023 17:19:13 ******/
CREATE USER [Ingener] FOR LOGIN [Ingener] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [Administrator]    Script Date: 07.12.2023 17:19:13 ******/
CREATE USER [Administrator] FOR LOGIN [Administrator] WITH DEFAULT_SCHEMA=[db_accessadmin]
GO
/****** Object:  DatabaseRole [Ingeners]    Script Date: 07.12.2023 17:19:13 ******/
CREATE ROLE [Ingeners]
GO
/****** Object:  DatabaseRole [Administrators]    Script Date: 07.12.2023 17:19:13 ******/
CREATE ROLE [Administrators]
GO
ALTER ROLE [Ingeners] ADD MEMBER [Ingener]
GO
ALTER ROLE [Administrators] ADD MEMBER [Administrator]
GO
/****** Object:  Table [dbo].[Machine]    Script Date: 07.12.2023 17:19:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Machine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatusId] [nchar](2) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DateOfPurchase] [date] NOT NULL,
	[Creator] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Machine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RepairRequest]    Script Date: 07.12.2023 17:19:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RepairRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdMachine] [int] NOT NULL,
	[DateOfCreate] [date] NOT NULL,
	[DateOfRepairing] [date] NOT NULL,
	[UserId] [int] NOT NULL,
	[Result] [nvarchar](max) NULL,
	[DateOfClose] [date] NULL,
	[Creator] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_RepairRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 07.12.2023 17:19:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 07.12.2023 17:19:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [nchar](2) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestRequests]    Script Date: 07.12.2023 17:19:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdMachine] [int] NOT NULL,
	[DateOfCreate] [date] NOT NULL,
	[DateOfTesting] [date] NOT NULL,
	[Result] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
	[DateOfClose] [date] NULL,
	[Creator] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TestRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersTable]    Script Date: 07.12.2023 17:19:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UsersTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Machine] ON 

INSERT [dbo].[Machine] ([Id], [StatusId], [Name], [DateOfPurchase], [Creator]) VALUES (1, N'Iw', N'Токарный', CAST(N'2023-04-25' AS Date), N'Лавров Сергей Витальевич')
INSERT [dbo].[Machine] ([Id], [StatusId], [Name], [DateOfPurchase], [Creator]) VALUES (2, N'Iw', N'Резной', CAST(N'2023-04-25' AS Date), N'Лавров Сергей Витальевич')
INSERT [dbo].[Machine] ([Id], [StatusId], [Name], [DateOfPurchase], [Creator]) VALUES (3, N'IP', N'Расточный', CAST(N'2023-04-19' AS Date), N'Лавров Сергей Витальевич')
INSERT [dbo].[Machine] ([Id], [StatusId], [Name], [DateOfPurchase], [Creator]) VALUES (4, N'Iw', N'Шлифовальный', CAST(N'2022-07-22' AS Date), N'Лавров Сергей Витальевич')
INSERT [dbo].[Machine] ([Id], [StatusId], [Name], [DateOfPurchase], [Creator]) VALUES (5, N'Iw', N'asdfas', CAST(N'2023-12-07' AS Date), N'22222')
SET IDENTITY_INSERT [dbo].[Machine] OFF
GO
SET IDENTITY_INSERT [dbo].[RepairRequest] ON 

INSERT [dbo].[RepairRequest] ([Id], [IdMachine], [DateOfCreate], [DateOfRepairing], [UserId], [Result], [DateOfClose], [Creator]) VALUES (1, 2, CAST(N'2023-04-25' AS Date), CAST(N'2023-04-25' AS Date), 2, N'Замена масла, работает в штатном режиме.', CAST(N'2023-04-25' AS Date), N'Лавров Сергей Витальевич')
INSERT [dbo].[RepairRequest] ([Id], [IdMachine], [DateOfCreate], [DateOfRepairing], [UserId], [Result], [DateOfClose], [Creator]) VALUES (2, 4, CAST(N'2023-04-25' AS Date), CAST(N'2023-02-22' AS Date), 1, N'Замена частей. Работает в штатном режиме.', CAST(N'2023-04-25' AS Date), N'Лавров Сергей Витальевич')
INSERT [dbo].[RepairRequest] ([Id], [IdMachine], [DateOfCreate], [DateOfRepairing], [UserId], [Result], [DateOfClose], [Creator]) VALUES (3, 3, CAST(N'2023-04-25' AS Date), CAST(N'2023-04-30' AS Date), 1, NULL, NULL, N'Лавров Сергей Витальевич')
INSERT [dbo].[RepairRequest] ([Id], [IdMachine], [DateOfCreate], [DateOfRepairing], [UserId], [Result], [DateOfClose], [Creator]) VALUES (4, 5, CAST(N'2023-12-07' AS Date), CAST(N'2023-12-07' AS Date), 2, N'aasdfasdf', CAST(N'2023-12-07' AS Date), N'Негр')
SET IDENTITY_INSERT [dbo].[RepairRequest] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Title]) VALUES (1, N'Администратор')
INSERT [dbo].[Role] ([Id], [Title]) VALUES (2, N'Инженер')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
INSERT [dbo].[Status] ([Id], [Title]) VALUES (N'IP', N'Работа приостановлена')
INSERT [dbo].[Status] ([Id], [Title]) VALUES (N'Iw', N'В работе')
GO
SET IDENTITY_INSERT [dbo].[TestRequests] ON 

INSERT [dbo].[TestRequests] ([Id], [IdMachine], [DateOfCreate], [DateOfTesting], [Result], [UserId], [DateOfClose], [Creator]) VALUES (1, 1, CAST(N'2023-04-25' AS Date), CAST(N'2023-03-30' AS Date), N'Станок прошёл тестирование! Всё работает в штатном режиме.', 1, CAST(N'2023-04-25' AS Date), N'Лавров Сергей Витальевич')
INSERT [dbo].[TestRequests] ([Id], [IdMachine], [DateOfCreate], [DateOfTesting], [Result], [UserId], [DateOfClose], [Creator]) VALUES (2, 2, CAST(N'2023-04-25' AS Date), CAST(N'2023-03-01' AS Date), N'Нужна замена запчастей.', 2, CAST(N'2023-04-25' AS Date), N'Лавров Сергей Витальевич')
INSERT [dbo].[TestRequests] ([Id], [IdMachine], [DateOfCreate], [DateOfTesting], [Result], [UserId], [DateOfClose], [Creator]) VALUES (3, 3, CAST(N'2023-04-25' AS Date), CAST(N'2023-05-10' AS Date), NULL, 1, NULL, N'Лавров Сергей Витальевич')
INSERT [dbo].[TestRequests] ([Id], [IdMachine], [DateOfCreate], [DateOfTesting], [Result], [UserId], [DateOfClose], [Creator]) VALUES (10, 3, CAST(N'2023-12-07' AS Date), CAST(N'2023-12-07' AS Date), NULL, 2, NULL, N'Негр')
SET IDENTITY_INSERT [dbo].[TestRequests] OFF
GO
SET IDENTITY_INSERT [dbo].[UsersTable] ON 

INSERT [dbo].[UsersTable] ([Id], [Login], [Password], [RoleId], [FullName]) VALUES (1, N'E3AFED0047B08059D0FADA10F400C1E5', N'21232F297A57A5A743894A0E4A801FC3', 1, N'Жуков Алексей Николаевич')
INSERT [dbo].[UsersTable] ([Id], [Login], [Password], [RoleId], [FullName]) VALUES (2, N'1A532207F5E492166C1D4A0F0A2CBB34', N'F727F70BEC0E6C8E36EFDE5ED2B9502A', 2, N'Лавров Сергей Витальевич')
SET IDENTITY_INSERT [dbo].[UsersTable] OFF
GO
ALTER TABLE [dbo].[Machine]  WITH CHECK ADD  CONSTRAINT [FK_Machine_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Machine] CHECK CONSTRAINT [FK_Machine_Status]
GO
ALTER TABLE [dbo].[RepairRequest]  WITH CHECK ADD  CONSTRAINT [FK_RepairRequest_Machine] FOREIGN KEY([IdMachine])
REFERENCES [dbo].[Machine] ([Id])
GO
ALTER TABLE [dbo].[RepairRequest] CHECK CONSTRAINT [FK_RepairRequest_Machine]
GO
ALTER TABLE [dbo].[RepairRequest]  WITH CHECK ADD  CONSTRAINT [FK_RepairRequest_UsersTable] FOREIGN KEY([UserId])
REFERENCES [dbo].[UsersTable] ([Id])
GO
ALTER TABLE [dbo].[RepairRequest] CHECK CONSTRAINT [FK_RepairRequest_UsersTable]
GO
ALTER TABLE [dbo].[TestRequests]  WITH CHECK ADD  CONSTRAINT [FK_TestRequests_Machine] FOREIGN KEY([IdMachine])
REFERENCES [dbo].[Machine] ([Id])
GO
ALTER TABLE [dbo].[TestRequests] CHECK CONSTRAINT [FK_TestRequests_Machine]
GO
ALTER TABLE [dbo].[TestRequests]  WITH CHECK ADD  CONSTRAINT [FK_TestRequests_UsersTable] FOREIGN KEY([UserId])
REFERENCES [dbo].[UsersTable] ([Id])
GO
ALTER TABLE [dbo].[TestRequests] CHECK CONSTRAINT [FK_TestRequests_UsersTable]
GO
ALTER TABLE [dbo].[UsersTable]  WITH CHECK ADD  CONSTRAINT [FK_UsersTable_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[UsersTable] CHECK CONSTRAINT [FK_UsersTable_Role]
GO
USE [master]
GO
ALTER DATABASE [Department] SET  READ_WRITE 
GO
