using MediatR;

namespace marvel.Application.Commands
{
    public class InsertCharacterCommand : IRequest<string>
    {
        public string Name { get; set; }
        public int DeveloperMarvelId { get; set; }
    }
}
