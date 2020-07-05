// <copyright file="EntityTable.cs"  company="NeoSistem">
//     Copyright (c) EnterpriseEntity. Her hakkı saklıdır.
// </copyright>
namespace NeoSistem.EnterpriseEntity.Business
{
    using Attributes;
    using Core;
    using Core.Data;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// EntityTable abstract class
    /// </summary>
    public abstract class EntityObject : BusinessDataEntity, ITable, IEntity
    {

        /// <summary>
        /// Parametre Cache
        /// </summary>
        private static readonly ParameterCache ParamCache = new ParameterCache();

        /// <summary> 
        ///  Islem türü (private)
        ///  <para>
        ///  Varsayılan deger EntityAction.Insert .
        /// </para>
        /// </summary>
        private EntityAction myAction = EntityAction.Insert;

        /// <summary>
        /// Kaynak tablo bilgisi
        /// </summary>
        public TableAttribute SourceTable
        {
            get { return AttributeTool<TableAttribute>.GetAttribute(this); }
        }

        /// <summary>
        /// Islem türü
        /// <para>
        /// Varsayılan deger EntityAction.Insert .
        /// </para>
        /// </summary>
        public EntityAction Action
        {
            get { return this.myAction; }
            set { this.myAction = value; }
        }

        /// <summary>
        /// Sql prosedür adı
        /// </summary>
        internal string ProcedureName { get; set; }

        /// <summary>
        /// Tablonun birincil anahtarını döndürür.
        /// </summary>
        /// <returns>Tablonun birincil anahtarını IColumn arayüzü olarak döndürür.</returns>
        public IColumn GetPrimaryKey()
        {
            IColumn item = this.GetColumns().SingleOrDefault(c =>
            {
                return c.PrimaryKey == true;
            });

            return item;
        }

        /// <summary>
        /// Tablonun birincil anahtarını döndürür.
        /// </summary>
        /// <returns>Tablonun birincil anahtarını PropertyInfo arayüzü olarak döndürür.</returns>
        internal PropertyInfo GetPrimaryProperty()
        {
            PropertyInfo item = this.GetColumnProperties().SingleOrDefault(c =>
            {
                return AttributeTool<ColumnAttribute>.GetAttribute(c).PrimaryKey == true;
            });
            return item;
        }

        /// <summary>
        /// Tablonun tüm alanlarını döndürür
        /// </summary>
        /// <returns>Tablonun tüm alanlarını IEnumerable olarak döndürür</returns>
        public IEnumerable<IColumn> GetColumns()
        {
            foreach (PropertyInfo item in this.GetColumnProperties())
            {
                ColumnAttribute column = AttributeTool<ColumnAttribute>.GetAttribute(item);
                column.Value = item.GetValue(this, null);

                yield return column;
            }
        }

        /// <summary>
        /// Tablonun tüm iliskili alanlarını döndürür
        /// </summary>
        /// <returns>Tablonun tüm iliskilli alanlarını IEnumerable olarak döndürür</returns>
        public IEnumerable<IColumn> GetForeignKeys()
        {
            throw new NotSupportedException("Bu özellik desteklenmiyor.");
        }

        /// <summary>
        /// Kayıt veya güncelleme islemi gerceklestirir.
        /// </summary>
        /// <returns>Etkilenen kayıtın birincil anahtar degerini döndürür.</returns>
        public virtual object Save()
        {
            return this.Save(null);
        }

        /// <summary>
        /// Belirtilen islem (TransactionUI) içinde kayıt veya güncelleme islemi gerceklestirir.
        /// </summary>
        /// <param name="transaction">Calıstırılacak islem (TransactionUI)</param>
        /// <returns>Etkilenen kayıtın birincil anahtar degerini döndürür.</returns>
        public virtual object Save(TransactionUI transaction)
        {
            ICollection<IDataParameter> prms = new HashSet<IDataParameter>();

            ParameterSetCache(prms);

            object result = null;

            if (transaction == null)
            {
                result = ExecuteNonQuery(this.ProcedureName, prms);
            }
            else
            {
                result = ExecuteNonQuery(this.ProcedureName, prms, transaction);
            }

            PropertyInfo pColumn = GetPrimaryProperty();


            if (pColumn != null)
            {
                if (result != null)
                {
                    pColumn.SetValue(this, Convert.ChangeType(result, pColumn.PropertyType), null);
                }
            }

            return result;
        }


        private void ParameterSetCache(ICollection<IDataParameter> prms)
        {
            bool fromCache = ParamCache.SetParameters(prms, this);
            if (fromCache)
            {
                this.DiscoverParameterValues(prms);
            }
        }

        /// <summary>
        /// Silme islemi gerceklestirir.
        /// </summary>
        /// <typeparam name="T">Birincil anahtar tipi</typeparam>
        /// <param name="value">Birincil anahtar degeri</param>
        /// <returns>Islem basarılı olup olamadıgının bilgisini döndürür.</returns>
        public virtual bool Delete(object value)
        {
            return Delete(value, null);
        }

        /// <summary>
        /// Belirtilen islem (TransactionUI) içinde silme islemi gerceklestirir.
        /// </summary> 
        /// <param name="value">Birincil anahtar degeri</param>
        /// <param name="transaction">Calıstırılacak islem (TransactionUI)</param>
        /// <returns>Islemin basarılı olup olamadıgının bilgisini döndürür.</returns>
        public virtual bool Delete(object value, TransactionUI transaction)
        {
            try
            {
                object rowAffeced = 0;

                IColumn column = this.GetPrimaryKey();
                column.Value = value;

                ICollection<IDataParameter> prms = new HashSet<IDataParameter>();
                IDataParameter parameter = this.AddParameter(column, ParameterDirection.Input);
                prms.Add(parameter);

                this.Action = EntityAction.Delete;

                ProcedureNameProcess();

                if (transaction == null)
                {
                    rowAffeced = ExecuteNonQuery(this.ProcedureName, prms);
                }
                else
                {
                    rowAffeced = ExecuteNonQuery(this.ProcedureName, prms, transaction);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Verilen degere uygun satır bilgisini yükler.
        /// </summary> 
        /// <param name="value">Birincil anahtar degeri</param>
        /// <returns>Islemin basarılı olup olamadıgının bilgisini döndürür</returns>
        public virtual bool LoadEntity(object value)
        {
            return this.LoadEntity(value, null);
        }

        /// <summary>
        /// Verilen degere uygun satır bilgisini yükler.
        /// </summary> 
        /// <param name="value">Birincil anahtar degeri</param>
        /// <returns>Islemin basarılı olup olamadıgının bilgisini döndürür</returns>
        public virtual bool LoadEntity(object value, TransactionUI transaction)
        {
            IDataReader dataReader = null;
            try
            {
                this.Action = EntityAction.Item;

                ProcedureNameProcess();

                IColumn column = this.GetPrimaryKey();
                column.Value = value;

                ICollection<IDataParameter> prms = new HashSet<IDataParameter>();
                IDataParameter parameter = this.AddParameter(column, ParameterDirection.Input);
                prms.Add(parameter);

                if (transaction == null)
                {
                    dataReader = ExecuteReader(this.ProcedureName, prms);
                }
                else
                {
                    dataReader = ExecuteReader(this.ProcedureName, prms, transaction);
                }

                object drValue = null;

                if (dataReader.Read())
                {

                    IEnumerable<PropertyInfo> properties = this.GetColumnProperties();

                    foreach (PropertyInfo item in properties)
                    {
                        try
                        {
                            drValue = dataReader[item.Name];
                            if (drValue != DBNull.Value)
                            {
                                item.SetValue(this, drValue, null);
                            }
                        }
                        catch
                        {

                        }
                    }

                    this.Action = EntityAction.Update;
                    dataReader.Close();
                    dataReader.Dispose();

                    return true;
                }

            }
            catch
            {
                dataReader.Close();
                dataReader.Dispose();
                return false;
            }

            return false;
        }

        /// <summary>
        /// Tüm verileri yükler.
        /// </summary>
        /// <returns>Tüm verileri okunabilir bir DataSet olarak döndürür.</returns> 
        public virtual System.Data.DataSet GetDataSet()
        {
            this.Action = EntityAction.Select;

            ProcedureNameProcess();

            DataSet ds = ExecuteDataSet(this.ProcedureName);

            return ds;
        }

        /// <summary>
        /// Tüm verileri yükler.
        /// </summary>
        /// <returns>Tüm verileri okunabilir bir DataTable olarak döndürür.</returns> 
        public virtual System.Data.DataTable GetDataTable()
        {
            DataSet ds = this.GetDataSet();
            return ds.Tables[0];
        }

        /// <summary>
        /// Cache üzerinde bulunan parametrelerin degerlerini yeniler.
        /// </summary>
        /// <param name="prms">Cache üzerinde bulunan parametre listesi</param>
        internal void DiscoverParameterValues(ICollection<IDataParameter> prms)
        {
            foreach (IDataParameter item in prms)
            {
                IColumn column = this.GetColumns().Single(c =>
                {
                    return c.ColumnName == item.SourceColumn;
                });
                if (column != null)
                {
                    item.Value = column.Value ?? DBNull.Value;
                }
            }
        }

        /// <summary>  
        /// </summary>
        /// <param name="prms">Sql parametre listesi</param>
        internal void DiscoverParameter(ICollection<IDataParameter> prms)
        {
            IDataParameter parameter = null;

            ProcedureNameProcess();

            foreach (IColumn item in this.GetColumns())
            {
                if (item.Identity)
                {
                    if (this.Action == EntityAction.Update)
                    {
                        parameter = this.AddParameter(item, ParameterDirection.Input);
                    }
                    else
                    {
                        parameter = this.AddParameter(item, ParameterDirection.Output);
                    }
                }
                else
                {
                    parameter = this.AddParameter(item, ParameterDirection.Input);
                }

                prms.Add((IDataParameter)((ICloneable)parameter).Clone());
            }
        }


        /// <summary>
        /// IColumn arayüzünü IDataParameter arayüzüne dönüstürülme islemini gerceklestirir.
        /// </summary>
        /// <param name="column">IColumn arayüzü</param>
        /// <param name="direction">Parametre yönü</param>
        /// <returns>IColumn arayüzünü IDataParameter arayüzüne döndürür</returns>
        protected IDataParameter AddParameter(IColumn column, ParameterDirection direction)
        {
            return SqlParameterConverter.CreateParameter(column.ColumnName, column.DbType, column.Value, direction, column.Lenght);
        }

        /// <summary>
        /// ColumnAttribute özelligine (Attribute) sahip özelliklerin (Property) listesini döndürür 
        /// </summary>
        /// <returns>ColumnAttribute özelligine (Attribute) sahip özelliklerin (Property) listesini Array olarak döndürür</returns>
        protected IEnumerable<PropertyInfo> GetColumnProperties()
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            return properties.Where(delegate (PropertyInfo p)
            {
                return AttributeTool<ColumnAttribute>.GetAttribute(p) != null;
            });
        }

        /// <summary>
        /// Tablo prosedür görüntü adı
        /// </summary> 
        /// <returns>Tablo prosedür görüntü adı döndürür</returns>
        internal string ProcedurePrefix()
        {
            return this.SourceTable.TableName;
        }

        internal void ProcedureNameProcess()
        {
            switch (this.Action)
            {
                case EntityAction.Insert:
                    this.ProcedureName = String.Format("sp{0}Insert", ProcedurePrefix());
                    break;
                case EntityAction.Update:
                    this.ProcedureName = String.Format("sp{0}Update", ProcedurePrefix());
                    break;
                case EntityAction.Delete:
                    this.ProcedureName = String.Format("sp{0}Delete", ProcedurePrefix());
                    break;
                case EntityAction.Select:
                    this.ProcedureName = String.Format("sp{0}GetDataSet", ProcedurePrefix());
                    break;
                case EntityAction.Item:
                    this.ProcedureName = String.Format("sp{0}GetItem", ProcedurePrefix());
                    break;
                default:
                    break;
            }
        }

    }
}