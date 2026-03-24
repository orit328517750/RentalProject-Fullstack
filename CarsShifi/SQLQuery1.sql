-- 1. מחיקה סופית של הטבלה
DROP TABLE [dbo].[PayMents];

-- 2. יצירה מחדש עם מנגנון מספור אוטומטי (IDENTITY)
CREATE TABLE [dbo].[PayMents] (
    [paymentCode] INT IDENTITY(1,1) NOT NULL, 
    [creditCard]  NVARCHAR (30) NOT NULL,
    [validit]      NVARCHAR (30) NOT NULL,
    [cvc]          INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([paymentCode] ASC)
);