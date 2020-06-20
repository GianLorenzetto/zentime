SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS(SELECT *
              FROM sys.schemas
              WHERE name = 'zt')
    BEGIN
        EXEC ( 'CREATE SCHEMA zt' );
    END
GO