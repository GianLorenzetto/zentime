SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

INSERT INTO [zt].[Project] (Name, CreatedAt)
VALUES ('Telstra Purple', SYSDATETIMEOFFSET()),
       ('ACME Pty Ltd', SYSDATETIMEOFFSET()),
       ('Client ABC', SYSDATETIMEOFFSET())

INSERT INTO [zt].[Activity] (Name, CreatedAt)
VALUES ('Leave - Annual', SYSDATETIMEOFFSET()),
       ('Leave - Personal', SYSDATETIMEOFFSET()),
       ('Consulting', SYSDATETIMEOFFSET()),
       ('LG Activities', SYSDATETIMEOFFSET())

-- Get id's for known Project and Activity
DECLARE @ProjectId AS int
DECLARE @ActivityId AS int

SELECT @ProjectId = [Id]
FROM [zt].[Project]
WHERE Name = 'Telstra Purple'

SELECT @ActivityId = [Id]
FROM [zt].[Activity]
WHERE Name = 'Consulting'

INSERT INTO [zt].[TimeSheet] (ProjectId, ActivityId, Details, DurationInMinutes, StartedAt, CreatedAt)
VALUES (@ProjectId, @ActivityId, 'Some details', 60, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET()),
       (@ProjectId, @ActivityId, 'Some other details', 15, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET())