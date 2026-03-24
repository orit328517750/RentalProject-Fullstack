-- 1. נשנה את שם הטבלה הבעייתית לשם אחר כדי "לפנות את הדרך"
EXEC sp_rename 'PayMents', 'PayMents_Backup';

-- 2. עכשיו ניצור את הטבלה החדשה והתקינה עם ה-IDENTITY
CREATE TABLE [dbo].[PayMents] (
    [paymentCode] INT IDENTITY(1,1) NOT NULL, 
    [creditCard]  NVARCHAR (30) NOT NULL,
    [validit]      NVARCHAR (30) NOT NULL,
    [cvc]          INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([paymentCode] ASC)
);