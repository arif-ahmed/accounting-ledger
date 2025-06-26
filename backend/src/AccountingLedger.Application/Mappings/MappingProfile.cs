using AccountingLedger.Application.Queries;
using AccountingLedger.Domain.Entities;
using AutoMapper;

namespace AccountingLedger.Application.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Account → AccountDto
        CreateMap<Account, AccountDto>().ReverseMap();

        // JournalEntry → JournalEntryDto
        CreateMap<JournalEntry, JournalEntryDto>();
        CreateMap<JournalEntryLine, JournalEntryLineDto>();
    }
}
