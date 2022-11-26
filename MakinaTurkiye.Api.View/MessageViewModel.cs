using MakinaTurkiye.Services.Messages;
using System;

namespace MakinaTurkiye.Api.View
{
    public class MessageViewModel
    {
        public int TargetMainPartyId { get; set; }

        public string Subject { get; set; }
        public string Content { get; set; }
        public int ProductId { get; set; }
        public string FileName { get; set; }
    }

    public class MessageViewItem
    {
        public int? MessageId { get; set; } = 0;
        public string Subject { get; set; } = "";
        public string Content { get; set; } = "";
        public int? ProductId { get; set; } = 0;
        public string ProductName { get; set; } = "";
        public string ProductNo { get; set; } = "";
        public string ProductResim { get; set; } = "";
        public MessageTypeEnum MessageTypeEnum { get; set; } = MessageTypeEnum.Banned;
        public MessageViewMemberItem From { get; set; } = new MessageViewMemberItem();
        public MessageViewMemberItem To { get; set; } = new MessageViewMemberItem();
        public DateTime? Date { get; set; }
        public bool IsRead { get; set; } =false;
        public int UnReadMessagesCount { get; set; } =0;
    }

    public class MessageViewMemberItem
    {
        public int? MainPartyId { get; set; } = 0;
        public string FirtName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefon { get; set; } = "";

    }

}
