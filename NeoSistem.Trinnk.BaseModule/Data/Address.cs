namespace NeoSistem.Trinnk.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class Address : BusinessDataEntity
    {
        public Classes.Address AddressGetByMainPartyId(int MainPartyId)
        {
            IDataReader dr = null;
            try
            {
                var prms = new List<IDataParameter>
        {
            MainPartyId.ToInt32().InSqlParameter("MainPartyId")
        };

                dr = ExecuteReader("spAddressGetByMainPartyId", prms);

                if (dr.Read())
                {
                    var curAddress = new Classes.Address
                    {
                        AddressDefault = dr["AddressDefault"].ToBoolean(),
                        AddressId = dr["AddressId"].ToInt32(),
                        CityId = dr["CityId"].ToInt32(),
                        CountryId = dr["CountryId"].ToInt32(),
                        LocalityId = dr["LocalityId"].ToInt32(),
                        MainPartyId = dr["MainPartyId"].ToInt32(),
                        TownId = dr["TownId"].ToInt32(),
                        AddressTypeId = dr["AddressTypeId"].ToByte(),
                        ApartmentNo = dr["ApartmentNo"].ToString(),
                        Avenue = dr["Avenue"].ToString(),
                        DoorNo = dr["DoorNo"].ToString(),
                        Street = dr["Street"].ToString(),
                    };
                    return curAddress;
                }
                return new Classes.Address();
            }
            finally
            {
                if (dr != null)
                    dr.Close();
            }
        }

        public DataTable CityGetItemByCountryId(int CountryId)
        {
            var prm = new List<IDataParameter>
      {
        CountryId.InSqlParameter("CountryId")
      };
            return ExecuteDataSet("spCityGetItemByCountryId", prm).Tables[0];
        }

        public DataTable GetAddressByMainPartyId(int MainPartyId)
        {
            var prm = new List<IDataParameter>
      {
        MainPartyId.InSqlParameter("MainPartyId")
      };
            return ExecuteDataSet("spAddressGetItemByMainPartyId", prm).Tables[0];
        }

        public DataTable DefaultAddressByMainPartyId(int MainPartyId)
        {
            var prm = new List<IDataParameter>
      {
        MainPartyId.InSqlParameter("MainPartyId")
      };
            return ExecuteDataSet("spAddressDefaulGettItemByMainPartyId", prm).Tables[0];
        }

        public DataTable GetAddressByMainPartyIdAndStoreDealer(int MainPartyId, byte DealerType)
        {
            var prms = new List<IDataParameter>
      {
        MainPartyId.InSqlParameter("MainPartyId"),
        DealerType.InSqlParameter("DealerType")
      };
            return ExecuteDataSet("spAddressGetItemsByMainPartyIdAndStoreDealer", prms).Tables[0];
        }

        public DataTable GetAddressByMainPartyIdAndStoreDealer(int StoreDealerId)
        {
            var prms = new List<IDataParameter>
      {
        StoreDealerId.InSqlParameter("StoreDealerId")
      };
            return ExecuteDataSet("spAddressGetItemsByStoreDealerId", prms).Tables[0];
        }

        public DataTable LocalityGetItemByCityId(int CityId)
        {
            var prm = new List<IDataParameter>
      {
        CityId.InSqlParameter("CityId")
      };
            return ExecuteDataSet("spLocalityGetItemByCityId", prm).Tables[0];
        }

        public DataTable TownGetItemByDistrictId(int DistrictId)
        {
            var prm = new List<IDataParameter>
      {
        DistrictId.InSqlParameter("DistrictId")
      };
            return ExecuteDataSet("spTownGetItemByDistrictId", prm).Tables[0];
        }

    }
}