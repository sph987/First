using System;
using System.Threading;
using System.Threading.Tasks;
using application.Errors;
using MediatR;
using persistence;

namespace application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                if (activity==null) 
                    throw new RestException(System.Net.HttpStatusCode.NotFound, new {activity ="not found"});

                _context.Remove(activity);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                throw new Exception("Problem saving Activity");
            }
        }
    }
}