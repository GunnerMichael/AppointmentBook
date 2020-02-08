DECLARE @db_id int;

SET @db_id = DB_ID(N'MyAppointment');

IF @db_id is NULL 
BEGIN
    CREATE DATABASE MyAppointment
END
ELSE PRINT 'MyAppointment exists'


use [MyAppointment]



if not exists (
    select 1
from information_schema.tables
where
            table_schema = 'dbo'
    and table_name   = 'AppointmentRequest'
    and table_type   = 'base table'
)
BEGIN
    CREATE TABLE [dbo].AppointmentRequest
    (
        [AppointmentRequestId] UNIQUEIDENTIFIER NOT NULL,
        [AppointmentDate] DATETIME2,
        [Details] nvarchar(max),
        [DateRequested] DATETIME2 DEFAULT GETDATE(),
        [Approved] bit DEFAULT 0
    )
END

if not exists (
    select 1
from information_schema.tables
where
            table_schema = 'dbo'
    and table_name   = 'Appointment'
    and table_type   = 'base table'
)
BEGIN
    CREATE TABLE [dbo].Appointment
    (
        [AppointmentId] int NOT NULL IDENTITY(1,1),
        [AppointmentRequestId] UNIQUEIDENTIFIER NOT NULL DEFAULT newid(),
        [AppointmentDate] DATETIME2,
        [Details] nvarchar(max),
        [DateRequested] DATETIME2
    )
END


-- stored procedures
GO

CREATE OR ALTER PROCEDURE dbo.NewAppointmentRequest
(
    @AppointmentDate DATETIME2,
    @Details nvarchar(max)
)
AS
    DECLARE @id UNIQUEIDENTIFIER = newid()

    INSERT INTO dbo.AppointmentRequest(AppointmentRequestId, AppointmentDate,Details)
    VALUES(@id, @AppointmentDate, @Details)

    SELECT @ID AS [AppointmentRequestId]

GO

CREATE OR ALTER PROCEDURE dbo.GetAppointmentRequest
(
    @AppointmentRequestId UNIQUEIDENTIFIER
)
AS 
    SELECT AppointmentDate
    , Details
    ,DateRequested
    ,Approved
    FROM dbo.AppointmentRequest (NOLOCK)
    WHERE @AppointmentRequestId = [AppointmentRequestId]

GO

CREATE OR ALTER PROCEDURE dbo.GetOutstandingAppointmentRequest
AS  
    SELECT
        AppointmentRequestId 
    ,AppointmentDate
    , Details
    ,DateRequested
    FROM dbo.AppointmentRequest (NOLOCK)

    WHERE approved = 0

GO


CREATE OR ALTER PROCEDURE dbo.ApproveAppointmentRequest
(
        @AppointmentRequestId UNIQUEIDENTIFIER
        ,@AppointmentDate DATETIME2,
        @Details nvarchar(max)
)
AS
    BEGIN TRANSACTION 
        INSERT INTO dbo.Appointment
        (
            [AppointmentRequestId],
            [AppointmentDate],
            [Details]
        )
        VALUES
        (
            @AppointmentRequestId,
            @AppointmentDate,
            @Details   
        )

        UPDATE DBO.AppointmentRequest
            SET [Approved] = 1
        WHERE @AppointmentRequestId = [AppointmentRequestId]


    COMMIT TRANSACTION;

    SELECT SCOPE_IDENTITY() AS [AppointmentId]

GO

CREATE OR ALTER PROCEDURE dbo.GetAppointments
(
    @start DATETIME2,
    @end DATETIME2
)
AS
    SELECT
     AppointmentId,
     AppointmentDate,
     Details
    FROM dbo.Appointment (NOLOCK)

    WHERE AppointmentDate between @start and @end
