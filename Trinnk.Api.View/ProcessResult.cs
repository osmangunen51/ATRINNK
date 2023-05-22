using System;

namespace Trinnk.Api.View
{
    public class Message
    {
        public string Header { get; set; } = "";
        public string Text { get; set; } = "";
    }

    public class ProcessResult
    {
        public bool Status { get; set; } = false;
        public object Result { get; set; }
        public Exception Error { get; set; } = null;
        public Message Message { get; set; } = new Message();
        public int TotolRowCount { get; set; } = 0;
        public int TotolPageCount { get; set; } = 0;
        public int ActiveResultRowCount { get; set; } = 0;
    }
}
