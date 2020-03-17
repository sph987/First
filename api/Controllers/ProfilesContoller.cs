using System.Threading.Tasks;
using application.Profiles;
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

    }
}