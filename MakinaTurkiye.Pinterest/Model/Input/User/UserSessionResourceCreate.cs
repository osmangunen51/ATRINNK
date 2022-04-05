namespace MakinaTurkiye.Pinterest.Model.Input.User
{
    public class UserSessionResourceCreate
    {
        public Options options { get; set; } = new Options();
        public Context context { get; set; } = new Context();
    }

    public class Options
    {
        public string username_or_email { get; set; } = "";
        public string password { get; set; } = "";
        public int app_type_from_client { get; set; } = 0;
        public object visited_pages_before_login { get; set; }
        public string recaptchaV3Token { get; set; } = "";
        public bool no_fetch_context_on_resource { get; set; } = false;
    }

    public class Context
    {

    }
}
