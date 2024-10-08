USE [master]
GO
/****** Object:  Database [Planning]    Script Date: 3/1/2024 7:43:10 PM ******/
CREATE DATABASE [Planning]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Planning', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Planning.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Planning_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Planning_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Planning] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Planning].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Planning] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Planning] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Planning] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Planning] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Planning] SET ARITHABORT OFF 
GO
ALTER DATABASE [Planning] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Planning] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Planning] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Planning] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Planning] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Planning] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Planning] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Planning] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Planning] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Planning] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Planning] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Planning] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Planning] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Planning] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Planning] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Planning] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Planning] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Planning] SET RECOVERY FULL 
GO
ALTER DATABASE [Planning] SET  MULTI_USER 
GO
ALTER DATABASE [Planning] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Planning] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Planning] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Planning] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Planning] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Planning] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Planning', N'ON'
GO
ALTER DATABASE [Planning] SET QUERY_STORE = ON
GO
ALTER DATABASE [Planning] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Planning]
GO
/****** Object:  User [NT AUTHORITY\SYSTEM]    Script Date: 3/1/2024 7:43:11 PM ******/
CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/1/2024 7:43:11 PM ******/
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
/****** Object:  Table [dbo].[Accesses]    Script Date: 3/1/2024 7:43:11 PM ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 3/1/2024 7:43:11 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 3/1/2024 7:43:11 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 3/1/2024 7:43:11 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 3/1/2024 7:43:11 PM ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 3/1/2024 7:43:11 PM ******/
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
/****** Object:  Table [dbo].[Clients]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Country] [nvarchar](max) NULL,
	[Email_Address] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Collections]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collections](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Phase_Id] [int] NOT NULL,
	[Project_Id] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Charge] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_dbo.Collections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Configs]    Script Date: 3/1/2024 7:43:11 PM ******/
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
	[port] [nvarchar](100) NULL,
	[RunTime] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Configs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Days_leave]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Days_leave](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Leave_Id] [int] NOT NULL,
	[Approved] [bit] NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Time] [int] NOT NULL,
	[From_Date] [datetime] NULL,
	[To_Date] [datetime] NULL,
	[days] [decimal](18, 1) NULL,
	[Return_Date] [datetime] NULL,
 CONSTRAINT [PK_dbo.Days_leave] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DprtName] [nvarchar](max) NOT NULL,
	[Contact] [nvarchar](max) NOT NULL,
	[Email_Address] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Direct_Activities]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Direct_Activities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Hours] [int] NOT NULL,
	[Day_Date] [datetime] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Project_Name] [nvarchar](max) NULL,
	[User] [nvarchar](max) NULL,
	[Comments] [nvarchar](max) NULL,
	[Charge] [decimal](18, 2) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Approved] [bit] NULL,
	[WeekNo] [int] NULL,
	[Category] [nvarchar](200) NULL,
 CONSTRAINT [PK_dbo.Direct_Activities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Directs]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Directs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[User] [nvarchar](max) NULL,
	[Step] [int] NULL,
 CONSTRAINT [PK_dbo.Directs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Username] [nvarchar](max) NULL,
	[Fullname] [nvarchar](max) NULL,
	[Contact] [nvarchar](max) NULL,
	[DprtName] [nvarchar](max) NULL,
	[Designation] [nvarchar](max) NULL,
	[Userid] [nvarchar](max) NULL,
	[Rate] [decimal](19, 2) NULL,
	[Email] [nvarchar](100) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HODs]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HODs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Department_Id] [int] NOT NULL,
	[DprtName] [nvarchar](max) NOT NULL,
	[Staff] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.HODs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Indirect_Activities]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Indirect_Activities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Hours] [int] NOT NULL,
	[Day_Date] [datetime] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[User] [nvarchar](max) NULL,
	[Comments] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Approved] [bit] NULL,
	[WeekNo] [int] NULL,
 CONSTRAINT [PK_dbo.Indirect_Activities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InDirects]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InDirects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[User] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.InDirects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[leaves]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[leaves](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[Employee] [nvarchar](max) NULL,
	[Message] [nvarchar](1000) NULL,
	[HR_Email] [nvarchar](1000) NULL,
	[Emp_Mail] [nvarchar](1000) NULL,
	[Department] [nvarchar](100) NULL,
	[Approver_Remarks] [nvarchar](max) NULL,
	[phone] [nvarchar](100) NULL,
	[Designation] [nvarchar](100) NULL,
	[Status] [int] NULL,
	[Days] [decimal](18, 1) NULL,
	[From_Date] [datetime] NULL,
	[To_Date] [datetime] NULL,
	[Return_Date] [datetime] NULL,
	[Type] [nvarchar](200) NULL,
	[Utilized] [bit] NULL,
	[Leave_Hours] [decimal](18, 1) NULL,
 CONSTRAINT [PK_dbo.leaves] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[leaves_Days_track]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[leaves_Days_track](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Requested_leaves] [decimal](18, 1) NULL,
	[Remaining_leaves] [decimal](18, 1) NULL,
	[Username] [nvarchar](100) NULL,
	[Type] [nvarchar](100) NULL,
	[Total_leaves_per_year] [decimal](18, 1) NULL,
 CONSTRAINT [PK_dbo.leaves_Days_track] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[leaves_logs]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[leaves_logs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[Approver] [nvarchar](100) NULL,
	[Approver_status] [nvarchar](100) NULL,
	[Approver_remarks] [nvarchar](100) NULL,
	[leave_id] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[leaves_Types]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[leaves_Types](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Type] [nvarchar](max) NULL,
	[Days] [decimal](18, 1) NULL,
	[Steps] [int] NULL,
 CONSTRAINT [PK_dbo.leaves_Types] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Createdon] [datetime] NULL,
	[Last_sent] [datetime] NULL,
	[Active] [bit] NULL,
	[Subject] [nvarchar](500) NULL,
	[Recepients] [nvarchar](500) NULL,
	[GroupName] [nvarchar](500) NULL,
	[Message] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Notifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OutgoingEmails]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OutgoingEmails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Recipient] [nvarchar](max) NULL,
	[Subject] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[Status] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[Files] [nvarchar](max) NULL,
	[Trials] [int] NULL,
	[Response] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.OutgoingEmails] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Outgoingsms]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Outgoingsms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MessageText] [nvarchar](max) NOT NULL,
	[IsSent] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[RecipientNumber] [nvarchar](max) NULL,
	[Trials] [int] NULL,
	[Response] [nvarchar](max) NULL,
	[Code] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pcategories]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pcategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Pcategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phases]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phases](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Budget] [decimal](18, 2) NOT NULL,
	[Project_id] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Start_Date] [datetime] NULL,
	[End_Date] [datetime] NULL,
	[Step] [int] NULL,
	[Collected] [decimal](18, 2) NULL,
 CONSTRAINT [PK_dbo.Phases] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Project_Name] [nvarchar](max) NULL,
	[location] [nvarchar](max) NULL,
	[User] [nvarchar](max) NULL,
	[Budget] [decimal](18, 2) NULL,
	[Category] [nvarchar](100) NULL,
	[Fee_Budget] [decimal](18, 2) NULL,
	[Client] [nvarchar](100) NULL,
	[Status] [nvarchar](100) NULL,
	[Project_No] [int] NULL,
	[Project_RegNo] [int] NULL,
 CONSTRAINT [PK_dbo.Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reopens]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reopens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Timesheet_id] [int] NOT NULL,
	[Reason] [nvarchar](max) NULL,
	[Department] [nvarchar](max) NULL,
	[Staff] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_dbo.Reopens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportAccesses]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportAccesses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GrantedOn] [datetime] NOT NULL,
	[Report] [nvarchar](max) NULL,
	[GroupId] [int] NOT NULL,
	[Staff] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ReportAccesses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportGroups]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportGroups](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[Menu_order] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ReportGroups] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reports]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Group_id] [int] NOT NULL,
	[Report_Name] [nvarchar](max) NULL,
	[Report_Path] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Reports] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sheet_comments]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sheet_comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Timesheeet_Id] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Approver] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Sheet_comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sms_configs]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sms_configs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[APIkey] [nvarchar](max) NULL,
	[partnerID] [nvarchar](max) NULL,
	[shortcode] [nvarchar](max) NULL,
	[APIUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Sms_configs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teams]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teams](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Project_Name] [nvarchar](max) NULL,
	[Username] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[Project_id] [int] NULL,
	[Hours] [decimal](18, 1) NULL,
	[Name] [nvarchar](200) NULL,
	[Step] [int] NULL,
 CONSTRAINT [PK_dbo.Teams] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Templates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Templates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Time_cat]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Time_cat](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Time_cat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Timesheets]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timesheets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Owner] [nvarchar](100) NULL,
	[Status] [int] NULL,
	[Department] [nvarchar](500) NULL,
	[From_Date] [datetime] NULL,
	[End_Date] [datetime] NULL,
	[Direct_Hours] [decimal](18, 1) NULL,
	[InDirect_Hours] [decimal](18, 1) NULL,
	[Locked] [bit] NULL,
	[Tt] [decimal](18, 1) NULL,
	[leave] [decimal](18, 1) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 3/1/2024 7:43:11 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 3/1/2024 7:43:11 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 3/1/2024 7:43:11 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 3/1/2024 7:43:11 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 3/1/2024 7:43:11 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 3/1/2024 7:43:11 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Outgoingsms] ADD  DEFAULT ((0)) FOR [IsSent]
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
/****** Object:  StoredProcedure [dbo].[Alltimesheets]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Alltimesheets]
    @UserName NVARCHAR(255) = NULL,
    @StartDate VARCHAR(50) = NULL,
    @EndDate VARCHAR(50) = NULL
AS
BEGIN
    SET DATEFIRST 7; -- Set the first day of the week to Sunday

    IF (@UserName = '' OR @UserName = 'All') SET @UserName = NULL;
    IF (ISDATE(@startDate) = 0) SET @startDate = NULL;
    IF (ISDATE(@endDate) = 0) SET @endDate = NULL;

    WITH IndirectActivitySummary AS (
        SELECT
            WeekNo AS Week_Number,
            MIN(IIF(DATENAME(WEEKDAY, Day_Date) = 'Sunday', Day_Date, NULL)) AS Sunday,
            MAX(IIF(DATENAME(WEEKDAY, Day_Date) = 'Saturday', Day_Date, NULL)) AS Saturday,
            DATENAME(MONTH, MIN(Day_Date)) AS Month_Name,
            [User],
            SUM(COALESCE(Hours, 0)) AS [Indirect Hours]
        FROM Indirect_Activities
        WHERE ((@UserName IS NULL OR [User] = @UserName) AND (@StartDate IS NULL OR Day_Date >= @StartDate) AND (@EndDate IS NULL OR Day_Date <= @EndDate))
        GROUP BY WeekNo, [User]
    ),
    DirectActivitySummary AS (
        SELECT
            WeekNo AS Week_Number,
            MIN(IIF(DATENAME(WEEKDAY, Day_Date) = 'Sunday', Day_Date, NULL)) AS Sunday,
            MAX(IIF(DATENAME(WEEKDAY, Day_Date) = 'Saturday', Day_Date, NULL)) AS Saturday,
            DATENAME(MONTH, MIN(Day_Date)) AS Month_Name,
            [User],
            SUM(COALESCE(Hours, 0)) AS [Direct Hours]
        FROM Direct_Activities
        WHERE ((@UserName IS NULL OR [User] = @UserName) AND (@StartDate IS NULL OR Day_Date >= @StartDate) AND (@EndDate IS NULL OR Day_Date <= @EndDate))
        GROUP BY WeekNo, [User]
    )
    SELECT
        COALESCE(IA.Week_Number, DA.Week_Number) AS Week_Number,
        COALESCE(IA.Month_Name, DA.Month_Name) AS Month_Name,
        COALESCE(IA.[User], DA.[User]) AS [User],
        COALESCE(IA.[Indirect Hours], 0) AS [Indirect Hours],
        COALESCE(DA.[Direct Hours], 0) AS [Direct Hours],
        COALESCE(IA.[Indirect Hours], 0) + COALESCE(DA.[Direct Hours], 0) AS [Total Hours],
        CASE
            WHEN COALESCE(IA.Week_Number, DA.Week_Number) IS NOT NULL AND DATEPART(WEEK, GETDATE()) = COALESCE(IA.Week_Number, DA.Week_Number) THEN 'In Progress'
            WHEN COALESCE(IA.Week_Number, DA.Week_Number) > DATEPART(WEEK, GETDATE()) THEN 'Ahead'
            ELSE 'Completed'
        END AS Status,
        COALESCE(IA.Sunday, DA.Sunday) AS Sunday,
        COALESCE(IA.Saturday, DA.Saturday) AS Saturday
    FROM
        IndirectActivitySummary IA
    FULL JOIN
        DirectActivitySummary DA ON IA.Week_Number = DA.Week_Number AND IA.[User] = DA.[User]
    ORDER BY
        Week_Number DESC;
END;
--exec Alltimesheets
GO
/****** Object:  StoredProcedure [dbo].[Assigned_user_roles]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Assigned_user_roles]
@Username nvarchar(100)
as begin
select
STRING_AGG(an.Name, ', ') AS [Role Name]
from AspNetUsers a
inner join AspNetUserRoles ar on a.id=ar.UserId
inner join AspNetRoles an on an.Id=ar.RoleId
where A.Id =@Username
end
GO
/****** Object:  StoredProcedure [dbo].[Employeeslist]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Employeeslist]
as begin
select 'All' as Username
union
select Username from Employees
end
GO
/****** Object:  StoredProcedure [dbo].[projectheader]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[projectheader]
@Projectid int
as begin
select
Project_Name
,p.Id
,Budget [FEE PLANNED]
,LL.[FEE LOGGED]
,Budget -ll.[FEE LOGGED] [Balance]
from 
Projects P 
cross apply (select sum(Charge)[FEE LOGGED] from Direct_Activities D where D.Project_Name=P.Project_Name and Approved=1) LL
where P.Id =@Projectid
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Calendar_Activities]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Calendar_Activities]
    @Username NVARCHAR(100)
AS 
BEGIN
   DECLARE @StartDate DATE = DATEADD(DAY, 2 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE));
DECLARE @EndDate DATE = DATEADD(DAY, 8 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE));

WITH DateRange AS (
    SELECT @StartDate AS DateValue
    UNION ALL
    SELECT DATEADD(DAY, 1, DateValue)
    FROM DateRange
    WHERE DateValue < @EndDate  
),


    B AS (
        SELECT
            dr.DateValue,
            ISNULL(SUM(a.hours), 0) AS [Direct Hours]
        FROM
            DateRange dr
        LEFT JOIN    
            Direct_Activities a ON CAST(a.Day_Date AS DATE) = dr.DateValue AND a.[User] = @Username
        GROUP BY
            dr.DateValue
    ),

    C AS (
        SELECT
            dr.DateValue,
            ISNULL(SUM(x.hours), 0) AS [InDirect Hours]
        FROM
            DateRange dr
        LEFT JOIN 
            Indirect_Activities x ON CAST(x.Day_Date AS DATE) = dr.DateValue AND x.[User] = @Username
        GROUP BY
            dr.DateValue
    )

    SELECT 
       UPPER(FORMAT(dr.DateValue, 'ddd')) AS WeekdayName,
		dr.DateValue,
		DATEPART(WEEK, dr.DateValue) AS WeekNumber,
        ISNULL(B.[Direct Hours], 0) AS [Direct Hours],
        ISNULL(C.[InDirect Hours], 0) AS [InDirect Hours],
		[Direct Hours] + [InDirect Hours] AS [Total Hours]
    FROM 
        DateRange dr
    LEFT JOIN 
        B ON dr.DateValue = B.DateValue
    LEFT JOIN 
        C ON dr.DateValue = C.DateValue
    ORDER BY 
        dr.DateValue;
END





		--exec  sp_Calendar_Activities 'Muru'

		
		--select *from Direct_Activities
		--select *from Indirect_Activities
GO
/****** Object:  StoredProcedure [dbo].[Sp_Employee_Timesheet]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_Employee_Timesheet]
    @TimeFilter VARCHAR(20),
    @Staff VARCHAR(20)
AS
BEGIN
    IF (@TimeFilter = ' ' OR @TimeFilter = 'All') SET @TimeFilter = NULL;
    IF (@Staff = ' ' OR @Staff = 'All') SET @Staff = NULL;

    SELECT
        COALESCE(D.WeekNo, I.WeekNo) AS WeekNo,
        COALESCE(D.[User], I.[User]) AS [Staff],
        DATENAME(MONTH, COALESCE(D.Day_Date, I.Day_Date)) AS MonthName,
        ISNULL(SUM(D.Hours), 0) AS [Billable Hours],
        ISNULL(SUM(I.Hours), 0) AS [Non Billable Hours],
        ISNULL(SUM(D.Hours), 0) + ISNULL(SUM(I.Hours), 0) AS [Total Hours]
    FROM
        Direct_Activities D
    FULL OUTER JOIN
        Indirect_Activities I ON D.[User] = I.[User] AND D.WeekNo = I.WeekNo
    WHERE
        (D.WeekNo =
            CASE
                WHEN @TimeFilter = 'ThisWeek' THEN DATEPART(WEEK, GETDATE())
				WHEN @TimeFilter = 'NextWeek' THEN DATEPART(WEEK, DATEADD(WEEK, 1, GETDATE()))
                WHEN @TimeFilter = 'LastWeek' THEN DATEPART(WEEK, DATEADD(WEEK, -1, GETDATE()))
                WHEN @TimeFilter = 'ThisMonth' THEN DATEPART(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0))
                WHEN @TimeFilter = 'LastMonth' THEN DATEPART(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 0))
                WHEN @TimeFilter = '1stQuarter' THEN DATEPART(WEEK, DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()), 0))
                WHEN @TimeFilter = '2ndQuarter' THEN DATEPART(WEEK, DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 1, 0))
                WHEN @TimeFilter = '3rdQuarter' THEN DATEPART(WEEK, DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 2, 0))
                WHEN @TimeFilter = '4thQuarter' THEN DATEPART(WEEK, DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 3, 0))
                ELSE D.WeekNo -- No filter
            END
         OR I.WeekNo =
            CASE
                WHEN @TimeFilter = 'ThisWeek' THEN DATEPART(WEEK, GETDATE())
				WHEN @TimeFilter = 'NextWeek' THEN DATEPART(WEEK, DATEADD(WEEK, 1, GETDATE()))
                WHEN @TimeFilter = 'LastWeek' THEN DATEPART(WEEK, DATEADD(WEEK, -1, GETDATE()))
                WHEN @TimeFilter = 'ThisMonth' THEN DATEPART(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0))
                WHEN @TimeFilter = 'LastMonth' THEN DATEPART(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 0))
                WHEN @TimeFilter = '1stQuarter' THEN DATEPART(WEEK, DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()), 0))
                WHEN @TimeFilter = '2ndQuarter' THEN DATEPART(WEEK, DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 1, 0))
                WHEN @TimeFilter = '3rdQuarter' THEN DATEPART(WEEK, DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 2, 0))
                WHEN @TimeFilter = '4thQuarter' THEN DATEPART(WEEK, DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 3, 0))
                ELSE I.WeekNo -- No filter
            END
         OR @TimeFilter IS NULL)
        AND ((@Staff IS NULL) OR (COALESCE(D.[User], I.[User]) = @Staff))
    GROUP BY
        COALESCE(D.WeekNo, I.WeekNo),
        COALESCE(D.[User], I.[User]),
        DATENAME(MONTH, COALESCE(D.Day_Date, I.Day_Date))
    ORDER BY
        COALESCE(D.WeekNo, I.WeekNo),
        COALESCE(D.[User], I.[User]),
        DATENAME(MONTH, COALESCE(D.Day_Date, I.Day_Date));
END



--exec [Sp_Employee_Timesheet] 'ThisWeek',''
GO
/****** Object:  StoredProcedure [dbo].[Sp_Employee_Timesheet_child]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_Employee_Timesheet_child]
     @TimeFilter VARCHAR(20),
    @Staff VARCHAR(20)
AS
BEGIN
    IF (@TimeFilter = ' ' OR @TimeFilter = 'All') SET @TimeFilter = NULL;
    IF (@Staff = ' ' OR @Staff = 'All') SET @Staff = NULL;

    SELECT
        COALESCE(D.WeekNo, I.WeekNo) AS WeekNo,
         DATENAME(MONTH, COALESCE(D.Day_Date, I.Day_Date)) AS MonthName,
        ISNULL(SUM(D.Hours), 0) AS [Billable Hours],
        ISNULL(SUM(I.Hours), 0) AS [Non Billable Hours],
        ISNULL(SUM(D.Hours), 0) + ISNULL(SUM(I.Hours), 0) AS [Total Hours]
    FROM
        Direct_Activities D
    FULL OUTER JOIN
        Indirect_Activities I ON D.[User] = I.[User] AND MONTH(D.Day_Date) = MONTH(I.Day_Date)
    WHERE
        COALESCE(D.Day_Date, I.Day_Date) >= CASE
                                               WHEN @TimeFilter = 'ThisWeek' THEN DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0)
                                               WHEN @TimeFilter = 'LastWeek' THEN DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) - 1, 0)
                                               WHEN @TimeFilter = 'ThisMonth' THEN DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)
                                               WHEN @TimeFilter = 'LastMonth' THEN DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 0)
                                               WHEN @TimeFilter = '1stQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()), 0)
                                               WHEN @TimeFilter = '2ndQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 1, 0)
                                               WHEN @TimeFilter = '3rdQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 2, 0)
                                               WHEN @TimeFilter = '4thQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 3, 0)
                                               ELSE COALESCE(D.Day_Date, I.Day_Date) -- No filter
                                           END
        AND ((@Staff IS NULL) OR (COALESCE(D.[User], I.[User]) = @Staff))
    GROUP BY
        COALESCE(D.WeekNo, I.WeekNo),
       
        DATENAME(MONTH, COALESCE(D.Day_Date, I.Day_Date))
    ORDER BY
        COALESCE(D.WeekNo, I.WeekNo),
        
        DATENAME(MONTH, COALESCE(D.Day_Date, I.Day_Date));
END




--exec [Sp_Employee_Timesheet_child] '',''
GO
/****** Object:  StoredProcedure [dbo].[sp_Project_details]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Project_details]
@Projectid int
as begin
select
P.Id,
P.Project_Name
,PH.Phase,PH.Budget
,ISNULL(logged.logged,0) [logged]
,PH.Budget - ISNULL(logged.logged,0) [Balance]
,Ph.Start_Date,Ph.End_Date
from Projects P
cross apply (select step, Name [Phase],Budget ,Project_id,Start_Date,End_Date  from Phases )PH
cross apply (select SUM(ISNULL(A.Charge, 0)) AS  [logged] from Direct_Activities A where A.Project_Name =P.Project_Name and  A.Name = PH.Phase)logged
where p.Id=@Projectid and PH.Project_id=@Projectid
order by Ph.Step 
end


--exec sp_Project_details 408
GO
/****** Object:  StoredProcedure [dbo].[sp_Projectlist]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Projectlist]
    @TimeFilter VARCHAR(20) 
	,@Status VARCHAR(50)
	,@Category VARCHAR(50),@Project VARCHAR(50),
	@StartDate DATE = NULL,
    @EndDate DATE = NULL
AS
BEGIN
IF(@TimeFilter =' ' or @TimeFilter='All') set @TimeFilter=null;
IF(@Status =' ' or @Status='All') set @Status=null;
IF(@Category =' ' or @Category='All') set @Category=null;
IF(@Project =' ' or @Project='All') set @Project=null;
SELECT
            Project_Name,p.Id
			,Status,isnull(Budget,0)[FEE PLANNED],isnull(D.[FEE LOGGED],0)[FEE LOGGED],isnull(Budget,0) -isnull(D.[FEE LOGGED],0) [PROFIT]
           FROM Projects P 
		cross apply (Select sum(isnull(Charge,0))[FEE LOGGED] from Direct_Activities D where P.Project_Name =D.Project_Name) D
		WHERE
		CreatedOn >= CASE 
                                           WHEN @TimeFilter = 'ThisWeek' THEN DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0)
                                           WHEN @TimeFilter = 'LastWeek' THEN DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) - 1, 0)
                                           WHEN @TimeFilter = 'ThisMonth' THEN DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)
                                           WHEN @TimeFilter = 'LastMonth' THEN DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 0)
                                           WHEN @TimeFilter = '1stQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()), 0)
                                           WHEN @TimeFilter = '2ndQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 1, 0)
                                           WHEN @TimeFilter = '3rdQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 2, 0)
                                           WHEN @TimeFilter = '4thQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 3, 0)
                                           ELSE CreatedOn -- No filter
                                       END
									   AND (
										  (@Status IS NULL) OR (Status = @Status))
									   AND (
										  (@Category IS NULL) OR (Category = @Category))
									   AND (
										  (@Project IS NULL) OR (Project_Name = @Project))
									   
end



--exec sp_Projectlist '','','','','',''
GO
/****** Object:  StoredProcedure [dbo].[Sp_Projects]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_Projects]
@Category nvarchar(100)
,@Status nvarchar(100)

as begin
if(@Category= '' or @Category='All' )set @Category=null;
if(@Status= '' or @Status='All' )set @Status=null;
select
CreatedOn
,Project_Name
,location
,Budget
,Category
,Fee_Budget
,Client
,Status
from Projects P
where (@Category is null or @Category=P.Category) and (@Status is null or @Status=p.Status)
end

--exec Sp_Projects '',''
GO
/****** Object:  StoredProcedure [dbo].[Sp_Projects_adds]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_Projects_adds]
@Category nvarchar(100)
,@Status nvarchar(100)

as begin
if(@Category= '' or @Category='All' )set @Category=null;
if(@Status= '' or @Status='All' )set @Status=null;
select
sum(p.Budget) [Total Budget]
,count(id) [Projects]
from Projects P
where (@Category is null or @Category=P.Category) and (@Status is null or @Status=p.Status)
end

--exec Sp_Projects_adds '',''
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_projectview]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_rpt_projectview]
    @TimeFilter VARCHAR(20) 
	,@Status VARCHAR(50)
	,@Category VARCHAR(50),@Project VARCHAR(50),
	@StartDate DATE = NULL,
    @EndDate DATE = NULL
AS
BEGIN
IF(@TimeFilter =' ' or @TimeFilter='All') set @TimeFilter=null;
IF(@Status =' ' or @Status='All') set @Status=null;
IF(@Category =' ' or @Category='All') set @Category=null;
IF(@Project =' ' or @Project='All') set @Project=null;

    WITH A AS
    (
        SELECT
            COUNT(id) AS [PROJECT],
            SUM(Budget) AS [FEE PLANNED]
        FROM Projects WHERE
		
          
		CreatedOn >= CASE 
                                           WHEN @TimeFilter = 'ThisWeek' THEN DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0)
                                           WHEN @TimeFilter = 'LastWeek' THEN DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) - 1, 0)
                                           WHEN @TimeFilter = 'ThisMonth' THEN DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)
                                           WHEN @TimeFilter = 'LastMonth' THEN DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 0)
                                           WHEN @TimeFilter = '1stQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()), 0)
                                           WHEN @TimeFilter = '2ndQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 1, 0)
                                           WHEN @TimeFilter = '3rdQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 2, 0)
                                           WHEN @TimeFilter = '4thQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 3, 0)
                                           ELSE CreatedOn -- No filter
                                       END
									   AND (
										  (@Status IS NULL) OR (Status = @Status))
									   AND (
										  (@Category IS NULL) OR (Category = @Category))
									   AND (
										  (@Project IS NULL) OR (Project_Name = @Project))
									   
									),
    
    B AS
    (
        SELECT
            SUM(Charge) AS [FEE LOGGED]
        FROM Direct_Activities D inner join Projects PP on PP.Project_Name = D.Project_Name
        WHERE 
		 Approved = 1
		    AND D.CreatedOn >= CASE 
                                              WHEN @TimeFilter = 'ThisWeek' THEN DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0)
                                              WHEN @TimeFilter = 'LastWeek' THEN DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) - 1, 0)
                                              WHEN @TimeFilter = 'ThisMonth' THEN DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)
                                              WHEN @TimeFilter = 'LastMonth' THEN DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 0)
                                              WHEN @TimeFilter = '1stQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()), 0)
                                              WHEN @TimeFilter = '2ndQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 1, 0)
                                              WHEN @TimeFilter = '3rdQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 2, 0)
                                              WHEN @TimeFilter = '4thQuarter' THEN DATEADD(QUARTER, DATEDIFF(QUARTER, 0, GETDATE()) - 3, 0)
                                              ELSE D.CreatedOn -- No filter
                                          END
										  AND (
										  (@Status IS NULL) OR (PP.Status = @Status))

										  AND (
										  (@Category IS NULL) OR (PP.Category = @Category))

										  AND (
										  (@Project IS NULL) OR (PP.Project_Name = @Project))

										  )

           
            
    

    SELECT
    A.PROJECT,
    ISNULL(A.[FEE PLANNED], 0) AS [FEE PLANNED],
    ISNULL(B.[FEE LOGGED], 0) AS [FEE LOGGED],
    ISNULL(A.[FEE PLANNED], 0) - ISNULL(B.[FEE LOGGED], 0) AS [PROFIT]
FROM A, B;


END


--exec sp_rpt_projectview '','','','','',''

--select*,from projects
GO
/****** Object:  StoredProcedure [dbo].[sp_timesheet_breakdown_Nonprojecttime]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_timesheet_breakdown_Nonprojecttime]
@Staff nvarchar(100),
@Activity nvarchar(100),
@startDate varchar(50),
@endDate varchar(50)
as begin
if(@Staff= '' or @Staff='All' )set @Staff=null;
if(@Activity= '' or @Activity='All' )set @Activity=null;
select
d.[User] [Staff]
,d.Day_Date
,D.Name [WorkStage]
,d.Hours
,D.WeekNo
,(case when d.Approved = 1 then 'YES' else 'NO' end) Approved

,d.Comments
from Indirect_Activities D
inner join Employees E on E.Username =D.[User]
inner join InDirects I on I.Name =D.Name
where (@Staff is null or @Staff =d.[User]) and (@Activity is null or @Activity=d.Name)  and  ((D.Day_Date >= @StartDate AND D.Day_Date <= @EndDate))
order by d.Day_Date desc
end

--exec sp_timesheet_breakdown 'tom','','1/1/2024','3/3/2024'
GO
/****** Object:  StoredProcedure [dbo].[sp_timesheet_breakdown_projecttime]    Script Date: 3/1/2024 7:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_timesheet_breakdown_projecttime]
@Staff nvarchar(100),
@Project nvarchar(100),
@startDate varchar(50),
@endDate varchar(50)
as begin
if(@Staff= '' or @Staff='All' )set @Staff=null;
if(@Project= '' or @Project='All' )set @Project=null;
select
d.[User] [Staff]
,d.Day_Date
,d.Project_Name
,p.Status
,D.Name [WorkStage]
,D.Category
,d.Hours
,E.Rate
,D.Charge
,D.WeekNo
,(case when d.Approved = 1 then 'YES' else 'NO' end) Approved

,d.Comments
from Direct_Activities D
inner join Projects P on D.Project_Name=p.Project_Name
inner join Employees E on E.Username =D.[User]
where (@Staff is null or @Staff =d.[User]) and (@Project is null or @Project=d.Project_Name)  and  ((D.Day_Date >= @StartDate AND D.Day_Date <= @EndDate))
order by d.Day_Date desc
end

--exec sp_timesheet_breakdown 'tom','','1/1/2024','3/3/2024'
GO
USE [master]
GO
ALTER DATABASE [Planning] SET  READ_WRITE 
GO
