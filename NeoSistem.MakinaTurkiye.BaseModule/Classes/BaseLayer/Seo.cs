namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("Seo")]
	public partial class Seo: EntityObject
	{
		[Column("SeoId", SqlDbType.Int, PrimaryKey=true, Identity=true)]
		public int SeoId{ get; set; }

		[Column("PageName", SqlDbType.VarChar)]
		public string PageName{ get; set; }

		[Column("Title", SqlDbType.VarChar)]
		public string Title{ get; set; }

		[Column("Description", SqlDbType.VarChar)]
		public string Description{ get; set; }

		[Column("Abstract", SqlDbType.VarChar)]
		public string Abstract{ get; set; }

		[Column("Keywords", SqlDbType.VarChar)]
		public string Keywords{ get; set; }

		[Column("Classification", SqlDbType.VarChar)]
		public string Classification{ get; set; }

		[Column("Robots", SqlDbType.VarChar)]
		public string Robots{ get; set; }

		[Column("RevisitAfter", SqlDbType.VarChar)]
		public string RevisitAfter{ get; set; }

	}


}
