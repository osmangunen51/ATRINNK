// <copyright file="TransactionUI.cs"  company="NeoSistem">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Business
{
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using System;
    using System.Data.Common;

    /// <summary>
    /// Etkin baglantıların tümü bu class üzerinden gerceklestirilebilir.
    /// </summary>
    public sealed class TransactionUI : BusinessDataEntity, IDisposable
  {
    /// <summary>
    /// Veritabanı baglantısı
    /// </summary>
    private DbConnection myDbConn;

    /// <summary>
    /// Veritabanı baglanti islemi (Transaction) (Private)
    /// </summary>
    private DbTransaction myDbTran; 

    /// <summary>
    /// Veritabanı örnegi (Private)
    /// </summary>
    private SqlDatabase myDatabaseInstance;

    /// <summary>
    /// Yeni islem TransactionUI örnegi
    /// </summary>
    public TransactionUI()
    {
      CreateDB();
      this.myDatabaseInstance = DatabaseInstance;
      this.myDbConn = this.myDatabaseInstance.CreateConnection();
      this.myDbConn.Open();
      this.myDbTran = this.myDbConn.BeginTransaction();
    }
     
    /// <summary>
    /// Veritabanı baglanti islemi (Transaction) 
    /// </summary>
    internal DbTransaction DbTransactionInstance
    {
      get { return this.myDbTran; }
    }

    /// <summary>
    /// Veritabanı örnegi
    /// </summary>
    internal SqlDatabase DataBaseInstance
    {
      get { return this.myDatabaseInstance; }
    } 

    /// <summary>
    /// Etkin tüm baglantılar sorunsuz islendiginde veritabanı islemini gerceklestirir.
    /// </summary>
    public void Commit()
    {
      try 
      {
        this.myDbTran.Commit(); 
      }
      finally
      {
        this.myDbConn.Close();
        this.myDbConn.Dispose();
      }
    }

    /// <summary>
    /// Herhangi bir baglantı üzerinde hata olustugunda tüm islemleri geriye alır.
    /// </summary>
    public void Rollback()
    {
      try 
      {
        this.myDbTran.Rollback(); 
      }
      finally
      {
        this.myDbConn.Close();
        this.myDbConn.Dispose();
      }
    }

    #region IDisposable Members

    /// <summary>
    /// Tüm baglantıları kapatır.
    /// </summary>
    public void Dispose()
    {
      this.myDbConn.Close();
      this.myDbConn.Dispose();
    }

    #endregion
  }
}
