namespace Trinnk.Pinterest.Model.Input.PinCreate
{

    public class PinCreate
    {
        public Options options { get; set; } = new Options();
        public Context context { get; set; } = new Context();
    }

    public class Options
    {
        public string board_id { get; set; }
        public string field_set_key { get; set; }
        public bool skip_pin_create_log { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string title { get; set; }
        public string image_url { get; set; }
        public string method { get; set; }
        public string section { get; set; }
        public Upload_Metric upload_metric { get; set; }
        public object[] user_mention_tags { get; set; }
        public bool no_fetch_context_on_resource { get; set; }
    }

    public class Upload_Metric
    {
        public string source { get; set; }
    }

    public class Context
    {
    }

}
