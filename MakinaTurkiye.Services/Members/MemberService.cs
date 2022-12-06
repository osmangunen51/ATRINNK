using MakinaTurkiye.Caching;
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
    public class MemberService : BaseService, IMemberService
    {
        #region Constants

        private const string MEMBERS_BY_MAINPARTY_ID_KEY = "makinaturkiye.member.bymainpartyId-{0}";

        #endregion

        #region Fields

        private readonly IRepository<Member> _memberRepository;
        private readonly IRepository<MainParty> _mainPartyRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;

        #endregion

        #region Ctor

        public MemberService(IRepository<Member> memberRepository, IRepository<MainParty> mainPartyRepository,
            ICacheManager cacheManager, IDataProvider dataProvider, IDbContext dbContext) : base(cacheManager)
        {
            this._memberRepository = memberRepository;
            this._mainPartyRepository = mainPartyRepository;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
        }

        #endregion

        #region Methods

        public Member GetMemberByMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                return null;

            string key = string.Format(MEMBERS_BY_MAINPARTY_ID_KEY, mainPartyId);
            return _cacheManager.Get(key, () =>
             {
                 var query = _memberRepository.Table;
                 return query.FirstOrDefault(m => m.MainPartyId == mainPartyId);
             });

        }

        public void UpdateMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException("member");

            _memberRepository.Update(member);

            string key = string.Format(MEMBERS_BY_MAINPARTY_ID_KEY, member.MainPartyId);
            _cacheManager.Remove(key);
        }

        public Member GetMemberByMemberEmail(string memberEmail)
        {

            if (string.IsNullOrEmpty(memberEmail))
                throw new ArgumentNullException("memberEmail");

            var query = _memberRepository.Table;
            return query.FirstOrDefault(x => x.MemberEmail == memberEmail);
        }

        public void InsertMainParty(MainParty mainParty)
        {
            if (mainParty == null)
                throw new ArgumentNullException("mainParty");

            _mainPartyRepository.Insert(mainParty);
        }

        public List<Member> GetAllMemberForSendEmail()
        {
            var query = _memberRepository.Table;
            return query.ToList();
        }
        public List<Member> GetAllMembers()
        {
            var query = _memberRepository.Table;
            return query.ToList();
        }
        public List<Member> GetMembersByMainPartyId(int Id)
        {
            var query = _memberRepository.Table;
            return query.Where(x => x.MainPartyId == Id).ToList();
        }

        public List<MemberListForMailSender> SP_GetAllMemberListForMailSender(byte phoneConfirm, byte memberType, int fastMembershipType, int packetId, byte mailActive)
        {
            var pPhoneConfirm = _dataProvider.GetParameter();
            pPhoneConfirm.ParameterName = "Phoneconfirm";
            pPhoneConfirm.Value = phoneConfirm;
            pPhoneConfirm.DbType = DbType.Byte;

            var pMemberType = _dataProvider.GetParameter();
            pMemberType.ParameterName = "MemberType";
            pMemberType.Value = memberType;
            pMemberType.DbType = DbType.Byte;

            var pFastMembershipType = _dataProvider.GetParameter();
            pFastMembershipType.ParameterName = "FastMembershipType";
            pFastMembershipType.Value = fastMembershipType;
            pFastMembershipType.DbType = DbType.Int32;

            var pPacketId = _dataProvider.GetParameter();
            pPacketId.ParameterName = "PacketId";
            pPacketId.Value = packetId;
            pPacketId.DbType = DbType.Int32;

            var pMailActive = _dataProvider.GetParameter();
            pMailActive.ParameterName = "MailActive";
            pMailActive.Value = mailActive;
            pMailActive.DbType = DbType.Byte;

            var memberListForMailSender = _dbContext.SqlQuery<MemberListForMailSender>("SP_GetMailListForWhereClause @PhoneConfirm,@MemberType,@FastMembershipType,@PacketId,@MailActive", pPhoneConfirm, pMemberType, pFastMembershipType, pPacketId, pMailActive);
            return memberListForMailSender.ToList();
        }

        public Member GetMemberByActivationCode(string activationCode)
        {
            if (string.IsNullOrEmpty(activationCode))
                throw new ArgumentException("activationCode");

            var query = _memberRepository.Table;
            return query.FirstOrDefault(m => m.ActivationCode == activationCode);

        }

        public Member GetMemberByMemberNo(string memberNo)
        {
            if (memberNo == "")
                throw new ArgumentException("");

            var query = _memberRepository.Table;
            return query.FirstOrDefault(x => x.MemberNo == memberNo);
        }

        public IList<Member> GetMembersByMainPartyIds(List<int?> mainPartyIds)
        {
            var query = _memberRepository.Table;
            query = query.Where(x => mainPartyIds.Contains(x.MainPartyId));
            return query.ToList();
        }

        public void InsertMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException("member");

            _memberRepository.Insert(member);
        }

        public MainParty GetMainPartyByMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");

            var query = _mainPartyRepository.Table;
            return query.FirstOrDefault(x => x.MainPartyId == mainPartyId);
        }

        public void UpdateMainParty(MainParty mainParty)
        {
            if (mainParty == null)
                throw new ArgumentNullException("UpdateMainParty");

            _mainPartyRepository.Update(mainParty);
        }

        public Member GetMemberByForgetPasswordCode(string forgetPasswordCode)
        {
            if (string.IsNullOrEmpty(forgetPasswordCode))
                throw new ArgumentNullException("forgetPasswordCode");

            var member = _memberRepository.Table;
            return member.FirstOrDefault(x => x.ForgetPasswodCode == forgetPasswordCode);
        }

        public Member GetMemberWithLogin(string memberEmail, string memberPassword)
        {
            if (string.IsNullOrEmpty(memberEmail))
                throw new ArgumentNullException("memberEmail");

            if (string.IsNullOrEmpty(memberPassword))
                throw new ArgumentNullException("memberPassword");

            var query = _memberRepository.Table;

            var member = query.FirstOrDefault(m => m.MemberEmail == memberEmail && m.MemberPassword == memberPassword);
            return member;
        }

        public Member GetMemberByMemberPassword(string memberPassword)
        {
            if (string.IsNullOrEmpty(memberPassword))
                throw new ArgumentNullException("memberPassword");

            var query = _memberRepository.Table;

            var member = query.FirstOrDefault(m => m.MemberPassword == memberPassword);
            return member;

        }

        public IList<MemberByPhoneResult> SPGetInfoByPhone(string phoneNumber)
        {
            phoneNumber = phoneNumber.Trim().Replace(" ", "").Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "");
            if (phoneNumber.StartsWith("0"))
            {
                phoneNumber = phoneNumber.Substring(1, phoneNumber.Length - 1);
            }
            else if (phoneNumber.StartsWith("9"))
            {
                phoneNumber = phoneNumber.Substring(2, phoneNumber.Length - 2);
            }
            var pPhoneNumber = _dataProvider.GetParameter();
            pPhoneNumber.ParameterName = "PhoneNumber";
            pPhoneNumber.Value = phoneNumber;
            pPhoneNumber.DbType = DbType.String;


            var list = _dbContext.SqlQuery<MemberByPhoneResult>("SP_GetInfoByPhone @PhoneNumber", pPhoneNumber);
            return list.ToList();
        }


        #endregion

    }
}
