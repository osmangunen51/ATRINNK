namespace Trinnk.Pinterest.Model.Input.PinDelete
{
    public class PinDelete
    {
        public Options options { get; set; } = new Options();
        public Context context { get; set; } = new Context();
    }

    public class Options
    {
        public string id { get; set; } = "";
        public bool no_fetch_context_on_resource { get; set; } = false;
    }

    public class Context
    {
    }


}
