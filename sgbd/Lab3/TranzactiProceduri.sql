
CREATE TABLE Logs (
	id INT PRIMARY KEY IDENTITY,
	source_procedure NVARCHAR(256),
	log_type Nvarchar(30),
	log_date DATETIME DEFAULT GETDATE(),
	log_message VARCHAR(MAX)
)
GO

Create or Alter Procedure Add_Echipa_Meci
	@Nume_competitie Nvarchar(60),
	@id_joc Int,
	@Data_inceperi Date,
	@Premii Nvarchar(300),
	@Data_Meciului Date,
	@Format_Meci Nvarchar(300),
	@Numele_Echipei Nvarchar(60),
	@Sponsor Nvarchar(60),
	@Numar_jucatori int
As Begin
	Begin Try
		Begin Transaction
		If @Nume_competitie = ''
			RAISERROR('Nume competitie este gol!',16,1)
		If (Select Count(*) from Esports Where @Nume_competitie = Numele_Competitiei) = 0
		Begin
			If (Select Count(*) from JocuriVideo Where id = @id_joc) = 0
				RAISERROR('Jocul nu exista',16,1)
			If @Data_inceperi = NULL or @Data_inceperi < '2004-01-01' 
				RAISERROR('Data concursului invalida!',16,1)
			If @Premii = ''
				RAISERROR('Campul premii este gol',16,1)

			Insert into Esports(Numele_Competitiei,id_j,Data_Inceperi,Premii)
			Values (@Nume_competitie,@id_joc,@Data_inceperi,@Premii)
			INSERT INTO Logs (source_procedure, log_type, log_message)
			Values (OBJECT_NAME(@@PROCID),'Info', 'Competitia ' + @Nume_competitie + 's-a inserat')
		End
		Else
			INSERT INTO Logs (source_procedure, log_type, log_message)
			Values (OBJECT_NAME(@@PROCID),'Info', 'Competitia ' + @Nume_competitie + ' exista in baza de date')

		If @Numele_Echipei = ''
			RAISERROR('Numele echipei este gol!',16,1)
		If (Select Count(*) from Esports_Teams Where @Numele_Echipei = Numele_Echipei) = 0
		Begin
			If @Sponsor = ''
				RAISERROR('Sponsor este gol!',16,1)
			If Not(@Numar_jucatori between 2 and 5)
				RAISERROR('Numar jucatori invalid!',16,1)

			Insert into Esports_Teams(Numele_Echipei,Numar_meciuri,Numar_victori,Sponsor,Numar_jucatori)
			Values(@Numele_Echipei,0,0,@Sponsor,@Numar_jucatori)
			INSERT INTO Logs (source_procedure, log_type, log_message)
			Values (OBJECT_NAME(@@PROCID),'Info', 'Echipa ' + @Numele_Echipei + 's-a inserat')
		End
		Else
			INSERT INTO Logs (source_procedure, log_type, log_message)
			Values (OBJECT_NAME(@@PROCID),'Info', 'Echipa ' + @Numele_Echipei + ' exista in Baza de date')

		Declare @id_c Int = (Select id from Esports where @Nume_competitie = Numele_Competitiei)
		Declare @id_e Int = (Select id from Esports_Teams where @Numele_Echipei = Numele_Echipei)

		If @Data_Meciului < @Data_inceperi or @Data_Meciului = NULL
			RAISERROR('Data meciului este invalida',16,1)
		If @Format_Meci = ''
			RAISERROR('Format meci este gol!',16,1)
		If @Data_Meciului = (Select Data_Meciului from Esports_Team_Planer where id_e=@id_e and id_t=@id_c) 
			RAISERROR('Echipa are deja meciu in aceasta data',16,1)

		Insert Into Esports_Team_Planer(id_e,id_t,Data_Meciului,Format_Meci)
		Values (@id_e,@id_c,@Data_Meciului,@Format_Meci)
		INSERT INTO Logs (source_procedure, log_type, log_message)
		Values (OBJECT_NAME(@@PROCID),'Info', 'Echipa ' + @Numele_Echipei + ' are un meci la ' + @Nume_competitie + ' pe data de ' + @Data_Meciului)
		Commit Transaction
	End Try
	Begin Catch
		Rollback Transaction
		INSERT INTO Logs (source_procedure, log_type, log_message)
		Values (OBJECT_NAME(@@PROCID), 'ERROR' , ERROR_MESSAGE())
	End Catch
End
Go

Create or ALTER   Procedure [dbo].[Add_Echipa_Meci_MORE]
	@Nume_competitie Nvarchar(60) = Null,
	@id_joc Int,
	@Data_inceperi Date = Null,
	@Premii Nvarchar(300) = Null,
	@Data_Meciului Date = Null,
	@Format_Meci Nvarchar(300) = Null,
	@Numele_Echipei Nvarchar(60) = Null,
	@Sponsor Nvarchar(60) = Null,
	@Numar_jucatori int
As Begin
	Declare @id_e Int = -1
	Declare @id_t Int = -1
	---Prima tranzactie
	Begin Try
		Begin Transaction

		If @Nume_competitie = '' or @Nume_competitie = Null
			RAISERROR('Nume competitie este gol!',16,1)
		If (Select Count(*) from Esports Where @Nume_competitie = Numele_Competitiei) = 0
		Begin
			If (Select Count(*) from JocuriVideo Where id = @id_joc) = 0
				RAISERROR('Jocul nu exista',16,1)
			If @Data_inceperi = NULL or @Data_inceperi < '2004-01-01' 
				RAISERROR('Data concursului invalida!',16,1)
			If @Premii = '' or @Premii = Null
				RAISERROR('Campul premii este gol',16,1)

			Insert into Esports(Numele_Competitiei,id_j,Data_Inceperi,Premii)
			Values (@Nume_competitie,@id_joc,@Data_inceperi,@Premii)
			INSERT INTO Logs (source_procedure, log_type, log_message)
			Values (OBJECT_NAME(@@PROCID),'Info', 'Competitia ' + @Nume_competitie + ' s-a inserat')
		End
		Else
			INSERT INTO Logs (source_procedure, log_type, log_message)
			Values (OBJECT_NAME(@@PROCID),'Info', 'Competitia ' + @Nume_competitie + ' exista in baza de date')

		Set @id_e = (Select id from Esports where @Nume_competitie = Numele_competitiei)

		Commit Transaction
	End Try
	Begin Catch
		Rollback Transaction
		INSERT INTO Logs (source_procedure, log_type, log_message)
		Values (OBJECT_NAME(@@PROCID), 'ERROR' , ERROR_MESSAGE())
	End Catch

	--- A doua Tranzactie
	Begin Try
		Begin Transaction

		If @Numele_Echipei = '' or @Numele_Echipei = Null
			RAISERROR('Numele echipei este gol!',16,1)
		If (Select Count(*) from Esports_Teams Where @Numele_Echipei = Numele_Echipei) = 0
		Begin
			If @Sponsor = '' or @Sponsor = Null
				RAISERROR('Sponsor este gol!',16,1)
			If Not(@Numar_jucatori between 2 and 5)
				RAISERROR('Numar jucatori invalid!',16,1)

			Insert into Esports_Teams(Numele_Echipei,Numar_meciuri,Numar_victori,Sponsor,Numar_jucatori)
			Values(@Numele_Echipei,0,0,@Sponsor,@Numar_jucatori)
			INSERT INTO Logs (source_procedure, log_type, log_message)
			Values (OBJECT_NAME(@@PROCID),'Info', 'Echipa ' + @Numele_Echipei + ' s-a inserat')
		End
		Else
			INSERT INTO Logs (source_procedure, log_type, log_message)
			Values (OBJECT_NAME(@@PROCID),'Info', 'Echipa ' + @Numele_Echipei + ' exista in Baza de date')

		Set @id_t = (Select id from Esports_Teams where @Numele_Echipei=Numele_Echipei)

		Commit Transaction
	End Try
	Begin Catch

		Rollback Transaction
		INSERT INTO Logs (source_procedure, log_type, log_message)
		Values (OBJECT_NAME(@@PROCID), 'ERROR' , ERROR_MESSAGE())
	
	End Catch
	-- A treia Tranzactie

	Begin Try
		Begin Transaction
			
		If @id_e = -1 or @id_t = -1
			RAISERROR('Una din cele 2 inserari nu s-au facut!',16,1)
		If @Data_Meciului = NULL or @Data_Meciului < @Data_inceperi 
			RAISERROR('Data meciului este invalida',16,1)
		If @Format_Meci = '' or @Format_Meci = Null
			RAISERROR('Format meci este gol!',16,1)
		If @Data_Meciului = (Select Data_Meciului from Esports_Team_Planer where id_e=@id_e and id_t=@id_t) 
			RAISERROR('Echipa are deja meciu in aceasta data',16,1)

		Insert Into Esports_Team_Planer(id_e,id_t,Data_Meciului,Format_Meci)
		Values (@id_e,@id_t,@Data_Meciului,@Format_Meci)
		INSERT INTO Logs (source_procedure, log_type, log_message)
		Values (OBJECT_NAME(@@PROCID),'Info', 'Echipa ' + @Numele_Echipei + ' are un meci la ' + @Nume_competitie + ' pe data de ' + Cast(@Data_Meciului as nvarchar))

		Commit Transaction
	End Try
	Begin Catch
		Rollback Transaction
		INSERT INTO Logs (source_procedure, log_type, log_message)
		Values (OBJECT_NAME(@@PROCID), 'ERROR' , ERROR_MESSAGE())
	End Catch
End