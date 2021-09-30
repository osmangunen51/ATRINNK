using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace HttpClient
{
    public static class Json
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string ToJson(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }

        public static T FromJson<T>(this object obj)
        {
            string Sonuc = "";
            if (obj != null)
            {
                Sonuc = obj.ToString();
            }
            return JsonConvert.DeserializeObject<T>(Sonuc);
        }
    }
}