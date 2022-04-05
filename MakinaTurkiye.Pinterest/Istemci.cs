using MakinaTurkiye.Pinterest.Model.Input.PinCreate;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace MakinaTurkiye.Pinterest
{
    public class Istemci : IDisposable
    {
        Random Rnd = new Random();
        public HttpClient.Istemci HttpIstemci { get; set; }
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        public string ProxyServer { get; set; } = "";
        public Istemci(string UserName, string Password, string ProxyServer = "")
        {
            this.UserName = UserName;
            this.Password = Password;
            HttpIstemci = new HttpClient.Istemci();
            this.ProxyServer = ProxyServer;
            if (this.ProxyServer != "")
            {
                string Adres = "";
                int Port = 80;
                string[] Liste = this.ProxyServer.Split(':');
                if (Liste.Length > 0)
                {
                    Adres = Liste[0].Trim();
                }
                if (Liste.Length > 1)
                {
                    Port = Convert.ToInt32(Liste[1].Trim());
                }
                this.HttpIstemci.WebClient.Proxy = new WebProxy(Adres, Port);
            }
            this.AddDefaultHeader();
            HttpIstemci.WebClient.CookieContainer.Add(new System.Uri(BaseUrl), new Cookie("csrftoken", "1234"));
        }


        public void Dispose()
        {

        }

        public void AddDefaultHeader()
        {
            HttpIstemci.WebClient.Headers.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            HttpIstemci.WebClient.Headers.Add("Accept-Encoding", "gzip, deflate");
            HttpIstemci.WebClient.Headers.Add("Accept-Language", "en-US,en;q=0.5");
            HttpIstemci.WebClient.Headers.Add("DNT", "1");
            HttpIstemci.WebClient.Headers.Add("Host", "www.pinterest.com");
            HttpIstemci.WebClient.Headers.Add("Origin", "https://www.pinterest.com/");
            HttpIstemci.WebClient.Headers.Add("Referer", "https://www.pinterest.com/");
            HttpIstemci.WebClient.Headers.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.95 Safari/537.36");
            HttpIstemci.WebClient.Headers.Add("X-APP-VERSION", "f9d1262");
            HttpIstemci.WebClient.Headers.Add("X-CSRFToken", "1234");
            HttpIstemci.WebClient.Headers.Add("X-NEW-APP", "1");
            HttpIstemci.WebClient.Headers.Add("X-Pinterest-AppState", "active");
            HttpIstemci.WebClient.Headers.Add("X-Requested-With", "XMLHttpRequest");
        }


        public string BaseUrl { get; set; } = "https://www.pinterest.com";
        public ProcessStatus Login()
        {
            string IstekKontrolIfadesi = "";
            ProcessStatus Result = new ProcessStatus();
            try
            {
                string IstekSonuc = "";
                Dictionary<string, string> IstekListesi = new Dictionary<string, string>();
                string RequestUrl = $"";
                #region IlkIstek
                RequestUrl = $"{BaseUrl}";
                IstekSonuc = HttpIstemci.HttpGet(RequestUrl);

                IstekListesi.Clear();
                RequestUrl = $"{BaseUrl}/resource/StatsLogResource/create/";
                IstekSonuc = HttpIstemci.HttpPost(RequestUrl, IstekListesi);
                #endregion
                RequestUrl = $"{BaseUrl}/resource/UserSessionResource/create/";
                MakinaTurkiye.Pinterest.Model.Input.User.UserSessionResourceCreate UserSessionResourceCreate = new MakinaTurkiye.Pinterest.Model.Input.User.UserSessionResourceCreate();
                UserSessionResourceCreate.options.username_or_email = this.UserName;
                UserSessionResourceCreate.options.password = this.Password;
                UserSessionResourceCreate.options.app_type_from_client = 5;
                UserSessionResourceCreate.options.visited_pages_before_login = null;
                UserSessionResourceCreate.options.recaptchaV3Token = "03AGdBq252jn9elRMN9PVdMT12rxDBml7b_PNWm38GRgvPPsEuwFcUBph5DL5ZQ1BPo_2HkbQXX26HiwahcZl723-V_0fEmfKUrTf618n_gIlq3_kZT372G1ZSCSbUrdGW3djJhyC22R9JX3p0mDLRnvv72Q9TfO1JZchzmjr-iK-VF7yqQwQ8HANZyQJPTN8fNkno0sQXcoBhfai5gJTg1SDVaGqQ7HHX3zNYBpAaHQUj1dCnALMGkxzzbYIKBCWIugiMz48QoRk7T_e_6CaP81j78PEJSDgghOJ_KOT0Yr_pwHfEvIkzT7zIGFkjXBxH4DMtqdtYzpmEErq818s-Hr7V5LYqoBt2qkqNJeeuSc5lG6oCsBAnTfaBQ6d5BsKCsE_KBPQEguSS_i-kytKP31MGzzImZo6KLBBHNf0fRl7CrvX5XQxVoQSPLvcvfOrhrptOenseGravhwMD2l43-dpzOlG5MA72P1RR68aqojOmqGmUtmcybA6jf7cE7ylTHJEP2eMCGVoZLFktxvyOv-aJBvIWP-NOjKw9SQfWQI6OPgfigzSYAFcPlH_JXiDDJcLod9PhZiFgNuh-HnbqraYgkefnRM-WUQGtJpLBmlmHH_YmS2NkCRgPj103IX4YoKNti6NIdN3ZdV6wGLV0_KSQ7Ea-B8p1fnXodl8-0tmwriSEFbT08ogx2U8KPM1pq5RjykxTd2wh45_3_scD2JdFkD95vw3kizoMyZBS7_uHbLZy4BK25E35lMAnajSdJAjcLHIwPbAwYfp-erEwcxr-ln9P_lmVWWdc5zmPwx-t5vZu9y1_B7Wdna6Ogxd6B7VlXI-7gbzJ4bzjoppqYB4uKnFkZif1sRBl2xzJ4wsg_4LmFWiJ3Zv2z_4qOKvCreBH9DDBS7T2QfTzZsuwbBqipsllVwpQmCCMUQ-ClQR9FGEimAeOLu2tzcj7QQEv--3fBo45flyDq0rQIvnKjR2vLGlxOmxQ-PaO82iCHNTwzG06l2MgQrV8NsoLZajKSJJVAsh06wmmwV61dKBOXhkGsnEMa-mBvG2pMHeyS3sfqhCpjdcTEwoxR_HN1LTlfb56ZbKKrymNPMsoMZedWzCnc4nH8WUhfPimNmJak97iRxl6s2jtX18rTS6jOMJrF4kukq0blMF-";
                UserSessionResourceCreate.options.no_fetch_context_on_resource = false;
                string JsUserSessionResourceCreate = Newtonsoft.Json.JsonConvert.SerializeObject(UserSessionResourceCreate);
                IstekListesi.Clear();
                IstekListesi.Add("source_url", "/");
                IstekListesi.Add("data", JsUserSessionResourceCreate);
                IstekSonuc = HttpIstemci.HttpPost(RequestUrl, IstekListesi);
                if (!string.IsNullOrEmpty(IstekSonuc))
                {
                    if (string.IsNullOrEmpty(IstekSonuc) | IstekSonuc.Contains(IstekKontrolIfadesi))
                    {
                        MakinaTurkiye.Pinterest.Model.Result.User.UserSessionResourceCreate Value = Newtonsoft.Json.JsonConvert.DeserializeObject<MakinaTurkiye.Pinterest.Model.Result.User.UserSessionResourceCreate>(IstekSonuc);
                        Result.Message = "İşlem Başarılı";
                        Result.Status = true;
                        Result.Tip = ProcessStatus.ProcessType.Success;
                        Result.Value = Value;
                    }
                }
            }
            catch (Exception Hata)
            {
                Result.Error = Hata;
                Result.Message = Hata.Message;
                Result.Status = false;
                Result.Tip = ProcessStatus.ProcessType.Error;
                Result.Value = Hata.Message;
            }
            return Result;
        }


        public ProcessStatus CreatePin(string ImageUrl, string BoardID, string Description, string Link, string Title, string SectionID = "")
        {
            ProcessStatus Result = new ProcessStatus() { Status = false };
            try
            {
                int Adet = 0;
                string IstekKontrolIfadesi = "";
                if (SectionID != "/pin-builder/") { SectionID = ""; };
                string IstekSonuc = "";
                Dictionary<string, string> IstekListesi = new Dictionary<string, string>();
                string RequestUrl = $"";
                RequestUrl = $"{BaseUrl}/resource/PinResource/create/";
                MakinaTurkiye.Pinterest.Model.Input.PinCreate.PinCreate PinCreate = new MakinaTurkiye.Pinterest.Model.Input.PinCreate.PinCreate();
                PinCreate.options.board_id = BoardID;
                PinCreate.options.description = Description;
                PinCreate.options.link = Link;
                PinCreate.options.title = Title;
                PinCreate.options.section = SectionID;
                PinCreate.options.method = "uploaded";
                PinCreate.options.upload_metric = new Upload_Metric() { source = "partner_upload_standalone" };
                PinCreate.options.user_mention_tags = new object[0];
                PinCreate.options.no_fetch_context_on_resource = false;
                PinCreate.options.image_url = ImageUrl;
                string JsUserSessionResourceCreate = Newtonsoft.Json.JsonConvert.SerializeObject(PinCreate);
                IstekListesi.Clear();
                IstekListesi.Add("source_url", "/pin-builder/");
                IstekListesi.Add("data", JsUserSessionResourceCreate);
                IstekSonuc = HttpIstemci.HttpPost(RequestUrl, IstekListesi);
                while ((IstekSonuc.Contains("(404)") | IstekSonuc.Contains("(429)")) && Adet < 5)
                {
                    int IstekTimeOut = 1000;
                    if (IstekSonuc.Contains("(429)"))
                    {
                        IstekTimeOut = Rnd.Next(300000, 600000);
                    }
                    else
                    {
                        IstekTimeOut = Rnd.Next(500, 1000);
                    }
                    Thread.Sleep(IstekTimeOut);
                    IstekSonuc = HttpIstemci.HttpPost(RequestUrl, IstekListesi);
                    Adet++;
                }
                IstekKontrolIfadesi = "resource_response\":{\"code\":0,";
                if (!string.IsNullOrEmpty(IstekSonuc))
                {
                    if (string.IsNullOrEmpty(IstekSonuc) | IstekSonuc.Contains(IstekKontrolIfadesi))
                    {
                        MakinaTurkiye.Pinterest.Model.Result.PinCreate.PinCreate Value = Newtonsoft.Json.JsonConvert.DeserializeObject<MakinaTurkiye.Pinterest.Model.Result.PinCreate.PinCreate>(IstekSonuc);
                        Result.Message = "İşlem Başarılı";
                        Result.Status = true;
                        Result.Tip = ProcessStatus.ProcessType.Success;
                        Result.Value = Value;
                    }
                }
            }
            catch (Exception Hata)
            {
                Result.Error = Hata;
                Result.Message = Hata.Message;
                Result.Status = false;
                Result.Tip = ProcessStatus.ProcessType.Error;
                Result.Value = "";
            }
            return Result;
        }


        public ProcessStatus ChangeBussines(string business_id, string owner_id)
        {
            ProcessStatus Result = new ProcessStatus();
            try
            {
                string IstekKontrolIfadesi = "";
                string IstekSonuc = "";
                Dictionary<string, string> IstekListesi = new Dictionary<string, string>();
                string RequestUrl = $"";
                RequestUrl = $"{BaseUrl}resource/UserSessionResource/create/";
                MakinaTurkiye.Pinterest.Model.Input.ChangeBussines.ChangeBussines ChangeBussines = new MakinaTurkiye.Pinterest.Model.Input.ChangeBussines.ChangeBussines()
                {
                    options = new Model.Input.ChangeBussines.Options()
                    {
                        business_id = business_id,
                        get_user = true,
                        app_type_from_client = 5,
                        no_fetch_context_on_resource = false,
                        visited_pages_before_login = null,
                        owner_id = owner_id
                    },
                    context = new Model.Input.ChangeBussines.Context()
                    {

                    }
                };
                string JsUserSessionResourceCreate = Newtonsoft.Json.JsonConvert.SerializeObject(ChangeBussines);
                IstekListesi.Clear();
                IstekListesi.Add("source_url", "");
                IstekListesi.Add("data", JsUserSessionResourceCreate);
                IstekSonuc = HttpIstemci.HttpPost(RequestUrl, IstekListesi);
                IstekKontrolIfadesi = "resource_response\":{\"code\":0,";
                if (!string.IsNullOrEmpty(IstekSonuc))
                {
                    if (string.IsNullOrEmpty(IstekSonuc) | IstekSonuc.Contains(IstekKontrolIfadesi))
                    {
                        Result.Message = "İşlem Başarılı";
                        Result.Status = true;
                        Result.Tip = ProcessStatus.ProcessType.Success;
                        Result.Value = "";
                    }
                }
            }
            catch (Exception Hata)
            {
                Result.Error = Hata;
                Result.Message = Hata.Message;
                Result.Status = false;
                Result.Tip = ProcessStatus.ProcessType.Error;
                Result.Value = "";
            }
            return Result;
        }





        public ProcessStatus CreateBoard(string name, string description, string privacy = "public", bool collaborator_invites_enabled = false, bool no_fetch_context_on_resource = false, string source_url = "")
        {
            ProcessStatus Result = new ProcessStatus();
            try
            {
                string IstekKontrolIfadesi = "";
                string IstekSonuc = "";
                Dictionary<string, string> IstekListesi = new Dictionary<string, string>();
                string RequestUrl = $"";
                RequestUrl = $"{BaseUrl}/resource/BoardResource/create/";
                MakinaTurkiye.Pinterest.Model.Input.BoardCreate.BoardCreate BoardCreate = new MakinaTurkiye.Pinterest.Model.Input.BoardCreate.BoardCreate();
                BoardCreate.options.name = name;
                BoardCreate.options.description = description;
                BoardCreate.options.privacy = privacy;
                BoardCreate.options.collaborator_invites_enabled = collaborator_invites_enabled;
                BoardCreate.options.no_fetch_context_on_resource = no_fetch_context_on_resource;
                string JsPinCreate = Newtonsoft.Json.JsonConvert.SerializeObject(BoardCreate);
                IstekListesi.Clear();
                IstekListesi.Add("source_url", source_url);
                IstekListesi.Add("data", JsPinCreate);
                IstekSonuc = HttpIstemci.HttpPost(RequestUrl, IstekListesi);
                IstekKontrolIfadesi = "resource_response\":{\"code\":0,";
                if (!string.IsNullOrEmpty(IstekSonuc))
                {
                    if (string.IsNullOrEmpty(IstekSonuc) | IstekSonuc.Contains(IstekKontrolIfadesi))
                    {
                        MakinaTurkiye.Pinterest.Model.Result.BoardCreate.BoardCreate Value = Newtonsoft.Json.JsonConvert.DeserializeObject<MakinaTurkiye.Pinterest.Model.Result.BoardCreate.BoardCreate>(IstekSonuc);
                        if (Value != null)
                        {
                            Result.Message = "İşlem Başarılı";
                            Result.Status = true;
                            Result.Tip = ProcessStatus.ProcessType.Success;
                            Result.Value = Value;
                        }

                    }
                }
            }
            catch (Exception Hata)
            {
                Result.Error = Hata;
                Result.Message = Hata.Message;
                Result.Status = false;
                Result.Tip = ProcessStatus.ProcessType.Error;
                Result.Value = "";
            }
            return Result;
        }

        public ProcessStatus ArchiveBoard(string boardId, bool no_fetch_context_on_resource = false, string source_url = "")
        {
            ProcessStatus Result = new ProcessStatus();
            try
            {
                string IstekKontrolIfadesi = "";
                string IstekSonuc = "";
                Dictionary<string, string> IstekListesi = new Dictionary<string, string>();
                string RequestUrl = $"";
                RequestUrl = $"{BaseUrl}/resource/BoardArchiveResource/update/";
                MakinaTurkiye.Pinterest.Model.Input.BoardArchive.BoardArchive BoardArchive = new MakinaTurkiye.Pinterest.Model.Input.BoardArchive.BoardArchive();
                BoardArchive.options.boardId = boardId;
                BoardArchive.options.no_fetch_context_on_resource = no_fetch_context_on_resource;
                string jsBoardArchive = Newtonsoft.Json.JsonConvert.SerializeObject(BoardArchive);
                IstekListesi.Clear();
                IstekListesi.Add("source_url", source_url);
                IstekListesi.Add("data", jsBoardArchive);
                IstekSonuc = HttpIstemci.HttpPost(RequestUrl, IstekListesi);
                IstekKontrolIfadesi = "resource_response\":{\"code\":0,";
                if (!string.IsNullOrEmpty(IstekSonuc))
                {
                    if (string.IsNullOrEmpty(IstekSonuc) | IstekSonuc.Contains(IstekKontrolIfadesi))
                    {
                        MakinaTurkiye.Pinterest.Model.Result.BoardCreate.BoardCreate Value = Newtonsoft.Json.JsonConvert.DeserializeObject<MakinaTurkiye.Pinterest.Model.Result.BoardCreate.BoardCreate>(IstekSonuc);
                        Result.Message = "İşlem Başarılı";
                        Result.Status = true;
                        Result.Tip = ProcessStatus.ProcessType.Success;
                        Result.Value = Value;
                    }
                }
            }
            catch (Exception Hata)
            {
                Result.Error = Hata;
                Result.Message = Hata.Message;
                Result.Status = false;
                Result.Tip = ProcessStatus.ProcessType.Error;
                Result.Value = "";
            }
            return Result;
        }



        public ProcessStatus DeletePin(string Id, bool no_fetch_context_on_resource = false, string source_url = "")
        {
            ProcessStatus Result = new ProcessStatus();
            try
            {
                if (source_url == "") { source_url = $"/pin/{Id}"; }
                string IstekKontrolIfadesi = "";
                string IstekSonuc = "";
                Dictionary<string, string> IstekListesi = new Dictionary<string, string>();
                string RequestUrl = $"";
                RequestUrl = $"{BaseUrl}/resource/PinResource/delete/";
                MakinaTurkiye.Pinterest.Model.Input.PinDelete.PinDelete PinDelete = new MakinaTurkiye.Pinterest.Model.Input.PinDelete.PinDelete();
                PinDelete.options.id = Id;
                PinDelete.options.no_fetch_context_on_resource = no_fetch_context_on_resource;
                string jsPinDelete = Newtonsoft.Json.JsonConvert.SerializeObject(PinDelete);
                IstekListesi.Clear();
                IstekListesi.Add("source_url", source_url);
                IstekListesi.Add("data", jsPinDelete);
                IstekSonuc = HttpIstemci.HttpPost(RequestUrl, IstekListesi);
                IstekKontrolIfadesi = "resource_response\":{\"code\":0,";
                if (!string.IsNullOrEmpty(IstekSonuc))
                {
                    if (string.IsNullOrEmpty(IstekSonuc) | IstekSonuc.Contains(IstekKontrolIfadesi))
                    {
                        MakinaTurkiye.Pinterest.Model.Result.PinCreate.PinCreate Value = Newtonsoft.Json.JsonConvert.DeserializeObject<MakinaTurkiye.Pinterest.Model.Result.PinCreate.PinCreate>(IstekSonuc);
                        Result.Message = "İşlem Başarılı";
                        Result.Status = true;
                        Result.Tip = ProcessStatus.ProcessType.Success;
                        Result.Value = Value;
                    }
                }
            }
            catch (Exception Hata)
            {
                Result.Error = Hata;
                Result.Message = Hata.Message;
                Result.Status = false;
                Result.Tip = ProcessStatus.ProcessType.Error;
                Result.Value = "";
            }
            return Result;
        }

    }
}
