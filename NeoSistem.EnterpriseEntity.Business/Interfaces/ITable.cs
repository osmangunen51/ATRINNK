// <copyright file="ITable.cs" company="EnterpriseEntity">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Business.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Sql tablo arayüzü
    /// </summary>
    public interface ITable
    {
        /// <summary>
        /// Tablonun birincil anahtar alanını döndürür.
        /// </summary>
        IColumn GetPrimaryKey();

        /// <summary>
        /// Tablonun tüm alanlarını döndürür.
        /// </summary>
        IEnumerable<IColumn> GetColumns();

        /// <summary>
        /// Tablonun yabancı anahtar alanlarını döndürür.
        /// </summary>
        IEnumerable<IColumn> GetForeignKeys();
    }
}
