Use StatisticiJocuriVideo
GO
--- Test scenarii de erori
--Dirty reads
Create or alter procedure test_dirtyReads as
Begin
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
-- Solutie : ridicam nivelul de isolation la Commited : SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRAN
SELECT * FROM GamePublishers
INSERT INTO Logs (source_procedure, log_type, log_message)
SELECT 
    OBJECT_NAME(@@PROCID),
    'Info',
    STRING_AGG(CONCAT('Nume: ', Nume, ', Locatie: ', Locatie, 'Venit' , Venituri), '; ')
FROM GamePublishers;
	
WAITFOR DELAY '00:00:10'
SELECT * FROM GamePublishers

INSERT INTO Logs (source_procedure, log_type, log_message)
SELECT 
    OBJECT_NAME(@@PROCID),
    'Info',
    STRING_AGG(CONCAT('Nume: ', Nume, ', Locatie: ', Locatie, 'Venit' , Venituri), '; ')
FROM GamePublishers;

COMMIT TRAN
End

Exec test_dirtyReads

SELECT TOP 10 *
FROM Logs
ORDER BY log_date DESC;

--NON-REPEATABLE READS
Go
Create or Alter Procedure Test_NonRepeatableReads as
begin
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
-- Solutie : ridicam nivelul de isolation la Commited : SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN
SELECT * FROM GamePublishers
INSERT INTO Logs (source_procedure, log_type, log_message)
SELECT 
    OBJECT_NAME(@@PROCID),
    'Info',
    STRING_AGG(CONCAT('Nume: ', Nume, ', Locatie: ', Locatie, 'Venit' , Venituri), '; ')
FROM GamePublishers;
	
WAITFOR DELAY '00:00:10'
SELECT * FROM GamePublishers

INSERT INTO Logs (source_procedure, log_type, log_message)
SELECT 
    OBJECT_NAME(@@PROCID),
    'Info',
    STRING_AGG(CONCAT('Nume: ', Nume, ', Locatie: ', Locatie, 'Venit' , Venituri), '; ')
FROM GamePublishers;

COMMIT TRAN
end
GO

Exec Test_NonRepeatableReads
GO

SELECT TOP 10 *
FROM Logs
ORDER BY log_date DESC;

--Phantom Reads
Go
Create or Alter Procedure Test_PhantomReads as
begin
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
-- Solutie : ridicam nivelul de isolation la Commited : SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
BEGIN TRAN
SELECT * FROM GamePublishers
INSERT INTO Logs (source_procedure, log_type, log_message)
SELECT 
    OBJECT_NAME(@@PROCID),
    'Info',
    STRING_AGG(CONCAT('Nume: ', Nume, ', Locatie: ', Locatie, 'Venit' , Venituri), '; ')
FROM GamePublishers;
	
WAITFOR DELAY '00:00:10'
SELECT * FROM GamePublishers

INSERT INTO Logs (source_procedure, log_type, log_message)
SELECT 
    OBJECT_NAME(@@PROCID),
    'Info',
    STRING_AGG(CONCAT('Nume: ', Nume, ', Locatie: ', Locatie, 'Venit' , Venituri), '; ')
FROM GamePublishers;

COMMIT TRAN
end

Exec Test_PhantomReads

SELECT TOP 10 *
FROM Logs
ORDER BY log_date DESC;

--- Deadlock T2
Go
Create or Alter Procedure DeadLockTwo As
Begin
begin tran
update GameDeveloperi set Nume='deadlock Authors Transaction 1' where id=1
INSERT INTO Logs (source_procedure, log_type, log_message)
	Values (OBJECT_NAME(@@PROCID),'Info', 'Name changed GameDeveloperi id 1')
--block GameDevelepori

waitfor delay '00:00:10'
update GamePublishers set Nume='deadlock Books Transaction 1' where id=2
INSERT INTO Logs (source_procedure, log_type, log_message)
	Values (OBJECT_NAME(@@PROCID),'Info', 'Name changed GamePublisher id 2')

commit tran
End

Exec DeadLockTwo

SELECT TOP 10 *
FROM Logs
ORDER BY log_date DESC;

---Solution dead lock
 -- I. set DEADLOCK_PRIORITY to HIGH or set DEADLOCK_PRIORITY to Low
 -- II. Inversare update comenzi