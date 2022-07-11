using MediatR;

namespace marvel.Application.Notifications
{
    public class InsertCharacterNotification : INotification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Modified { get; set; }
    }
}
