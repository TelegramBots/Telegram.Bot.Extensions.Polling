using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Extensions.Polling.Tests
{
    public class ReceiveAsyncTests
    {
        [Fact]
        public async Task ReceivesUpdatesAndRespectsTheCancellationToken()
        {
            MockTelegramBotClient bot = new ("start-end", "foo");

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            int updateCount = 0;
            async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                updateCount++;
                Assert.Contains(update.Message.Text, "start end");
                await Task.Delay(10, cancellationTokenSource.Token);
                if (update.Message.Text == "end")
                    cancellationTokenSource.Cancel();
            }

            DefaultUpdateHandler updateHandler = new (
                HandleUpdate,
                errorHandler: async (client, e, token) => await Task.Delay(10, token)
            );

            CancellationToken cancellationToken = cancellationTokenSource.Token;
            await bot.ReceiveAsync(updateHandler, cancellationToken);

            Assert.True(cancellationToken.IsCancellationRequested);
            Assert.Equal(2, updateCount);
            Assert.Equal(1, bot.MessageGroupsLeft);
        }

        [Fact]
        public async Task UserExceptionsPropagateToSurface()
        {
            MockTelegramBotClient bot = new ("foo-bar", "throw");

            int updateCount = 0;
            async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                updateCount++;
                await Task.Delay(10, cancellationToken);
                if (update.Message.Text == "throw")
                    throw new InvalidOperationException("Oops");
            }

            DefaultUpdateHandler updateHandler = new (
                HandleUpdate,
                errorHandler: async (client, e, token) => await Task.Delay(10, token)
            );

            try
            {
                await bot.ReceiveAsync(updateHandler);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.IsType<InvalidOperationException>(ex);
                Assert.Contains("Oops", ex.Message);
            }

            Assert.Equal(3, updateCount);
            Assert.Equal(0, bot.MessageGroupsLeft);
        }
    }
}
