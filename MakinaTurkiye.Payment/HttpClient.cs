using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MakinaTurkiye.Payment
{
    public class HttpClient : RestSharp.RestClient
    {

        public Task<RestSharp.IRestResponse> PostAsync(string url, StringContent Content)
        {
            RestSharp.RestRequest Request = new RestSharp.RestRequest(url);
            Request.AddObject(Content);
            return this.ExecuteGetTaskAsync(Request);
        }
        public Task<RestSharp.IRestResponse> PostAsync(string url, FormUrlEncodedContent ContentListesi)
        {
            RestSharp.RestRequest Request = new RestSharp.RestRequest(url);
            Request.Parameters.AddRange((IEnumerable<RestSharp.Parameter>)ContentListesi);
            return this.ExecuteGetTaskAsync(Request);
        }
    }
}
