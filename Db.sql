USE [master]
GO
/****** Object:  Database [HR]    Script Date: 2/14/2023 10:38:26 AM ******/
CREATE DATABASE [HR]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HR', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\HR.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HR_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\HR_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [HR] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HR].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HR] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HR] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HR] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HR] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HR] SET ARITHABORT OFF 
GO
ALTER DATABASE [HR] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HR] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HR] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HR] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HR] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HR] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HR] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HR] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HR] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HR] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HR] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HR] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HR] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HR] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HR] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HR] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [HR] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HR] SET RECOVERY FULL 
GO
ALTER DATABASE [HR] SET  MULTI_USER 
GO
ALTER DATABASE [HR] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HR] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HR] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HR] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HR] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HR] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'HR', N'ON'
GO
ALTER DATABASE [HR] SET QUERY_STORE = ON
GO
ALTER DATABASE [HR] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [HR]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[Accesses]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[Attendances]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendances](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[Business_location] [nvarchar](max) NULL,
	[latitude] [nvarchar](100) NULL,
	[longitude] [nvarchar](100) NULL,
	[user_latitude] [nvarchar](100) NULL,
	[user_longitude] [nvarchar](100) NULL,
	[Check_status] [nvarchar](100) NULL,
	[User_Account] [nvarchar](100) NULL,
	[Distance] [decimal](19, 2) NULL,
	[device] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Attendances] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bugs]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[Configs]    Script Date: 2/14/2023 10:38:26 AM ******/
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
 CONSTRAINT [PK_dbo.Configs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Data]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[Departments]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[Employees]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[Expenses]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expenses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[Amount] [decimal](19, 2) NULL,
	[Status] [nvarchar](max) NULL,
	[Additional_Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Expenses] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facilities]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facilities](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Facility] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[Facility_Type] [nvarchar](max) NULL,
	[Owner] [nvarchar](100) NULL,
	[Phone] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Facilities] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[Invoices]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[Due_Date] [datetime] NULL,
	[Client_Name] [nvarchar](max) NULL,
	[Amount] [decimal](19, 2) NULL,
	[Status] [nvarchar](max) NULL,
	[Additional_Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Invoices] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[leaves]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[leaves](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[Employee] [nvarchar](max) NULL,
	[Status] [nvarchar](100) NULL,
	[Message] [nvarchar](1000) NULL,
	[HR_Email] [nvarchar](1000) NULL,
	[Emp_Mail] [nvarchar](1000) NULL,
	[From_Date] [datetime] NULL,
	[To_Date] [datetime] NULL,
	[Type] [nvarchar](200) NULL,
	[Department] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.leaves] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Office]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[Reports]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[Staffs]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  Table [dbo].[Users_Facility]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users_Facility](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Facility] [nvarchar](max) NULL,
	[Username] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Users_Facility] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 2/14/2023 10:38:26 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 2/14/2023 10:38:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 2/14/2023 10:38:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 2/14/2023 10:38:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 2/14/2023 10:38:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Acc_Profile]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  StoredProcedure [dbo].[AccessRolesSp]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  StoredProcedure [dbo].[AddPhone]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Checkin/Checkout]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Delete_File]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Delete_User]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Em_Agency]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Em_Agency]
@employee nvarchar(100)
as begin
select ID=7,ISNULL(Facility,'None') [Agency]
from Users_Facility
where username=@employee
end
GO
/****** Object:  StoredProcedure [dbo].[GetData]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  StoredProcedure [dbo].[GetEmp]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  StoredProcedure [dbo].[GetEmpUsername]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  StoredProcedure [dbo].[GetShifts]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[GetShifts]
as begin
select
s.id [EventId]
,s.Createdon + ''+(FORMAT(s.Start_time,'hh:mm tt')) [StartDate]
,s.Facility [Subject]
,f.Facility_Type
,s.location
,Created_by
,(FORMAT(s.Start_time,'hh:mm tt'))+ ' --'+(FORMAT(s.End_time,'hh:mm tt'))   [Shift]
,Status
,certification
,IsFullDay
,Employee
,status [Shift Status]
,(case when Status ='Open' then '#2FABED'  when status='Taken' then '#90EE90'  when  status ='Cancelled' then '#ff5b5b' else '#8e8e8e' end ) Color
,Staffing_Agency
from shifts s inner join Facilities f on f.Facility=s.Facility
end

--exec [GetShifts]


GO
/****** Object:  StoredProcedure [dbo].[PushEmp]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Role_Set]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  StoredProcedure [dbo].[RoleGrant]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RoleGrant]
As begin
select
u.Id [UserId]
,R.Id [RoleId]
From Accesses A inner join AspNetUsers U 
              On A.Username=U.UserName
			  inner join AspNetRoles R
			  On A.Role=R.Name where A.Status='1' end


GO
/****** Object:  StoredProcedure [dbo].[RolesAdd]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[RolesAdd]
As
Begin
DELETE FROM AspNetUserRoles
WHERE UserId = (select u.Id [UserId]
              From Accesses A inner join AspNetUsers U 
              On A.Username=U.UserName  where A.Status='0')
			  and
      RoleId = (select R.Id [RoleId]
              From Accesses A inner join AspNetUsers U 
              On A.Username=U.UserName  inner join AspNetRoles R On A.Role=R.Name where A.Status='0')
update Employees
set Current_Access='Pending' where Username=(select Username from Accesses where Status=0)



Insert into  AspNetUserRoles exec RoleGrant
update Employees
set Current_Access='Activated' where Username=(select Username from Accesses where Status=1)
delete from Accesses

end
GO
/****** Object:  StoredProcedure [dbo].[rolesL]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[rolesL]
as begin
select
A.Name [Role]
,A.Id [RoleId]
From  AspNetRoles A  where  Name ='Can_Take_Shift'
end

GO
/****** Object:  StoredProcedure [dbo].[SetRole]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SetRole]
@username nvarchar(100)
,@role nvarchar(100)
as begin
Declare @user nvarchar(100)
set @user =(select id from AspNetUsers where username=@username)
Declare @Roleid nvarchar(100)
set @Roleid =(select id from AspNetRoles where name=@Role)
SET IDENTITY_INSERT AspNetUserRoles ON
insert into AspNetUserRoles (UserId,RoleId) values (@user,@Roleid)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_departments]    Script Date: 2/14/2023 10:38:26 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Sp_Employee]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_Employee]
as begin
select
r.id
,r.username [Employee]
,r.email
,COALESCE(f.Facility,'Unassigned') [Agency]
,r.EmailConfirmed
,STRING_AGG (CONVERT(NVARCHAR(max),v.name), ' + ') AS Role
from AspNetUserRoles a inner join AspNetRoles v on a.roleid=v.id
                       inner join AspNetUsers r on r.id=a.userid
					   left join Users_Facility F on F.Username=r.UserName
					   where v.name in ('Employee','Can_Take_Shift')
					   group by r.username,r.EmailConfirmed,r.email,r.id,f.Facility
end

--exec Sp_Employee
GO
/****** Object:  StoredProcedure [dbo].[sp_expenses]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_expenses]
as begin
select
isnull(sum(Amount),0)[Expenses]
,DATENAME(month, GETDATE()) AS [M]
from expenses where status='New' and DATENAME(month, CreatedOn)=DATENAME(month, GETDATE())
end
GO
/****** Object:  StoredProcedure [dbo].[sp_expenses_report]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_expenses_report]
 @startDate varchar(50),
 @endDate varchar(50),
 @Status nvarchar(100)
as begin
  IF(isdate(@startDate)=0) SET @startDate=null;
  IF(isdate(@endDate)=0)	SET @endDate=null;
  IF(@Status ='' or @Status='All')set @Status=null;
select
CreatedOn
,Amount
,Additional_Notes
,Status
from Expenses where ((@Status is null or @Status=Status) and Createdon between Convert(DateTime, DATEDIFF(DAY, 0, @startDate))
and Convert(DateTime, DATEDIFF(DAY, -1, @endDate)))
end


--exec [sp_expenses_report] '2022-1-1','2022-12-31',''
GO
/****** Object:  StoredProcedure [dbo].[sp_getDrpt]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getDrpt] 
@user nvarchar(200)
as begin
select Department from Employees
where Username=@user
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getHodEmail]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getHodEmail] 
@user nvarchar(200)
as begin
declare @Depart as nvarchar(100)
set @Depart=(select  Department from Employees  where UserName=@user)
declare @hodmail as nvarchar(100)
set @hodmail=(select Email from AspNetUsers where UserName=(select manager from Departments where DprtName=@Depart))
select @hodmail as [HR_Email]
end


--exec sp_getHodEmail 'kanaan'
GO
/****** Object:  StoredProcedure [dbo].[sp_invoices]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_invoices]
as begin
with A as
(select
sum(Amount)[New]
,DATENAME(month, GETDATE()) AS [M]
from Invoices where Status='New' and DATENAME(month, CreatedOn)=DATENAME(month, GETDATE())),

B as 
(select
sum(Amount)[Paid]
from Invoices where Status='Paid'and DATENAME(month, CreatedOn)=DATENAME(month, GETDATE()))
select
CONVERT(DECIMAL(7,2),isnull(A.New,0) ) [New]
,CONVERT(DECIMAL(7,2),isnull(b.Paid,0) ) [Paid]
,A.M
from A,B
end
GO
/****** Object:  StoredProcedure [dbo].[sp_invoices_report]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_invoices_report]
 @startDate varchar(50),
 @endDate varchar(50),
 @Status nvarchar(100)
as begin
  IF(isdate(@startDate)=0) SET @startDate=null;
  IF(isdate(@endDate)=0)	SET @endDate=null;
  IF(@Status ='' or @Status='All')set @Status=null;
select
CreatedOn
,Due_Date
,Client_Name
,Amount
,Status
from Invoices where ((@Status is null or @Status=Status) and Createdon between Convert(DateTime, DATEDIFF(DAY, 0, @startDate))
and Convert(DateTime, DATEDIFF(DAY, -1, @endDate)))
end
GO
/****** Object:  StoredProcedure [dbo].[sp_markcancelled_expense]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_markcancelled_expense]
@Id int
as begin
update Expenses set Status='Cancelled'
where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_markcancelled_Invoice]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_markcancelled_Invoice]
@Id int
as begin
update Invoices set Status='Cancelled'
where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_markleave_approved]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_markleave_approved]
@Id int
as begin
update leaves set Status='Approved'
where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_markleave_denied]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_markleave_denied]
@Id int
as begin
update leaves set Status='Denied'
where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_markpaid_Invoice]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_markpaid_Invoice]
@Id int
as begin
update Invoices set Status='Paid'
where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_timesheet_report]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_timesheet_report]
 @startDate varchar(50),
 @endDate varchar(50),
 @Status nvarchar(100)
as begin
  IF(isdate(@startDate)=0) SET @startDate=null;
  IF(isdate(@endDate)=0)	SET @endDate=null;
  IF(@Status ='' or @Status='All')set @Status=null;
select
CreatedOn
,User_Account
,Check_status
,'https://www.google.com/maps/search/' + user_latitude +' '+user_longitude [Cordinates]
,Distance
from Attendances   where ((@Status is null or @Status=Check_status) and Createdon between Convert(DateTime, DATEDIFF(DAY, 0, @startDate))
and Convert(DateTime, DATEDIFF(DAY, -1, @endDate)))
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_u]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_u]
as begin
with A as
(select
count(a.id) [Employees]
from aspnetusers a inner join aspnetuserroles r on r.userid=a.id
                    inner join aspnetroles x on x.id=r.roleid
					where x.Name='Employee'),
B as (select
count(a.id) [Clients]
from aspnetusers a inner join aspnetuserroles r on r.userid=a.id
                    inner join aspnetroles x on x.id=r.roleid
					where x.Name='Staffing_Agency'
),
C as (select
count(a.id) [Admin]
from aspnetusers a inner join aspnetuserroles r on r.userid=a.id
                    inner join aspnetroles x on x.id=r.roleid
					where x.Name='Admin' and not a.username ='muru'
)
 
         select
		 A.Employees
		 ,B.Clients
		 ,C.Admin
		 ,A.Employees +B.Clients + C.Admin [Total Users]
		 From A,B,C
		 end
GO
/****** Object:  StoredProcedure [dbo].[Tasks]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Tasks]
as begin
select
distinct s.Employee,
(Count(s.id) over (partition by s.Employee))[Total Assigned Shifts]
from shifts s where  
 (not s.status  ='Open') and (not s.Employee='Unassigned')
end
--exec Tasks ''

GO
/****** Object:  StoredProcedure [dbo].[userlist]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[userlist]
as begin
select
id,
username
,email
,(case when EmailConfirmed=1 then 'yes' else 'No' end) [Emai Confirmed]
from aspnetusers where not  username='muru'
end
GO
/****** Object:  StoredProcedure [dbo].[Users]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Users]
As begin 
select
distinct Au.UserName
,Au.Email
From AspNetUserRoles AR inner join AspNetUsers Au on Ar.UserId=Au.Id
                        inner join AspNetRoles A on A.Id=AR.RoleId
						where A.Name='Employee' 
						
						end

						--exec AccessRolesSp
GO
/****** Object:  StoredProcedure [dbo].[Users22]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Users22]
As begin 
select
 s.UserName
From AspNetUsers s 
inner join AspNetUserRoles r  on s.id=r.userid
inner join AspNetRoles x on x.Id=r.RoleId
where s.username not in (select username from [dbo].[Users_Facility]) 
and x.name='Employee'
						
						end

						--exec Users22
GO
/****** Object:  StoredProcedure [dbo].[Users23]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  proc [dbo].[Users23]
As begin 
select
 s.UserName
From AspNetUsers s 
inner join AspNetUserRoles r  on s.id=r.userid
inner join AspNetRoles x on x.Id=r.RoleId
where s.username not in (select username from [dbo].[Users_Facility]) 
and x.name='Staffing_Agency'
						
						end

						--exec Users22
GO
/****** Object:  StoredProcedure [dbo].[Users30]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Users30]
As begin 
select
 s.UserName
From AspNetUsers s 
inner join AspNetUserRoles r  on s.id=r.userid
inner join AspNetRoles x on x.Id=r.RoleId
where s.username not in (select username from [dbo].[Users_Facility]) 
and x.name='Facility'
						
						end

						--exec Users22
GO
/****** Object:  StoredProcedure [dbo].[Users31]    Script Date: 2/14/2023 10:38:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Users31]
@user nvarchar(100)
As begin 
Declare @Agency nvarchar(100)
set @Agency=(select Facility from Users_Facility where username=@user)
select
distinct Au.UserName
,Au.Email
From AspNetUserRoles AR inner join AspNetUsers Au on Ar.UserId=Au.Id
                        inner join AspNetRoles A on A.Id=AR.RoleId
						inner join Users_Facility u on u.Username=Au.UserName
						where A.Name='Employee' and u.Facility=@Agency
						
						end

						--exec AccessRolesSp
GO
USE [master]
GO
ALTER DATABASE [HR] SET  READ_WRITE 
GO
