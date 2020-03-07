using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using application.Errors;
using application.Interfaces;
using domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using persistence;

namespace application.User
{
    public class Register
    {
        public class Command : IRequest<User>
        {
            public string DisplayName { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandValdiator : AbstractValidator<Command>
        {
            public CommandValdiator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();


            }
        }

        public class Handler : IRequestHandler<Command, User>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(DataContext context, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
            }

            public int Anan { get; }

            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                if(await  _context.Users.Where(u=>u.Email== request.Email).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new {Email="Email already exists"});

                if(await  _context.Users.Where(u=>u.UserName== request.Username).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new {username="Username already exists"});

                var user = new AppUser{
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    UserName = request.Username
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if(result.Succeeded)
                {
                    return new User
                    {
                        DisplayName = user.DisplayName,
                        Token = _jwtGenerator.CreateToken(user),
                        Username = user.UserName,
                        Image = null
                    };             
                }

                
                throw new Exception("Problem creating user");
            }
        }
    }
}