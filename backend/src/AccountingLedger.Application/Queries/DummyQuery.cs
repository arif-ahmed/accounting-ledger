using MediatR;

namespace AccountingLedger.Application.Queries
{
    public class DummyQuery : IRequest<int>
    {
        public string? Query { get; set; }
    }

    public class DummyQueryHandler : IRequestHandler<DummyQuery, int>
    {
        public Task<int> Handle(DummyQuery request, CancellationToken cancellationToken)
        {
            // Simulate some processing
            Console.WriteLine($"Processing query: {request.Query}");
            return Task.FromResult(1);
        }
    }
}
