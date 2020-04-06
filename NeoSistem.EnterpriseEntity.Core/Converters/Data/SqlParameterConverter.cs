// <copyright file="SqlParameterConverter.cs" company="EnterpriseEntity">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Core.Data
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    /// <summary>
    /// SqlParameter Converter Class
    /// </summary>
    public sealed class SqlParameterConverter
  {
    /// <summary>
    /// Sql Input parametre metodu
    /// </summary>
    /// <param name="paramName">Sql parametre adı</param>
    /// <param name="sqlType">Sql parametre tipi</param>
    /// <param name="value">Değişken (Property | Field) değeri</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter InSqlParameter(string paramName, SqlDbType sqlType, object value)
    {
      DbParameter prm = CreateParameter(paramName, sqlType, value);
      return prm;
    }

    /// <summary>
    /// Sql Input parametre metodu (Size)
    /// </summary>
    /// <param name="paramName">Sql parametre adı</param>
    /// <param name="sqlType">Sql parametre tipi</param>
    /// <param name="value">Değişken (Property | Field) değeri</param>
    /// <param name="size">Değişken Sql Parametre boyutu</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter InSqlParameter(string paramName, SqlDbType sqlType, object value, int size)
    {
      DbParameter prm = CreateParameter(paramName, sqlType, value, ParameterDirection.Input, size);
      return prm;
    }

    /// <summary>
    /// Sql InOutput parametre metodu
    /// </summary>
    /// <param name="paramName">Sql parametre adı</param>
    /// <param name="sqlType">Sql parametre tipi</param>
    /// <param name="value">Değişken (Property | Field) değeri</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter InOutSqlParameter(string paramName, SqlDbType sqlType, object value)
    {
      DbParameter prm = CreateParameter(paramName, sqlType, value, ParameterDirection.InputOutput);
      return prm;
    }

    /// <summary>
    /// Sql InOutput parametre metodu (Size)
    /// </summary>
    /// <param name="paramName">Sql parametre adı</param>
    /// <param name="sqlType">Sql parametre tipi</param>
    /// <param name="value">Değişken (Property | Field) değeri</param>
    /// <param name="size">Değişken Sql Parametre boyutu</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter InOutSqlParameter(string paramName, SqlDbType sqlType, object value, int size)
    {
      DbParameter prm = CreateParameter(paramName, sqlType, value, ParameterDirection.InputOutput, size);
      return prm;
    }

    /// <summary>
    /// Sql Output parametre metodu
    /// </summary>
    /// <param name="paramName">Sql parametre adı</param>
    /// <param name="sqlType">Sql parametre tipi</param>
    /// <param name="value">Değişken (Property | Field) değeri</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter OutSqlParameter(string paramName, SqlDbType sqlType, object value)
    {
      DbParameter prm = CreateParameter(paramName, sqlType, value, ParameterDirection.Output);
      return prm;
    }

    /// <summary>
    /// Sql Output parametre metodu (Size)
    /// </summary>
    /// <param name="paramName">Sql parametre adı</param>
    /// <param name="sqlType">Sql parametre tipi</param>
    /// <param name="value">Değişken (Property | Field) değeri</param>
    /// <param name="size">Değişken Sql Parametre boyutu</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter OutSqlParameter(string paramName, SqlDbType sqlType, object value, int size)
    {
      DbParameter prm = CreateParameter(paramName, sqlType, value, ParameterDirection.Output, size);
      return prm;
    }

    /// <summary>
    /// Sql Return parametre metodu
    /// </summary>
    /// <param name="paramName">Sql parametre adı</param>
    /// <param name="sqlType">Sql parametre tipi</param>
    /// <param name="value">Değişken (Property | Field) değeri</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter ReturnSqlParameter(string paramName, SqlDbType sqlType, object value)
    {
      DbParameter prm = CreateParameter(paramName, sqlType, value, ParameterDirection.ReturnValue);
      return prm;
    }

    /// <summary>
    /// Sql Return parametre metodu (Size)
    /// </summary>
    /// <param name="paramName">Sql parametre adı</param>
    /// <param name="sqlType">Sql parametre tipi</param>
    /// <param name="value">Değişken (Property | Field) değeri</param>
    /// <param name="size">Değişken Sql Parametre boyutu</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter ReturnSqlParameter(string paramName, SqlDbType sqlType, object value, int size)
    {
      DbParameter prm = CreateParameter(paramName, sqlType, value, ParameterDirection.ReturnValue, size);
      return prm;
    }

    /// <summary>
    /// Sql Parametre metodu
    /// </summary>
    /// <param name="paramName">Sql parametre adı</param>
    /// <param name="sqlType">Sql parametre tipi</param>
    /// <param name="value">Değişken (Property | Field) değeri</param>
    /// <param name="direction">Sql Parametre yönü</param>
    /// <param name="size">Değişken Sql Parametre boyutu</param> 
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter CreateParameter(string paramName, SqlDbType sqlType, object value, ParameterDirection direction = ParameterDirection.Input, int size = 0)
    {
      DbParameter prm = CreateParameter();
      prm.ParameterName = paramName;
      prm.SourceColumn = paramName;
      prm.Direction = direction;
      prm.DbType = DataTypeConverter.SqlDbTypeToDbType(sqlType);
      prm.Value = value ?? DBNull.Value;
      prm.Size = size;
      return prm;
    }


    /// <summary>
    /// DbParameter oluşturur
    /// </summary>
    /// <returns>DbParameter ornegi sonuçlar</returns>
    private static DbParameter CreateParameter()
    {
      return new SqlParameter();
    }
  }
}
