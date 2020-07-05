// <copyright file="IColumn.cs" company="EnterpriseEntity">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Business.Interfaces
{
    using System.Data;

    /// <summary>
    /// Sql alan arayüzü
    /// </summary>
    public interface IColumn
  { 
    /// <summary>
    /// Birincil alan anahtarını döndürür ve düzenler
    /// </summary>
    bool PrimaryKey { get; set; }
    /// <summary>
    /// Alan adını döndürür veya düzenler.
    /// </summary>
    string ColumnName { get; set; }

    /// <summary>
    /// Alan tipini döndürür veya düzenler.
    /// </summary>
    SqlDbType DbType { get; set; }

    /// <summary>
    /// Alan tipini döndürür veya düzenler.
    /// </summary>
    object Value { get; set; }

    /// <summary>
    /// Alan boş değer bilgisini döndürür veya düzenler.
    /// </summary>
    bool AllowNull { get; set; }

    /// <summary>
    /// Alan boyutunu döndürür veya düzenler.
    /// </summary>
    int Lenght { get; set; }

    /// <summary>
    /// Alan otomatik sayı bilgisini döndürür veya düzenler.
    /// </summary>
    bool Identity { get; set; }
  }
}