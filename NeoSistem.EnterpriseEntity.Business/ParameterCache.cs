//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

namespace NeoSistem.EnterpriseEntity.Business
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    /// <summary>
    /// <para>
    /// Provides parameter caching services for dynamic parameter discovery of stored procedures.
    /// Eliminates the round-trip to the database to derive the parameters and types when a command
    /// is executed more than once.
    /// </para>
    /// </summary>
    public class ParameterCache
  {
    private CachingMechanism cache = new CachingMechanism();

    /// <summary>
    /// <para>
    /// Populates the parameter collection for a command wrapper from the cache 
    /// or performs a round-trip to the database to query the parameters.
    /// </para>
    /// </summary>
    /// <param name="parametreCollection">
    /// <para>The command to add the parameters.</para>
    /// </param>
    /// <param name="entity">
    /// <para>The database to use to set the parameters.</para>
    /// </param>
    public bool SetParameters(ICollection<IDataParameter> parametreCollection, EntityObject entity)
    {
      if(parametreCollection == null)
        throw new ArgumentNullException("parametreCollection");
      if(entity == null)
        throw new ArgumentNullException("entity");
      bool fromCache = false;
      if(AlreadyCached(entity)) {
        AddParametersFromCache(parametreCollection, entity);
        entity.ProcedureNameProcess();
        fromCache = true;
      }
      else {
        entity.DiscoverParameter(parametreCollection);
        IDataParameter[] copyOfParameters = CreateParameterCopy(parametreCollection);
        this.cache.AddParameterSetToCache(entity, parametreCollection, copyOfParameters);
      }
      return fromCache;
    }

    /// <summary>
    /// <para>Empties the parameter cache.</para>
    /// </summary>
    protected internal void Clear()
    {
      this.cache.Clear();
    }

    /// <summary>
    /// <para>Adds parameters to a command using the cache.</para>
    /// </summary>
    /// <param name="parametreCollection">
    /// <para>The command to add the parameters.</para>
    /// </param>
    /// <param name="entity">The database to use.</param>
    protected virtual void AddParametersFromCache(ICollection<IDataParameter> parametreCollection, EntityObject entity)
    {
      IDataParameter[] parameters = this.cache.GetCachedParameterSet(entity);
      foreach(var item in parameters) {
        parametreCollection.Add(item);
      }
    }

    /// <summary>
    /// <para>Checks to see if a cache entry exists for a specific command on a specific connection</para>
    /// </summary>
    /// <param name="command">
    /// <para>The command to check.</para>
    /// </param>
    /// <param name="entity">The database to check.</param>
    /// <returns>True if the parameters are already cached for the provided command, false otherwise</returns>
    private bool AlreadyCached(EntityObject entity)
    {
      return this.cache.IsParameterSetCached(entity);
    }

    private static IDataParameter[] CreateParameterCopy(ICollection<IDataParameter> parametreCollection)
    {
      ICollection<IDataParameter> parameters = parametreCollection;
      IDataParameter[] parameterArray = new IDataParameter[parameters.Count];
      parameters.CopyTo(parameterArray, 0);

      return CachingMechanism.CloneParameters(parameterArray);
    }
  }
}
