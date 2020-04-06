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
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    /// <devdoc>
    /// CachingMechanism provides caching support for stored procedure 
    /// parameter discovery and caching
    /// </devdoc>
    internal class CachingMechanism
  {
    private Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

    /// <devdoc>
    /// Create and return a copy of the IDataParameter array.
    /// </devdoc>        
    public static IDataParameter[] CloneParameters(IDataParameter[] originalParameters)
    {
      IDataParameter[] clonedParameters = new IDataParameter[originalParameters.Length];

      for(int i = 0, j = originalParameters.Length; i < j; i++) {
        clonedParameters[i] = (IDataParameter)((ICloneable)originalParameters[i]).Clone();
      }

      return clonedParameters;
    }

    /// <devdoc>
    /// Empties all items from the cache
    /// </devdoc>
    public void Clear()
    {
      this.paramCache.Clear();
    }

    /// <devdoc>
    /// Add a parameter array to the cache for the command.
    /// </devdoc>        
    public void AddParameterSetToCache(EntityObject entity, ICollection<IDataParameter> parametreCollection, IDataParameter[] parameters)
    {
      //string storedProcedure = command.CommandText;
      string key = CreateHashKey(entity);
      this.paramCache[key] = parameters;
    }

    /// <devdoc>
    /// Gets a parameter array from the cache for the command. Returns null if no parameters are found.
    /// </devdoc>        
    public IDataParameter[] GetCachedParameterSet(EntityObject entity)
    {
      //string storedProcedure = command.CommandText;
      string key = CreateHashKey(entity);
      IDataParameter[] cachedParameters = (IDataParameter[])(this.paramCache[key]);
      return CloneParameters(cachedParameters);
    }

    /// <devdoc>
    /// Gets if a given stored procedure on a specific connection string has a cached parameter set
    /// </devdoc>        
    public bool IsParameterSetCached(EntityObject entity)
    {
      string hashKey = CreateHashKey(entity);
      return this.paramCache[hashKey] != null;
    }

    private static string CreateHashKey(EntityObject entity)
    {
      return entity.SourceTable + ":" + entity.Action.ToString();
    }
  }
}
