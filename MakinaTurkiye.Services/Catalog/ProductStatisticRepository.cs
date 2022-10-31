using Dapper;
using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.Tables.Catalog;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MakinaTurkiye.Services.Catalog
{
    public class ProductStatisticRepository
    {
        private string connectionString;
        public ProductStatisticRepository()
        {
            connectionString = AppSettings.PostgreSqlConnectionString;
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public int Add(ProductStatistic item)
        {
            int lastId = 0;

            using (var dbConnection = new NpgsqlConnection(connectionString))
            {
                dbConnection.Open();
                lastId = dbConnection.ExecuteScalar<int>("INSERT INTO productstatistics1 (ProductId,MemberMainPartyId,IpAdress,UserCity,UserCountry,SingularViewCount,Hour,ViewCount, RecordDate) VALUES(@ProductId,@MemberMainPartyId,@IpAdress,@UserCity,@UserCountry,@SingularViewCount,@Hour,@ViewCount, @RecordDate) RETURNING idpos;", item);
                //lastId=dbConnection.Query<int>("select idpos from productstatistics1 order by idpos desc limit 1").Single();

            }
            return lastId;

        }

        public IEnumerable<ProductStatistic> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ProductStatistic>("SELECT * FROM productstatistics1");
            }
        }

        public List<ProductStatistic> GetProductStatisticsForReport(int memberMainPartyId, DateTime beginDate, DateTime endDate, bool forOneDay)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                string parEndDate = endDate.Date.ToString("yyyyMMdd");
                string parStartDate = beginDate.Date.ToString("yyyyMMdd");
                if (forOneDay)
                {
                    return dbConnection.Query<ProductStatistic>("select * from productstatistics1 where MemberMainPartyId = @memberMainPartyId and date(recorddate)= TO_DATE(@BeginDate, 'YYYYMMDD')", new { memberMainPartyId = memberMainPartyId, BeginDate = parStartDate }).ToList();
                }
                else
                {
                    return dbConnection.Query<ProductStatistic>("select * from productstatistics1 where MemberMainPartyId = @memberMainPartyId  and date(RecordDate) >= TO_DATE(@BeginDate, 'YYYYMMDD') and date(RecordDate) <= TO_DATE(@EndDate, 'YYYYMMDD'); ", new { memberMainPartyId = memberMainPartyId, BeginDate = parStartDate, EndDate = parEndDate }).ToList();

                }
            }
        }

        public List<ProductStatistic> GetProductStatisticsByProductId(int productId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ProductStatistic>("SELECT * FROM productstatistics1 where productid=@id", new { Id = productId }).ToList();
            }
        }

        public List<ProductStatistic> GetProductStatisticByMemberMainPartyID(int memberMainPartyID)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ProductStatistic>($"SELECT * FROM productstatistics1 where membermainpartyid='{memberMainPartyID}'").ToList();
            }
        }

        public ProductStatistic FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ProductStatistic>("SELECT * FROM productstatistics1 WHERE idpos = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM productstatistics1 WHERE Id=@Id", new { Id = id });
            }
        }

        public void Update(ProductStatistic item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE productstatistics1 SET MemberMainPartyId = @MemberMainPartyId,  IpAdress  = @IpAdress, UserCity= @UserCity, UserCountry= @UserCountry, SingularViewCount=@SingularViewCount,Hour=@Hour, ViewCount=@ViewCount  WHERE idpos = @idpos", item);
            }
        }


    }
}

