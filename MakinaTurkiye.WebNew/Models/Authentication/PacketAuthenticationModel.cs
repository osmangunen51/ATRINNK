using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Stores;
using System.Linq;

namespace NeoSistem.MakinaTurkiye.Web.Models.Authentication
{
    public enum PacketFeatureType
    {
        ProcessCount = 1,
        Active = 2,
        Content = 3
    }

    public enum PacketPage
    {
        FirmaBilgileri = 1,
        IletisimBilgileri = 2,
        UrunEkleme = 3,
        UrunResimSayisi = 4,
        VideoEkleme = 5,
        FAGFA = 6,
        FaaliyetTipi = 7,
        Mesajlasma = 8,
        Logo = 9,
        SirketGorseli = 10,
        Subeler = 11,
        Bayiler = 12,
        YetkiliServisler = 13,
        Markalarimiz = 14,
        Hakkimizda = 15,
        FirmaVitrini = 16,
        UrunVitrini = 17,
        RaporBildirimi = 18,
        UrunGoruntulenmeSayisi = 19,
        FirmaGoruntulenmeSayisi = 20,
        UrunFavorilereEkleme = 21,
        FirmaFavorilereEkleme = 22,
        TeklifGonderme = 23,
        SEO = 24,
        SirketProfilGorseli = 25,
        OzelHizmetDestegi = 26,
        AranmaTalebi = 27
    }
    public class PacketAuthenticationModel
    {

        public static bool IsAccess(PacketPage page, int value = 0)
        {
            bool access = false;
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Individual)
            {
                return true;
            }

            IMemberStoreService memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();

            var memberStore = memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId);
            if (memberStore != null)
            {
                IStoreService storeService = EngineContext.Current.Resolve<IStoreService>();
                IPacketService packetService = EngineContext.Current.Resolve<IPacketService>();

                var store = storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                var packet = packetService.GetPacketByPacketId(store.PacketId);
                access = packet.IsStandart.GetValueOrDefault(false);

                if (!access)
                {
                    var packetFeatures = packet.PacketFeatures;
                    var packFeat = packetFeatures.FirstOrDefault(c => c.PacketFeatureTypeId == (byte)page);

                    switch ((PacketFeatureType)packFeat.FeatureType)
                    {
                        case PacketFeatureType.ProcessCount:
                            access = packFeat.FeatureProcessCount >= value;
                            break;
                        case PacketFeatureType.Active:
                        case PacketFeatureType.Content:
                            access = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            return access;
        }
    }
}