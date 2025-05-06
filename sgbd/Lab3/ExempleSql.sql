---Logs
SELECT TOP 20 * FROM Logs ORDER BY id DESC 
---Tabels
Select Top 1 * from Esports order by id Desc
Select Top 1 * from Esports_Team_Planer order by id Desc
Select Top 1 * from Esports_Teams order by id Desc

---Procedure 1
---Exemplu eroare 1
DECLARE	@return_value int
EXEC	@return_value = [dbo].[Add_Echipa_Meci]
		@Nume_competitie = N'Competitie 1',
		@id_joc = 50,
		@Data_inceperi = '2004-01-02',
		@Premii = N'',
		@Data_Meciului = '2005-01-02',
		@Format_Meci = N'best of 3',
		@Numele_Echipei = N'Echipa 1',
		@Sponsor = N'Pepsi',
		@Numar_jucatori = 6
GO
---Exemplu eroare 2
DECLARE	@return_value int
EXEC	@return_value = [dbo].[Add_Echipa_Meci]
		@Nume_competitie = N'Competitie 1',
		@id_joc = 50,
		@Data_inceperi = '2004-01-02',
		@Premii = N'Premiile sunt mari',
		@Data_Meciului = '2005-01-02',
		@Format_Meci = N'best of 3',
		@Numele_Echipei = N'Echipa 1',
		@Sponsor = N'Pepsi',
		@Numar_jucatori = 6
GO
---Exemplu functional
DECLARE	@return_value int
EXEC	@return_value = [dbo].[Add_Echipa_Meci]
		@Nume_competitie = N'Competitie ABC',
		@id_joc = 50,
		@Data_inceperi = '2004-01-02',
		@Premii = N'Premiile sunt mari',
		@Data_Meciului = '2005-01-02',
		@Format_Meci = N'best of 3',
		@Numele_Echipei = N'Echipa 1',
		@Sponsor = N'Pepsi',
		@Numar_jucatori = 4
GO

---Procedura 2
---Exemplu eroare 1
DECLARE	@return_value int
EXEC	@return_value = [dbo].[Add_Echipa_Meci_MORE]
		@Nume_competitie = N'Competitie 2',
		@id_joc = 40,
		@Data_inceperi = '2004-02-01',
		@Premii = N'Premii si mai mari',
		@Data_Meciului = '2003-02-01',
		@Format_Meci = N'Format turneu',
		@Numele_Echipei = N'',
		@Sponsor = N'Cola',
		@Numar_jucatori = 4
GO
---Exemplu eroare 2
DECLARE	@return_value int
EXEC	@return_value = [dbo].[Add_Echipa_Meci_MORE]
		@Nume_competitie = N'Competitie DBC',
		@id_joc = 50,
		@Data_inceperi = '2004-02-01',
		@Premii = N'Premii si mai mari',
		@Data_Meciului = '2003-02-01',
		@Format_Meci = N'Format turneu',
		@Numele_Echipei = N'',
		@Sponsor = N'Cola',
		@Numar_jucatori = 4
GO
---Exemplu eroare 3
DECLARE	@return_value int
EXEC	@return_value = [dbo].[Add_Echipa_Meci_MORE]
		@Nume_competitie = N'',
		@id_joc = 50,
		@Data_inceperi = '2004-02-01',
		@Premii = N'Premii si mai mari',
		@Data_Meciului = '2003-02-01',
		@Format_Meci = N'Format turneu',
		@Numele_Echipei = N'Echipa de aur',
		@Sponsor = N'Cola',
		@Numar_jucatori = 4
GO
---Exemplu eroare 4
DECLARE	@return_value int
EXEC	@return_value = [dbo].[Add_Echipa_Meci_MORE]
		@Nume_competitie = N'Competitia 4',
		@id_joc = 50,
		@Data_inceperi = '2004-02-01',
		@Premii = N'Premii si mai mari',
		@Data_Meciului = '2003-02-01',
		@Format_Meci = N'Format turneu',
		@Numele_Echipei = N'Echipa de aur',
		@Sponsor = N'Cola',
		@Numar_jucatori = 4
GO
---Exemplu functional
DECLARE	@return_value int
EXEC	@return_value = [dbo].[Add_Echipa_Meci_MORE]
		@Nume_competitie = N'Competitia cea mai mare',
		@id_joc = 50,
		@Data_inceperi = '2004-02-01',
		@Premii = N'Premii si mai mari',
		@Data_Meciului = '2005-02-01',
		@Format_Meci = N'Format turneu',
		@Numele_Echipei = N'Echipa de aur',
		@Sponsor = N'Cola',
		@Numar_jucatori = 4
GO