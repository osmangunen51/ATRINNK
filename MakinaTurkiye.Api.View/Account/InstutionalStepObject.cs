using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Api.View.Account
{
    public class InstutionalStepObject
    {

        public string NextStep { get; set; }
        public byte MemberTitleType { get; set; }
        public string StoreLogo { get; set; }
        public string UploadedLogo { get; set; }
        public string StoreUrlName { get; set; }
        public string StoreShortName { get; set; }
        public string StoreName { get; set; }
        public string StoreWeb { get; set; }
        public string[] ActivityName { get; set; }
        public byte StoreCapital { get; set; }
        public int? StoreEstablishmentDate { get; set; }
        public byte? StoreEmployeesCount { get; set; }
        public byte? StoreEndorsement { get; set; }
        public string StoreAbout { get; set; }
        public string PurchasingDepartmentEmail { get; set; }
        public string PurchasingDepartmentName { get; set; }
        public byte StoreType { get; set; }

        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }

        public string[] StoreActivityCategoryIdList { get; set; }
     


    }
}
