// <copyright file="DateTimeExtension.cs" company="EnterpriseEntity">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Extensions
{
    using System;

    /// <summary>
    /// DateTime Extension Class
    /// </summary>
    public static class DateTimeExtension
  {
    /// <summary>
    /// Gün öncesi metodu
    /// </summary>
    /// <param name="datetime">DateTime değişken</param>
    /// <param name="value">Gün değeri</param>
    /// <returns>DateTime tipi değer sonuçlar</returns>
    public static DateTime LastDays(this DateTime datetime, double value)
    {
      return datetime - TimeSpan.FromDays(value);
    }

    /// <summary>
    /// Çalışma Gün sonrası metodu
    /// </summary>
    /// <param name="datetime">DateTime değişken</param>
    /// <param name="value">Gün değeri</param>
    /// <returns>DateTime tipi değer sonuçlar</returns>
    public static DateTime AddWorkDays(this DateTime datetime, double value)
    {
      DateTime tempDate = datetime.AddDays(1);
      bool isWeekend = false;

      while (tempDate.DayOfWeek == DayOfWeek.Sunday || tempDate.DayOfWeek == DayOfWeek.Saturday)
      {
        isWeekend = true;
        value += 1;
        tempDate = tempDate.AddDays(1);
      }

      if (!isWeekend)
      {
        tempDate = datetime.AddDays(2);
        while (tempDate.DayOfWeek == DayOfWeek.Sunday || tempDate.DayOfWeek == DayOfWeek.Saturday)
        {
          value += 1;
          tempDate = tempDate.AddDays(1);
        }
      }

      return datetime.AddDays(value);
    }

    /// <summary>
    /// Çalışma Gün öncesi metodu
    /// </summary>
    /// <param name="datetime">DateTime değişken</param>
    /// <param name="value">Gün değeri</param>
    /// <returns>DateTime tipi değer sonuçlar</returns>
    public static DateTime LastWorkDays(this DateTime datetime, double value)
    {
      DateTime tempDate = datetime.LastDays(1);
      bool isWeekend = false;
      while (tempDate.DayOfWeek == DayOfWeek.Sunday || tempDate.DayOfWeek == DayOfWeek.Saturday)
      {
        isWeekend = true;
        value += 1;
        tempDate = tempDate.LastDays(1);
      }

      if (!isWeekend)
      {
        tempDate = datetime.LastDays(2);
        while (tempDate.DayOfWeek == DayOfWeek.Sunday || tempDate.DayOfWeek == DayOfWeek.Saturday)
        {
          value += 1;
          tempDate = tempDate.LastDays(1);
        }
      }

      return datetime.LastDays(value);
    }

    /// <summary>
    /// Saat öncesi metodu
    /// </summary>
    /// <param name="datetime">DateTime değişken</param>
    /// <param name="value">Saat değeri</param>
    /// <returns>DateTime tipi değer sonuçlar</returns>
    public static DateTime LastHours(this DateTime datetime, double value)
    {
      return datetime - TimeSpan.FromHours(value);
    }

    /// <summary>
    /// Milli saniye öncesi metodu
    /// </summary>
    /// <param name="datetime">DateTime değişken</param>
    /// <param name="value">Mili saniye değeri</param>
    /// <returns>DateTime tipi değer sonuçlar</returns>
    public static DateTime LastMilliseconds(this DateTime datetime, double value)
    {
      return datetime - TimeSpan.FromMilliseconds(value);
    }

    /// <summary>
    /// Dakika öncesi metodu
    /// </summary>
    /// <param name="datetime">DateTime değişken</param>
    /// <param name="value">Dakika değeri</param>
    /// <returns>DateTime tipi değer sonuçlar</returns>
    public static DateTime LastMinutes(this DateTime datetime, double value)
    {
      return datetime - TimeSpan.FromMinutes(value);
    }

    /// <summary>
    /// Saniye öncesi metodu
    /// </summary>
    /// <param name="datetime">DateTime değişken</param>
    /// <param name="value">Saniye değeri</param>
    /// <returns>DateTime tipi değer sonuçlar</returns>
    public static DateTime LastSeconds(this DateTime datetime, double value)
    {
      return datetime - TimeSpan.FromSeconds(value);
    }

    /// <summary>
    /// Saniye öncesi metodu
    /// </summary>
    /// <param name="datetime">DateTime değişken</param>
    /// <param name="value">Saniye değeri</param>
    /// <returns>DateTime tipi değer sonuçlar</returns>
    public static DateTime LastTicks(this DateTime datetime, long value)
    {
      return datetime - TimeSpan.FromTicks(value);
    }

    /// <summary>
    /// Ay öncesi öncesi metodu
    /// </summary>
    /// <param name="datetime">DateTime değişken</param>
    /// <param name="months">Ay  değeri</param>
    /// <returns>DateTime tipi değer sonuçlar</returns>
    public static DateTime LastMonths(this DateTime datetime, int months)
    {
      int month = datetime.Month;

      int value = (month - 1) - months;
      if (value >= 0)
      {
        month = (value % 12) + 1;
      }
      else
      {
        month = 12 + ((value + 1) % 12);
      }

      int year = (months + month) / 12;
      year = datetime.Year - year;

      return new DateTime(year, month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
    }

    /// <summary>
    /// Yıl öncesi metodu
    /// </summary>
    /// <param name="datetime">DateTime değişken</param>
    /// <param name="years">Yıl değeri</param>
    /// <returns>DateTime tipi değer sonuçlar</returns>
    public static DateTime LastYears(this DateTime datetime, int years)
    { 
      return datetime.LastMonths(years * 12);
    } 
  }
}