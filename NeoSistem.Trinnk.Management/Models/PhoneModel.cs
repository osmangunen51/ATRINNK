namespace NeoSistem.Trinnk.Management.Models
{
    using System;
    using System.Web.Mvc;

    public class PhoneModel
    {
        public string PhoneFormName { get; set; }
        public string PhoneListName { get; set; }

        public bool hasStore { get; set; }

        public int PhoneId { get; set; }

        public int MainPartyId { get; set; }

        public string PhoneNumber { get; set; }

        public string PhoneCulture { get; set; }

        public string PhoneAreaCode { get; set; }

        public byte PhoneType { get; set; }

        public byte GsmType { get; set; }

        public SelectList PhoneTypes
        {
            get
            {
                var items = Enum.GetNames(typeof(TypeVariables.PhoneType));
                return new SelectList(items);
            }
        }
    }
}