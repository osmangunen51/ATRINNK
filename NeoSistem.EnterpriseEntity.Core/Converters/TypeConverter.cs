// <copyright file="TypeConverter.cs" company="EnterpriseEntity">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Core
{
    using System;

    /// <summary>
    /// Type Converter Class
    /// </summary>
    public sealed class TypeConverter
  {
    /// <summary>
    /// Convert Byte
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>Byte tipi sonuçlar</returns>
    public static byte ToByte(object value)
    {
      byte e;
      value = value ?? 0;
      byte.TryParse(value.ToString(), out e);
      return e;
    }

    /// <summary>
    /// Convert Short
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>Short tipi sonuçlar</returns>
    public static short ToInt16(object value)
    {
      short e;
      value = value ?? 0;
      short.TryParse(value.ToString(), out e);
      return e;
    }

    /// <summary>
    /// Convert Int
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>Int tipi sonuçlar</returns>
    public static int ToInt32(object value)
    {
      int e;
      value = value ?? 0;
      int.TryParse(value.ToString(), out e);
      return e;
    }

    /// <summary>
    /// Convert Long
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>Long tipi sonuçlar</returns>
    public static long ToInt64(object value)
    {
      long e;
      value = value ?? 0;
      long.TryParse(value.ToString(), out e);
      return e;
    }

    /// <summary>
    /// Convert SByte
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>SByte tipi sonuçlar</returns>
    public static sbyte ToSByte(object value)
    {
      sbyte e;
      value = value ?? 0;
      sbyte.TryParse(value.ToString(), out e);
      return e;
    }

    /// <summary>
    /// Convert UShort
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>UShort tipi sonuçlar</returns>
    public static ushort ToUInt16(object value)
    {
      ushort e;
      value = value ?? 0;
      ushort.TryParse(value.ToString(), out e);
      return e;
    }

    /// <summary>
    /// Convert UInt
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>UInt tipi sonuçlar</returns>
    public static uint ToUInt32(object value)
    {
      uint e;
      value = value ?? 0;
      uint.TryParse(value.ToString(), out e);
      return e;
    }

    /// <summary>
    /// Convert ULong
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>ULong tipi sonuçlar</returns>
    public static ulong ToUInt64(object value)
    {
      ulong e;
      value = value ?? 0;
      ulong.TryParse(value.ToString(), out e);
      return e;
    }

    /// <summary>
    /// Convert Decimal
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>Decimal tipi sonuçlar</returns>
    public static decimal ToDecimal(object value)
    {
      decimal e;
      value = value ?? 0M;
      decimal.TryParse(value.ToString(), out e);
      return e;
    }

    /// <summary>
    /// Convert Double
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>Double tipi sonuçlar</returns>
    public static double ToDouble(object value)
    {
      double e;
      value = value ?? 0D;
      double.TryParse(value.ToString(), out e);
      return e;
    }

    /// <summary>
    /// Convert Float
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>Float tipi sonuçlar</returns>
    public static float ToSingle(object value)
    {
      float e;
      value = value ?? 0F;
      float.TryParse(value.ToString(), out e);
      return e;
    }

    /// <summary>
    /// Convert Boolean
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>Boolean tipi sonuçlar</returns>
    public static bool ToBoolean(object value)
    {
      bool e;
      value = value ?? false;
      bool.TryParse(value.ToString(), out e);
      return e;
    }

    /// <summary>
    /// Convert DateTime
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>DateTime tipi sonuçlar</returns>
    public static DateTime ToDateTime(object value)
    {
      DateTime e;
      value = value ?? new DateTime();
      System.DateTime.TryParse(value.ToString(), out e);
      return e;
    }

    /// <summary>
    /// Convert Char
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>Char tipi sonuçlar</returns>
    public static char ToChar(object value)
    {
      char e;
      value = value ?? System.Char.MinValue;
      char.TryParse(value.ToString(), out e);
      return e;
    }

    /// <summary>
    /// Convert String
    /// </summary>
    /// <param name="value">Değişken değeri</param>
    /// <returns>String tipi sonuçlar</returns>
    public static string ToString(object value)
    {
      value = value ?? System.String.Empty;

      if (value is DateTime)
      {
        if (Convert.ToDateTime(value) == new DateTime())
        {
          return System.String.Empty;
        }
      } 

      return value.ToString();
    }

    /// <summary>
    /// VB IsNumeric fonksiyonu
    /// </summary>
    /// <param name="value">Hedef değişken</param>
    /// <returns>Boolean tipi değer sonuçlar</returns>
    public static bool IsNumeric(object value)
    {
      bool isNum;
      double retNum;
      isNum = Double.TryParse(Convert.ToString(value), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
      return isNum;
    }
  }
}