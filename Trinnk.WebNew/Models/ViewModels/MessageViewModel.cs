using Trinnk.Entities.Tables.Catalog;
using Trinnk.Entities.Tables.Messages;
using Trinnk.Entities.Tables.Stores;
using NeoSistem.Trinnk.Web.Areas.Account.Models;
using NeoSistem.Trinnk.Web.Areas.Account.Models.Messages;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models
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


        public MemberMessageDetailModel MemberMessageDetail { get; set; }

        public Message EntitiesMessage { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }

        public Store Store { get; set; }
    }
}