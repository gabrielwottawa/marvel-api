using marvel.Application.Commands;
using marvel.Models;
using marvel.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace marvel.Controllers
{
    public class CharacterController : Controller
    {
        private readonly IMediator mediator;
        private readonly IRepository<CharacterModel> repository;

        public CharacterController(IMediator mediator, IRepository<CharacterModel> repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }

        [Route("/v1/public/insert-character")]
        [HttpPost]
        public async Task<IActionResult> Post(InsertCharacterCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [Route("/v1/public/delete-character")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = new DeleteCharacterCommand { Id = id };
            var result = await mediator.Send(obj);
            return Ok(result);
        }
    }
}
