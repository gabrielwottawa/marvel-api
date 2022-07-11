using MediatR;

namespace marvel.Application.Commands
{
    public class DeleteCharacterCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
