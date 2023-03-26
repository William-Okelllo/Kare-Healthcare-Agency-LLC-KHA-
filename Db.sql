USE [master]
GO
/****** Object:  Database [HR2]    Script Date: 3/26/2023 3:29:47 PM ******/
CREATE DATABASE [HR2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HR2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\HR2.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HR2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\HR2_log.ldf' , SIZE = 204800KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [HR2] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HR2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HR2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HR2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HR2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HR2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HR2] SET ARITHABORT OFF 
GO
ALTER DATABASE [HR2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HR2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HR2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HR2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HR2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HR2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HR2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HR2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HR2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HR2] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HR2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HR2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HR2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HR2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HR2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HR2] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [HR2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HR2] SET RECOVERY FULL 
GO
ALTER DATABASE [HR2] SET  MULTI_USER 
GO
ALTER DATABASE [HR2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HR2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HR2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HR2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HR2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HR2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'HR2', N'ON'
GO
ALTER DATABASE [HR2] SET QUERY_STORE = ON
GO
ALTER DATABASE [HR2] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [HR2]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Accesses]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Advances]    Script Date: 3/26/2023 3:29:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Advances](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Employee] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Employee_Fullnames] [nvarchar](max) NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Amount_Words] [nvarchar](max) NULL,
	[Pay_Date] [datetime] NOT NULL,
	[Due_to] [datetime] NOT NULL,
	[Approved_by_Finance] [nvarchar](100) NULL,
	[Approved_by_Manager] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Advances] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Attendances]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Bugs]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Configs]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Data]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Departments]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Employees]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Employees_Config]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Expenses]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Facilities]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Files]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Invoices]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[leaves]    Script Date: 3/26/2023 3:29:48 PM ******/
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
	[HR2_Email] [nvarchar](1000) NULL,
	[Emp_Mail] [nvarchar](1000) NULL,
	[From_Date] [datetime] NULL,
	[To_Date] [datetime] NULL,
	[Type] [nvarchar](200) NULL,
	[Department] [nvarchar](100) NULL,
	[Return_Date] [datetime] NULL,
	[Approver_Remarks] [nvarchar](max) NULL,
	[Requested_Days] [int] NULL,
	[phone] [nvarchar](100) NULL,
	[HR_Email] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.leaves] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[leaves_Days_track]    Script Date: 3/26/2023 3:29:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[leaves_Days_track](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Total_leaves_per_year] [int] NULL,
	[Requested_leaves] [int] NULL,
	[Remaining_leaves] [int] NULL,
	[Username] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.leaves_Days_track] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[leaves_logs]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Office]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Reports]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Staffs]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Table [dbo].[Ticketing_Clients]    Script Date: 3/26/2023 3:29:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticketing_Clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Client] [nvarchar](100) NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_dbo.Ticketing_Ticketing_Clients] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Timesheet_replies]    Script Date: 3/26/2023 3:29:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timesheet_replies](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Sheetid] [int] NULL,
	[feedback] [nvarchar](max) NULL,
	[staff] [nvarchar](100) NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_dbo.Timesheet_replies] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TTs]    Script Date: 3/26/2023 3:29:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TTs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Employee] [nvarchar](max) NULL,
	[Status] [bit] NULL,
	[Project] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[feedback] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.TTs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users_Facility]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  Index [RoleNameIndex]    Script Date: 3/26/2023 3:29:48 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 3/26/2023 3:29:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 3/26/2023 3:29:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 3/26/2023 3:29:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 3/26/2023 3:29:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Acc_Profile]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[AccessRolesSp]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[AddPhone]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Checkin/Checkout]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Delete_File]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Delete_User]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetData]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetEmp]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetEmpUsername]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Gettimesheet]    Script Date: 3/26/2023 3:29:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Gettimesheet]
@Employees nvarchar(100)
, @From_Date varchar(50),
  @End_Date varchar(50)
as begin
IF(@Employees =' ' or @Employees='All') set @Employees=null;
IF(isdate(@From_Date)=0) SET @From_Date=null;
IF(isdate(@End_Date)=0)	SET @End_Date=null;
select
id,employee,status,Project,Description,CreatedOn,feedback
From TTs where @Employees is null or @Employees=Employee and  
  Createdon between Convert(DateTime, DATEDIFF(DAY, 0, @From_Date)) and Convert(DateTime, DATEDIFF(DAY, -1, @End_Date))
  order by CreatedOn desc
end




--exec Gettimesheet ''

GO
/****** Object:  StoredProcedure [dbo].[Gettimesheet2]    Script Date: 3/26/2023 3:29:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  proc [dbo].[Gettimesheet2]
@Employees nvarchar(100)

as begin
IF(@Employees =' ' or @Employees='All') set @Employees=null;

select
id,employee,status,Project,Description,CreatedOn,feedback
From TTs where @Employees is null or @Employees=Employee 
 order by CreatedOn desc
end




--exec Gettimesheet ''

GO
/****** Object:  StoredProcedure [dbo].[PushEmp]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Role_Set]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[SetRole]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_departments]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_emp_update]    Script Date: 3/26/2023 3:29:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_emp_update]
@Total_leaves int
as begin
update leaves_Days_track
set Total_leaves_per_year=@Total_leaves

update leaves_Days_track set Remaining_leaves = (Total_leaves_per_year -Requested_leaves)
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_Employee]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_employee_prog]    Script Date: 3/26/2023 3:29:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_employee_prog]
@staff nvarchar(100)
as begin
select
distinct staff
,id=7
,isnull(tt.[Total Tickets],0)[Total Tickets]
,isnull(i.Invoiced,0)[Invoiced]
,isnull(A.Approved,0)[Approved]
,isnull(p.Pending,0)[Pending]
from Tickets t 
cross apply (select count(id) over ( partition by staff) [Total Tickets] from Tickets where  Staff =@staff and MONTH(CreatedOn) =MONTH(GETDATE()))TT
cross apply (select Count(id)[Invoiced] from Tickets where ticket_status=2 and Staff =@staff and MONTH(CreatedOn) =MONTH(GETDATE()))I
cross apply (select Count(id)[Approved] from Tickets where ticket_status=1and Staff =@staff and MONTH(CreatedOn) =MONTH(GETDATE()))A
cross apply (select Count(id)[Pending] from Tickets where ticket_status=0 and  Staff =@staff and MONTH(CreatedOn) =MONTH(GETDATE()))P
where Staff =@staff and MONTH(CreatedOn) =MONTH(GETDATE())
end


--exec Emp_dash 'muru'
GO
/****** Object:  StoredProcedure [dbo].[sp_expenses]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_expenses_report]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_getDrpt]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_getHodEmail]    Script Date: 3/26/2023 3:29:48 PM ******/
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
select @hodmail as [HR2_Email]
end


--exec sp_getHodEmail 'kanaan'
GO
/****** Object:  StoredProcedure [dbo].[sp_GetRoles]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_invoices]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_invoices_report]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_sheet_reply]    Script Date: 3/26/2023 3:29:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  proc [dbo].[sp_sheet_reply]
@User nvarchar(100)
,@feedback nvarchar(100)
,@Id int

as begin

insert into Timesheet_replies (Sheetid,feedback,staff,CreatedOn)
values (@Id,@feedback,@User,GetDate())

end

--select*  from Timesheet_replies

--

GO
/****** Object:  StoredProcedure [dbo].[sp_timesheet_report]    Script Date: 3/26/2023 3:29:48 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TimesheetDash]    Script Date: 3/26/2023 3:29:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_TimesheetDash]
@Employee nvarchar(100)
as begin
IF(@Employee ='' or @Employee='All')set @Employee=null;

with A as
( select  Count(id) [Total]  From TTs where Employee is null or Employee=@Employee ),

B as (SELECT COUNT(id)[Current Week] FROM TTs  WHERE (DATEPART(wk, CreatedOn) = DATEPART(wk, GETDATE())) and (Employee is null or Employee=@Employee))

,C as(select  Count(id) [Today]  From TTs where CONVERT(date, CreatedOn) = CONVERT(date, GETDATE()) and  (Employee is null or Employee=@Employee))

Select
@Employee [Employee]
,A.Total
,B.[Current Week]
,C.Today
From A,B,C

end


--exec sp_TimesheetDash 'jusper'
GO
/****** Object:  StoredProcedure [dbo].[sp_track_leave]    Script Date: 3/26/2023 3:29:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[sp_track_leave]
@TT_Leaves int
,@Request_days int
,@User nvarchar(100)
,@Id int
,@Approver_Remarks nvarchar(max)
,@Approver nvarchar(100)
as begin
insert into  leaves_logs (leave_id,createdon,Approver,Approver_status,Approver_remarks)
values
(@id,GetDate(),@Approver,'Aproved',@Approver_Remarks)

if exists(select *from leaves_Days_track where Username= @User)
    
	Update leaves_Days_track set Total_leaves_per_year=@TT_Leaves,
	Requested_leaves=(Requested_leaves + @Request_days) ,Remaining_leaves=(Total_leaves_per_year-Requested_leaves)
	where Username=@User



else 
insert into leaves_Days_track (Total_leaves_per_year,Requested_leaves,Remaining_leaves,Username)
values                        (@TT_Leaves,@Request_days, @TT_Leaves-@Request_days,@User)

update leaves_Days_track set Remaining_leaves =(Total_leaves_per_year-Requested_leaves) where Username=@User
end


--select*  from leaves_Days_track

--exec sp_track_leave 50,7,'muru'
GO
USE [master]
GO
ALTER DATABASE [HR2] SET  READ_WRITE 
GO
