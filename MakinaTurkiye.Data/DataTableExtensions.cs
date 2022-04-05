using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MakinaTurkiye.Data
{
    public static class DataTableExtensions
    {
        public static DataTable Where(this DataTable datatable, string whereClause)
        {
            DataTable newDt = datatable.Clone();
            datatable.Select(whereClause).CopyToDataTable(newDt, LoadOption.OverwriteChanges,
              delegate (object sender, FillErrorEventArgs f)
              {
                  throw f.Errors;
              });
            return newDt;
        }

        public static bool HasRows(this DataTable dt)
        {
            return dt != null && dt.Rows != null && dt.Rows.Count > 0;
        }


        public static DataTable SelectTop(this DataTable datatable, int count)
        {
            DataTable newDt = datatable.Clone();
            datatable.Select().Take(count).CopyToDataTable(newDt, LoadOption.OverwriteChanges,
              delegate (object sender, FillErrorEventArgs f)
              {
                  throw f.Errors;
              });
            return newDt;
        }

        public static DataTable SelectTop(this DataTable datatable, int count, string whereClause)
        {
            DataTable newDt = datatable.Clone();
            datatable.Select(whereClause).Take(count).CopyToDataTable(newDt, LoadOption.OverwriteChanges,
              delegate (object sender, FillErrorEventArgs f)
              {
                  throw f.Errors;
              });
            return newDt;
        }


        public static object RowScalar(this DataTable datatable, string columnname)
        {
            DataRow row = datatable.Select().SingleOrDefault();

            if (row != null)
            {
                return row[columnname];
            }

            return null;
        }

        public static object RowScalar(this DataTable datatable, string columnname, string whereClause)
        {
            DataRow row = datatable.Select(whereClause).SingleOrDefault();

            if (row != null)
            {
                return row[columnname];
            }

            return null;
        }

        public static DataTable Between(this DataTable dt, int beginRow, int endRow)
        {
            DataTable newDt = dt.Clone();

            for (int i = beginRow; i < endRow; i++)
            {
                DataRow row = newDt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row[j] = dt.Rows[i][j];
                }
                newDt.Rows.Add(row);
            }
            return newDt;
        }

        public static List<TType> DataTableToObjectList<TType>(this DataTable datatable) where TType : new()
        {
            TType generic = new TType();

            List<TType> list = new List<TType>();
            if (datatable == null)
            {
                return list;
            }

            foreach (DataRow row in datatable.Rows)
            {
                generic = new TType();
                foreach (DataColumn column in datatable.Columns)
                {
                    PropertyInfo pInfo = generic.GetType().GetProperty(column.ColumnName);
                    if (pInfo != null)
                    {
                        object value = row[column.ColumnName];
                        if (value != DBNull.Value)
                        {
                            pInfo.SetValue(generic, row[column.ColumnName], null);
                        }
                    }
                }
                list.Add(generic);
            }

            return list;
        }

        public static List<TType> DataTableToObjectList<TType, T1>(this DataTable datatable)
            where TType : new()
            where T1 : new()
        {
            TType generic = new TType();

            List<TType> list = new List<TType>();
            if (datatable == null)
            {
                return list;
            }

            foreach (DataRow row in datatable.Rows)
            {
                generic = new TType();
                foreach (DataColumn column in datatable.Columns)
                {
                    PropertyInfo pInfo = generic.GetType().GetProperty(column.ColumnName);
                    if (pInfo != null)
                    {
                        if (pInfo.PropertyType == typeof(T1))
                        {
                            var innerpInfo = pInfo.PropertyType.GetProperty(pInfo.Name);
                            if (innerpInfo != null)
                            {
                                object value = row[column.ColumnName];
                                if (value != DBNull.Value)
                                {
                                    innerpInfo.SetValue(generic, row[column.ColumnName], null);
                                }
                            }
                        }
                        else
                        {
                            object value = row[column.ColumnName];
                            if (value != DBNull.Value)
                            {
                                pInfo.SetValue(generic, row[column.ColumnName], null);
                            }
                        }

                    }
                }
                list.Add(generic);
            }

            return list;
        }



        public static TResult AsRowScalar<TResult>(this DataTable datatable) where TResult : new()
        {
            TResult generic = new TResult();

            DataRow row = null;
            if (datatable != null && datatable.Rows.Count > 0)
            {
                row = datatable.Rows[0];
            }
            else
            {
                return generic;
            }

            foreach (DataColumn column in datatable.Columns)
            {
                var pInfo = generic.GetType().GetProperty(column.ColumnName);
                if (pInfo != null)
                {
                    object value = row[column.ColumnName];
                    if (value != DBNull.Value)
                    {
                        pInfo.SetValue(generic, row[column.ColumnName], null);
                    }
                }
            }

            return generic;
        }
    }
}
