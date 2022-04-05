// <copyright file="GeneralTypeExtension.cs" company="EnterpriseEntity">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Extensions
{
    using EnterpriseEntity.Core;
    using System;

    /// <summary>
    /// General Type Extension Class
    /// </summary>
    public static class GeneralTypeExtension
    {
        /// <summary>
        /// Byte tipine çevirir
        /// </summary>
        /// <param name="value">Hedef değişken</param>
        /// <returns>Byte tipi değer sonuçlar</returns>
        public static byte ToByte(this object value)
        {
            return TypeConverter.ToByte(value);
        }

        /// <summary>
        /// Short tipine çevirir
        /// </summary>
        /// <param name="value">Hedef değişken</param>
        /// <returns>Short tipi değer sonuçlar</returns>
        public static short ToInt16(this object value)
        {
            return TypeConverter.ToInt16(value);
        }

        /// <summary>
        /// Int32 tipine çevirir
        /// </summary>
        /// <param name="value">Hedef değişken</param>
        /// <returns>Int32 tipi değer sonuçlar</returns>
        public static int ToInt32(this object value)
        {
            return TypeConverter.ToInt32(value);
        }

        /// <summary>
        /// Int64 tipine çevirir
        /// </summary>
        /// <param name="value">Hedef değişken</param>
        /// <returns>Int64 tipi değer sonuçlar</returns>
        public static long ToInt64(this object value)
        {
            return TypeConverter.ToInt64(value);
        }

        /// <summary>
        /// SByte tipine çevirir
        /// </summary>
        /// <param name="value">Hedef değişken</param>
        /// <returns>SByte tipi değer sonuçlar</returns>
        public static sbyte ToSByte(this object value)
        {
            return TypeConverter.ToSByte(value);
        }

        /// <summary>
        /// UShort tipine çevirir
        /// </summary>
        /// <param name="value">Hedef değişken</param>
        /// <returns>UShort tipi değer sonuçlar</returns>
        public static ushort ToUInt16(this object value)
        {
            return TypeConverter.ToUInt16(value);
        }

        /// <summary>
        /// UInt32 tipine çevirir
        /// </summary>
        /// <param name="value">Hedef değişken</param>
        /// <returns>UInt32 tipi değer sonuçlar</returns>
        public static uint ToUInt32(this object value)
        {
            return TypeConverter.ToUInt32(value);
        }

        /// <summary>
        /// UInt64 tipine çevirir
        /// </summary>
        /// <param name="value">Hedef değişken</param>
        /// <returns>UInt64 tipi değer sonuçlar</returns>
        public static ulong ToUInt64(this object value)
        {
            return TypeConverter.ToUInt64(value);
        }

        /// <summary>
        /// Decimal tipine çevirir
        /// </summary>
        /// <param name="value">Hedef değişken</param>
        /// <returns>Decimal tipi değer sonuçlar</returns>
        public static decimal ToDecimal(this object value)
        {
            return TypeConverter.ToDecimal(value);
        }

        /// <summary>
        /// Double tipine çevirir
        /// </summary>
        /// <param name="value">Hedef değişken</param>
        /// <returns>Double tipi değer sonuçlar</returns>
        public static double ToDouble(this object value)
        {
            return TypeConverter.ToDouble(value);
        }

        /// <summary>
        /// Boolean tipine çevirir
        /// </summary>
        /// <param name="value">Hedef değişken</param>
        /// <returns>Boolean tipi değer sonuçlar</returns>
        public static bool ToBoolean(this object value)
        {
            return TypeConverter.ToBoolean(value);
        }

        /// <summary>
        /// DateTime tipine çevirir
        /// </summary>
        /// <param name="value">Hedef değişken</param>
        /// <returns>DateTime tipi değer sonuçlar</returns>
        public static DateTime ToDateTime(this object value)
        {
            return TypeConverter.ToDateTime(value);
        }

        /// <summary>
        /// Char tipine çevirir
        /// </summary>
        /// <param name="value">Hedef değişken</param>
        /// <returns>Char tipi değer sonuçlar</returns>
        public static char ToChar(this object value)
        {
            return TypeConverter.ToChar(value);
        }

        /// <summary>
        /// Belirtilen "T" tipine çevirir
        /// </summary>
        /// <typeparam name="T">IConvertible değişken</typeparam>
        /// <param name="value">Hedef değişken</param>
        /// <returns>T tipi değer sonuçlar</returns>
        public static T ToType<T>(this IConvertible value) where T : IConvertible
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}