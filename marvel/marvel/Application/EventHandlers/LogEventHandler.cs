using marvel.Application.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace marvel.Application.EventHandlers
{
    public class LogEventHandler : INotificationHandler<InsertCharacterNotification>, INotificationHandler<DeleteCharacterNotification>, INotificationHandler<ErrorNotification>
    {
        public Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ERROR: '{notification.ExceptionMessage} \n {notification.StackTrace}'");
            });
        }

        public Task Handle(InsertCharacterNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"INSERT: '{notification.Id} - {notification.Name} - {notification.Description}'");
            });
        }

        public Task Handle(DeleteCharacterNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"DELETE: '{notification.Id}'");
            });
        }
    }
}
