namespace MakinaTurkiye.Services.Messages
{
    public enum MessageTypeEnum : byte
    {
        Inbox = 0,
        Send = 1,
        Outbox = 2,
        Banned = 3,
        RecyleBin = 4
    }
}
