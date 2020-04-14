using System;
using System.Threading;
using System.Threading.Tasks;
using application.Errors;
using application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using persistence;

namespace application.Profiles
{
    public class Edit
    {
        public class Command : IRequest
        {
            public string DisplayName { get; set; }
            public string Bio { get; set; }
        }



        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Bio).NotEmpty();
                    RuleFor(x => x.DisplayName).NotEmpty();
                }
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUsername = _userAccessor.GetCurrentUsername();
                var profile = await _context.Users.SingleOrDefaultAsync(u => u.UserName == currentUsername);
                if (profile == null) throw new RestException(System.Net.HttpStatusCode.NotFound, new { Profile = "not found" });

                profile.Bio = request.Bio ?? profile.Bio;
                profile.DisplayName = request.DisplayName ?? profile.DisplayName;

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                throw new Exception("Problem saving Profile");
            }
        }
    }
}