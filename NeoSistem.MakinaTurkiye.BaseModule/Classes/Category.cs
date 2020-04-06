namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public partial class Category
  {
    public Classes.Category GetParentItem(int CategoryId)
    {
      IDataReader reader = null;
      try
      {
        var prms = new HashSet<IDataParameter> { 
          CategoryId.InSqlParameter("CategoryId"),
        };
        Classes.Category category = new Classes.Category();
        reader = ExecuteReader("spCategoryParentByCategoryId", prms);
                if (reader.Read())
                {
                    category.CategoryId = reader["CategoryId"].ToInt32();
                    category.CategoryParentId = reader["CategoryParentId"].ToInt32();
                    category.CategoryName = reader["CategoryName"].ToString();
                    category.CategoryOrder = reader["CategoryOrder"].ToInt32();
                    category.CategoryType = reader["CategoryType"].ToByte();
                    category.Active = reader["Active"].ToBoolean();
                    category.Title = reader["Title"].ToString();
                    category.Description = reader["Description"].ToString();
                    category.Keywords = reader["Keywords"].ToString();
                }
        return category;
      }
      finally
      {
        if (reader != null)
          reader.Close();
      }
    }


    public void MemberGetByBirthDateAndEMail(string Email, DateTime BirthDate)
    {
      var prms = new HashSet<IDataParameter> 
      { 
        Email.InSqlParameter("MemberEmail"),
        BirthDate.InSqlParameter("BirthDate")
      };

      IDataReader reader = null;
      try
      {
        reader = ExecuteReader("spMemberGetByBirthDateAndEmail", prms);
        if (reader.Read())
        {
          for (int i = 0; i < reader.FieldCount; i++)
          {
            var value = reader[i];
            if (value != DBNull.Value)
            {
              var pi = this.GetType().GetProperty(reader.GetName(i));
              if (pi != null)
              {
                pi.SetValue(this, value, null);
              }
            }
          }
        }
      }
      finally
      {
        if (reader != null)
          reader.Close();
      }
    }

  }
}
