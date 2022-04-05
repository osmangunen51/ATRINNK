using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakinaTurkiye.Utilities.FileHelpers
{
    public class FileHelper
    {
        static List<T> DataSource<T>(List<T> values)
        {
            // This generic method returns a List with ten elements initialized.
            // ... It uses a type parameter.
            // ... It uses the "open type" T.
            List<T> list = new List<T>();

            list.AddRange(values);

            return list;
        }

        public void ExportExcel<T>(List<T> values, string filename)
        {

            var response = HttpContext.Current.Response;

            var gv = new GridView();
            gv.DataSource = values;
            gv.DataBind();
            gv.AllowPaging = false;
            response.Clear();
            response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
            response.ContentType = "application/ms-excel";
            response.ContentEncoding = System.Text.Encoding.Unicode;
            response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            StringWriter objStringWriter = new StringWriter();
            //Encoding.GetEncoding(1254).GetBytes(objStringWriter.ToString());
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);
            //response.Output.Write(objStringWriter.ToString());

            response.Write(objStringWriter.ToString());
            //response.Flush();
            response.End();
        }

        public static string ImageThumbnailName(string imageName)
        {
            if (!string.IsNullOrWhiteSpace(imageName) && imageName.Contains("."))
                return imageName.Substring(0, imageName.LastIndexOf(".")) + "_th" + imageName.Substring(imageName.LastIndexOf("."));
            else
                return "";
        }
    }
}
