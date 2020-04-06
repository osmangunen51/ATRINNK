using NeoSistem.EnterpriseEntity.Business;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using System.Collections.Generic;
using System.Data;

namespace NeoSistem.MakinaTurkiye.Data
{

    public class SurveyOption : BusinessDataEntity
  {

    public DataTable GetItemsBySurveyId(int surveyId)
    {
      var prm = new HashSet<IDataParameter> { 
        surveyId.InSqlParameter("SurveyId")
      };

      DataSet ds = ExecuteDataSet("spSurveyOptionGetItemsBySurveyId", prm);
      
      return ds.Tables[0];
    }

  }

}