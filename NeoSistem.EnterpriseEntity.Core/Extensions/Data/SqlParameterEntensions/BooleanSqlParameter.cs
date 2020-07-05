// <copyright file="ByteSqlParameter.cs" company="EnterpriseEntity">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Extensions.Data
{
    using Core.Data;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// Boolean SqlParameter Extension Methods
    /// </summary>
    public static class BooleanSqlParameter
  {
    /// <summary>
    /// Input SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <param name="size">Değişken Sql parametre boyutu</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter InSqlParameter(this bool thisValue, string fieldName, int size = 0)
    {
      return CreateParameter(thisValue, fieldName, ParameterDirection.Input, size);
    }


    /// <summary>
    /// Input SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter InSqlParameter(this bool thisValue, string fieldName)
    {
      return CreateParameter(thisValue, fieldName, ParameterDirection.Input);
    }

    /// <summary>
    /// Output SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <param name="size">Değişken Sql parametre boyutu</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter OutSqlParameter(this bool thisValue, string fieldName, int size = 0)
    {
      return CreateParameter(thisValue, fieldName, ParameterDirection.Output, size);
    }

    /// <summary>
    /// Output SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param> 
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter OutSqlParameter(this bool thisValue, string fieldName)
    {
      return CreateParameter(thisValue, fieldName, ParameterDirection.Output);
    }

    /// <summary>
    /// InOutput SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <param name="size">Değişken Sql parametre boyutu</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter InOutSqlParameter(this bool thisValue, string fieldName, int size = 0)
    {
      return CreateParameter(thisValue, fieldName, ParameterDirection.InputOutput, size);
    }

    /// <summary>
    /// InOutput SqlParameter Extension Method
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param> 
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter InOutSqlParameter(this bool thisValue, string fieldName)
    {
      return CreateParameter(thisValue, fieldName, ParameterDirection.InputOutput);
    }

    /// <summary>
    /// ReturnValue SqlParameter Extension Metod
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <param name="size">Değişken Sql parametre boyutu</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter ReturnSqlParameter(this bool thisValue, string fieldName, int size = 0)
    {
      return CreateParameter(thisValue, fieldName, ParameterDirection.ReturnValue, size);
    }

    /// <summary>
    /// ReturnValue SqlParameter Extension Metod
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    public static DbParameter ReturnSqlParameter(this bool thisValue, string fieldName)
    {
      return CreateParameter(thisValue, fieldName, ParameterDirection.ReturnValue);
    }

    /// <summary>
    /// Sql Parameter Extension Metod
    /// </summary>
    /// <param name="thisValue">Değişken değeri</param>
    /// <param name="fieldName">Değişken Sql parametre adı</param>
    /// <param name="direction">Değişken Sql parametre yönü</param>
    /// <param name="size">Değişken Sql parametre boyutu</param>
    /// <returns>DbParameter değer sonuçlar</returns>
    private static DbParameter CreateParameter(bool thisValue, string fieldName, ParameterDirection direction, int size = 0)
    {
      return SqlParameterConverter.CreateParameter(fieldName, System.Data.SqlDbType.Bit, thisValue, direction, size);
    } 
  }
} 