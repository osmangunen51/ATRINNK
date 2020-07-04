using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Stores;
using NeoSistem.MakinaTurkiye.Management.Helper;
using NeoSistem.MakinaTurkiye.Management.Models;
using NeoSistem.MakinaTurkiye.Management.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{

    public class MailMessageSenderController : BaseController
    {
        #region Fields

        private readonly IPacketService _packetService;
        private readonly IMemberService _memberService;
        private readonly IStoreService _storeService;
        private readonly IMemberStoreService _memberStoreService;

        #endregion

        #region Ctor

        public MailMessageSenderController(IPacketService packetService, IMemberService memberService, IStoreService storeService, IMemberStoreService memberStoreService)
        {
            this._packetService = packetService;
            this._memberService = memberService;
            this._memberStoreService = memberStoreService;
            this._storeService = storeService;
        }

        #endregion

        #region Methods

        public ActionResult Index(string result)
        {
            if (result == "noMember")
            {
                ViewData["noMember"] = "1";
            }
            MailSenderViewModel model = new MailSenderViewModel();
            model.MemberTypes.Add(new MemberTypeModel { Value = 0, IsChecked = true, DisplayName = "Tüm Üyeler" });
            model.MemberTypes.Add(new MemberTypeModel { Value = (byte)MemberType.Individual, IsChecked = false, DisplayName = "Bireysel Üyeler" });
            model.MemberTypes.Add(new MemberTypeModel { Value = (byte)MemberType.Enterprise, IsChecked = false, DisplayName = "Kurumsal Üyeler" });
            model.MemberTypes.Add(new MemberTypeModel { Value = (byte)MemberType.FastIndividual, IsChecked = false, DisplayName = "Hızlı Üyeler" });
            var packets = _packetService.GetAllPacket();
            model.Packets = packets.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string memberType, string fileName, string FastMembershipType, string memberPhoneConfirm, string packetId, string mailActive)
        {
            LinkHelper linkHelper = new LinkHelper();
            var MemberListSendMail = new List<MemberSendMailModel>();
            byte pMemberType = 0;
            byte pPhoneConfirm = 0;
            int pFastMembershipType = 0;
            int pPacketId = 0;
            byte pMailActive = 0;
            if (mailActive != "0") pMailActive = Convert.ToByte(mailActive);
            if (memberType != "0") pMemberType = Convert.ToByte(memberType);
            if (memberPhoneConfirm != "0") pPhoneConfirm = Convert.ToByte(memberPhoneConfirm);
            if (FastMembershipType != "0") pFastMembershipType = Convert.ToInt32(FastMembershipType);
            if (packetId != "0") pPacketId = Convert.ToInt32(packetId);
            if (memberType == "5")
            {
                if (FastMembershipType == "10") pMailActive = 2;
            }
            var member = _memberService.SP_GetAllMemberListForMailSender(pPhoneConfirm, pMemberType, pFastMembershipType, pPacketId, pMailActive);
            if (member.ToList().Count > 0)
            {
                CsvExportHelper csvFile = new CsvExportHelper();
                foreach (var item in member)
                {
                    csvFile.AddRow();
                    csvFile["MemberEmail"] = item.MemberEmail;
                    csvFile["MemberID"] = item.MainPartyId;
                    csvFile["MemberNameSurname"] = item.MemberName + " " + item.MemberSurname;
                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(item.MainPartyId);
                    if (memberStore != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(memberStore.StoreMainPartyId));
                        csvFile["StoreName"] = store.StoreName;
                        csvFile["StoreShortName"] = store.StoreShortName;
                    }
                    csvFile["IdForCrypto"] = linkHelper.Encrypt(item.MainPartyId.ToString());
                }
                fileName = fileName.Replace(" ", "-") + "-" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + "-" + DateTime.Now.Date.Month + "-" + DateTime.Now.Date.Day + "-" + DateTime.Now.Date.Year;
                return File(csvFile.ExportToBytes(), "text/csv", fileName + ".csv");
            }
            else
            {
                return RedirectToAction("index", "MailMessageSender", new { result = "noMember" });
            }
        }

        #endregion

    }
    public class LinkHelper
    {

        TripleDESCryptoServiceProvider cryptDES3 = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider cryptMD5Hash = new MD5CryptoServiceProvider();
        string key = "info@makinaturkiye.com";
        public string Encrypt(string text)
        {
            cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            cryptDES3.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = cryptDES3.CreateEncryptor();
            byte[] buff = ASCIIEncoding.ASCII.GetBytes(text);
            string Encrypt = Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
            Encrypt = Encrypt.Replace("+", "!");
            return Encrypt;
        }

        public string Decypt(string text)
        {
            text = text.Replace("!", "+");
            byte[] buf = new byte[text.Length];
            cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            cryptDES3.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = cryptDES3.CreateDecryptor();
            buf = Convert.FromBase64String(text);
            string Decrypt = ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buf, 0, buf.Length));
            return Decrypt;
        }

    }
}