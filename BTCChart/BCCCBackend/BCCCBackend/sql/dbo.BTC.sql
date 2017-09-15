CREATE TABLE [dbo].[BTC] (
[Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name] NCHAR (20)   NULL,
    [Rate] DECIMAL (18) NULL,
    [Date] NCHAR (15)   NULL,
    [Time] NCHAR (10)   NULL,
    [Ask]  DECIMAL (18) NULL,
    [Bid]  DECIMAL (18) NULL
);

