SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [zt].[TimeSheetProject]
(
    [Id] [int] IDENTITY (1,1) NOT NULL
        CONSTRAINT [PK_TimeSheetProject] PRIMARY KEY CLUSTERED ([Id] ASC),

    [Name] [nvarchar](100) NOT NULL,
    [CreatedAt] [datetimeoffset] NOT NULL,
    [UpdatedAt] [datetimeoffset] NULL
) ON [PRIMARY]
GO

ALTER TABLE [zt].[TimeSheetProject]
    WITH CHECK
        ADD CONSTRAINT UQ_TimeSheetProject_Name UNIQUE (Name)