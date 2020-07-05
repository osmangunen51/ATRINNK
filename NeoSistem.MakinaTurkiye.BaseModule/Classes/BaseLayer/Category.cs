namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System;
    using System.Data;

    [Table("Category")]
  public partial class Category : EntityObject
  {
    [Column("CategoryId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
    public int CategoryId { get; set; }

    [Column("CategoryParentId", SqlDbType.Int)]
    public int? CategoryParentId { get; set; }

    [Column("CategoryName", SqlDbType.NVarChar)]
    public string CategoryName { get; set; }

    [Column("CategoryOrder", SqlDbType.Int)]
    public int CategoryOrder { get; set; }

    [Column("CategoryType", SqlDbType.TinyInt)]
    public byte CategoryType { get; set; }

    [Column("Active", SqlDbType.Bit)]
    public bool Active { get; set; }

    [Column("RecordDate", SqlDbType.DateTime)]
    public DateTime RecordDate { get; set; }

    [Column("RecordCreatorId", SqlDbType.Int)]
    public int RecordCreatorId { get; set; }

    [Column("LastUpdateDate", SqlDbType.DateTime)]
    public DateTime LastUpdateDate { get; set; }

    [Column("LastUpdaterId", SqlDbType.Int)]
    public int LastUpdaterId { get; set; }

    [Column("MainCategoryType", SqlDbType.TinyInt)]
    public byte MainCategoryType { get; set; }

    [Column("Content", SqlDbType.Text)]
    public string Content { get; set; }

    [Column("Title", SqlDbType.NVarChar)]
    public string Title { get; set; }

    [Column("Description", SqlDbType.NVarChar)]
    public string Description { get; set; }

    [Column("Keywords", SqlDbType.NVarChar)]
    public string Keywords { get; set; }

  }


}
