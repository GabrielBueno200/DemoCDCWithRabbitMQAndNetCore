-- Create database and table to be monitored with CDC
CREATE DATABASE TestDatabase

USE TestDatabase

CREATE TABLE Person (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Address VARCHAR(200) NOT NULL,
    Phone VARCHAR(11)
)

-- Enable CDC
EXEC sys.sp_cdc_enable_db

EXEC sys.sp_cdc_enable_table
@source_schema = N'dbo',
@source_name   = N'Person',
@role_name     = N'Admin',
@supports_net_changes = 1

-- Check if CDC is active
SELECT s.name AS Schema_Name, tb.name AS Table_Name
, tb.object_id, tb.type, tb.type_desc, tb.is_tracked_by_cdc
FROM sys.tables tb
INNER JOIN sys.schemas s on s.schema_id = tb.schema_id
WHERE tb.is_tracked_by_cdc = 1

SELECT * 
FROM sys.change_tracking_databases 
WHERE database_id=DB_ID('TestDatabase')

-- Start cdc job if not started (MSSQL Server Agent has to be enabled)
EXEC sys.sp_cdc_start_job;  


-- CDC Test scripts
INSERT INTO TestDatabase.dbo.Person VALUES ('Name Test', 'Address Test', '12345')

UPDATE TestDatabase.dbo.Person SET Address = 'Test 2' WHERE Id = 14

DELETE FROM TestDatabase.dbo.Person