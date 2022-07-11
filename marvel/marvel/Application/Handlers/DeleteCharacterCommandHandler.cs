using marvel.Application.Commands;
using marvel.Application.Notifications;
using marvel.Models;
using marvel.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace marvel.Application.Handlers
{
    public class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, string>
    {
        private readonly IMediator mediator;
        private readonly IRepository<CharacterModel> repository;

        public DeleteCharacterCommandHandler(IMediator mediator, IRepository<CharacterModel> repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }

        public async Task<string> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await repository.Delete(request.Id);

                await mediator.Publish(new DeleteCharacterNotification { Id = request.Id });

                return await Task.FromResult("Registro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                await mediator.Publish(new DeleteCharacterNotification { Id = request.Id });
                await mediator.Publish(new ErrorNotification { ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro no momento da exclusão.");
            }
        }
    }
}
