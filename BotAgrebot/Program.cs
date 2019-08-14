using MihaZupan;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotAgrebot
{
    class Program
    {
        private static ITelegramBotClient botClient;
        static void Main(string[] args)
        {
            var proxy = new HttpToSocks5Proxy("103.254.126.182", 61511);
            botClient = new TelegramBotClient("858962799:AAE0aYHFYjCsXyD0wyT-wk05ArDDktDeJls") { Timeout = TimeSpan.FromSeconds(10)};
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Bot ID: {me.Id}, Bot Name: {me.FirstName}.");

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            Console.ReadKey();
        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var text = e?.Message?.Text;
            if (text == null)
            {
                return;
            }
            Console.WriteLine($"recived text message '{text}' in chat '{e.Message.Chat.Id}'.");

            await botClient.SendTextMessageAsync
                (
                chatId: e.Message.Chat,
                text: $"You said '{text}'"
                )
                .ConfigureAwait(false);
        }

    }
}
