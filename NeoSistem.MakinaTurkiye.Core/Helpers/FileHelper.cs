namespace NeoSistem.MakinaTurkiye.Core.Helpers
{
    using System.IO;
    using System.Text;
    using System.Web;

    public partial class FileHelper
    {

        public static bool WriteToFile(string path, string content)
        {
            try
            {
                string serverPath = HttpContext.Current.Server.MapPath(path);

                if (File.Exists(serverPath))
                    File.Delete(serverPath);

                File.WriteAllText(serverPath, content, Encoding.UTF8);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}