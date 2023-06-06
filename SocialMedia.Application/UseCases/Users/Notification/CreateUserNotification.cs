using MediatR;
using Serilog;
using Telegram.Bot;

namespace SocialMedia.Application.UseCases.Users.Notification
{

    public record UserCreatedNotification(string Name) : INotification;


    public class UserCreatedLogNotificationHandler : INotificationHandler<UserCreatedNotification>
    {
        public Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            Log.Information($" Social Media: New user create with {notification.Name} with username. " );
            return Task.CompletedTask;
        }
    }


    public class UserCreatedConsoleNotificationHandler : INotificationHandler<UserCreatedNotification>
    {
        public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync($"Social Media : New user create with {notification.Name} username.");
        }
    }

    public class UserCreatedTelegramNotificationHandler : INotificationHandler<UserCreatedNotification>
    {
        public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            var message = $"Social Media: New user created with  {notification.Name} username.";
            await SendTelegramNotification(message);
        }



        private static async Task SendTelegramNotification(string message)
        {
            var botToken = "6095895259:AAFlv0QIM8YHWseYzOKVM8WTucgv0BzSTms";
            var botClient = new TelegramBotClient(botToken);
            var chatId = "1468353886";
            await botClient.SendTextMessageAsync(chatId, message);
        }

    }
}
