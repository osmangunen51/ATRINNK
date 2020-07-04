// <copyright file="BusinessDataEntity.cs" company="NeoSistem">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Business
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// BusinessDataEntity Class
    /// </summary>
    public abstract class BusinessDataEntity
  {

    public BusinessDataEntity(string constr)
    {
      this.Constr = constr;
    }

    public BusinessDataEntity() : this("DatabaseConnString") { }

    /// <summary>
    /// Baglanti cümlesi
    /// </summary>
    private readonly string Constr = string.Empty;


    private Lazy<SqlDatabase> _databaseInstance = null;
    /// <summary>
    /// Database örnegi
    /// </summary>
    protected SqlDatabase DatabaseInstance { get { return _databaseInstance.Value; } }

    /// <summary>
    /// Yeni database örnegi
    /// </summary>
    protected virtual void CreateDB()
    {

            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            this._databaseInstance = new Lazy<SqlDatabase>(() => (SqlDatabase)factory.Create(this.Constr));
     }

    /// <summary>
    /// Belirtilen islem (Transaction) üzerine komut ekler veya yeni bir islem (Transaction) olusturur
    /// </summary>
    /// <param name="transaction"></param>
    protected void TranDB(TransactionUI transaction)
    {
      if (DatabaseInstance == null)
        this._databaseInstance = new Lazy<SqlDatabase>(() => transaction.DatabaseInstance);
    }

    /// <summary>
    /// Belirtilen komutu yürütür ve etkilenen satırların sayısını verir.
    /// </summary>
    /// <param name="procName">Sql prosedür adı</param>
    /// <returns>Çalıştırılan ve etkilenen satırların sayısını yada sorgulanan tablo üzerindeki ilk satır ilk kolonu çevirir.</returns>
    protected virtual object ExecuteNonQuery(string procName)
    {
      object rowAffected = null;

      rowAffected = this.ExecuteNonQuery(procName, null);

      return rowAffected;
    }

    /// <summary>
    /// Verilen parametreler ile komutu yürütür ve etkilenen satırların sayısını verir.
    /// </summary>
    /// <param name="procName">Sql prosedür adı</param>
    /// <param name="prms">Sql parametreleri</param>
    /// <returns>Çalıştırılan ve etkilenen satırların sayısını yada sorgulanan tablo üzerindeki ilk satır ilk kolonu çevirir.</returns>
    protected virtual object ExecuteNonQuery(string procName, ICollection<IDataParameter> prms)
    {
      object rowAffected = null;
      DbCommand command = null;

      this.CreateDB();

      command = this.CommandBuilder(procName, prms);

      try
      {
        rowAffected = this.DatabaseInstance.ExecuteScalar(command);
        command.Dispose();
      }
      catch (System.Exception)
      {
        command.Dispose();
      }

      return rowAffected;
    }

    /// <summary>
    /// Belirtilen işlem (Transaction) içinde verilen parametreler ile komutu yürütür ve etkilenen satırların sayısını verir.
    /// </summary>
    /// <param name="procName">Sql prosedür adı</param>
    /// <param name="prms">Sql parametreleri</param>
    /// <param name="transaction">Komut içinde çalıştırılıcak işlem (Transaction)</param>
    /// <returns>Çalıştırılan ve etkilenen satırların sayısını yada sorgulanan tablo üzerindeki ilk satır ilk kolonu çevirir.</returns>
    protected virtual object ExecuteNonQuery(string procName, ICollection<IDataParameter> prms, TransactionUI transaction)
    {
      object rowAffected = null;
      DbCommand command = null;

      this.TranDB(transaction);

      command = this.CommandBuilder(procName, prms);

      try
      {
        rowAffected = this.DatabaseInstance.ExecuteScalar(command, transaction.DbTransactionInstance);
        command.Dispose();
      }
      catch (System.Exception)
      {
        command.Dispose();
      }

      return rowAffected;
    }

    /// <summary>
    /// Belirtilen komutu yürütür ve yeni bir DataSet döndürür.
    /// </summary>
    /// <param name="procName">Sql cümleciği</param>
    /// <returns>Yeni bir DataSet döndürür.</returns>
    protected virtual DataSet ExecuteDataQuery(string commandText)
    {
      return this.ExecuteDataQuery(commandText, null);
    }

    /// <summary>
    /// Belirtilen komutu yürütür ve yeni bir DataSet döndürür.
    /// </summary>
    /// <param name="procName">Sql cümleciği</param>
    /// <returns>Yeni bir DataSet döndürür.</returns>
    protected virtual DataSet ExecuteDataQuery(string commandText, ICollection<IDataParameter> prms)
    {
      CreateDB();

      DataSet dataSet = null;

      var command = CommandBuilder(commandText, prms, CommandType.Text);

      try
      {
        dataSet = this.DatabaseInstance.ExecuteDataSet(command);
        command.Dispose();
      }
      catch (System.Exception)
      {
        command.Dispose();
      }

      return dataSet;
    }

    /// <summary>
    /// Belirtilen komutu yürütür ve yeni bir DataSet döndürür.
    /// </summary>
    /// <param name="procName">Sql prosedür adı</param>
    /// <returns>Yeni bir DataSet döndürür.</returns>
    protected virtual DataSet ExecuteDataSet(string procName)
    {
      DataSet dataSet = null;

      dataSet = this.ExecuteDataSet(procName, null);

      return dataSet;
    }

    /// <summary>
    /// Verilen parametreler ile komutu yürütür ve yeni bir DataSet döndürür.
    /// </summary>
    /// <param name="procName">Sql prosedür adı</param>
    /// <param name="prms">Sql parametreleri</param>
    /// <returns>Yeni bir DataSet döndürür.</returns>
    protected virtual DataSet ExecuteDataSet(string procName, ICollection<IDataParameter> prms)
    {
      DataSet dataSet = null;
      DbCommand command = null;

      this.CreateDB();

      command = this.CommandBuilder(procName, prms);

      try
      {
        dataSet = this.DatabaseInstance.ExecuteDataSet(command);
        command.Dispose();
      }
      catch (System.Exception)
      {
        command.Dispose();
      }

      return dataSet;
    }

    /// <summary>
    /// Komutu yürütür ve sonucu okunabilir bir IDataReader döndürür.
    /// </summary>
    /// <param name="procName">Sql prosedür adı</param>
    /// <returns>Okunabilir bir IDataReader döndürür.</returns>
    protected virtual IDataReader ExecuteReader(string procName)
    {
      IDataReader dataReader = null;
      dataReader = this.ExecuteReader(procName, null);
      return dataReader;
    }

    /// <summary>
    /// Verilen parametreler ile komutu yürütür ve sonucu okunabilir bir IDataReader döndürür.
    /// </summary>
    /// <param name="procName">Sql prosedür adı</param>
    /// <param name="prms">Sql parametreleri</param>
    /// <returns>Okunabilir bir IDataReader döndürür.</returns>
    protected virtual IDataReader ExecuteReader(string procName, ICollection<IDataParameter> prms)
    {
      IDataReader dataReader = null;
      DbCommand command = null;

      this.CreateDB();

      command = this.CommandBuilder(procName, prms);

      try
      {
        dataReader = this.DatabaseInstance.ExecuteReader(command);
        command.Dispose();
      }
      catch (System.Exception)
      {
        command.Dispose();
      }

      return dataReader;
    }

    /// <summary>
    /// Belirtilen işlem (Transaction) içinde verilen parametreler ile komutu yürütür ve sonucu okunabilir bir IDataReader döndürür.
    /// </summary>
    /// <param name="procName">Sql prosedür adı</param>
    /// <param name="prms">Sql parametreleri</param>
    /// <param name="transaction">Komut içinde çalıştırılıcak işlem (Transaction)</param>
    /// <returns>Okunabilir bir IDataReader döndürür.</returns>
    protected virtual IDataReader ExecuteReader(string procName, ICollection<IDataParameter> prms, TransactionUI transaction)
    {
      IDataReader dataReader = null;
      DbCommand command = null;

      this.TranDB(transaction);

      command = this.CommandBuilder(procName, prms);

      try
      {
        dataReader = this.DatabaseInstance.ExecuteReader(command, transaction.DbTransactionInstance);
        command.Dispose();
      }
      catch (System.Exception)
      {
        command.Dispose();
      }

      return dataReader;
    }

    /// <summary>
    /// <para>Komutu yürütür ve sorgu tarafından döndürülen sonuç kümesinin ilk satırının ilk sütununu verir.</para> 
    /// <para>Ek sütunlar veya satırlar yok sayılır.</para>
    /// </summary>
    /// <param name="procName">Sql prosedür adı</param>
    /// <returns>Sonuç ilk satırının ilk sütununu object olarak döndürür.</returns>
    protected virtual object ExecuteScalar(string procName)
    {
      object value = null;
      value = this.ExecuteScalar(procName, null);
      return value;
    }

    /// <summary>
    /// <para>Verilen parametreler ile komutu yürütür ve sorgu tarafından döndürülen sonuç kümesinin ilk satırının ilk sütununu verir.</para> 
    /// <para>Ek sütunlar veya satırlar yok sayılır.</para>
    /// </summary>
    /// <param name="procName">Sql prosedür adı</param>
    /// <param name="prms">Sql parametreleri</param>
    /// <returns>Sonuç ilk satırının ilk sütununu object olarak döndürür.</returns>
    protected virtual object ExecuteScalar(string procName, ICollection<IDataParameter> prms)
    {
      object value = null;
      DbCommand command = null;

      this.CreateDB();

      command = this.CommandBuilder(procName, prms);

      try
      {
        value = this.DatabaseInstance.ExecuteScalar(command);
        command.Dispose();
      }
      catch (System.Exception)
      {
        command.Dispose();
      }

      return value;
    }

    /// <summary>
    /// <para>Belirtilen işlem (Transaction) içinde verilen parametreler ile komutu yürütür ve sorgu tarafından döndürülen sonuç kümesinin ilk satırının ilk sütununu verir.</para> 
    /// <para>Ek sütunlar veya satırlar yok sayılır.</para>
    /// </summary>
    /// <param name="procName">Sql prosedür adı</param>
    /// <param name="prms">Sql parametreleri</param>
    /// <param name="transaction">Komut içinde çalıştırılıcak işlem (Transaction)</param>
    /// <returns>Sonuç ilk satırının ilk sütununu object olarak döndürür.</returns>
    protected virtual object ExecuteScalar(string procName, ICollection<IDataParameter> prms, TransactionUI transaction)
    {
      object value = null;
      DbCommand command = null;

      this.TranDB(transaction);

      command = this.CommandBuilder(procName, prms);

      try
      {
        value = this.DatabaseInstance.ExecuteScalar(command, transaction.DbTransactionInstance);
        command.Dispose();
      }
      catch (System.Exception)
      {
        command.Dispose();
      }

      return value;
    }

    /// <summary>
    /// Verilen parametreler ile komut olusturur
    /// </summary>
    /// <param name="sqlcommand">Sql komutu</param>
    /// <param name="parameters">Komutta kullanılacak parametreler</param>
    /// <returns>Calistirilabilir bir DbCommand döndürür</returns>
    protected internal DbCommand CommandBuilder(string sqlcommand, ICollection<IDataParameter> parameters)
    {
      return this.CommandBuilder(sqlcommand, parameters, CommandType.StoredProcedure);
    }

    /// <summary>
    /// Verilen parametreler ile komut olusturur.
    /// </summary>
    /// <param name="sqlcommand">Sql komutu</param>
    /// <param name="parameters">Komutta kullanılacak parametreler</param>
    /// <param name="commandType">Sql  komut türü</param>
    /// <returns>Calistirilabilir bir DbCommand döndürür</returns>
    protected internal DbCommand CommandBuilder(string sqlcommand, ICollection<IDataParameter> parameters, CommandType commandType)
    {
      DbCommand command = null;

      if (commandType == CommandType.StoredProcedure)
      {
        command = this.DatabaseInstance.GetStoredProcCommand(sqlcommand);
      }
      else
      {
        command = this.DatabaseInstance.GetSqlStringCommand(sqlcommand);
      }

      if ((parameters != null) && (parameters.Count > 0))
      {
        foreach (IDataParameter parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      return command;
    }
  }
}
