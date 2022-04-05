namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Extensions.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public partial class Member
    {
        public void Login()
        {
            IDataReader reader = null;
            try
            {
                var prms = new HashSet<IDataParameter> {
          MemberEmail.InSqlParameter("EMail"),
          MemberPassword.InSqlParameter("Password")
        };

                reader = ExecuteReader("spMemberLogin", prms);
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
