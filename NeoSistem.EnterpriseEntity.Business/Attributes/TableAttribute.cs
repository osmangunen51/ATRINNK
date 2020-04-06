namespace NeoSistem.EnterpriseEntity.Business.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
  public sealed class TableAttribute : Attribute
  { 
    public string ShemaName { get; set; }
    public string TableName { get; set; }

    public TableAttribute(string ShemaName, string TableName)
    {
      this.ShemaName = ShemaName;
      this.TableName = TableName;
    }
     
    public static implicit operator string(TableAttribute tableAttribute)
    { 
      return "[" + tableAttribute.ShemaName + "].[" + tableAttribute.TableName + "]";
    }
     
    public TableAttribute(string TableName) : this("dbo", TableName) { }  
  }
}
