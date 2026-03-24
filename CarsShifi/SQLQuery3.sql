-- מחיקת הטבלה הקודמת (אם שינית לה שם ל-Backup, תשני כאן את השם)
IF OBJECT_ID('dbo.PayMents', 'U') IS NOT NULL DROP TABLE dbo.PayMents;

CREATE TABLE [dbo].[PayMents] (
    [paymentCode] INT IDENTITY(1,1) NOT NULL, 
    [creditCard]  NVARCHAR (50) NOT NULL, -- הגדלנו ל-50
    [validit]      NVARCHAR (50) NOT NULL, -- הגדלנו ל-50
    [cvc]          INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([paymentCode] ASC)
);