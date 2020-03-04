using System.Threading.Tasks;
using application.User;
using domain;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class UserController : BaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login( Login.Query query)
        {
            return await Mediator.Send(query);
        }
    }
}