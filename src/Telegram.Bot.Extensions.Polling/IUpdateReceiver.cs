using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.Polling
{
    /// <summary>
    /// Requests new <see cref="Update"/>s and processes them using provided <see cref="IUpdateHandler"/> instance
    /// </summary>
    public interface IUpdateReceiver
    {
        /// <summary>
        /// Starts receiving <see cref="Update"/>s invoking <see cref="IUpdateHandler.HandleUpdateAsync"/>
        /// for each <see cref="Update"/>.
        /// <para>This method will block if awaited.</para>
        /// </summary>
        /// <param name="updateHandler">
        /// The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> with which you can stop receiving
        /// </param>
        /// <returns></returns>
        Task ReceiveAsync(
            IUpdateHandler updateHandler,
            CancellationToken cancellationToken = default);
    }
}