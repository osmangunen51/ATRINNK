using System;

namespace MMakinaTurkiye.Api.View
{
    public class Message
    {
        public string Header { get; set; } ="";
        public string Text { get; set; } = "";
    }

    public  class ProcessStatus
    {
        public bool Status { get; set; } = false;
        public object Result { get; set; }
        public Exception Error { get; set; } = null;
        public Message Message { get; set; } = new Message();
    }
}
