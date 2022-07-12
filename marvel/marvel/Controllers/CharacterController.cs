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

        [Route("/v1/public/characters")]
        [HttpGet]
        public async Task<IActionResult> GetAllCharacters(int orderBy = 0) => Ok(await repository.GetAllCharacters(orderBy));

        [Route("/v1/public/character-by-name")]
        [HttpGet]
        public async Task<IActionResult> GetCharactersByName(string name) => Ok(await repository.GetCharactersByName(name));

        [Route("/v1/public/favorite-characters")]
        [HttpGet]
        public async Task<IActionResult> GetFavoriteCharacters() => Ok(await repository.GetFavoritesCharacters());

        [Route("/v1/public/insert-character")]
        [HttpPost]
        public async Task<IActionResult> Post(InsertCharacterCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [Route("/v1/public/delete-character")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = new DeleteCharacterCommand { Id = id };
            var result = await mediator.Send(obj);
            return Ok(result);
        }
    }
}
