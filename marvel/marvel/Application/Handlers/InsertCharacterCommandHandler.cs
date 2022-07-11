using marvel.Application.Commands;
using marvel.Application.Notifications;
using marvel.Database.Characters;
using marvel.Models;
using marvel.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace marvel.Application.Handlers
{
    public class InsertCharacterCommandHandler : IRequestHandler<InsertCharacterCommand, string>
    {
        private readonly IMediator mediator;
        private readonly IRepository<CharacterModel> repository;

        public InsertCharacterCommandHandler(IMediator mediator, IRepository<CharacterModel> repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }

        public async Task<string> Handle(InsertCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = new CharacterModel
            {
                Name = request.Name,
                Id = request.DeveloperMarvelId
            };

            try
            {
                if (verifyQtyCharacter())
                    return await Task.FromResult("Número máximo de personagens favoritos atingido.");
                else
                {
                    await repository.Add(character);
                    await mediator.Publish(new InsertCharacterNotification { Id = character.Id, Name = character.Name, Description = character.Description, Modified = character.Modified });
                    return await Task.FromResult("Personagem favorito salvo com sucesso.");
                }

            } catch (Exception ex)
            {
                await mediator.Publish(new InsertCharacterNotification { Id = character.Id, Name = character.Name, Description = character.Description, Modified = character.Modified });
                await mediator.Publish(new ErrorNotification { ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro na inserção dos dados.");
            }
        }

        private bool verifyQtyCharacter()
        {
            var charactersReader = new CharactersReader();
            var result = charactersReader.SelectCharacter();
            return result.Count >= 5;
        }
    }
}
