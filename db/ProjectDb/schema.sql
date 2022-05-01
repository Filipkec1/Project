/* Drop Foreign Key Constraints */
EXEC sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all";
GO

while(exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_TYPE='FOREIGN KEY'))
begin
declare @sql nvarchar(2000)
SELECT TOP 1 @sql=('ALTER TABLE ' + TABLE_SCHEMA + '.[' + TABLE_NAME
+ '] DROP CONSTRAINT [' + CONSTRAINT_NAME + ']')
FROM information_schema.table_constraints
WHERE CONSTRAINT_TYPE = 'FOREIGN KEY'
exec (@sql)
end

/* Drop Tables */
EXEC sp_msforeachtable "DROP TABLE ?";
GO

EXEC sp_msforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all";
GO

/* Create Tables */
/* People */
CREATE TABLE [People].[Person] (
  [Id] uniqueidentifier PRIMARY KEY DEFAULT (NEWID()),
  [Name] varchar(250) NOT NULL,
  [Surname] varchar(250) NOT NULL,
  [City] varchar(250) NOT NULL,
  [Address] varchar(250) NOT NULL,
  [TelephoneNumber] varchar(20) NOT NULL,
  [Email] varchar(100) NOT NULL,
  [OIB] char(11) NOT NULL
)
GO

/* Create Primary Keys, Indexes, Uniques, Checks */
/* People */
CREATE UNIQUE INDEX [IX_Person_Oib] ON [People].[Person] ("Oib")
GO

/* Create Foreign Key Constraints */
/* People */
