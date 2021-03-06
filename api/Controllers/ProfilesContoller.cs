using System.Collections.Generic;
using System.Threading.Tasks;
using application.Profiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class ProfilesController : BaseController
    {
        // [HttpGet]
        // public async Task<ActionResult<Profile>> Get()
        // {
        //     string username = "Bob";
        //     return await Mediator.Send(new Details.Query{Username = username});
        // }

        [HttpGet("{username}")]
        public async Task<ActionResult<Profile>> Get(string username)
        {
            return await Mediator.Send(new Details.Query{Username = username});
        }

        [HttpPut()]
        public async Task<ActionResult<Unit>> Put(Edit.Command command){
            return await Mediator.Send(command);
        }

        [HttpGet("{username}/activities")]
        public  async Task<ActionResult<List<UserActivityDto>>> GetUserActivities(string username, string predicate)
        {
            return await Mediator.Send(new ListActivities.Query{Username=username, Predicate=predicate});
        } 

    }
}