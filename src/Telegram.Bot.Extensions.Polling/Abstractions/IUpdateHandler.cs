﻿using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Extensions.Polling;

/// <summary>
/// Processes <see cref="Update"/>s and errors.
/// <para>See <see cref="DefaultUpdateHandler"/> for a simple implementation</para>
/// </summary>
[PublicAPI]
public interface IUpdateHandler
{
    /// <summary>
    /// Handles an <see cref="Update"/>
    /// </summary>
    /// <param name="botClient">
    /// The <see cref="ITelegramBotClient"/> instance of the bot receiving the <see cref="Update"/>
    /// </param>
    /// <param name="update">The <see cref="Update"/> to handle</param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> which will notify that method execution should be cancelled
    /// </param>
    /// <returns></returns>
    Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

    /// <summary>
    /// Handles an <see cref="Exception"/>
    /// </summary>
    /// <param name="botClient">
    /// The <see cref="ITelegramBotClient"/> instance of the bot receiving the <see cref="Exception"/>
    /// </param>
    /// <param name="exception">The <see cref="Exception"/> to handle</param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> which will notify that method execution should be cancelled
    /// </param>
    /// <returns></returns>
    Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken);
}
