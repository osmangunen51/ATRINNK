// <copyright file="StringSqlParameter.cs" company="EnterpriseEntity">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Extensions.Data
{
    using EnterpriseEntity.Core.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// DateTime SqlParameter Extension Methods
    /// </summary>
    public static class DateTimeSqlParameter
  {
    /// <summary>
    /// Input SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <param name="sqlType">Değişken Sql parametre tipi</param>
    /// <param name="size">Değişken Sql parametre boyutu</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter InSqlParameter(this DateTime thisValue, string fieldName, SqlDbType sqlType, int size)
    {
      return CreateParameter(thisValue, fieldName, sqlType, ParameterDirection.Input, size);
    }

    /// <summary>
    /// Input SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <param name="sqlType">Değişken Sql parametre tipi</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter InSqlParameter(this DateTime thisValue, string fieldName, SqlDbType sqlType)
    {
      return CreateParameter(thisValue, fieldName, sqlType, ParameterDirection.Input);
    }

    /// <summary>
    /// Input SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter InSqlParameter(this DateTime thisValue, string fieldName)
    {
      return CreateParameter(thisValue, fieldName, SqlDbType.DateTime, ParameterDirection.Input);
    }

    /// <summary>
    /// Output SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <param name="sqlType">Değişken Sql parametre tipi</param>
    /// <param name="size">Değişken Sql parametre boyutu</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter OutSqlParameter(this DateTime thisValue, string fieldName, SqlDbType sqlType, int size)
    {
      return CreateParameter(thisValue, fieldName, sqlType, ParameterDirection.Output, size);
    }

    /// <summary>
    /// Output SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <param name="sqlType">Değişken Sql parametre tipi</param> 
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter OutSqlParameter(this DateTime thisValue, string fieldName, SqlDbType sqlType)
    {
      return CreateParameter(thisValue, fieldName, sqlType, ParameterDirection.Output);
    }

    /// <summary>
    /// Output SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param> 
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter OutSqlParameter(this DateTime thisValue, string fieldName)
    {
      return CreateParameter(thisValue, fieldName, SqlDbType.DateTime, ParameterDirection.Output);
    }

    /// <summary>
    /// InOutput SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <param name="sqlType">Değişken Sql parametre tipi</param>
    /// <param name="size">Değişken Sql parametre boyutu</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter InOutSqlParameter(this DateTime thisValue, string fieldName, SqlDbType sqlType, int size)
    {
      return CreateParameter(thisValue, fieldName, sqlType, ParameterDirection.InputOutput, size);
    }

    /// <summary>
    /// InOutput SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <param name="sqlType">Değişken Sql parametre tipi</param> 
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter InOutSqlParameter(this DateTime thisValue, string fieldName, SqlDbType sqlType)
    {
      return CreateParameter(thisValue, fieldName, sqlType, ParameterDirection.InputOutput);
    }

    /// <summary>
    /// InOutput SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param> 
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter InOutSqlParameter(this DateTime thisValue, string fieldName)
    {
      return CreateParameter(thisValue, fieldName, SqlDbType.DateTime, ParameterDirection.InputOutput);
    }

    /// <summary>
    /// ReturnValue SqlParameter Extension Metod
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <param name="sqlType">Değişken Sql parametre tipi</param>
    /// <param name="size">Değişken Sql parametre boyutu</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter ReturnSqlParameter(this DateTime thisValue, string fieldName, SqlDbType sqlType, int size)
    {
      return CreateParameter(thisValue, fieldName, sqlType, ParameterDirection.ReturnValue, size);
    }

    /// <summary>
    /// ReturnValue SqlParameter Extension Metod
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <param name="sqlType">Değişken Sql parametre tipi</param> 
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter ReturnSqlParameter(this DateTime thisValue, string fieldName, SqlDbType sqlType)
    {
      return CreateParameter(thisValue, fieldName, sqlType, ParameterDirection.ReturnValue);
    }

    /// <summary>
    /// ReturnValue SqlParameter Extension Metod
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param> 
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter ReturnSqlParameter(this DateTime thisValue, string fieldName)
    {
      return CreateParameter(thisValue, fieldName, SqlDbType.DateTime, ParameterDirection.ReturnValue);
    }

    /// <summary>
    /// Sql Parameter Extension Metod
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <param name="sqlType">Değişken Sql parametre tipi</param>
    /// <param name="direction">Değişken Sql parametre yönü</param>
    /// <param name="size">Değişken Sql parametre boyutu</param>
    /// <returns></returns>
    private static DbParameter CreateParameter(DateTime thisValue, string fieldName, SqlDbType sqlType, ParameterDirection direction, int size = 0)
    {
      return SqlParameterConverter.CreateParameter(fieldName, sqlType, thisValue, direction, size);
    }
  }
}