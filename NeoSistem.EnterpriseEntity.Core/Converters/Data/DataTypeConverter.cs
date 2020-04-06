// <copyright file="DataTypeConverter.cs" company="EnterpriseEntity">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Core.Data
{
    using System;
    using System.Data;

    /// <summary>
    /// DataType Converter Class
    /// </summary>
    public sealed class DataTypeConverter
  { 
    /// <summary>
    /// SqlDbType değişkenini DbType değişkenine çeviren metod
    /// </summary>
    /// <param name="type">SqlDbType tipinde değişken</param>
    /// <returns>DbType değeri sonuçlar</returns>
    public static DbType SqlDbTypeToDbType(SqlDbType type)
    {
      switch (type)
      {
        case SqlDbType.BigInt:
          return DbType.Int64;
        case SqlDbType.Binary:
          return DbType.Binary;
        case SqlDbType.Bit:
          return DbType.Boolean;
        case SqlDbType.Char:
          return DbType.AnsiStringFixedLength;
        case SqlDbType.Date:
          return DbType.Date;
        case SqlDbType.DateTime:
          return DbType.DateTime;
        case SqlDbType.DateTime2:
          return DbType.DateTime2;
        case SqlDbType.DateTimeOffset:
          return DbType.DateTimeOffset;
        case SqlDbType.Decimal:
          return DbType.Decimal;
        case SqlDbType.Float:
          return DbType.Double;
        case SqlDbType.Image:
          return DbType.Binary;
        case SqlDbType.Int:
          return DbType.Int32;
        case SqlDbType.Money:
          return DbType.Decimal;
        case SqlDbType.NChar:
          return DbType.StringFixedLength;
        case SqlDbType.NText:
          return DbType.String;
        case SqlDbType.NVarChar:
          return DbType.String;
        case SqlDbType.Real:
          return DbType.Single;
        case SqlDbType.SmallDateTime:
          return DbType.DateTime;
        case SqlDbType.SmallInt:
          return DbType.Int16;
        case SqlDbType.SmallMoney:
          return DbType.Decimal;
        case SqlDbType.Structured:
          return DbType.Object;
        case SqlDbType.Text:
          return DbType.AnsiString;
        case SqlDbType.Time:
          return DbType.Time;
        case SqlDbType.Timestamp:
          return DbType.Time;
        case SqlDbType.TinyInt:
          return DbType.Byte;
        case SqlDbType.Udt:
          return DbType.Object; 
        case SqlDbType.UniqueIdentifier:
          return DbType.Guid;
        case SqlDbType.VarBinary:
          return DbType.Binary;
        case SqlDbType.VarChar:
          return DbType.AnsiString;
        case SqlDbType.Variant:
          return DbType.Object;
        case SqlDbType.Xml:
          return DbType.Xml;
        default:
          return DbType.Object;
      } 
    }

    /// <summary>
    /// TypeCode değişkenini SqlDbType değişkenine çeviren metod
    /// </summary>
    /// <param name="tc">TypeCode tipinde değişken</param>
    /// <returns>SqlDbType değeri sonuçlar</returns>
    public static SqlDbType TypeCodeToSqlDbType(TypeCode tc)
    {
      switch (tc)
      {
        case TypeCode.Boolean:
          return SqlDbType.Bit;
        case TypeCode.Byte:
          return SqlDbType.TinyInt;
        case TypeCode.Char:
          return SqlDbType.Char;
        case TypeCode.DateTime:
          return SqlDbType.DateTime;
        case TypeCode.Decimal:
          return SqlDbType.Decimal;
        case TypeCode.Double:
          return SqlDbType.Float;
        case TypeCode.Int16:
          return SqlDbType.SmallInt;
        case TypeCode.Int32:
          return SqlDbType.Int;
        case TypeCode.Int64:
          return SqlDbType.BigInt;
        case TypeCode.Object:
          return SqlDbType.Variant;
        case TypeCode.Single:
          return SqlDbType.Real;
        case TypeCode.String:
          return SqlDbType.VarChar; 
        default:
          return SqlDbType.Variant;
      }
    }
  }
}
