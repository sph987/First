using System;
using System.Threading;
using System.Threading.Tasks;
using application.Errors;
using domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using persistence;

namespace application.Activities
{
    public class Details
    {
        public class Query : IRequest<Activity>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Activity>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }
            public async Task<Activity> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities
                    .Include(x=>x.UserActivities)
                    .ThenInclude(x=>x.AppUser)
                    .SingleOrDefaultAsync(x=>x.Id == request.Id);

                if (activity==null) 
                    throw new RestException(System.Net.HttpStatusCode.NotFound, new {activity ="not found"});

                return activity;
            }
        }
    }
}