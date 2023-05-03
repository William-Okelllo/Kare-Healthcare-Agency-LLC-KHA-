USE [master]
GO
/****** Object:  Database [HR2_Cashmate]    Script Date: 5/3/2023 11:32:12 PM ******/
CREATE DATABASE [HR2_Cashmate]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HR2_Cashmate', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\HR2_Cashmate.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HR2_Cashmate_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\HR2_Cashmate_log.ldf' , SIZE = 204800KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [HR2_Cashmate] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HR2_Cashmate].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HR2_Cashmate] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET ARITHABORT OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HR2_Cashmate] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HR2_Cashmate] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HR2_Cashmate] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HR2_Cashmate] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [HR2_Cashmate] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET RECOVERY FULL 
GO
ALTER DATABASE [HR2_Cashmate] SET  MULTI_USER 
GO
ALTER DATABASE [HR2_Cashmate] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HR2_Cashmate] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HR2_Cashmate] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HR2_Cashmate] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HR2_Cashmate] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HR2_Cashmate] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'HR2_Cashmate', N'ON'
GO
ALTER DATABASE [HR2_Cashmate] SET QUERY_STORE = ON
GO
ALTER DATABASE [HR2_Cashmate] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [HR2_Cashmate]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accesses]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accesses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Role] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Accesses] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bugs]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bugs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[Support_Message] [nvarchar](max) NULL,
	[Posted_By] [nvarchar](max) NULL,
	[severity] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[Post_Date] [datetime] NULL,
 CONSTRAINT [PK_dbo.Bugs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[item] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Configs]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[SSRSReportsUrl] [nvarchar](max) NULL,
	[Business_mail] [nvarchar](max) NULL,
	[Smtp] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Configs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Data]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Data] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DprtName] [nvarchar](max) NULL,
	[Contact] [nvarchar](max) NULL,
	[Email_Address] [nvarchar](max) NULL,
	[manager] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Departments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Fullname] [nvarchar](max) NULL,
	[Phone] [nvarchar](100) NULL,
	[Employee_Address] [nvarchar](max) NULL,
	[Home_Address] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[Last_Update] [datetime] NULL,
	[Username] [nvarchar](100) NULL,
	[email] [nvarchar](100) NULL,
	[Emergency_Contact] [nvarchar](100) NULL,
	[Wage] [decimal](19, 2) NULL,
	[Current_Access] [nvarchar](100) NULL,
	[Userid] [nvarchar](100) NULL,
	[Department] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees_Config]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees_Config](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Total_leaves_per_year] [int] NULL,
	[Leave_pre_days] [int] NULL,
	[Advance_pay_limit] [int] NULL,
 CONSTRAINT [PK_dbo.Employees_Config] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expenses]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expenses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[Amount] [decimal](19, 2) NULL,
	[Fuliza] [decimal](19, 2) NULL,
	[Transaction_cost] [decimal](19, 2) NULL,
	[Item] [nvarchar](max) NULL,
	[Additional_Notes] [nvarchar](max) NULL,
	[Total] [decimal](19, 2) NULL,
	[Mode] [nvarchar](100) NULL,
	[Staff] [nvarchar](100) NULL,
	[Item2] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Expenses] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Path] [nvarchar](max) NULL,
	[Uploaded_By] [nvarchar](max) NULL,
	[Access] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[UploadedOn] [datetime] NULL,
	[Category] [nvarchar](100) NULL,
	[Agency] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Files] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[Amount] [decimal](19, 2) NULL,
	[Additional_Notes] [nvarchar](max) NULL,
	[Source] [nvarchar](100) NULL,
	[Mode] [nvarchar](100) NULL,
	[User_Acc] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Invoices] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Office]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Office](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[latitude] [nvarchar](100) NULL,
	[longitude] [nvarchar](100) NULL,
	[Office_location] [nvarchar](100) NULL,
	[Radius] [int] NULL,
 CONSTRAINT [PK_dbo.Office] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reports]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Report_Name] [nvarchar](100) NULL,
	[Report_Path] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Report] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staffs]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staffs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Contact_Person] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[Phone] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Staffs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 5/3/2023 11:32:12 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 5/3/2023 11:32:12 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 5/3/2023 11:32:12 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 5/3/2023 11:32:12 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 5/3/2023 11:32:12 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 5/3/2023 11:32:12 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
/****** Object:  StoredProcedure [dbo].[Acc_Profile]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Acc_Profile]
@u nvarchar(100)
as begin
select
A.id
,A.Username
,A.email
,A.PhoneNumber
,STRING_AGG (CONVERT(NVARCHAR(max),X.name), ' , ') AS Role
from AspNetUsers A inner join AspNetUserRoles R on A.id=R.UserId
                   inner join AspNetRoles X on X.Id=R.RoleId
where username=@U
group by A.username,A.Email,A.id,A.PhoneNumber
end

--exec Acc_Profile 'Muru'
GO
/****** Object:  StoredProcedure [dbo].[AccessRolesSp]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  proc [dbo].[AccessRolesSp]
As begin
select
distinct Au.UserName
,Au.Email
,A.Name
,A.Id [RoleId]
From AspNetUserRoles AR inner join AspNetUsers Au on Ar.UserId=Au.Id
                        inner join AspNetRoles A on A.Id=AR.RoleId
						where  NOT au.UserName='Muru'
						end


						--exec AccessRolesSp
GO
/****** Object:  StoredProcedure [dbo].[AddPhone]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[AddPhone]
@phone nvarchar(100)
,@email nvarchar(100)
,@user nvarchar(100)
as begin
update AspNetUsers
set PhoneNumber=@phone,PhoneNumberConfirmed=1,Email=@email
where username=@user 
end
GO
/****** Object:  StoredProcedure [dbo].[Checkin/Checkout]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Checkin/Checkout]
@Username nvarchar(100)
,@type nvarchar(100)
as 
if ((select device from Attendances  where User_Account=@Username )= (@type+FORMAT (getdate(), 'dd')))
 
    Begin
update Vacation_Notice set Account_exists='234d2343443343d34d2423424343423x2232x3ze323z232323r23rr23e3232'

end
else
begin
  select id=007

  end
   

	--exec [Checkin/Checkout] 'Kanaan','Checkout'
	
	--select * from Attendances


GO
/****** Object:  StoredProcedure [dbo].[Delete_File]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Delete_File]
@id int
as begin
delete from files where id=@id
end


--select*from files
GO
/****** Object:  StoredProcedure [dbo].[Delete_User]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Delete_User]
@Username nvarchar(500)
as begin
delete  From aspnetusers where id=@Username
delete  From Employees where Userid=@Username
end
GO
/****** Object:  StoredProcedure [dbo].[GetData]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[GetData]
@id int
--,@status nvarchar(100)
as begin
Declare @Userdata nvarchar(100) 
set  @Userdata=(select id from AspNetUsers where username= (select Username from employees where id=@id))




select @Userdata
end


--exec GetData 513156
GO
/****** Object:  StoredProcedure [dbo].[GetEmp]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetEmp]
@Staffing nvarchar(100)
as begin
select
r.id
,r.username [Employee]
,r.email
,r.PhoneNumber
,COALESCE(f.Facility,'Unassigned') [Agency]
,r.EmailConfirmed
,STRING_AGG (CONVERT(NVARCHAR(max),v.name), ' + ') AS Role
from AspNetUserRoles a inner join AspNetRoles v on a.roleid=v.id
                       inner join AspNetUsers r on r.id=a.userid
					   left join Users_Facility F on F.Username=r.UserName
					   where v.name in ('Employee','Can_Take_Shift') and F.Facility=@Staffing
					   group by r.username,r.EmailConfirmed,r.email,r.id,f.Facility,PhoneNumber

					 end
GO
/****** Object:  StoredProcedure [dbo].[GetEmpUsername]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetEmpUsername]
@id int
as begin
select id=7,username  from  Employees
where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[Getmail_TT]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Getmail_TT]
@id int
as begin
Declare @Email nvarchar(100)
Set @Email=(select Email from AspNetUsers where UserName= (select Employee from TTs where id=@id));

select @Email [Email]
end

--exec Getmail_TT 20
GO
/****** Object:  StoredProcedure [dbo].[PushEmp]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[PushEmp]
@Username nvarchar(100)
,@Email nvarchar(100)
,@Role nvarchar(100)
As begin

IF(@Role ='Employee')
begin
SET IDENTITY_INSERT employees ON
insert into employees (Id,Username,email,Last_Update,Wage,Current_Access,Userid)
values ((ABS(CHECKSUM(NEWID()) % 1000000)),@Username,@Email,GetDate(),0,'Pending' ,(select id from AspNetUsers where username=@username))


end
else
select*from Employees


end

--exec PushEmp 'Charles','murucharls@gmail.com','Employee'
--delete from Employees


GO
/****** Object:  StoredProcedure [dbo].[Role_Set]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Role_Set]
@id int
,@status nvarchar(100)

as begin
Declare @Username nvarchar(100)
set @Username= (select Username from Employees where id=@id)
SET IDENTITY_INSERT Accesses ON
insert into Accesses(id,Username,Role,Status) values (ABS(CHECKSUM(NEWID()) % 100000),@username,'Can_Take_Shift',@Status)
exec RolesAdd
end


--exec [Role_Set] 513156 ,'Add'

--select*from TempData
--select*from Accesses
--delete from TempData=
GO
/****** Object:  StoredProcedure [dbo].[SetRole]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SetRole]
@status nvarchar(100),
@id nvarchar(100),
@role nvarchar(100)
as begin
Declare @roleid nvarchar(100)
set @roleid =(select id from AspNetRoles where Name=@role)

if(@status='1')
begin

insert into AspNetUserRoles (UserId,RoleId) values (@id,@Roleid) end

else if(@status='0')
begin delete from AspNetUserRoles where UserId= @id and RoleId=@roleid end
else
update AspNetUserRoles set UserId=11111
end


--exec SetRole '1','0c5827c6-86b8-4c0a-bcd1-3af182018efb','Tickets_Approval'
GO
/****** Object:  StoredProcedure [dbo].[sp_add_item]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_add_item]
@item nvarchar(100)
as begin
 IF EXISTS(SELECT * FROM Categories WHERE item = @item)
 begin
   select*from Categories
end
else
begin
insert into Categories (CreatedOn,item) values (GETDATE(),@item) 
end
end


--exec sp_add_item 'testing'



GO
/****** Object:  StoredProcedure [dbo].[sp_dash]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  proc [dbo].[sp_dash]
@user nvarchar(100)
as begin
select
Distinct  w.Total_Week
,T.Total_Today
,M.Total_Month
,id=7
from Expenses
cross apply(select ISNULL(sum(Total),0) [Total_Week] from Expenses where  DATEPART(wk, CreatedOn) =  DATEPART(wk, GETDATE()) and staff=@user)W
cross apply(select ISNULL(sum(Total),0) [Total_Today] from Expenses where CONVERT(date, CreatedOn)=CONVERT(date, GETDATE())and staff=@user)T
cross apply(select ISNULL(sum(Total),0) [Total_Month] from Expenses where MONTH(CreatedOn)=MONTH(GETDATE())and staff=@user)M

end

--exec [sp_dash] 'Muru'
GO
/****** Object:  StoredProcedure [dbo].[sp_dash_2]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_dash_2]
@user nvarchar(100)
as begin
select Top 5
DATENAME(month, GETDATE()) AS [Period]
,Item + ' - '+ ISNULL(Additional_Notes,'-')[items] ,Total
from Expenses 
where MONTH(CreatedOn)=month(GetDate()) and Staff=@user
order by Total desc
---shows the top 5 highest expense in the current month
end
GO
/****** Object:  StoredProcedure [dbo].[sp_dash_3]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_dash_3]
@user nvarchar(100)
as begin
select Top 5
DATENAME(month, GETDATE()) AS [Period]
,DATEPART(WEEK, GETDATE()) - DATEPART(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)) + 1 AS WeekNumber
,Item + ' - '+ ISNULL(Additional_Notes,'-')[items] ,Total
from Expenses 
where DATEPART(week, CreatedOn) = DATEPART(week, GetDate()) and Staff=@user
order by Total desc
---shows the top 5 highest expense in the current week
end
GO
/****** Object:  StoredProcedure [dbo].[sp_dash_4]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_dash_4]
@user nvarchar(100)
as begin
select Top 5
DATENAME(month, DATEADD(month, -1, GETDATE())) AS [Period]
,Item + ' - '+ ISNULL(Additional_Notes,'-')[items] ,Total
from Expenses 
WHERE CreatedOn >= DATEADD(month, DATEDIFF(month, 0, GETDATE()) - 1, 0)
AND CreatedOn < DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0) and (Staff=@user)
order by Total desc
---shows the top 5 highest expense in the current month
end
GO
/****** Object:  StoredProcedure [dbo].[sp_dash_Income]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  proc [dbo].[sp_dash_Income]
@user nvarchar(100)
as begin
select
Distinct  w.Total_Week
,T.Total_Today
,M.Total_Month
,id=7
from Expenses
cross apply(select ISNULL(sum(Amount),0) [Total_Week] from Invoices where  DATEPART(wk, CreatedOn) =  DATEPART(wk, GETDATE()) and User_Acc=@user)W
cross apply(select ISNULL(sum(Amount),0) [Total_Today] from Invoices where CONVERT(date, CreatedOn)=CONVERT(date, GETDATE())and User_Acc=@user)T
cross apply(select ISNULL(sum(Amount),0) [Total_Month] from Invoices where MONTH(CreatedOn)=MONTH(GETDATE())and User_Acc=@user)M

end

--exec [sp_dash] 'Muru'
GO
/****** Object:  StoredProcedure [dbo].[sp_departments]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_departments]
as begin
select
DprtName [Department] from [Departments]
end
GO
/****** Object:  StoredProcedure [dbo].[sp_drop_expense]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_drop_expense]
@id int
as begin
delete from Expenses where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_drop_Income]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_drop_Income]
@id int
as begin
delete from Invoices where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_expenses]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_expenses]
as begin
select
isnull(sum(Amount),0)[Expenses]
,DATENAME(month, GETDATE()) AS [M]
from expenses where DATENAME(month, CreatedOn)=DATENAME(month, GETDATE())
end
GO
/****** Object:  StoredProcedure [dbo].[sp_GetRoles]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_GetRoles]
as begin
select
id [Roleid],
name [Role]
from AspNetRoles
end
GO
/****** Object:  StoredProcedure [dbo].[sp_monthly_statement]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_monthly_statement]
@user nvarchar(100),
@Month_Name nvarchar(100)
as begin
select
id
,[CreatedOn]
      ,[Amount]
      ,[Fuliza]
      ,[Transaction_cost]
      ,[Item]
      ,[Additional_Notes]
      ,[Total]
      ,[Mode]
from Expenses 
where   Staff=@user and ((DATENAME(month,CreatedOn))= @Month_Name)
order by CreatedOn desc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_monthly_statement_Inc]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_monthly_statement_Inc]
@user nvarchar(100),
@Month_Name nvarchar(100)
as begin
select
id
,[CreatedOn]
      ,[Amount]
     
      ,[Additional_Notes]
      ,Source
	  ,User_Acc
      ,[Mode]
from Invoices 
where   User_Acc=@user and ((DATENAME(month,CreatedOn))= @Month_Name)
order by CreatedOn desc
end

select*from Invoices
GO
/****** Object:  StoredProcedure [dbo].[sp_months_list]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  proc [dbo].[sp_months_list]
@user nvarchar(100)
as begin
select

Distinct (DATENAME(month,CreatedOn)) [Month_Name]
,FLOOR(RAND()*(20-1+1)+1) [Id]
from Expenses 
where   Staff=@user
--ORDER BY MONTH(CreatedOn) ASC
end

--exec [sp_months_list] 'tasha'

GO
/****** Object:  StoredProcedure [dbo].[sp_statement_footer]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_statement_footer]
@user nvarchar(100)
,@Month_Name nvarchar(100)
as begin
select
ISNULL(sum(Transaction_cost),0)[Transaction_cost]
,ISNULL(sum(Fuliza),0) [Fuliza]
,ISNULL(sum(Amount),0) [Amount]
,isnull(sum(Total),0) [Total]
from Expenses where ((DATENAME(month,CreatedOn))= @Month_Name) and Staff=@user
end


--exec sp_statement_footer 'muru',4
GO
/****** Object:  StoredProcedure [dbo].[sp_Statements]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Statements]
@username nvarchar(100)
as begin
select

Distinct DATENAME(MONTH,CreatedOn) [Month]
,MONTH(CreatedOn) [Id]
From Expenses where Staff=@username
order by id desc
end

--exec [sp_Statements] 'Muru'
GO
/****** Object:  StoredProcedure [dbo].[sp_Statements_Inc]    Script Date: 5/3/2023 11:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_Statements_Inc]
@username nvarchar(100)
as begin
select

Distinct DATENAME(MONTH,CreatedOn) [Month]
,MONTH(CreatedOn) [Id]
From Invoices where User_Acc=@username
order by id desc
end

--exec [sp_Statements] 'Muru'
GO
USE [master]
GO
ALTER DATABASE [HR2_Cashmate] SET  READ_WRITE 
GO
