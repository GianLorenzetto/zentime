SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [zt].[TimeSheetEntry]
(
    [Id] [int] IDENTITY (1,1) NOT NULL
        CONSTRAINT [PK_TimeSheetEntry] PRIMARY KEY CLUSTERED ([Id] ASC),

    [ProjectId] [int] NOT NULL
        CONSTRAINT FK_TimeSheetEntry_TimeSheetProject FOREIGN KEY (ProjectId) REFERENCES [zt].[TimeSheetProject] (Id),

    [ActivityId] [int] NOT NULL
        CONSTRAINT FK_TimeSheetEntry_TimeSheetActivity FOREIGN KEY (ActivityId) REFERENCES [zt].[TimeSheetActivity] (Id),

    [Details] [nvarchar](500) NOT NULL,
    [DurationInMinutes] [int] NOT NULL,
    [StartedAt] [datetimeoffset] NOT NULL,
    [CreatedAt] [datetimeoffset] NOT NULL,
    [UpdatedAt] [datetimeoffset] NULL
) ON [PRIMARY]
GO