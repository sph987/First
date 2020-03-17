using System.Threading.Tasks;
using application.Photos;
using domain;
using Infrastructure.Photos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class PhotosController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Photo>> Add([FromForm]Add.Command command)
        {
            return await Mediator.Send(command);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(string id)
        {
            var deleteCommand = new Delete.Command { Id = id };
            return await Mediator.Send(deleteCommand);
        }

        [HttpPost("{id}/setmain")]
        public async Task<ActionResult<Unit>> SetMain(string id)
        {
            return await Mediator.Send(new SetMain.Command{Id = id});
        }
    }
}