using System.Collections.Generic;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Entities.Tables.Messages;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Messages;

namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class MessageViewModel
    {
        public MessageViewModel()
        {
            this.MemberMessageDetail = new MemberMessageDetailModel();
        }
        public LeftMenuModel LeftMenu { get; set; }

        public Product Product { get; set; }

        public ICollection<MessageModel> MessageItems { get; set; }

        private LoginModel log;

        public LoginModel Loginitems
        {
            get
            {
                if (log == null)
                {
                    log = new LoginModel();
                }
                return log;
            }
            set { log = value; }
        }

        public MessageModel Message { get; set; }

  
        public MemberMessageDetailModel MemberMessageDetail {get; set;}

    public Message EntitiesMessage { get; set; }
    public string ProductName { get; set; }
    public string ProductUrl { get; set; }

    public Store Store { get; set; }
  }
}