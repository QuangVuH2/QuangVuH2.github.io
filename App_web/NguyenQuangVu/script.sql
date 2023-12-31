USE [master]
GO
/****** Object:  Database [Thoi_Trang]    Script Date: 10/5/2023 3:32:13 PM ******/
CREATE DATABASE [Thoi_Trang]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Thoi_Trang', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Thoi_Trang.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Thoi_Trang_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Thoi_Trang_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Thoi_Trang] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Thoi_Trang].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Thoi_Trang] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Thoi_Trang] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Thoi_Trang] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Thoi_Trang] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Thoi_Trang] SET ARITHABORT OFF 
GO
ALTER DATABASE [Thoi_Trang] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Thoi_Trang] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Thoi_Trang] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Thoi_Trang] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Thoi_Trang] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Thoi_Trang] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Thoi_Trang] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Thoi_Trang] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Thoi_Trang] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Thoi_Trang] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Thoi_Trang] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Thoi_Trang] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Thoi_Trang] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Thoi_Trang] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Thoi_Trang] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Thoi_Trang] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Thoi_Trang] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Thoi_Trang] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Thoi_Trang] SET RECOVERY FULL 
GO
ALTER DATABASE [Thoi_Trang] SET  MULTI_USER 
GO
ALTER DATABASE [Thoi_Trang] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Thoi_Trang] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Thoi_Trang] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Thoi_Trang] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Thoi_Trang', N'ON'
GO
USE [Thoi_Trang]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 10/5/2023 3:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Categorys]    Script Date: 10/5/2023 3:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Slug] [nvarchar](max) NULL,
	[ParentId] [int] NULL,
	[Orders] [int] NULL,
	[MetaDesc] [nvarchar](max) NOT NULL,
	[MetaKey] [nvarchar](max) NOT NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Categorys] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Configs]    Script Date: 10/5/2023 3:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Value] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Configs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 10/5/2023 3:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[CreateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Links]    Script Date: 10/5/2023 3:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Links](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Slug] [nvarchar](max) NOT NULL,
	[TableId] [int] NULL,
	[Type] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Links] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menus]    Script Date: 10/5/2023 3:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Link] [nvarchar](max) NOT NULL,
	[TableId] [int] NULL,
	[TypeMenu] [nvarchar](max) NULL,
	[Position] [nvarchar](max) NULL,
	[ParentId] [int] NULL,
	[Orders] [int] NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Oderdetails]    Script Date: 10/5/2023 3:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Oderdetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Qty] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_dbo.Oderdetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 10/5/2023 3:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ReceiverName] [nvarchar](max) NOT NULL,
	[ReceiverAddress] [nvarchar](max) NOT NULL,
	[ReceiverPhone] [nvarchar](max) NOT NULL,
	[ReceiverEmail] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[CreateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Posts]    Script Date: 10/5/2023 3:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TopId] [int] NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Slug] [nvarchar](max) NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Img] [nvarchar](max) NULL,
	[PostTye] [nvarchar](max) NULL,
	[MetaDesc] [nvarchar](max) NOT NULL,
	[MetaKey] [nvarchar](max) NOT NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Posts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 10/5/2023 3:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CatId] [int] NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Slug] [nvarchar](max) NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Img] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[PriceSale] [decimal](18, 2) NOT NULL,
	[Number] [int] NOT NULL,
	[MetaKey] [nvarchar](max) NOT NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[Status] [int] NOT NULL,
	[SupplierId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sliders]    Script Date: 10/5/2023 3:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sliders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Url] [nvarchar](max) NULL,
	[Img] [nvarchar](max) NULL,
	[Orders] [int] NULL,
	[Position] [nvarchar](50) NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Sliders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 10/5/2023 3:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Slug] [nvarchar](max) NULL,
	[Img] [nvarchar](max) NULL,
	[Orders] [int] NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[UrlSite] [nvarchar](max) NOT NULL,
	[MetaDesc] [nvarchar](max) NOT NULL,
	[MetaKey] [nvarchar](max) NOT NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Suppliers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Topics]    Script Date: 10/5/2023 3:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Slug] [nvarchar](max) NULL,
	[ParentId] [int] NULL,
	[Orders] [int] NULL,
	[MetaDesc] [nvarchar](max) NOT NULL,
	[MetaKey] [nvarchar](max) NOT NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Topics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/5/2023 3:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[Gender] [int] NULL,
	[Roles] [int] NULL,
	[Address] [nvarchar](max) NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Categorys] ON 

INSERT [dbo].[Categorys] ([Id], [Name], [Slug], [ParentId], [Orders], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (1, N'Thời trang nam', N'thoi-trang-nam', 0, 7, N'dad', N'adadf', 1, CAST(0x0000AE5D00401AF0 AS DateTime), 1, CAST(0x0000B09100A46C6B AS DateTime), 2)
INSERT [dbo].[Categorys] ([Id], [Name], [Slug], [ParentId], [Orders], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (2, N'Thời trang nữ', N'thoi-trang-nu', 0, 2, N'Thời trang nữ', N'Thời trang nữ', 1, CAST(0x0000AE87010477B0 AS DateTime), 1, CAST(0x0000B01300CA2B49 AS DateTime), 1)
INSERT [dbo].[Categorys] ([Id], [Name], [Slug], [ParentId], [Orders], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (3, N'Thời trang thể thao', N'thoi-trang-the-thao', 0, 3, N'Thời trang thể thao', N'Thời trang thể thao', 1, CAST(0x0000AE87010FECE4 AS DateTime), 1, CAST(0x0000B01300CA3529 AS DateTime), 1)
INSERT [dbo].[Categorys] ([Id], [Name], [Slug], [ParentId], [Orders], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (6, N'thời trang của bé trai', N'thoi-trang-cua-be-trai', 0, 2, N'afad', N'fasd', 1, CAST(0x0000B01300C70344 AS DateTime), 1, CAST(0x0000B01300C8E6B1 AS DateTime), 1)
INSERT [dbo].[Categorys] ([Id], [Name], [Slug], [ParentId], [Orders], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (1006, N'Túi sách nữ', N'tui-sach-nu', 2, 3, N'fsdfa', N'dsdf', 1, CAST(0x0000B02901107EBA AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Categorys] OFF
SET IDENTITY_INSERT [dbo].[Contacts] ON 

INSERT [dbo].[Contacts] ([Id], [UserId], [FullName], [Phone], [Email], [Title], [Detail], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (1, 0, N'Lê Văn Thông', N'01234567887', N'vanthong@gmail.com', N'hahahihi', N'xin chào các bạn.', CAST(0x0000B09001127BDF AS DateTime), 1, CAST(0x0000B09100B7F487 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Contacts] OFF
SET IDENTITY_INSERT [dbo].[Links] ON 

INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (2, N'tin-tuc', 2, N'topic')
INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (3, N'dich-vu', 3, N'topic')
INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (4, N'trang-phuc-mua-he', 2, N'page')
INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (5, N'thoi-trang-cua-be-trai', 6, N'category')
INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (6, N'thoi-trang-nam', 1, N'category')
INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (7, N'thoi-trang-nu', 2, N'category')
INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (8, N'thoi-trang-the-thao', 3, N'category')
INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (1004, N'calvin-klein', 7, N'supplier')
INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (1005, N'calvin-klein', 7, N'supplier')
INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (1006, N'nike', 8, N'supplier')
INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (1007, N'viet-tien', 9, N'supplier')
INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (1008, N'louis-vuitton', 10, N'supplier')
INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (1009, N'chanel', 11, N'supplier')
INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (1010, N'dsf', 12, N'supplier')
INSERT [dbo].[Links] ([Id], [Slug], [TableId], [Type]) VALUES (1011, N'tui-sach-nu', 1006, N'category')
SET IDENTITY_INSERT [dbo].[Links] OFF
SET IDENTITY_INSERT [dbo].[Menus] ON 

INSERT [dbo].[Menus] ([Id], [Name], [Link], [TableId], [TypeMenu], [Position], [ParentId], [Orders], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (1, N'Thời trang nam', N'thoi-trang-nam', 1, N'category', N'mainmenu', 8, 1, 1, CAST(0x0000B00E00C18054 AS DateTime), 1, CAST(0x0000B0240102D6BB AS DateTime), 1)
INSERT [dbo].[Menus] ([Id], [Name], [Link], [TableId], [TypeMenu], [Position], [ParentId], [Orders], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (2, N'Thời trang nữ', N'thoi-trang-nu', 2, N'category', N'mainmenu', 8, 1, 1, CAST(0x0000B00E00C18180 AS DateTime), 1, CAST(0x0000B0240105838E AS DateTime), 1)
INSERT [dbo].[Menus] ([Id], [Name], [Link], [TableId], [TypeMenu], [Position], [ParentId], [Orders], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (3, N'Thời trang thể thao', N'thoi-trang-the-thao', 3, N'category', N'mainmenu', 8, 1, 1, CAST(0x0000B00E00C18180 AS DateTime), 1, CAST(0x0000B02401058EFA AS DateTime), 1)
INSERT [dbo].[Menus] ([Id], [Name], [Link], [TableId], [TypeMenu], [Position], [ParentId], [Orders], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (4, N'Mỹ nhân Việt hack dáng tối ưu nhờ 5 mẫu giày', N'my-nhan-viet-hack-dang-toi-uu-nho-5-mau-giay', 1, N'topic', N'mainmenu', 0, NULL, 1, CAST(0x0000B00F00F23A47 AS DateTime), 1, CAST(0x0000B00F01053E1F AS DateTime), 0)
INSERT [dbo].[Menus] ([Id], [Name], [Link], [TableId], [TypeMenu], [Position], [ParentId], [Orders], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (5, N'tin tức', N'tin-tuc', 2, N'topic', N'mainmenu', 0, 4, 1, CAST(0x0000B00F00FED6E3 AS DateTime), 1, CAST(0x0000B01A01043C33 AS DateTime), 1)
INSERT [dbo].[Menus] ([Id], [Name], [Link], [TableId], [TypeMenu], [Position], [ParentId], [Orders], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (6, N'dịch vụ', N'dich-vu', 3, N'topic', N'mainmenu', 0, 3, 1, CAST(0x0000B00F00FED759 AS DateTime), 1, CAST(0x0000B01A01043CE1 AS DateTime), 1)
INSERT [dbo].[Menus] ([Id], [Name], [Link], [TableId], [TypeMenu], [Position], [ParentId], [Orders], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (7, N'Trang chủ', N'http://localhost:44345/', NULL, N'custom', N'mainmenu', 0, 1, 1, CAST(0x0000B024010172CA AS DateTime), 1, CAST(0x0000B0240101E7DA AS DateTime), 1)
INSERT [dbo].[Menus] ([Id], [Name], [Link], [TableId], [TypeMenu], [Position], [ParentId], [Orders], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (8, N'Thời trang', N'#', NULL, N'custom', N'mainmenu', 0, 2, 1, CAST(0x0000B0240102C2DB AS DateTime), 1, CAST(0x0000B0240102C57B AS DateTime), 1)
INSERT [dbo].[Menus] ([Id], [Name], [Link], [TableId], [TypeMenu], [Position], [ParentId], [Orders], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (9, N'thời trang của bé trai', N'thoi-trang-cua-be-trai', 6, N'category', N'mainmenu', 8, 1, 1, CAST(0x0000B02600FC1F5C AS DateTime), 1, CAST(0x0000B02600FC4681 AS DateTime), 1)
INSERT [dbo].[Menus] ([Id], [Name], [Link], [TableId], [TypeMenu], [Position], [ParentId], [Orders], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (12, N'Liên hệ', N'lien-he', NULL, N'custom', N'mainmenu', 0, 6, 1, CAST(0x0000B09000A6278C AS DateTime), 1, CAST(0x0000B09000A671F1 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Menus] OFF
SET IDENTITY_INSERT [dbo].[Oderdetails] ON 

INSERT [dbo].[Oderdetails] ([Id], [OrderId], [ProductId], [Price], [Qty], [Amount]) VALUES (1031, 17, 1015, CAST(200000.00 AS Decimal(18, 2)), 3, CAST(600000.00 AS Decimal(18, 2)))
INSERT [dbo].[Oderdetails] ([Id], [OrderId], [ProductId], [Price], [Qty], [Amount]) VALUES (1032, 17, 4, CAST(200000.00 AS Decimal(18, 2)), 2, CAST(400000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Oderdetails] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [UserId], [ReceiverName], [ReceiverAddress], [ReceiverPhone], [ReceiverEmail], [Note], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (17, 0, N'Nguyễn văn tèo', N'23 đoàn kết,-Phường Đạt Hiếu-Thị Xã Buôn Hồ-Tỉnh Đắk Lắk', N'012456123789', N'vanteo@gmail.com', N',', CAST(0x0000B08B010A4803 AS DateTime), 1, CAST(0x0000B09000FF4EFB AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET IDENTITY_INSERT [dbo].[Posts] ON 

INSERT [dbo].[Posts] ([Id], [TopId], [Title], [Slug], [Detail], [Img], [PostTye], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (1, NULL, N'dfa', N'dfa', N'dasd', NULL, N'post', N'dfasd', N'asda', 1, CAST(0x0000B01300C471E0 AS DateTime), NULL, NULL, 2)
INSERT [dbo].[Posts] ([Id], [TopId], [Title], [Slug], [Detail], [Img], [PostTye], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (2, NULL, N'trang phục mùa hè', N'trang-phuc-mua-he', N'fsadadasfsdfsdaf', NULL, N'page', N'asfsdfadfdsaf', N'as', 1, CAST(0x0000B01300C4EF9B AS DateTime), NULL, NULL, 2)
SET IDENTITY_INSERT [dbo].[Posts] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [CatId], [Name], [Slug], [Detail], [Img], [Price], [PriceSale], [Number], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status], [SupplierId]) VALUES (4, 1, N'Áo gió nam', N'ao-gio-nam', N'fdsf', N'ao-gio-nam.jpg', CAST(250000.00 AS Decimal(18, 2)), CAST(200000.00 AS Decimal(18, 2)), 4, N'Áo gió nam', 1, CAST(0x0000B00C011DD5CD AS DateTime), 1, CAST(0x0000B02900A4AE89 AS DateTime), 1, 7)
INSERT [dbo].[Products] ([Id], [CatId], [Name], [Slug], [Detail], [Img], [Price], [PriceSale], [Number], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status], [SupplierId]) VALUES (5, 1, N'áo thun nam', N'ao-thun-nam', N'fsdfadfadfdfdfdf', N'ao-thun-nam.jpg', CAST(250000.00 AS Decimal(18, 2)), CAST(200000.00 AS Decimal(18, 2)), 7, N'ao thun', 1, CAST(0x0000B01300C052D7 AS DateTime), 1, CAST(0x0000B02900A4AD55 AS DateTime), 1, 7)
INSERT [dbo].[Products] ([Id], [CatId], [Name], [Slug], [Detail], [Img], [Price], [PriceSale], [Number], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status], [SupplierId]) VALUES (1006, 1, N'Áo khoát nam', N'ao-khoat-nam', N'dfsafasdfafad', N'ao-khoat-nam.jpg', CAST(595000.00 AS Decimal(18, 2)), CAST(400000.00 AS Decimal(18, 2)), 10, N'áo nam', 1, CAST(0x0000B02900A4A737 AS DateTime), NULL, NULL, 1, 7)
INSERT [dbo].[Products] ([Id], [CatId], [Name], [Slug], [Detail], [Img], [Price], [PriceSale], [Number], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status], [SupplierId]) VALUES (1007, 1, N'áo vest nam', N'ao-vest-nam', N'hflaljiwieanflas', N'ao-vest-nam.jpg', CAST(600000.00 AS Decimal(18, 2)), CAST(500000.00 AS Decimal(18, 2)), 10, N'áo nam', 1, CAST(0x0000B02900A50E2E AS DateTime), NULL, NULL, 1, 11)
INSERT [dbo].[Products] ([Id], [CatId], [Name], [Slug], [Detail], [Img], [Price], [PriceSale], [Number], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status], [SupplierId]) VALUES (1008, 2, N'áo thun nữ', N'ao-thun-nu', N'fsdfsdfde', N'ao-thun-nu.jpg', CAST(250000.00 AS Decimal(18, 2)), CAST(200000.00 AS Decimal(18, 2)), 15, N'áo nữ', 1, CAST(0x0000B02900A54BAA AS DateTime), NULL, NULL, 1, 10)
INSERT [dbo].[Products] ([Id], [CatId], [Name], [Slug], [Detail], [Img], [Price], [PriceSale], [Number], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status], [SupplierId]) VALUES (1009, 2, N'áo thun nữ đen', N'ao-thun-nu-den', N'fdgdgagafd', N'dam-nu.png', CAST(500000.00 AS Decimal(18, 2)), CAST(400000.00 AS Decimal(18, 2)), 10, N'áo nữ', 1, CAST(0x0000B02900A7495B AS DateTime), NULL, NULL, 1, 11)
INSERT [dbo].[Products] ([Id], [CatId], [Name], [Slug], [Detail], [Img], [Price], [PriceSale], [Number], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status], [SupplierId]) VALUES (1010, 2, N'áo khoát nhữ', N'ao-khoat-nhu', N'ghfhfbrttyrtyr', N'ao-khoat-nhu.png', CAST(600000.00 AS Decimal(18, 2)), CAST(500000.00 AS Decimal(18, 2)), 15, N'áo nữ', 1, CAST(0x0000B02900A69D35 AS DateTime), NULL, NULL, 1, 10)
INSERT [dbo].[Products] ([Id], [CatId], [Name], [Slug], [Detail], [Img], [Price], [PriceSale], [Number], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status], [SupplierId]) VALUES (1011, 2, N'vấy nữ trắng', N'vay-nu-trang', N'gghfhfghf', N'vay-nu-trang.jpg', CAST(595000.00 AS Decimal(18, 2)), CAST(500000.00 AS Decimal(18, 2)), 10, N'áo nữ', 1, CAST(0x0000B02900A8038F AS DateTime), NULL, NULL, 1, 8)
INSERT [dbo].[Products] ([Id], [CatId], [Name], [Slug], [Detail], [Img], [Price], [PriceSale], [Number], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status], [SupplierId]) VALUES (1012, 2, N'đầm nữa đen', N'dam-nua-den', N'ggdfgdgdgd', N'dam-nua-den.jpg', CAST(595000.00 AS Decimal(18, 2)), CAST(500000.00 AS Decimal(18, 2)), 10, N'áo nữ', 1, CAST(0x0000B02900A7EF03 AS DateTime), NULL, NULL, 1, 8)
INSERT [dbo].[Products] ([Id], [CatId], [Name], [Slug], [Detail], [Img], [Price], [PriceSale], [Number], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status], [SupplierId]) VALUES (1013, 2, N'áo khoát nữa đen', N'ao-khoat-nua-den', N'dfdfgd', N'ao-khoat-nua-den.jpg', CAST(600000.00 AS Decimal(18, 2)), CAST(550000.00 AS Decimal(18, 2)), 10, N'áo nam', 1, CAST(0x0000B02900A86287 AS DateTime), NULL, NULL, 1, 11)
INSERT [dbo].[Products] ([Id], [CatId], [Name], [Slug], [Detail], [Img], [Price], [PriceSale], [Number], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status], [SupplierId]) VALUES (1014, 1006, N'túi sách', N'tui-sach', N'dffddfsd', N'tui-sach.jpg', CAST(600000.00 AS Decimal(18, 2)), CAST(550000.00 AS Decimal(18, 2)), 10, N'túi sách', 1, CAST(0x0000B0290111625E AS DateTime), NULL, NULL, 1, 7)
INSERT [dbo].[Products] ([Id], [CatId], [Name], [Slug], [Detail], [Img], [Price], [PriceSale], [Number], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status], [SupplierId]) VALUES (1015, 1, N'áo sơ mi nam xanh lam', N'ao-so-mi-nam-xanh-lam', N'dssdfsdafsderer', N'ao-so-mi-nam-xanh-lam.jpg', CAST(250000.00 AS Decimal(18, 2)), CAST(200000.00 AS Decimal(18, 2)), 15, N'áo nam', 1, CAST(0x0000B02900A95DF4 AS DateTime), NULL, NULL, 1, 9)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Sliders] ON 

INSERT [dbo].[Sliders] ([Id], [Name], [Url], [Img], [Orders], [Position], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (1, N'khuyến mãi một', N'khuyen-mai-mot', N'khuyen-mai-mot.jpg', 1, N'slidershow', 1, CAST(0x0000B01D012130C4 AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Sliders] ([Id], [Name], [Url], [Img], [Orders], [Position], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (3, N'khuyến mãi hai', N'khuyen-mai-hai', N'khuyen-mai-hai.jpg', 8, N'slidershow', 1, CAST(0x0000B01E010E1ACE AS DateTime), 1, CAST(0x0000B01E011B2851 AS DateTime), 1)
INSERT [dbo].[Sliders] ([Id], [Name], [Url], [Img], [Orders], [Position], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (5, N'thiên nhiên ', N'thien-nhien-', N'thien-nhien-.jpg', 1, N'slidershow', 1, CAST(0x0000B0290117A90E AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Sliders] OFF
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

INSERT [dbo].[Suppliers] ([Id], [Name], [Slug], [Img], [Orders], [FullName], [Phone], [Email], [UrlSite], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (7, N'Calvin Klein', N'calvin-klein', N'calvin-klein.jpg', 1, N'Calvin Klein', N'0123456789', N'ck@gmail.com', N'www.ck.com', N'Calvin Klein
Thương hiệu Calvin Klein là thương hiệu thời trang được sáng lập bởi nhà thiết kế cùng tên, Calvin Klein. Ông là người Do Thái gốc Hungari, sinh năm 1942 tại The Bronx, New York. Năm 1968, với sự đầu tư hợp tác của người bạn thời thơ ấu – Barry Schwartz, công ty Calvin Klein Inc được thành lập, có trụ sở đặt tại Midtown Manhattan, New York.

Năm 2015, Calvin Klein hướng đến đầu tư thương mại điện tử ở thị trường Châu Á, nơi tiềm lực vượt qua cả Châu Âu. Tại Việt Nam, hãng Calvin Klein đã có khá thương hiệu như quần áo, jeans, underwear, đầm nữ, đồng hồ và nước hoa. Calvin Klein hứa hẹn sẽ mang đến cho các người đam mê thời trang những phong cách đẳng cấp và thời thượng. Hiện nay, thương hiệu này đã có 11 nhãn hiệu nhỏ, đồng thời được cả người sử dụng nam và nữ vô cùng ưa chuộng. Trong đó, Calvin Klein Jeans và Underwear là hai dòng sản phẩm chủ đạo, tính thị trường cao, phổ biến và phân bố toàn cầu.

US Outlet Store – Cửa hàng thời trang cung cấp hàng hiệu Mỹ giá rẻ, săn sale các chương trình ưu đãi giá sốc từ những trung tâm mua sắm (Mall), hay các cửa hàng Outlet (nơi chuyên bán hàng giảm giá) tại Mỹ để có được những sản phẩm hàng hiệu chất lượng, giá rẻ sale shock đưa đến tận tay khách hàng. Với nhiều thương hiệu thời trang cao cấp nổi tiếng thế giới như: Calvin Klein, Polo Ralph Lauren, Michael Kors, Coach, Tommy Hilfiger, GAP, Nautica, Guess, Ivanka Trump, Banana Republic…', N'ck', 1, CAST(0x0000B00C0116D1BC AS DateTime), 1, CAST(0x0000B01300F35A75 AS DateTime), 1)
INSERT [dbo].[Suppliers] ([Id], [Name], [Slug], [Img], [Orders], [FullName], [Phone], [Email], [UrlSite], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (8, N'Nike', N'nike', N'nike.jpg', 1, N'Nike', N'0123456788', N'nike@gmail.com', N'www.nike.com.vn', N'Nike là một trong những thương hiệu thời trang thể thao cao cấp lớn nhất thế giới hiện nay. Cùng với các thiết bị thể thao danh tiếng, địa vị của thương hiệu Nike trên thị trường thế giới khó có thể lung lay trong tương lai.', N'ck', 1, CAST(0x0000B00C01171A64 AS DateTime), 1, CAST(0x0000B01300F366D1 AS DateTime), 1)
INSERT [dbo].[Suppliers] ([Id], [Name], [Slug], [Img], [Orders], [FullName], [Phone], [Email], [UrlSite], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (9, N'Việt Tiến', N'viet-tien', N'viet-tien.jpg', 1, N'Việt Tiến', N'0961637234', N'viettien@gmail.com', N'www.viettien.com.vn', N'Quần áo Việt Tiến là thương hiệu thời trang nổi tiếng dành cho dân công sở tại Việt Nam. Các sản phẩm của thương hiệu có sự đa dạng về mẫu mã và kiểu dáng, bao gồm những dòng trang phục chính như: áo sơ mi Việt Tiến, quần tây và áo thun có cổ. Thiết kế của quần áo Việt Tiến hầu hết phù hợp cho dân công sở với chất lượng được đảm bảo. Mức giá những sản phẩm quần áo của hãng cũng rất thân thiện với đại đa số người tiêu dùng trong nước, biến Việt Tiến trở thành một trong những thương hiệu thời trang quốc dân dành cho nam giới.', N'viettien', 1, CAST(0x0000B00C0117E2C8 AS DateTime), 1, CAST(0x0000B01300F37295 AS DateTime), 1)
INSERT [dbo].[Suppliers] ([Id], [Name], [Slug], [Img], [Orders], [FullName], [Phone], [Email], [UrlSite], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (10, N'Louis Vuitton', N'louis-vuitton', N'louis-vuitton.jpg', 1, N'Louis Vuitton', N'096163723499', N'LouisVuitton@gmail.com', N'www.VL.com.vn', N'Công ty Louis Vuitton (được biết đến nhiều hơn dưới tên gọi đơn giản là Louis Vuitton) là một công ty và nhãn hiệu thời trang xa xỉ của Pháp, có trụ sở tại Paris, Pháp. Đây là một ban của công ty cổ phần Pháp LVMH Louis Vuitton Moët Hennessy S.A. Công ty được đặt tên theo tên người sáng lập ra hãng là Louis Vuitton (4 tháng 8 năm 1821-27 tháng 2 năm 1892), người đã thiết kế và sản xuất hành lý như một Malletier trong nửa sau của thế kỷ 19.', N'VL', 1, CAST(0x0000B00C0118A0A0 AS DateTime), 1, CAST(0x0000B01300F38113 AS DateTime), 1)
INSERT [dbo].[Suppliers] ([Id], [Name], [Slug], [Img], [Orders], [FullName], [Phone], [Email], [UrlSite], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (11, N'Chanel', N'chanel', N'chanel.jpg', 1, N'Chanel', N'01234567887', N'chanel@gmail.com', N'www.chanel.com.vn', N'Thương hiệu Chanel được thành lập từ những năm 1909-1910 bởi nhà thiết kế người Pháp, huyền thoại thời trang Gabrielle Chanel. Mồ côi mẹ năm 12 tuổi và được cha đưa vào trại trẻ mồ côi, người ta cho rằng cuộc sống trong cô nhi viện đã tạo cho bà tính cách mạnh mẽ, thẳng thắn pha lẫn với vẻ nổi loạn. Năm 18 tuổi, Coco Chanel rời cô nhi viện đến Moulins và trở thành ca sĩ phòng trà. Bà có được biệt danh Coco cũng trong thời kỳ này. Hiếm có nhà thiết kế thời trang nào gây nhiều ảnh hưởng sâu sắc đến cuộc sống của người phụ nữ hiện đại như Coco Chanel. Bà mất ngày 10-01-1971', N'Chanel', 1, CAST(0x0000B00C01266F00 AS DateTime), 1, CAST(0x0000B01300F38CE6 AS DateTime), 1)
INSERT [dbo].[Suppliers] ([Id], [Name], [Slug], [Img], [Orders], [FullName], [Phone], [Email], [UrlSite], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (12, N'dsf', N'dsf', N'dsf.jpg', 1, N'sffsaf', N'01234567887', N'viettien@gmail.com', N'www.VL.com.vn', N'dfsadaf', N'dfasdsf', 1, CAST(0x0000B01E0117DF85 AS DateTime), 1, CAST(0x0000B01E0117E1FF AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
SET IDENTITY_INSERT [dbo].[Topics] ON 

INSERT [dbo].[Topics] ([Id], [Name], [Slug], [ParentId], [Orders], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (2, N'tin tức', N'tin-tuc', 0, 1, N'dadad', N'ad', 1, CAST(0x0000B00F00F2C9E6 AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Topics] ([Id], [Name], [Slug], [ParentId], [Orders], [MetaDesc], [MetaKey], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (3, N'dịch vụ', N'dich-vu', 0, 1, N'dsaf', N'adf', 1, CAST(0x0000B00F00F2DB47 AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Topics] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [Email], [Phone], [Gender], [Roles], [Address], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (1, N'admin', N'/OqSD3QStdp74M9CuMk3WQ==', N'Văn A', N'vanA@gmail.com', N'0123456789', 1, 1, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [Email], [Phone], [Gender], [Roles], [Address], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (2, N'khachhang', N'/OqSD3QStdp74M9CuMk3WQ==', N'Nguyễn Văn A', N'VanA@gmail.com', N'0123456789', 1, 5, N'123 võ thị sáu-Xã Biển Hồ-Thành phố Pleiku-Tỉnh Gia Lai', NULL, CAST(0x0000B07B00F8CDA2 AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [Email], [Phone], [Gender], [Roles], [Address], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [Status]) VALUES (3, N'khachhang2', N'/OqSD3QStdp74M9CuMk3WQ==', N'Việt Tiến', N'viettien@gmail.com', N'0123456788', 2, 5, N'123 võ thị sáu-Phường Điện Biên-Quận Ba Đình-Thành phố Hà Nội', NULL, CAST(0x0000B07B00FE9DBD AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
USE [master]
GO
ALTER DATABASE [Thoi_Trang] SET  READ_WRITE 
GO
