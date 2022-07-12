using marvel.Application.Commands;
using marvel.Application.Notifications;
using marvel.Database;
using marvel.Database.Characters;
using marvel.Models;
using marvel.Repositories;
using marvel.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace marvel.Application.Handlers
{
    public class InsertCharacterCommandHandler : IRequestHandler<InsertCharacterCommand, string>
    {
        private readonly IMediator mediator;
        private readonly IRepository<CharacterModel> repository;
        private CharactersReader charactersReader;
        private List<CharactersEntity> charactersEntity = null;

        public InsertCharacterCommandHandler(IMediator mediator, IRepository<CharacterModel> repository)
        {
            this.mediator = mediator;
            this.repository = repository;

            charactersReader = new CharactersReader();
        }

        public async Task<string> Handle(InsertCharacterCommand request, CancellationToken cancellationToken)
        {
            charactersEntity = charactersReader.SelectCharacter();

            var character = new CharacterModel
            {
                Name = request.Name,
                Id = request.DeveloperMarvelId
            };

            try
            {
                if (!verifyExistsCharacter(character))
                    return await Task.FromResult("Personagem não existe.");
                if (verifyCharacter(character))
                    return await Task.FromResult("Personagem já adicionado a sua lista de favoritos.");
                if (verifyQtyCharacter())
                    return await Task.FromResult("Número máximo de personagens favoritos atingido.");

                await repository.Add(character);
                await mediator.Publish(new InsertCharacterNotification { Id = character.Id, Name = character.Name, Description = character.Description, Modified = character.Modified });
                return await Task.FromResult("Personagem favorito salvo com sucesso.");

            }
            catch (Exception ex)
            {
                await mediator.Publish(new InsertCharacterNotification { Id = character.Id, Name = character.Name, Description = character.Description, Modified = character.Modified });
                await mediator.Publish(new ErrorNotification { ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro na inserção dos dados.");
            }
        }

        private bool verifyExistsCharacter(CharacterModel character)
        {
            var marvelController = new RequestApiMarvel();
            var characters = marvelController.GetCharactersByName(character.Name);
            return characters != null && characters.Where(el => el.Id == character.Id && el.Name == character.Name).Any();
        }

        private bool verifyCharacter(CharacterModel character) => charactersEntity.Where(el => el.Name == character.Name && el.DeveloperMarvelId == character.Id).Any();

        private bool verifyQtyCharacter() => charactersEntity.Count >= 5;

    }
}
