// <copyright file="IEntity.cs" company="Neo Sistem">
//     Copyright (c) Neo Sistem. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Business.Interfaces
{

    /// <summary>
    /// IEntity arayüzü
    /// </summary>
    public interface IEntity
  {
    EntityAction Action { get; set; }

    object Save();
    object Save(TransactionUI transaction);

    bool Delete(object value);
    bool Delete(object value, TransactionUI transaction);

    bool LoadEntity(object value);
    bool LoadEntity(object value, TransactionUI transaction);
  }
}
