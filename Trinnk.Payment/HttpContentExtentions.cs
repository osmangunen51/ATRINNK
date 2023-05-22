using System.Net.Http;
using System.Threading.Tasks;

namespace Trinnk.Payment
{
    public static class HttpContentExtentions
    {
        public static string Read(this HttpContent HttpContent)
        {
            var taskresponseContent = Task.Run(() => HttpContent.ReadAsStringAsync());
            taskresponseContent.Wait();
            var responseContent = taskresponseContent.Result;
            return responseContent;
        }
    }
}
