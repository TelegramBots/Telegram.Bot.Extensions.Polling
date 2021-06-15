using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Requests;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;

#nullable disable

namespace Telegram.Bot.Extensions.Polling.Tests
{
    public class MockTelegramBotClient : ITelegramBotClient
    {
        private readonly Queue<string[]> _messages;
        public int MessageGroupsLeft => _messages.Count;

        public MockTelegramBotClient(params string[] messages)
        {
            _messages = new Queue<string[]>(messages.Select(message => message.Split('-').ToArray()));
        }

        public async Task<TResponse> MakeRequestAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            if (request is not GetUpdatesRequest getUpdatesRequest)
                throw new NotImplementedException();

            await Task.Delay(10, cancellationToken);

            if (!_messages.TryDequeue(out string[] messages))
                return (TResponse)(object)Array.Empty<Update>();

            return (TResponse)(object)messages.Select((message, i) => new Update()
            {
                Message = new Message()
                {
                    Text = messages[i]
                },
                Id = getUpdatesRequest.Offset + i + 1
            }).ToArray();
        }

        public TimeSpan Timeout { get; set; } = TimeSpan.FromMilliseconds(50);

        // ---------------
        // NOT IMPLEMENTED
        // ---------------

#pragma warning disable CS0067

        public long? BotId => throw new NotImplementedException();
        public bool IsReceiving => throw new NotImplementedException();
        public int MessageOffset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event AsyncEventHandler<ApiRequestEventArgs> OnMakingApiRequest;
        public event AsyncEventHandler<ApiResponseEventArgs> OnApiResponseReceived;
        public event EventHandler<ApiRequestEventArgs> MakingApiRequest;
        public event EventHandler<ApiResponseEventArgs> ApiResponseReceived;
        public event EventHandler<UpdateEventArgs> OnUpdate;
        public event EventHandler<MessageEventArgs> OnMessage;
        public event EventHandler<MessageEventArgs> OnMessageEdited;
        public event EventHandler<InlineQueryEventArgs> OnInlineQuery;
        public event EventHandler<ChosenInlineResultEventArgs> OnInlineResultChosen;
        public event EventHandler<CallbackQueryEventArgs> OnCallbackQuery;
        public event EventHandler<ReceiveErrorEventArgs> OnReceiveError;
        public event EventHandler<ReceiveGeneralErrorEventArgs> OnReceiveGeneralError;

        public Task AddAnimatedStickerToSetAsync(long userId, string name, InputFileStream tgsSticker, string emojis, MaskPosition maskPosition, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task AddStickerToSetAsync(long userId, string name, InputOnlineFile pngSticker, string emojis, MaskPosition maskPosition, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task AnswerCallbackQueryAsync(string callbackQueryId, string text, bool showAlert, string url, int cacheTime, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task AnswerInlineQueryAsync(string inlineQueryId, IEnumerable<InlineQueryResultBase> results, int? cacheTime, bool isPersonal, string nextOffset, string switchPmText, string switchPmParameter, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task AnswerPreCheckoutQueryAsync(string preCheckoutQueryId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task AnswerPreCheckoutQueryAsync(string preCheckoutQueryId, string errorMessage, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task AnswerShippingQueryAsync(string shippingQueryId, IEnumerable<ShippingOption> shippingOptions, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task AnswerShippingQueryAsync(string shippingQueryId, string errorMessage, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task CloseAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<MessageId> CopyMessageAsync(ChatId chatId, ChatId fromChatId, int messageId, string caption, ParseMode parseMode, IEnumerable<MessageEntity> captionEntities, int replyToMessageId, bool disableNotification, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<ChatInviteLink> CreateChatInviteLinkAsync(ChatId chatId, DateTime? expireDate, int? memberLimit, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task CreateNewAnimatedStickerSetAsync(long userId, string name, string title, InputFileStream tgsSticker, string emojis, bool isMasks, MaskPosition maskPosition, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task CreateNewStickerSetAsync(long userId, string name, string title, InputOnlineFile pngSticker, string emojis, bool isMasks, MaskPosition maskPosition, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task DeleteChatPhotoAsync(ChatId chatId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task DeleteChatStickerSetAsync(ChatId chatId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task DeleteMessageAsync(ChatId chatId, int messageId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task DeleteStickerFromSetAsync(string sticker, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task DeleteWebhookAsync(bool dropPendingUpdates, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task DownloadFileAsync(string filePath, Stream destination, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<ChatInviteLink> EditChatInviteLinkAsync(ChatId chatId, string inviteLink, DateTime? expireDate, int? memberLimit, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> EditMessageCaptionAsync(ChatId chatId, int messageId, string caption, ParseMode parseMode, IEnumerable<MessageEntity> captionEntities, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task EditMessageCaptionAsync(string inlineMessageId, string caption, ParseMode parseMode, IEnumerable<MessageEntity> captionEntities, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> EditMessageLiveLocationAsync(ChatId chatId, int messageId, float latitude, float longitude, float horizontalAccuracy, int heading, int proximityAlertRadius, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task EditMessageLiveLocationAsync(string inlineMessageId, float latitude, float longitude, float horizontalAccuracy, int heading, int proximityAlertRadius, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> EditMessageMediaAsync(ChatId chatId, int messageId, InputMediaBase media, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task EditMessageMediaAsync(string inlineMessageId, InputMediaBase media, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> EditMessageReplyMarkupAsync(ChatId chatId, int messageId, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task EditMessageReplyMarkupAsync(string inlineMessageId, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> EditMessageTextAsync(ChatId chatId, int messageId, string text, ParseMode parseMode, IEnumerable<MessageEntity> entities, bool disableWebPagePreview, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task EditMessageTextAsync(string inlineMessageId, string text, ParseMode parseMode, IEnumerable<MessageEntity> entities, bool disableWebPagePreview, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<string> ExportChatInviteLinkAsync(ChatId chatId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> ForwardMessageAsync(ChatId chatId, ChatId fromChatId, int messageId, bool disableNotification, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<ChatMember[]> GetChatAdministratorsAsync(ChatId chatId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Chat> GetChatAsync(ChatId chatId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<ChatMember> GetChatMemberAsync(ChatId chatId, long userId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<int> GetChatMembersCountAsync(ChatId chatId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Types.File> GetFileAsync(string fileId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<GameHighScore[]> GetGameHighScoresAsync(long userId, long chatId, int messageId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<GameHighScore[]> GetGameHighScoresAsync(long userId, string inlineMessageId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Types.File> GetInfoAndDownloadFileAsync(string fileId, Stream destination, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<User> GetMeAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<BotCommand[]> GetMyCommandsAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<StickerSet> GetStickerSetAsync(string name, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Update[]> GetUpdatesAsync(int offset, int limit, int timeout, IEnumerable<UpdateType> allowedUpdates, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<UserProfilePhotos> GetUserProfilePhotosAsync(long userId, int offset, int limit, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<WebhookInfo> GetWebhookInfoAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task KickChatMemberAsync(ChatId chatId, long userId, DateTime untilDate, bool? revokeMessages, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task LeaveChatAsync(ChatId chatId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task LogOutAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task PinChatMessageAsync(ChatId chatId, int messageId, bool disableNotification, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task PromoteChatMemberAsync(ChatId chatId, long userId, bool? isAnonymous, bool? canManageChat, bool? canChangeInfo, bool? canPostMessages, bool? canEditMessages, bool? canDeleteMessages, bool? canManageVoiceChats, bool? canInviteUsers, bool? canRestrictMembers, bool? canPinMessages, bool? canPromoteMembers, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task RestrictChatMemberAsync(ChatId chatId, long userId, ChatPermissions permissions, DateTime untilDate, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<ChatInviteLink> RevokeChatInviteLinkAsync(ChatId chatId, string inviteLink, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendAnimationAsync(ChatId chatId, InputOnlineFile animation, int duration, int width, int height, InputMedia thumb, string caption, ParseMode parseMode, IEnumerable<MessageEntity> captionEntities, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendAudioAsync(ChatId chatId, InputOnlineFile audio, string caption, ParseMode parseMode, IEnumerable<MessageEntity> captionEntities, int duration, string performer, string title, InputMedia thumb, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task SendChatActionAsync(ChatId chatId, ChatAction chatAction, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendContactAsync(ChatId chatId, string phoneNumber, string firstName, string lastName, string vCard, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendDiceAsync(ChatId chatId, Emoji? emoji, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendDocumentAsync(ChatId chatId, InputOnlineFile document, InputMedia thumb, string caption, ParseMode parseMode, IEnumerable<MessageEntity> captionEntities, bool disableContentTypeDetection, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendGameAsync(long chatId, string gameShortName, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendInvoiceAsync(long chatId, string title, string description, string payload, string providerToken, string currency, IEnumerable<LabeledPrice> prices, int maxTipAmount, int[] suggestedTipAmounts, string startParameter, string providerData, string photoUrl, int photoSize, int photoWidth, int photoHeight, bool needName, bool needPhoneNumber, bool needEmail, bool needShippingAddress, bool sendPhoneNumberToProvider, bool sendEmailToProvider, bool isFlexible, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendLocationAsync(ChatId chatId, float latitude, float longitude, int livePeriod, int heading, int proximityAlertRadius, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message[]> SendMediaGroupAsync(ChatId chatId, IEnumerable<IAlbumInputMedia> media, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendPhotoAsync(ChatId chatId, InputOnlineFile photo, string caption, ParseMode parseMode, IEnumerable<MessageEntity> captionEntities, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendPollAsync(ChatId chatId, string question, IEnumerable<string> options, bool? isAnonymous, PollType? type, bool? allowsMultipleAnswers, int? correctOptionId, string explanation, ParseMode explanationParseMode, IEnumerable<MessageEntity> explanationEntities, int? openPeriod, DateTime? closeDate, bool? isClosed, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendStickerAsync(ChatId chatId, InputOnlineFile sticker, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendTextMessageAsync(ChatId chatId, string text, ParseMode parseMode, IEnumerable<MessageEntity> entities, bool disableWebPagePreview, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendVenueAsync(ChatId chatId, float latitude, float longitude, string title, string address, string foursquareId, string foursquareType, string googlePlaceId, string googlePlaceType, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendVideoAsync(ChatId chatId, InputOnlineFile video, int duration, int width, int height, InputMedia thumb, string caption, ParseMode parseMode, IEnumerable<MessageEntity> captionEntities, bool supportsStreaming, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendVideoNoteAsync(ChatId chatId, InputTelegramFile videoNote, int duration, int length, InputMedia thumb, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SendVoiceAsync(ChatId chatId, InputOnlineFile voice, string caption, ParseMode parseMode, IEnumerable<MessageEntity> captionEntities, int duration, bool disableNotification, int replyToMessageId, bool allowSendingWithoutReply, IReplyMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task SetChatAdministratorCustomTitleAsync(ChatId chatId, long userId, string customTitle, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task SetChatDescriptionAsync(ChatId chatId, string description, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task SetChatPermissionsAsync(ChatId chatId, ChatPermissions permissions, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task SetChatPhotoAsync(ChatId chatId, InputFileStream photo, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task SetChatStickerSetAsync(ChatId chatId, string stickerSetName, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task SetChatTitleAsync(ChatId chatId, string title, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> SetGameScoreAsync(long userId, int score, long chatId, int messageId, bool force, bool disableEditMessage, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task SetGameScoreAsync(long userId, int score, string inlineMessageId, bool force, bool disableEditMessage, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task SetMyCommandsAsync(IEnumerable<BotCommand> commands, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task SetStickerPositionInSetAsync(string sticker, int position, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task SetStickerSetThumbAsync(string name, long userId, InputOnlineFile thumb, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task SetWebhookAsync(string url, InputFileStream certificate, string ipAddress, int maxConnections, IEnumerable<UpdateType> allowedUpdates, bool dropPendingUpdates, CancellationToken cancellationToken) => throw new NotImplementedException();
        public void StartReceiving(UpdateType[] allowedUpdates, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Message> StopMessageLiveLocationAsync(ChatId chatId, int messageId, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task StopMessageLiveLocationAsync(string inlineMessageId, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Poll> StopPollAsync(ChatId chatId, int messageId, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken) => throw new NotImplementedException();
        public void StopReceiving() => throw new NotImplementedException();
        public Task<bool> TestApiAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task UnbanChatMemberAsync(ChatId chatId, long userId, bool onlyIfBanned, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task UnpinAllChatMessages(ChatId chatId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task UnpinChatMessageAsync(ChatId chatId, int messageId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Types.File> UploadStickerFileAsync(long userId, InputFileStream pngSticker, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
