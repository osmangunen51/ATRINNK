using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Data;
using MakinaTurkiye.Entities.StoredProcedures.Members;
using MakinaTurkiye.Entities.Tables.Members;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MakinaTurkiye.Services.Members
{
    public partial class MemberDescriptionService : IMemberDescriptionService
    {
        #region Fields

        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;
        private readonly IRepository<MemberDescription> _memberDescRepository;
        private readonly IRepository<MemberDescriptionLog> _memberDescriptionLog;

        #endregion

        #region Ctor

        public MemberDescriptionService(IDbContext dbContext, IDataProvider dataProvider, IRepository<MemberDescription> memberDescRepository,
            IRepository<MemberDescriptionLog> memberDescriptionLog)
        {
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
            this._memberDescRepository = memberDescRepository;
            this._memberDescriptionLog = memberDescriptionLog;
        }

        #endregion

        #region Methods

        public IList<MemberDescriptionForStore> GetByMainPartyIDOrderByColumn(string orderDesc, int mainPartyId)
        {
            var pOrderDesc = _dataProvider.GetParameter();
            pOrderDesc.ParameterName = "Colname";
            pOrderDesc.Value = orderDesc;
            pOrderDesc.DbType = DbType.String;

            var pMainPartyId = _dataProvider.GetParameter();
            pMainPartyId.ParameterName = "MainPartyId";
            pMainPartyId.Value = mainPartyId;
            pMainPartyId.DbType = DbType.Int32;

            return _dbContext.SqlQuery<MemberDescriptionForStore>("SP_DescriptionGetByMainPartyIdDescDate @Colname, @MainPartyId", pOrderDesc, pMainPartyId).ToList();
        }

        public IList<MemberDescriptionForStore> GetMemberDescByOnDate(int userId, int userGroupId, int pageDimension, int pageIndex, int orderBy, int consttandtId, out int totalRecord)
        {

            var pUserId = _dataProvider.GetParameter();
            pUserId.ParameterName = "UserId";
            pUserId.Value = userId;
            pUserId.DbType = DbType.Int32;

            var pUserGroupId = _dataProvider.GetParameter();
            pUserGroupId.ParameterName = "UserGroupId";
            pUserGroupId.Value = userGroupId;
            pUserGroupId.DbType = DbType.Int32;

            var pPageIndex = _dataProvider.GetParameter();
            pPageIndex.ParameterName = "PageIndex";
            pPageIndex.Value = pageIndex;
            pPageIndex.DbType = DbType.Int32;

            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "PageDimension";
            pPageSize.Value = pageDimension;
            pPageSize.DbType = DbType.Int32;

            var pOrderBy = _dataProvider.GetParameter();
            pOrderBy.ParameterName = "OrderBy";
            pOrderBy.Value = orderBy;
            pOrderBy.DbType = DbType.Byte;

            var pConstantId = _dataProvider.GetParameter();
            pConstantId.ParameterName = "ConstantId";
            pConstantId.Value = consttandtId;
            pConstantId.DbType = DbType.Int32;

            var pTotalRecords = _dataProvider.GetParameter();
            pTotalRecords.ParameterName = "TotalRecord";
            pTotalRecords.DbType = DbType.Int32;
            pTotalRecords.Direction = ParameterDirection.Output;


            var descripitons = _dbContext.SqlQuery<MemberDescriptionForStore>("SP_GETMEMBERDESCBYONDATE @UserId, @UserGroupId, @PageIndex, @PageDimension,@OrderBy, @ConstantId, @TotalRecord output", pUserId, pUserGroupId, pPageIndex, pPageSize, pOrderBy, pConstantId, pTotalRecords).ToList();
            totalRecord = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;
            return descripitons;
        }

        public IList<MemberDescriptionForStore> GetMemberDescNoUpdateDate(int Page, int Pagesize)
        {
            var pPage = _dataProvider.GetParameter();
            pPage.ParameterName = "Page";
            pPage.Value = Page;
            pPage.DbType = DbType.Int32;

            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "PageSize";
            pPageSize.Value = Pagesize;
            pPageSize.DbType = DbType.Int32;

            return _dbContext.SqlQuery<MemberDescriptionForStore>("SP_GetMemberDescriptionAllOpr @Page,@PageSize", pPage, pPageSize).ToList();
        }

        public IList<BaseMemberDescriptionForStore> GetMemberDescCloseDate(int Page, int PageSize, string Type, int UserId, out int TotalRecord)
        {
            var pPage = _dataProvider.GetParameter();
            pPage.ParameterName = "Page";
            pPage.Value = Page;
            pPage.DbType = DbType.Int32;

            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "PageSize";
            pPageSize.Value = PageSize;
            pPageSize.DbType = DbType.Int32;

            var pType = _dataProvider.GetParameter();
            pType.ParameterName = "Type";
            pType.Value = Type;
            pType.DbType = DbType.String;

            var pUserId = _dataProvider.GetParameter();
            pUserId.ParameterName = "UserId";
            pUserId.Value = UserId;
            pUserId.DbType = DbType.Int32;

            var pTotalRecords = _dataProvider.GetParameter();
            pTotalRecords.ParameterName = "TotalRecord";
            pTotalRecords.DbType = DbType.Int32;
            pTotalRecords.Direction = ParameterDirection.Output;

            var memberdescriptions = _dbContext.SqlQuery<BaseMemberDescriptionForStore>("SP_GetMemberDescCloseDate @Page,@PageSize,@Type,@UserId,@TotalRecord output", pPage, pPageSize, pType, pUserId, pTotalRecords).ToList();

            TotalRecord = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;

            return memberdescriptions;
        }

        public IList<MemberDescriptionForStore> GetMemberDescDate(int Page, int PageSize, string Type)
        {
            var pPage = _dataProvider.GetParameter();
            pPage.ParameterName = "Page";
            pPage.Value = Page;
            pPage.DbType = DbType.Int32;

            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "PageSize";
            pPageSize.Value = PageSize;
            pPageSize.DbType = DbType.Int32;

            var pType = _dataProvider.GetParameter();
            pType.ParameterName = "Type";
            pType.Value = Type;
            pType.DbType = DbType.String;
            return _dbContext.SqlQuery<MemberDescriptionForStore>("SP_GetMemberDateDesc @Page,@PageSize,@Type", pPage, pPageSize, pType).ToList();
        }

        public IList<MemberDescriptionForStore> GetMemberDescSearchText(string Text)
        {
            var pText = _dataProvider.GetParameter();
            pText.ParameterName = "Text";
            pText.Value = Text;
            pText.DbType = DbType.String;
            return _dbContext.SqlQuery<MemberDescriptionForStore>("SP_GetBaseMemberDescBySearchText @Text", pText).ToList();
        }

        public IList<BaseMemberDescriptionForStore> GetMemberDateDesc(int Page, int PageSize, string Type, int UserId, out int TotalRecord)
        {
            var pPage = _dataProvider.GetParameter();
            pPage.ParameterName = "Page";
            pPage.Value = Page;
            pPage.DbType = DbType.Int32;

            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "PageSize";
            pPageSize.Value = PageSize;
            pPageSize.DbType = DbType.Int32;

            var pType = _dataProvider.GetParameter();
            pType.ParameterName = "Type";
            pType.Value = Type;
            pType.DbType = DbType.String;

            var pUserId = _dataProvider.GetParameter();
            pUserId.ParameterName = "UserId";
            pUserId.Value = UserId;
            pUserId.DbType = DbType.Int32;

            var pTotalRecords = _dataProvider.GetParameter();
            pTotalRecords.ParameterName = "TotalRecord";
            pTotalRecords.DbType = DbType.Int32;
            pTotalRecords.Direction = ParameterDirection.Output;

            var memberdescriptions = _dbContext.SqlQuery<BaseMemberDescriptionForStore>("SP_GetMemberDateDesc @Page,@PageSize,@Type,@UserId,@TotalRecord output", pPage, pPageSize, pType, pUserId, pTotalRecords).ToList();

            TotalRecord = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;

            return memberdescriptions;
        }

        public List<MemberDescriptionTaskItem> SP_GetMemberDescriptionSearch(int Page, int PageSize, int UserId, DateTime? Date, string OrderColumn, byte OrderType, int ConstantId, string addedDate, out int TotalRecord)
        {
            var pPage = _dataProvider.GetParameter();
            pPage.ParameterName = "Page";
            pPage.Value = Page;
            pPage.DbType = DbType.Int32;

            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "PageSize";
            pPageSize.Value = PageSize;
            pPageSize.DbType = DbType.Int32;



            var pUserId = _dataProvider.GetParameter();
            pUserId.ParameterName = "UserId";
            pUserId.Value = UserId;
            pUserId.DbType = DbType.Int32;

            var pDate = _dataProvider.GetParameter();
            pDate.ParameterName = "Date";
            pDate.Value = Date;
            pDate.DbType = DbType.DateTime;

            var pOrderColumn = _dataProvider.GetParameter();
            pOrderColumn.ParameterName = "OrderColumn";
            pOrderColumn.Value = OrderColumn;
            pOrderColumn.DbType = DbType.String;

            var pOrderType = _dataProvider.GetParameter();
            pOrderType.ParameterName = "OrderType";
            pOrderType.Value = OrderType;
            pOrderType.DbType = DbType.Byte;

            var pConstantId = _dataProvider.GetParameter();
            pConstantId.ParameterName = "ConstantId";
            pConstantId.Value = ConstantId;
            pConstantId.DbType = DbType.Int32;

            var pCreatedDate = _dataProvider.GetParameter();
            pCreatedDate.ParameterName = "CreatedDate";
            pCreatedDate.Value = addedDate;
            pCreatedDate.DbType = DbType.String;

            var pTotalRecords = _dataProvider.GetParameter();
            pTotalRecords.ParameterName = "TotalRecord";
            pTotalRecords.DbType = DbType.Int32;
            pTotalRecords.Direction = ParameterDirection.Output;

            var memberdescriptions = _dbContext.SqlQuery<MemberDescriptionTaskItem>("SP_GetMemberDescriptionSearch @Page,@UserId,@PageSize,@Date,@OrderColumn,@OrderType,@ConstantId, @CreatedDate, @TotalRecord out", pPage, pPageSize, pUserId, pDate, pOrderColumn, pOrderType, pConstantId, pCreatedDate, pTotalRecords).ToList();

            TotalRecord = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;

            return memberdescriptions;
        }

        public List<MemberDescriptionCount> SP_GetMemberDescriptionCount()
        {
            var memberdescriptions = _dbContext.SqlQuery<MemberDescriptionCount>("SP_GetMemberdescriptionCountByDate").ToList();

            return memberdescriptions.ToList();
        }

        public List<MemberDescription> GetMemberDescriptionByPreRegistrationStoreId(int preRegistrationStoreId)
        {
            if (preRegistrationStoreId == 0)
                throw new ArgumentNullException("preRegistrationStoreId");
            var query = _memberDescRepository.Table;
            query = query.Where(x => x.PreRegistrationStoreId == preRegistrationStoreId);
            return query.ToList();
        }

        public void UpdateMemberDescription(MemberDescription memberDescription)
        {
            if (memberDescription == null)
                throw new ArgumentNullException("memberDescription");
            _memberDescRepository.Update(memberDescription);
        }

        public void DeleteMemberDescription(MemberDescription memberDescription)
        {
            if (memberDescription == null)
                throw new ArgumentNullException("memberDescription");
            _memberDescRepository.Delete(memberDescription);
        }

        public void InsertMemberDescription(MemberDescription memberDescription)
        {
            if (memberDescription == null)
                throw new ArgumentNullException("memberDescription");
            _memberDescRepository.Insert(memberDescription);
        }

        public List<MemberDescription> GetMemberDescriptionsByMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");

            var query = _memberDescRepository.Table;
            query = query.Where(x => x.MainPartyId == mainPartyId);
            return query.ToList();
        }

        public void InsertMemberDescriptionLog(MemberDescriptionLog memberDescriptionLog)
        {
            if (memberDescriptionLog == null)
                throw new ArgumentNullException("memberDescriptionLog");

            _memberDescriptionLog.Insert(memberDescriptionLog);
        }

        public void SP_MemberDescriptionsUpdateDateForRest()
        {
            _dbContext.ExecuteSqlCommand("exec SP_MemberDescriptionChangeDateForRest");
        }

        public void SP_UpdateMemberDescriptions()
        {
            _dbContext.ExecuteSqlCommand("exec SP_UpdateMemberDescriptions");
        }


        #endregion
    }
}
