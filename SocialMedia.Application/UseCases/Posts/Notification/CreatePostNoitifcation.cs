using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace SocialMedia.Application.UseCases.Posts.Notification
{
    public record PostCreatedNotification(string Name, string Username) : INotification;


    public class PostCreatedLogNotificationHandler : INotificationHandler<PostCreatedNotification>
    {
        public Task Handle(PostCreatedNotification notification, CancellationToken cancellationToken)
        {
            Log.Information($" Social Media: New post create with {notification.Name} with postname. ");
            return Task.CompletedTask;
        }
    }


    public class PostCreatedConsoleNotificationHandler : INotificationHandler<PostCreatedNotification>
    {
        public async Task Handle(PostCreatedNotification notification, CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync($"{notification.Username} sent this  New post create with {notification.Name} postname.");
        }
    }

    public class PostCreatedTelegramNotificationHandler : INotificationHandler<PostCreatedNotification>
    {
        public async Task Handle(PostCreatedNotification notification, CancellationToken cancellationToken)
        {
            var message = $" {notification.Username} send this =>   {notification.Name} ";
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
