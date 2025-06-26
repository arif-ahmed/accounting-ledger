-- usp_AddAccount
CREATE PROCEDURE usp_AddAccount
    @Name NVARCHAR(100),
    @Type NVARCHAR(50),
    @Id INT OUTPUT
AS
BEGIN
    INSERT INTO Accounts (Name, Type)
    VALUES (@Name, @Type);

    SET @Id = SCOPE_IDENTITY();
END
GO

-- usp_GetAccounts
CREATE PROCEDURE usp_GetAccounts
AS
BEGIN
    SELECT Id, Name, Type FROM Accounts
END
GO

-- usp_AddJournalEntry
CREATE PROCEDURE usp_AddJournalEntry
    @Date DATETIME,
    @Description NVARCHAR(255),
    @JournalEntryId INT OUTPUT
AS
BEGIN
    INSERT INTO JournalEntries (Date, Description)
    VALUES (@Date, @Description)

    SET @JournalEntryId = SCOPE_IDENTITY()
END
GO

-- usp_AddJournalEntryLine
CREATE PROCEDURE usp_AddJournalEntryLine
    @JournalEntryId INT,
    @AccountId INT,
    @Debit DECIMAL(18,2),
    @Credit DECIMAL(18,2)
AS
BEGIN
    INSERT INTO JournalEntryLines (JournalEntryId, AccountId, Debit, Credit)
    VALUES (@JournalEntryId, @AccountId, @Debit, @Credit)
END
GO

-- usp_GetJournalEntries
CREATE PROCEDURE usp_GetJournalEntries
AS
BEGIN
    SELECT 
        j.Id AS JournalEntryId,
        j.Date,
        j.Description,
        l.AccountId,
        a.Name AS AccountName,
        l.Debit,
        l.Credit
    FROM JournalEntries j
    JOIN JournalEntryLines l ON l.JournalEntryId = j.Id
    JOIN Accounts a ON a.Id = l.AccountId
    ORDER BY j.Date, j.Id
END


-- usp_GetTrialBalance
CREATE PROCEDURE usp_GetTrialBalance
    @AccountNameFilter NVARCHAR(100) = NULL,
    @SortBy NVARCHAR(20) = NULL,
    @SortOrder NVARCHAR(4) = 'ASC'
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        a.Name AS AccountName,
        SUM(jl.Debit) AS Debit,
        SUM(jl.Credit) AS Credit
    INTO #TrialBalanceTemp
    FROM Accounts a
    LEFT JOIN JournalEntryLines jl ON jl.AccountId = a.Id
    GROUP BY a.Name;

    IF @AccountNameFilter IS NOT NULL
    BEGIN
        DELETE FROM #TrialBalanceTemp
        WHERE AccountName NOT LIKE '%' + @AccountNameFilter + '%';
    END

    DECLARE @SortSql NVARCHAR(MAX) = N'
        SELECT AccountName, Debit, Credit
        FROM #TrialBalanceTemp
        ORDER BY ' +
        CASE 
            WHEN @SortBy = 'debit' THEN 'Debit'
            WHEN @SortBy = 'credit' THEN 'Credit'
            WHEN @SortBy = 'accountname' THEN 'AccountName'
            ELSE 'AccountName'
        END + ' ' +
        CASE 
            WHEN UPPER(@SortOrder) = 'DESC' THEN 'DESC'
            ELSE 'ASC'
        END;

    EXEC sp_executesql @SortSql;

    DROP TABLE #TrialBalanceTemp;
END