using MediatR;

namespace marvel.Application.Notifications
{
    public class ErrorNotification : INotification
    {
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
    }
}
