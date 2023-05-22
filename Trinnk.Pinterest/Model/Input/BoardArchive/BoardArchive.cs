namespace Trinnk.Pinterest.Model.Input.BoardArchive
{

    public class BoardArchive
    {
        public Options options { get; set; } = new Options();
        public Context context { get; set; } = new Context();
    }

    public class Options
    {
        public string boardId { get; set; }
        public bool no_fetch_context_on_resource { get; set; }
    }

    public class Context
    {
    }

}
