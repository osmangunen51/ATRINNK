// <copyright file="DataTableExtension.cs" company="EnterpriseEntity">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Extensions.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// DataTable Extension Class
    /// </summary>
    public static class DataTableExtension
  {
    /// <summary>
    /// DataTable Where sorgu extension
    /// </summary>
    /// <param name="datatable">DataTable değişken</param>
    /// <param name="whereClause">Where filtresi</param>
    /// <returns>DataTable değer sonuçlar</returns>
    public static DataTable Where(this DataTable datatable, string whereClause)
    {
      DataTable newDt = datatable.Clone();
      datatable.Select(whereClause).CopyToDataTable(newDt, LoadOption.OverwriteChanges,
        delegate(object sender, FillErrorEventArgs f) {
          throw f.Errors;
        });
      return newDt;
    }

    /// <summary>
    /// DataTable Satır Varmı extension
    /// </summary>
    /// <param name="datatable">DataTable değişken</param>
    /// <returns>bool değer sonuçlar</returns>
    public static bool HasRows(this DataTable dt)
    {
      return dt != null && dt.Rows != null && dt.Rows.Count > 0;
    }

    /// <summary>
    /// DataTable Top sorgu extension
    /// </summary>
    /// <param name="datatable">DataTable değişken</param>
    /// <param name="count">Kayıt sayısı</param>
    /// <returns>DataTable değer sonuçlar</returns>
    public static DataTable SelectTop(this DataTable datatable, int count)
    {
      DataTable newDt = datatable.Clone();
      datatable.Select().Take(count).CopyToDataTable(newDt, LoadOption.OverwriteChanges,
        delegate(object sender, FillErrorEventArgs f) {
          throw f.Errors;
        });
      return newDt;
    }

    /// <summary>
    /// DataTable Top sorgu extension
    /// </summary>
    /// <param name="datatable">DataTable değişken</param>
    /// <param name="count">Kayıt sayısı</param>
    /// <param name="whereClause">Where filtresi</param>
    /// <returns>DataTable değer sonuçlar</returns>
    public static DataTable SelectTop(this DataTable datatable, int count, string whereClause)
    {
      DataTable newDt = datatable.Clone();
      datatable.Select(whereClause).Take(count).CopyToDataTable(newDt, LoadOption.OverwriteChanges,
        delegate(object sender, FillErrorEventArgs f) {
          throw f.Errors;
        });
      return newDt;
    }


    public static object RowScalar(this DataTable datatable, string columnname)
    {
      DataRow row = datatable.Select().SingleOrDefault();

      if(row != null) {
        return row[columnname];
      }

      return null;
    }

    public static object RowScalar(this DataTable datatable, string columnname, string whereClause)
    {
      DataRow row = datatable.Select(whereClause).SingleOrDefault();

      if(row != null) {
        return row[columnname];
      }

      return null;
    }

    public static DataTable Between(this DataTable dt, int beginRow, int endRow)
    {
      DataTable newDt = dt.Clone();

      for(int i = beginRow; i < endRow; i++) {
        DataRow row = newDt.NewRow();
        for(int j = 0; j < dt.Columns.Count; j++) {
          row[j] = dt.Rows[i][j];
        }
        newDt.Rows.Add(row);
      }
      return newDt;
    }

    public static ICollection<TResult> AsCollection<TResult>(this DataTable datatable) where TResult : new()
    {
      TResult generic = new TResult();

      HashSet<TResult> hashCollection = new HashSet<TResult>();
      if(datatable == null) {
        return hashCollection;
      }

      foreach(DataRow row in datatable.Rows) {
        generic = new TResult();
        foreach(DataColumn column in datatable.Columns) {
          PropertyInfo pInfo = generic.GetType().GetProperty(column.ColumnName);
          if(pInfo != null) {
            object value = row[column.ColumnName];
            if(value != DBNull.Value) {
              pInfo.SetValue(generic, row[column.ColumnName], null);
            }
          }
        }
        hashCollection.Add(generic);
      }

      return hashCollection;
    }

    public static ICollection<TResult> AsCollection<TResult, T1>(this DataTable datatable)
      where TResult : new()
      where T1 : new()
    {
      TResult generic = new TResult();

      HashSet<TResult> hashCollection = new HashSet<TResult>();
      if(datatable == null) {
        return hashCollection;
      }

      foreach(DataRow row in datatable.Rows) {
        generic = new TResult();
        foreach(DataColumn column in datatable.Columns) {
          PropertyInfo pInfo = generic.GetType().GetProperty(column.ColumnName);
          if(pInfo != null) {
            if(pInfo.PropertyType == typeof(T1)) {
              var innerpInfo = pInfo.PropertyType.GetProperty(pInfo.Name);
              if(innerpInfo != null) {
                object value = row[column.ColumnName];
                if(value != DBNull.Value) {
                  innerpInfo.SetValue(generic, row[column.ColumnName], null);
                }
              }
            }
            else {
              object value = row[column.ColumnName];
              if(value != DBNull.Value) {
                pInfo.SetValue(generic, row[column.ColumnName], null);
              }
            }

          }
        }
        hashCollection.Add(generic);
      }

      return hashCollection;
    }

    public static TResult AsModel<TResult>(this IDataReader reader) where TResult : new()
    {
      TResult generic = new TResult();
      try {

        if(reader.Read()) {
          for(int i = 0; i < reader.FieldCount; i++) {
            PropertyInfo pInfo = generic.GetType().GetProperty(reader.GetName(i));
            if(pInfo != null) {
              object value = reader.GetValue(i);
              if(value != DBNull.Value) {
                pInfo.SetValue(generic, value, null);
              }
            }
          }
        }
      }
      finally {
        if(reader != null)
          reader.Close();
      }
      return generic;
    }

    public static TResult AsModel<TResult>(this IDataReader reader, TResult generic) where TResult : new()
    {
      try {
        if(reader.Read()) {
          for(int i = 0; i < reader.FieldCount; i++) {
            PropertyInfo pInfo = generic.GetType().GetProperty(reader.GetName(i));
            if(pInfo != null) {
              object value = reader.GetValue(i);
              if(value != DBNull.Value) {
                pInfo.SetValue(generic, value, null);
              }
            }
          }
        }
      }
      finally {
        if(reader != null)
          reader.Close();
      }
      return generic;
    }

    public static TResult AsRowScalar<TResult>(this DataTable datatable) where TResult : new()
    {
      TResult generic = new TResult();

      DataRow row = null;
      if(datatable != null && datatable.Rows.Count > 0) {
        row = datatable.Rows[0];
      }
      else {
        return generic;
      }

      foreach(DataColumn column in datatable.Columns) {
        var pInfo = generic.GetType().GetProperty(column.ColumnName);
        if(pInfo != null) {
          object value = row[column.ColumnName];
          if(value != DBNull.Value) {
            pInfo.SetValue(generic, row[column.ColumnName], null);
          }
        }
      }

      return generic;
    }



  }
}
