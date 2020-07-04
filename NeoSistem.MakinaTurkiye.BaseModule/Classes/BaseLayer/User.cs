namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("User")]
    public partial class User : EntityObject
    {
        [Column("UserId", SqlDbType.TinyInt, PrimaryKey = true, Identity = true)]
        public byte UserId { get; set; }

        [Column("UserName", SqlDbType.VarChar)]
        public string UserName { get; set; }

        [Column("UserPass", SqlDbType.VarChar)]
        public string UserPass { get; set; }

        [Column("UserMail", SqlDbType.VarBinary)]
        public string UserMail { get; set; }
        [Column("MailSmtp", SqlDbType.VarBinary)]
        public string MailSmtp { get; set; }
        [Column("MailPassword", SqlDbType.VarBinary)]
        public string MailPassword { get; set; }
        [Column("SendCode", SqlDbType.Int)]
        public int? SendCode { get; set; }
        [Column("UserColor", SqlDbType.VarChar)]
        public string UserColor { get; set; }

        [Column("Active", SqlDbType.Bit)]
        public bool Active { get; set; }

        [Column("ActiveForDesc", SqlDbType.Bit)]
        public bool ActiveForDesc { get; set; }


    }


}
