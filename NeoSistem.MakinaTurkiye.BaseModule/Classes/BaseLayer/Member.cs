namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System;
    using System.Data;

    [Table("Member")]
    public partial class Member : EntityObject
    {
        [Column("MainPartyId", SqlDbType.Int, PrimaryKey = true)]
        public int MainPartyId { get; set; }

        [Column("MemberNo", SqlDbType.VarChar)]
        public string MemberNo { get; set; }

        [Column("MemberType", SqlDbType.TinyInt)]
        public byte MemberType { get; set; }

        [Column("MemberName", SqlDbType.NVarChar)]
        public string MemberName { get; set; }

        [Column("MemberSurname", SqlDbType.NVarChar)]
        public string MemberSurname { get; set; }

        [Column("MemberEmail", SqlDbType.NVarChar)]
        public string MemberEmail { get; set; }

        [Column("MemberPassword", SqlDbType.NVarChar)]
        public string MemberPassword { get; set; }

        [Column("MemberTitleType", SqlDbType.TinyInt)]
        public byte MemberTitleType { get; set; }

        [Column("Active", SqlDbType.Bit)]
        public bool Active { get; set; }

        [Column("ActivationCode", SqlDbType.NVarChar)]
        public string ActivationCode { get; set; }

        [Column("Gender", SqlDbType.Bit)]
        public bool Gender { get; set; }

        [Column("BirthDate", SqlDbType.DateTime)]
        public DateTime? BirthDate { get; set; }

        [Column("ReceiveEmail", SqlDbType.Bit)]
        public bool ReceiveEmail { get; set; }

    }


}
