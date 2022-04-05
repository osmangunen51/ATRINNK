namespace NeoSistem.MakinaTurkiye.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class Member : BusinessDataEntity
    {
        public DataTable Search(ref int TotalRecord, int PageDimension, int Page, string Where, string OrderName, string Order)
        {
            var prms = new List<IDataParameter> {
        TotalRecord.InOutSqlParameter("TotalRecord"),
        PageDimension.InSqlParameter("PageDimension"),
        Page.InSqlParameter("Page"),
        Where.InSqlParameter("Where", SqlDbType.NVarChar),
        OrderName.InSqlParameter("OrderName", SqlDbType.NVarChar),
        Order.InSqlParameter("Order", SqlDbType.NVarChar)
      };

            DataTable dt = ExecuteDataSet("spMemberSearchNew", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();
            return dt;
        }
        public DataTable MemberActivationCategory(int CategoryId)
        {
            var prms = new List<IDataParameter> {
        CategoryId.InSqlParameter("CategoryId")
      };

            DataTable dt = ExecuteDataSet("MemberActivationCategory", prms).Tables[0];
            return dt;
        }

        public DataTable MemberGetItemsByMainPartyFullName(string MainPartyFullName)
        {
            var prm = new HashSet<IDataParameter>
      {
        MainPartyFullName.InSqlParameter("MainPartyFullName")
      };
            DataTable dt = ExecuteDataSet("spMemberGetItemsByMainPartyFullName", prm).Tables[0];
            return dt;
        }

        public bool MemberActivation(string activationCode)
        {
            bool Statu = false;
            var prm = new List<IDataParameter>
      {
        activationCode.InSqlParameter("ActivationCode"),
        Statu.OutSqlParameter("Statu")
      };
            ExecuteScalar("spMemberActivation", prm);
            Statu = prm[1].Value.ToBoolean();
            return Statu;
        }

        public bool HasEmail(string email)
        {
            bool statu = false;
            var prm = new List<IDataParameter>
      {
        email.InSqlParameter("MemberEmail"),
        statu.OutSqlParameter("Statu")
      };
            ExecuteScalar("spMemberHasEmail", prm);
            statu = prm[1].Value.ToBoolean();
            return statu;
        }

    }
}