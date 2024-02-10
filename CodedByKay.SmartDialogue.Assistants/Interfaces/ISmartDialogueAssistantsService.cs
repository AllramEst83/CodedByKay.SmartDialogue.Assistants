﻿namespace CodedByKay.SmartDialogue.Assistants.Interfaces
{
    public interface ISmartDialogueAssistantsService
    {
        Task<string> SendChatMessageAsync(Guid chatId, string message);
    }
}
