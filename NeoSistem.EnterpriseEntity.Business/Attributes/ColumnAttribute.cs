namespace NeoSistem.EnterpriseEntity.Business.Attributes
{
    using Interfaces;
    using System;
    using System.Data;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
  public sealed class ColumnAttribute : Attribute, IColumn
  {

    bool myPrimaryKey;
    string myColumnName;
    SqlDbType myDbType;
    object myValue;
    bool myAllowNull;
    int myLenght;
    bool myIdentity;

    public ColumnAttribute(string FieldName, SqlDbType DbType, int Length, bool Identity, bool PrimaryKey, bool AllowNulls)
    {
      this.myColumnName = FieldName;
      this.myDbType = DbType;
      this.myLenght = Length;
      this.myIdentity = Identity;
      this.myPrimaryKey = PrimaryKey;
      this.myAllowNull = AllowNulls;
    }

    public ColumnAttribute(string FieldName, SqlDbType DbType, int Length) : this(FieldName, DbType, Length, false, false, true) { }

    public ColumnAttribute(string FieldName, SqlDbType DbType) : this(FieldName, DbType, 0, false, false, true) { }


    public bool PrimaryKey
    {
      get { return myPrimaryKey; }
      set { myPrimaryKey = value; }
    }

    public string ColumnName
    {
      get { return myColumnName; }
      set { myColumnName = value; }
    }

    public SqlDbType DbType
    {
      get { return myDbType; }
      set { myDbType = value; }
    }

    public object Value
    {
      get { return myValue; }
      set { myValue = value; }
    }

    public bool AllowNull
    {
      get { return myAllowNull; }
      set { myAllowNull = value; }
    }

    public int Lenght
    {
      get { return myLenght; }
      set { myLenght = value; }
    }

    public bool Identity
    {
      get { return myIdentity; }
      set { myIdentity = value; }
    }
  }
}
