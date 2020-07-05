#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
#endregion

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants
{
    public class LeftMenuConstants
    {
        public static LeftMenuModel CreateLeftMenuModel(GroupName groupActive, int itemActive = 0)
        {
            LeftMenuModel leftMenuModel = new LeftMenuModel();
            List<LeftMenuItems> leftMenuItems = new List<LeftMenuItems>();

            leftMenuItems.Add(new LeftMenuItems { Name = "Hesabim", IsActive = (groupActive == LeftMenuConstants.GroupName.MyAccount ? true : false), ControlNubmer = (byte)LeftMenuConstants.GroupName.MyAccount, IconName = "dashboard" });
            leftMenuItems.Add(new LeftMenuItems
            {
                Name = "Profilim",
                IsActive = (groupActive == LeftMenuConstants.GroupName.MyProfile ? true : false),
                ControlNubmer = (byte)LeftMenuConstants.GroupName.MyProfile,
                IconName = "user",
                GroupItems = new List<LeftMenuGroup>(){
                                                new LeftMenuGroup{Items = LeftMenuConstants.CreateMyProfileItems(itemActive)},
                                            },
            });
            leftMenuItems.Add(new LeftMenuItems
            {
                Name = "İlanlarım",
                IsActive = (groupActive == LeftMenuConstants.GroupName.MyAds ? true : false),
                ControlNubmer = (byte)LeftMenuConstants.GroupName.MyAds,
                IconName = "shopping-cart",
                GroupItems = new List<LeftMenuGroup>()
                                              {
                                                  new LeftMenuGroup{Items = LeftMenuConstants.CreateMyAdItems(itemActive)},
                                              },
            });
            leftMenuItems.Add(new LeftMenuItems
            {
                Name = "Mesajlarım",
                IsActive = (groupActive == LeftMenuConstants.GroupName.MyMessage ? true : false),
                ControlNubmer = (byte)LeftMenuConstants.GroupName.MyMessage,
                IconName = "inbox",
                GroupItems = new List<LeftMenuGroup>()
                                              {
                                                  new LeftMenuGroup{Items = LeftMenuConstants.CreateMyMessageItems(itemActive)},
                                              },
            });
            leftMenuItems.Add(new LeftMenuItems
            {
                Name = "Favorilerim",
                IsActive = (groupActive == LeftMenuConstants.GroupName.MyFavorites ? true : false),
                ControlNubmer = (byte)LeftMenuConstants.GroupName.MyFavorites,
                IconName = "star",
                GroupItems = new List<LeftMenuGroup>()
                                              {
                                                  new LeftMenuGroup{Items = LeftMenuConstants.CreateMyFavoriteItems(itemActive)}
                                              },
            });
            leftMenuItems.Add(new LeftMenuItems
            {
                Name = "Firma Ayarları",
                IsActive = (groupActive == LeftMenuConstants.GroupName.StoreSettings ? true : false),
                ControlNubmer = (byte)LeftMenuConstants.GroupName.StoreSettings,
                IconName = "briefcase",
                GroupItems = new List<LeftMenuGroup>()
                                              {
                                                  new LeftMenuGroup{ Name = "Genel Bilgiler", Items = LeftMenuConstants.CreateStoreSettingGeneralInfoItems(itemActive)},
                                                  new LeftMenuGroup{ Name = "Diğer Bilgiler", Items = LeftMenuConstants.CreateStoreSettingOtherInfoItems(itemActive)}
                                              },
            });
            leftMenuItems.Add(new LeftMenuItems
            {
                Name = "İstatistikler",
                IsActive = (groupActive == LeftMenuConstants.GroupName.Statistics ? true : false),
                ControlNubmer = (byte)LeftMenuConstants.GroupName.Statistics,
                IconName = "stats",
                GroupItems = new List<LeftMenuGroup>()
                                              {
                                                  new LeftMenuGroup{Items = LeftMenuConstants.CreateStatisticItems(itemActive)}
                                              },
            });
            leftMenuItems.Add(new LeftMenuItems
            {
                Name = "Durum",
                IsActive = (groupActive == LeftMenuConstants.GroupName.Status ? true : false),
                ControlNubmer = (byte)LeftMenuConstants.GroupName.Status,
                IconName = "time",
                GroupItems = new List<LeftMenuGroup>()
                                              {
                                                  new LeftMenuGroup{Items = LeftMenuConstants.CreateStatusItems(itemActive)}
                                              },
            });
#if DEBUG
            leftMenuItems.Add(new LeftMenuItems { Name = "Siparişlerim", IsActive = (groupActive == LeftMenuConstants.GroupName.Order ? true : false), ControlNubmer = (byte)LeftMenuConstants.GroupName.Order, IconName = "shopping-cart", Url = "Account/Order" });
#endif


            leftMenuItems.Add(new LeftMenuItems
            {
                Name = "Diğer Ayarlar",
                IsActive = (groupActive == LeftMenuConstants.GroupName.OtherSettings ? true : false),
                ControlNubmer = (byte)LeftMenuConstants.GroupName.OtherSettings,
                IconName = "time",
                GroupItems = new List<LeftMenuGroup>()
                                              {
                                                  new LeftMenuGroup{Items = LeftMenuConstants.CreateOtherSettingsItems(itemActive)}
                                              },
            });
            leftMenuModel.Items = leftMenuItems;

            return leftMenuModel;
        }
        private static List<LeftMenuItems> CreateMyProfileItems(int itemActive = 0)
        {
            List<LeftMenuItems> leftMenuItems = new List<LeftMenuItems>();
            leftMenuItems.Add(new LeftMenuItems { Name = "Profilim Anasayfa", IsActive = (itemActive == (byte)MyProfile.MyProfileHomePage ? true : false), ControlNubmer = (byte)MyProfile.MyProfileHomePage, Url = "/Account/Personal" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Kişisel Bilgilerim", IsActive = (itemActive == (byte)MyProfile.MyPersonalInfoUpdate ? true : false), ControlNubmer = (byte)MyProfile.MyPersonalInfoUpdate, Url = "/Account/Personal/Update" });
            //leftMenuItems.Add(new LeftMenuItems { Name = "İlgi alanlarımı güncelle", IsActive = (itemActive == (byte)MyProfile.MyInterestsUpdate ? true : false), ControlNubmer = (byte)MyProfile.MyInterestsUpdate, Url = "/Account/Personal/MyInterestsUpdate" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Adres ve İletişim Bilgileri", IsActive = (itemActive == (byte)MyProfile.MyPersonalAdressUpdate ? true : false), ControlNubmer = (byte)MyProfile.MyPersonalAdressUpdate, Url = "/Account/Personal/ChangeAddress" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Eposta Adresi Düzenle", IsActive = (itemActive == (byte)MyProfile.EmailAddressChange ? true : false), ControlNubmer = (byte)MyProfile.EmailAddressChange, Url = "/Account/Personal/ChangeEmail" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Şifre Düzenle", IsActive = (itemActive == (byte)MyProfile.PasswordChange ? true : false), ControlNubmer = (byte)MyProfile.PasswordChange, Url = "/Account/Personal/ChangePassword" });

            return leftMenuItems;
        }
        private static List<LeftMenuItems> CreateMyAdItems(int itemActive = 0)
        {
            List<LeftMenuItems> leftMenuItems = new List<LeftMenuItems>();
            leftMenuItems.Add(new LeftMenuItems { Name = "İlan Ekle", IsActive = (itemActive == (byte)MyAd.AddAd ? true : false), ControlNubmer = (byte)MyAd.AddAd, Url = "/Account/ilan/Advert" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Tüm İlanlar", IsActive = (itemActive == (byte)MyAd.AllAd ? true : false), ControlNubmer = (byte)MyAd.AllAd, Url = "/Account/ilan/Index?ProductActiveType=7&DisplayType=2" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Yorumlar", IsActive = (itemActive == (byte)MyAd.Comments ? true : false), ControlNubmer = (byte)MyAd.Comments, Url = "/Account/ilan/Comments" });
            //leftMenuItems.Add(new LeftMenuItems { Name = "İlan Ayarları", IsActive = (itemActive == (byte)MyAd.Settings ? true : false), ControlNubmer = (byte)MyAd.Settings, Url = "/Account/ilan/ProductCreateSetting" });



            return leftMenuItems;
        }
        private static List<LeftMenuItems> CreateMyMessageItems(int itemActive = 0)
        {
            List<LeftMenuItems> leftMenuItems = new List<LeftMenuItems>();
            leftMenuItems.Add(new LeftMenuItems { Name = "Mesaj Gönder", IsActive = (itemActive == (byte)MyMessage.SendMessage ? true : false), ControlNubmer = (byte)MyMessage.SendMessage, Url = "/Account/Message/Index?MessagePageType=1" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Gelen Mesajlar", IsActive = (itemActive == (byte)MyMessage.IncomingMessages ? true : false), ControlNubmer = (byte)MyMessage.IncomingMessages, Url = "/Account/Message/Index?MessagePageType=0" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Giden Mesajlar", IsActive = (itemActive == (byte)MyMessage.OutgoingMessages ? true : false), ControlNubmer = (byte)MyMessage.OutgoingMessages, Url = "/Account/Message/Index?MessagePageType=2" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Silinen Mesajlar", IsActive = (itemActive == (byte)MyMessage.DeletedMessages ? true : false), ControlNubmer = (byte)MyMessage.DeletedMessages, Url = "/Account/Message/Index?MessagePageType=4" });


            return leftMenuItems;
        }
        private static List<LeftMenuItems> CreateMyFavoriteItems(int itemActive = 0)
        {
            List<LeftMenuItems> leftMenuItems = new List<LeftMenuItems>();
            leftMenuItems.Add(new LeftMenuItems { Name = "Favori İlanlarım", IsActive = (itemActive == (byte)MyFavorite.MyFavoriteAd ? true : false), ControlNubmer = (byte)MyFavorite.MyFavoriteAd, Url = "/Account/Favorite/Product" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Favori Firmalarım", IsActive = (itemActive == (byte)MyFavorite.MyFavoriteStore ? true : false), ControlNubmer = (byte)MyFavorite.MyFavoriteStore, Url = "/Account/Favorite/Store" });

            return leftMenuItems;
        }
        private static List<LeftMenuItems> CreateStoreSettingGeneralInfoItems(int itemActive = 0)
        {
            List<LeftMenuItems> leftMenuItems = new List<LeftMenuItems>();
            leftMenuItems.Add(new LeftMenuItems { Name = "Logo", IsActive = (itemActive == (byte)StoreSettingGeneralInfo.LogoUpdate ? true : false), ControlNubmer = (byte)StoreSettingGeneralInfo.LogoUpdate, Url = "/Account/Store/UpdateLogo" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Tanıtım Bilgileri", IsActive = (itemActive == (byte)StoreSettingGeneralInfo.PromotionInformUpdate ? true : false), ControlNubmer = (byte)StoreSettingGeneralInfo.PromotionInformUpdate, Url = "/Account/Store/UpdateStore" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Adres ve İletişim Bilgileri", IsActive = (itemActive == (byte)StoreSettingGeneralInfo.StoreAddresUpdate ? true : false), ControlNubmer = (byte)StoreSettingGeneralInfo.StoreAddresUpdate, Url = "/Account/Personal/ChangeAddress?display=firm" });

            leftMenuItems.Add(new LeftMenuItems { Name = "Vergi Daire/No Bilgileri", IsActive = (itemActive == (byte)StoreSettingGeneralInfo.StoreTaxUpdate ? true : false), ControlNubmer = (byte)StoreSettingGeneralInfo.StoreTaxUpdate, Url = "/Account/Personal/TaxUpdate" });

            leftMenuItems.Add(new LeftMenuItems { Name = "Banner Fotoğraflar", IsActive = (itemActive == (byte)StoreSettingGeneralInfo.StoreBannerUpdate ? true : false), ControlNubmer = (byte)StoreSettingGeneralInfo.StoreBannerUpdate, Url = "/Account/Store/UpdateBanner" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Slider Fotoğraflar", IsActive = (itemActive == (byte)StoreSettingGeneralInfo.StoreSliderImage ? true : false), ControlNubmer = (byte)StoreSettingGeneralInfo.StoreSliderImage, Url = "/account/store/createstoresliderimage" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Sertifikalarım", IsActive = (itemActive == (byte)StoreSettingGeneralInfo.StoreCertificate ? true : false), ControlNubmer = (byte)StoreSettingGeneralInfo.StoreCertificate, Url = "/account/store/Certificate" });

            leftMenuItems.Add(new LeftMenuItems { Name = "Katologlarım", IsActive = (itemActive == (byte)StoreSettingGeneralInfo.StoreCatolog ? true : false), ControlNubmer = (byte)StoreSettingGeneralInfo.StoreCatolog, Url = "/account/store/mycatologlist" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Yetkili Kullanıcılar", IsActive = (itemActive == (byte)StoreSettingGeneralInfo.Users ? true : false), ControlNubmer = (byte)StoreSettingGeneralInfo.Users, Url = "/account/Users" });
            leftMenuItems.Add(new LeftMenuItems { Name = "İletişim Saatleri", IsActive = (itemActive == (byte)StoreSettingGeneralInfo.ContactSettings ? true : false), ControlNubmer = (byte)StoreSettingGeneralInfo.ContactSettings, Url = "/account/Store/ContactSettings" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Tanıtım Videoları", IsActive = (itemActive == (byte)StoreSettingGeneralInfo.Videos ? true : false), ControlNubmer = (byte)StoreSettingGeneralInfo.Videos, Url = "/account/Video" });

            //leftMenuItems.Add(new LeftMenuItems { Name = "Faaliyet Alanları Değişikliği", IsActive = (itemActive == (byte)StoreSettingGeneralInfo.ActivityAreasChange ? true : false), ControlNubmer = (byte)StoreSettingGeneralInfo.ActivityAreasChange, Url = "/Account/Store/UpdateActivity" });


            return leftMenuItems;
        }
        private static List<LeftMenuItems> CreateStoreSettingOtherInfoItems(int itemActive = 0)
        {
            List<LeftMenuItems> leftMenuItems = new List<LeftMenuItems>();
            leftMenuItems.Add(new LeftMenuItems { Name = "Hakkımızda", IsActive = (itemActive == (byte)StoreSettingOtherInfo.AboutUs ? true : false), ControlNubmer = (byte)StoreSettingOtherInfo.AboutUs, Url = "/Account/Profile/AboutUs" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Firma Anasayfa Bilgi", IsActive = (itemActive == (byte)StoreSettingOtherInfo.StoreProfileHomeDescription ? true : false), ControlNubmer = (byte)StoreSettingOtherInfo.StoreProfileHomeDescription, Url = "/Account/Profile/StoreProfileHomeDescription" });

            leftMenuItems.Add(new LeftMenuItems { Name = "Şubelerimiz", IsActive = (itemActive == (byte)StoreSettingOtherInfo.OurBranches ? true : false), ControlNubmer = (byte)StoreSettingOtherInfo.OurBranches, Url = "/Account/Profile?DealerType=3" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Servis Ağımız", IsActive = (itemActive == (byte)StoreSettingOtherInfo.ServiceNetwork ? true : false), ControlNubmer = (byte)StoreSettingOtherInfo.ServiceNetwork, Url = "/Account/Profile?DealerType=2" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Bayi Ağımız", IsActive = (itemActive == (byte)StoreSettingOtherInfo.OurDealerNetwork ? true : false), ControlNubmer = (byte)StoreSettingOtherInfo.OurDealerNetwork, Url = "/Account/Profile?DealerType=1" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Firma Faaliyet Alanları", IsActive = (itemActive == (byte)StoreSettingOtherInfo.ActivityType ? true : false), ControlNubmer = (byte)StoreSettingOtherInfo.ActivityType, Url = "/Account/StoreActivity" });
            leftMenuItems.Add(new LeftMenuItems { Name = "İlgili Sektör", IsActive = (itemActive == (byte)StoreSettingOtherInfo.StoreSector ? true : false), ControlNubmer = (byte)StoreSettingOtherInfo.StoreSector, Url = "/Account/Store/Sector" });

            leftMenuItems.Add(new LeftMenuItems { Name = "Bayilikler", IsActive = (itemActive == (byte)StoreSettingOtherInfo.Dealerships ? true : false), ControlNubmer = (byte)StoreSettingOtherInfo.Dealerships, Url = "/Account/Profile/Dealership" });

            leftMenuItems.Add(new LeftMenuItems { Name = "Markalarımız", IsActive = (itemActive == (byte)StoreSettingOtherInfo.OurBrands ? true : false), ControlNubmer = (byte)StoreSettingOtherInfo.OurBrands, Url = "/Account/Profile/Brand" });

            leftMenuItems.Add(new LeftMenuItems { Name = "Şirket Görselleri", IsActive = (itemActive == (byte)StoreSettingOtherInfo.CompanyVisual ? true : false), ControlNubmer = (byte)StoreSettingOtherInfo.CompanyVisual, Url = "/Account/Profile/StoreImage" });

            leftMenuItems.Add(new LeftMenuItems { Name = "Profil Görseli", IsActive = (itemActive == (byte)StoreSettingOtherInfo.ProfileVisual ? true : false), ControlNubmer = (byte)StoreSettingOtherInfo.ProfileVisual, Url = "/Account/Profile/ProfilePicture" });

            return leftMenuItems;
        }
        private static List<LeftMenuItems> CreateStatisticItems(int itemActive = 0)
        {
            List<LeftMenuItems> leftMenuItems = new List<LeftMenuItems>();
            leftMenuItems.Add(new LeftMenuItems { Name = "Firma İstatistikleri", IsActive = (itemActive == (byte)Statistic.StoreStatistics ? true : false), ControlNubmer = (byte)Statistic.StoreStatistics, Url = "/Account/Statistic/Index?pagetype=1" });
            leftMenuItems.Add(new LeftMenuItems { Name = "İlan İstatistikleri", IsActive = (itemActive == (byte)Statistic.AdStatistics), ControlNubmer = (byte)Statistic.AdStatistics, Url = "/Account/Statistic/Index?pagetype=3" });
            return leftMenuItems;
        }
        private static List<LeftMenuItems> CreateStatusItems(int itemActive = 0)
        {
            List<LeftMenuItems> leftMenuItems = new List<LeftMenuItems>();
            leftMenuItems.Add(new LeftMenuItems { Name = "Satın Aldıklarım", IsActive = (itemActive == (byte)Status.MyBuy ? true : false), ControlNubmer = (byte)Status.MyBuy, Url = "MyBuy" });
            leftMenuItems.Add(new LeftMenuItems { Name = "Satılan Ürünlerim", IsActive = (itemActive == (byte)Status.SaleMyProducts ? true : false), ControlNubmer = (byte)Status.SaleMyProducts, Url = "SaleMyProducts" });

            return leftMenuItems;
        }
        private static List<LeftMenuItems> CreateOtherSettingsItems(int itemActive = 0)
        {
            List<LeftMenuItems> leftMenuItems = new List<LeftMenuItems>();
            leftMenuItems.Add(new LeftMenuItems { Name = "İlan Sıra Düzenle", IsActive = (itemActive == (byte)OtherSettings.SortEdit), ControlNubmer = (byte)OtherSettings.SortEdit, Url = "/Account/OtherSettings/Index?pagetype=4" });
            leftMenuItems.Add(new LeftMenuItems { Name = "İlan Fiyat Düzenle", IsActive = (itemActive == (byte)OtherSettings.PriceEdit), ControlNubmer = (byte)OtherSettings.PriceEdit, Url = "/Account/OtherSettings/Index?pagetype=5" });

            return leftMenuItems;
        }


        public enum GroupName
        {
            MyAccount = 1,
            MyProfile = 2,
            MyAds = 3,
            MyMessage = 4,
            MyFavorites = 5,
            StoreSettings = 6,
            Statistics = 7,
            Status = 8,
            OtherSettings = 9,
            Order = 10
        }

        public enum MyProfile
        {
            MyProfileHomePage = 9,
            MyPersonalInfoUpdate = 10,
            MyPersonalAdressUpdate = 11,
            EmailAddressChange = 12,
            PasswordChange = 13,
            MyInterestsUpdate = 14,
        }

        public enum MyAd
        {
            AllAd = 15,
            AddAd = 16,
            Comments = 250,
            Settings = 251
        }

        public enum MyMessage
        {
            SendMessage = 17,
            IncomingMessages = 18,
            OutgoingMessages = 19,
            DeletedMessages = 20
        }

        public enum MyFavorite
        {
            MyFavoriteAd = 21,
            MyFavoriteStore = 22,
        }

        public enum StoreSettingGeneralInfo
        {
            LogoUpdate = 23,
            PromotionInformUpdate = 24,
            StoreAddresUpdate = 25,
            ActivityAreasChange = 26,
            StoreTaxUpdate = 27,
            StoreBannerUpdate = 28,
            StoreSliderImage = 29,
            StoreCatolog = 30,
            Users = 31,
            ContactSettings = 32,
            Videos = 33,
            StoreCertificate = 34
        }

        public enum StoreSettingOtherInfo
        {
            AboutUs = 27,
            OurBranches = 28,
            ServiceNetwork = 29,
            OurDealerNetwork = 30,
            Dealerships = 31,
            OurBrands = 32,
            CompanyVisual = 33,
            ProfileVisual = 34,
            ActivityType = 35,
            StoreProfileHomeDescription = 36,
            StoreSector = 38
        }

        public enum Statistic
        {
            StoreStatistics = 35,
            AdStatistics = 36
        }

        public enum Status
        {
            MyBuy = 37,
            SaleMyProducts = 38
        }
        public enum OtherSettings
        {
            SortEdit = 39,
            PriceEdit = 40
        }
    }
}