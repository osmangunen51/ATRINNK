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
}
