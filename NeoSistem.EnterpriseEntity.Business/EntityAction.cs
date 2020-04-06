// <copyright file="EntityAction.cs" company="NeoSistem">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Business
{

    /// <summary>
    /// Islem türü
    /// </summary>
    public enum EntityAction
  {
    /// <summary>
    /// Ekleme islemi
    /// </summary>
    Insert = 0,

    /// <summary>
    /// Güncelleme islemi
    /// </summary>
    Update = 1,

    /// <summary>
    /// Silme islemi
    /// </summary>
    Delete = 2,

    /// <summary>
    /// Listeleme 
    /// </summary>
    Select = 3,

    /// <summary>
    /// Tek kayıt
    /// </summary>
    Item = 4 
  } 
}
