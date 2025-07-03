﻿namespace AccountingLedger.Application.Queries;
public class JournalEntryDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<JournalEntryLineDto> Lines { get; set; } = new();
}