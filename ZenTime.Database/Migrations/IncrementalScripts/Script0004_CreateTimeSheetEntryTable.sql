SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [zt].[TimeSheet]
(
    [Id] [int] IDENTITY (1,1) NOT NULL
        CONSTRAINT [PK_TimeSheet] PRIMARY KEY CLUSTERED ([Id] ASC),

    [ProjectId] [int] NOT NULL
        CONSTRAINT FK_TimeSheet_Project FOREIGN KEY (ProjectId) REFERENCES [zt].[Project] (Id),

    [ActivityId] [int] NOT NULL
        CONSTRAINT FK_Entry_Activity FOREIGN KEY (ActivityId) REFERENCES [zt].[Activity] (Id),

    [Details] [nvarchar](500) NOT NULL,
    [DurationInMinutes] [int] NOT NULL,
    [StartedAt] [datetimeoffset] NOT NULL,
    [CreatedAt] [datetimeoffset] NOT NULL,
    [UpdatedAt] [datetimeoffset] NULL
) ON [PRIMARY]
GO