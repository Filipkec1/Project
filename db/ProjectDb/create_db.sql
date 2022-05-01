CREATE DATABASE [ProjectDb];
GO

USE [ProjectDb];
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'People')
  BEGIN
    EXEC ('CREATE SCHEMA People;');
  END
