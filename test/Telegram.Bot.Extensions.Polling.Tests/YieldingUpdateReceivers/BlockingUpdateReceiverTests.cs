using System.Threading.Tasks;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Extensions.Polling.Tests.YieldingUpdateReceivers
{
    public class BlockingUpdateReceiverTests
    {
        [Fact]
        public async Task BlocksWhileProcessingAsync()
        {
            MockTelegramBotClient mockClient = new ("test", "break", "test");
            BlockingUpdateReceiver receiver = new (mockClient);

            Assert.Equal(3, mockClient.MessageGroupsLeft);

            await foreach (Update update in receiver.YieldUpdatesAsync())
            {
                if (update.Message.Text == "break")
                    break;
            }

            Assert.Equal(1, mockClient.MessageGroupsLeft);
        }

        [Fact]
        public async Task ReturnsReceivedPendingUpdates()
        {
            MockTelegramBotClient mockClient = new ("foo-bar", "123");
            BlockingUpdateReceiver receiver = new (mockClient);

            Assert.Equal(2, mockClient.MessageGroupsLeft);
            Assert.Equal(0, receiver.PendingUpdates);

            await foreach (Update update in receiver.YieldUpdatesAsync())
            {
                Assert.Equal("foo", update.Message.Text);
                break;
            }

            Assert.Equal(1, mockClient.MessageGroupsLeft);
            Assert.Equal(1, receiver.PendingUpdates);

            await foreach (Update update in receiver.YieldUpdatesAsync())
            {
                Assert.Equal("bar", update.Message.Text);
                break;
            }

            Assert.Equal(1, mockClient.MessageGroupsLeft);
            Assert.Equal(0, receiver.PendingUpdates);

            await foreach (Update update in receiver.YieldUpdatesAsync())
            {
                Assert.Equal("123", update.Message.Text);
                break;
            }

            Assert.Equal(0, mockClient.MessageGroupsLeft);
            Assert.Equal(0, receiver.PendingUpdates);
        }
    }
}
