USE [CompanyStores]
GO

/****** Object:  Table [dbo].[Stores]    Script Date: 09/02/2017 16:16:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Stores](
	[Id] [uniqueidentifier] NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](512) NOT NULL,
	[City] [nvarchar](512) NOT NULL,
	[Zip] [nvarchar](16) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Longitude] [nvarchar](15) NULL,
	[Latitude] [nvarchar](15) NULL,
 CONSTRAINT [PK_Stores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Stores] ADD  CONSTRAINT [DF_Stores_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[Stores]  WITH CHECK ADD  CONSTRAINT [FK_Stores_Companies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO

ALTER TABLE [dbo].[Stores] CHECK CONSTRAINT [FK_Stores_Companies]
GO