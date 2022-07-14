using MakinaTurkiye.Core;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.CallCenter
{
  public class CallCenterService : ICallCenterService
  {
    public string Token { get; set; } = "";
    public string BaseUrl { get; set; } = "https://capi.ncvav.com";
    public CallCenterService()
    {

    }
    public ResponseModel<CallInfo> Calling(string destination, string number)
    {
      ResponseModel<CallInfo> result = new ResponseModel<CallInfo>();
      if (string.IsNullOrEmpty(this.Token))
      {
        result.Error = new Exception("Token Boş Olamaz");
        result.IsSuccess = false;
        result.Message = "Token Boş Olamaz.";
        result.Result = new CallInfo();
        return result;
      }

      try
      {
        using (RestClient restClient = new RestClient(BaseUrl))
        {
          ServicePointManager.Expect100Continue = true;
          ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
          RestSharp.RestRequest Istek = new RestRequest("/call/call", Method.Post);
          Istek.RequestFormat = DataFormat.Json;
          Istek.OnBeforeDeserialization = response =>
          {
            if (response.IsSuccessful)
            {
              string byteOrderMarkUtf8 = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.UTF8.GetPreamble());
              if (response.Content.StartsWith(byteOrderMarkUtf8))
                response.Content.Remove(0, byteOrderMarkUtf8.Length);
            }
          };
          Istek.AddOrUpdateHeader("token",this.Token);
          number = number.Replace("+90","0");
//          #if DEBUG
//                {
//                number = "05057916447";
//                }
//#endif
                    if (!number.StartsWith("0"))
                    {
                        number = "0" + number;
                    }
                    number = destination + number;
                    string Txt= "{\"application\":\"EXTENSIONS\",\"destination\":\""+destination+"\",\"callerid\":\"902129455841\",\"responseurl\":\"\",\"variable\":\"\",\"caller\":{\"1\":\""+number+"\"}}";
          Istek.AddStringBody(Txt,DataFormat.Json);
          var IstekSonuc = restClient.ExecutePost<MakinaTurkiye.Services.CallCenter.CallInfoResponse>(Istek);
          if (IstekSonuc.StatusCode == System.Net.HttpStatusCode.OK)
          {
                MakinaTurkiye.Services.CallCenter.CallInfoResponse callInfoResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<MakinaTurkiye.Services.CallCenter.CallInfoResponse>(IstekSonuc.Content);
                if (callInfoResponse.callInfo.Count() > 0)
                {
                  result.IsSuccess = true;
                  result.Message = "Çağrı Başlatıldı.";
                  result.Result = callInfoResponse.callInfo.FirstOrDefault();
                }
                else
                {
                  result.Error = new Exception("Çağrı Başlatılamadı.");
                  result.IsSuccess = false;
                  result.Message = "Çağrı Başlatılamadı.";
                  result.Result = new CallInfo();
                }
          }
          else
          {
            result.Error = new Exception(IstekSonuc.ErrorMessage);
            result.IsSuccess = false;
            result.Message = IstekSonuc.ErrorMessage;
            result.Result = new CallInfo();
          }
        }
      }
      catch (Exception Error)
      {
        result.Error = Error;
        result.IsSuccess = false;
        result.Message = Error.Message;
        result.Result = new CallInfo();
      }
      return result;
    }
    public ResponseModel<string>  StopCall(string id)
    {
      ResponseModel<string> result = new ResponseModel<string>();
      if (string.IsNullOrEmpty(this.Token))
      {
        result.Error = new Exception("Token Boş Olamaz");
        result.IsSuccess = false;
        result.Message = "Token Boş Olamaz.";
        result.Result = "";
        return result;
      }

      try
      {
        
      }
      catch (Exception Error)
      {
        result.Error = Error;
        result.IsSuccess = false;
        result.Message = Error.Message;
        result.Result = "";
      }
      return result;
    }
  }
}
