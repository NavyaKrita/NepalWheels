USE [NepaliWheels]
GO
/****** Object:  Table [dbo].[BlogPostLike]    Script Date: 5/3/2022 7:02:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogPostLike](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[IsLike] [bit] NULL,
	[BlogPostId] [int] NULL,
	[CreatedOnUtc] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE	dbo.BlogPost 
ADD AllowLikes BIT

--ALTER TABLE	dbo.News 
--ADD NewsCategoryId INT