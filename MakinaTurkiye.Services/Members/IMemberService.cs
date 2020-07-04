using MakinaTurkiye.Entities.StoredProcedures.Members;
using MakinaTurkiye.Entities.Tables.Members;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Members
{
    public interface IMemberService : ICachingSupported
    {
        Member GetMemberByMainPartyId(int mainPartyId);
        Member GetMemberByMemberEmail(string memberEmail);
        Member GetMemberWithLogin(string memberEmail, string memberPassword);

        MainParty GetMainPartyByMainPartyId(int mainPartyId);
       
        void InsertMainParty(MainParty mainParty);


        void InsertMember(Member member);
        void UpdateMember(Member member);

        Member GetMemberByActivationCode(string activationCode);
        List<MemberListForMailSender> SP_GetAllMemberListForMailSender(byte phoneConfirm, byte memberType, int fastMembershipType, int packetId, byte mailActive);
        Member GetMemberByMemberNo(string memberNo);
        IList<Member> GetMembersByMainPartyIds(List<int?> mainPartyIds);
        Member GetMemberByForgetPasswordCode(string forgetPasswordCode);

        Member GetMemberByMemberPassword(string memberPassword);

        IList<MemberByPhoneResult> SPGetInfoByPhone(string phoneNumber);

    }
}
