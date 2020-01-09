using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using application.Activities;
using domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator _mdeiator;
        public ActivitiesController(IMediator mdeiator)
        {
            _mdeiator = mdeiator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> List(){
            return await _mdeiator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> Details(Guid id)
        {
            return await _mdeiator.Send(new Details.Query{Id=id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mdeiator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
        {
            command.Id = id;
            return await _mdeiator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await _mdeiator.Send(new Delete.Command{Id=id});
        }
    }
}