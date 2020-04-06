namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System;
    using System.Data;

    [Table("Store")]
   public partial class Store : EntityObject
   {
      [Column("MainPartyId", SqlDbType.Int, PrimaryKey = true)]
      public int MainPartyId { get; set; }

      [Column("StorePacketId", SqlDbType.Int)]
      public int StorePacketId { get; set; }

      [Column("StoreNo", SqlDbType.VarChar)]
      public string StoreNo { get; set; }

      [Column("StoreName", SqlDbType.NVarChar)]
      public string StoreName { get; set; }

      [Column("StoreEMail", SqlDbType.NVarChar)]
      public string StoreEMail { get; set; }

      [Column("StoreWeb", SqlDbType.NVarChar)]
      public string StoreWeb { get; set; }

      [Column("StoreLogo", SqlDbType.NVarChar)]
      public string StoreLogo { get; set; }

      [Column("StoreActiveType", SqlDbType.TinyInt)]
      public byte StoreActiveType { get; set; }

      [Column("PacketStatu", SqlDbType.TinyInt)]
      public byte PacketStatu { get; set; }

      [Column("StorePacketBeginDate", SqlDbType.DateTime)]
      public DateTime StorePacketBeginDate { get; set; }

      [Column("StorePacketEndDate", SqlDbType.DateTime)]
      public DateTime? StorePacketEndDate { get; set; }

      [Column("StoreAbout", SqlDbType.NText)]
      public string StoreAbout { get; set; }

      [Column("StoreRecordDate", SqlDbType.DateTime)]
      public DateTime StoreRecordDate { get; set; }

      [Column("StoreEstablishmentDate", SqlDbType.Int)]
      public int? StoreEstablishmentDate { get; set; }

      [Column("StoreCapital", SqlDbType.Money)]
      public byte StoreCapital { get; set; }

      [Column("StoreEmployeesCount", SqlDbType.VarChar)]
      public byte StoreEmployeesCount { get; set; }

      [Column("StoreEndorsement", SqlDbType.Money)]
      public byte StoreEndorsement { get; set; }

      [Column("StoreType", SqlDbType.TinyInt)]
      public byte StoreType { get; set; }

      [Column("PurchasingDepartmentName", SqlDbType.VarChar)]
      public string PurchasingDepartmentName { get; set; }

      [Column("PurchasingDepartmentEmail", SqlDbType.VarChar)]
      public string PurchasingDepartmentEmail { get; set; }

      [Column("GeneralText", SqlDbType.NText)]
      public string GeneralText { get; set; }

      [Column("HistoryText", SqlDbType.NText)]
      public string HistoryText { get; set; }

      [Column("FounderText", SqlDbType.NText)]
      public string FounderText { get; set; }

      [Column("PhilosophyText", SqlDbType.NText)]
      public string PhilosophyText { get; set; }

   }


}
