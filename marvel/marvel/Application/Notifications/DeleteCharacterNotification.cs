using MediatR;

namespace marvel.Application.Notifications
{
    public class DeleteCharacterNotification : INotification
    {
        public int Id { get; set; }
    }
}
