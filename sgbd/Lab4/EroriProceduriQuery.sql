Use StatisticiJocuriVideo
GO

Go
--- Dirty Reads 
Create or Alter Procedure DirtyReads As
Begin
	BEGIN TRANSACTION
	UPDATE GamePublishers SET
	Locatie='Romania' WHERE id = 2
	
	INSERT INTO Logs (source_procedure, log_type, log_message)
	Values (OBJECT_NAME(@@PROCID),'Info', 'Procedure a updatat Locatia')
	
	WAITFOR DELAY '00:00:05'
	ROLLBACK TRANSACTION
	INSERT INTO Logs (source_procedure, log_type, log_message)
	Values (OBJECT_NAME(@@PROCID),'Info', 'RollBack transaction , Locatie reverted')
End
go

Exec DirtyReads

--Non-Repeatable Reads
GO
Create or Alter Procedure NonRepeatableReads as
Begin
	INSERT INTO GamePublishers(nume, locatie, Venituri) VALUES
	('New Blood Interactive','California 92054, US','300000')
	INSERT INTO Logs (source_procedure, log_type, log_message)
	Values (OBJECT_NAME(@@PROCID),'Info', 'RollBack transaction , Publisher Inserted')

	BEGIN TRAN
	WAITFOR DELAY '00:00:05'
	UPDATE GamePublishers SET Locatie='Romania' 
	WHERE nume = 'New Blood Interactive'
	INSERT INTO Logs (source_procedure, log_type, log_message)
	Values (OBJECT_NAME(@@PROCID),'Info', 'RollBack transaction , Publisher Updated')
	COMMIT TRAN
End
Go

Delete from GamePublishers 	WHERE nume = 'New Blood Interactive'

Exec NonRepeatableReads

Select * from Logs

-- Phantom Reads
Go
Create or Alter Procedure PhantomReads As
Begin
	BEGIN TRANSACTION

	
	WAITFOR DELAY '00:00:05'
	INSERT INTO GamePublishers(nume, locatie, Venituri) VALUES
	('New Blood Interactive','California 92054, US','300000')
	INSERT INTO Logs (source_procedure, log_type, log_message)
	Values (OBJECT_NAME(@@PROCID),'Info', 'Inserat nou publisher')
	COMMIT TRANSACTION

	INSERT INTO Logs (source_procedure, log_type, log_message)
	Values (OBJECT_NAME(@@PROCID),'Info', 'Transaction Commited , new publisher')
end
Go

Exec PhantomReads

Delete from GamePublishers 	WHERE nume = 'New Blood Interactive'

--- Deadlock T1
Go
Create or Alter Procedure DeadLockOne As
Begin
begin tran
update GamePublishers set Nume='deadlock Books Transaction 1' where id=2
INSERT INTO Logs (source_procedure, log_type, log_message)
	Values (OBJECT_NAME(@@PROCID),'Info', 'Name changed GamePublisher id 2')
--block GamePublishers
waitfor delay '00:00:10'
update GameDeveloperi set Nume='deadlock Authors Transaction 1' where id=1
INSERT INTO Logs (source_procedure, log_type, log_message)
	Values (OBJECT_NAME(@@PROCID),'Info', 'Name changed GameDeveloperi id 1')
commit tran
End

Exec DeadLockOne