
using Trinnk.Entities.Tables.Catalog;
using Trinnk.Entities.Tables.Members;
using Trinnk.Entities.Tables.Stores;
using NeoSistem.Trinnk.Web.Areas.Account.Models;
using System;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models
{
    public class MemberModel
    {
        public LeftMenuModel LeftMenu { get; set; }

        public Member Member { get; set; }

        public int MainPartyId { get; set; }

        public byte MemberType { get; set; }

        public string MemberName { get; set; }

        public string MemberSurname { get; set; }

        public string MemberEmail { get; set; }

        public string MemberEmailAgain { get; set; }

        public string MemberPassword { get; set; }

        public string MemberPasswordAgain { get; set; }

        public string NewPasswordAgain { get; set; }

        public byte MemberTitleType { get; set; }

        public bool MemberStatu { get; set; }

        public bool Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string NewPassword { get; set; }

        public string NewEmail { get; set; }

        public string SecurityCode { get; set; }

        public bool ReceiveEmail { get; set; }

        public int Day { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public IEnumerable<Category> CategoryItems { get; set; }
        public IEnumerable<StoreActivityCategory> StoreActivityCategory { get; set; }

        //public IEnumerable<RelMainPartyCategory> MemberRelatedCategory { get; set; }

        //public ICollection<CategoryModel> CategoryGroupParentItemsByCategoryId(int CategoryId)
        //{
        //  var dataCategory = new Data.Category();
        //  return dataCategory.CategoryGroupParentItemsByCategoryId(CategoryId).AsCollection<CategoryModel>();
        //}

        //public SelectList StoreAuthorizedTitleTypeItems
        //{
        //  get
        //  {
        //    var dataConstant = new Data.Constant();
        //    var model = dataConstant.ConstantGetByConstantType((byte)ConstantType.MemberTitleType).AsCollection<ConstantModel>();
        //    return new SelectList(model, "ConstantId", "ConstantName");
        //  }
    }

}